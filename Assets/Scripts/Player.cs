using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject obstacle;
    public GameObject car;
    public GameObject coin;
    public GameObject laserRadar;
    public GameObject floor1;
    public GameObject floor2;
    public Text scoreText;
    public GameObject firstCameraPerson;
    public GameObject thirdCameraPerson;
    public AudioClip coinClip;
    public AudioClip jumpClip;
    public AudioClip crashClip;
    public AudioClip laserClip;
    public AudioClip pauseClip;

    private int currFloor;
    private GameObject currCamera;
    private int score;
    public float speed;
    private Floor floor1Obj;
    private Floor floor2Obj;
    private int scoreCount;
    private bool wasBtnPauseClicked;

    // Use this for initialization
    void Start () {
        this.currFloor = 1;
        this.score = 0;
        this.scoreCount = 0;
        this.speed = 2f;
        this.floor1Obj = new Floor(coin, laserRadar, obstacle, floor1);
        this.floor2Obj = new Floor(coin, laserRadar, obstacle, floor2);
        floor1Obj.createGameObjectsOnFloor();
        floor2Obj.createGameObjectsOnFloor();
        currCamera = thirdCameraPerson;

        if (Configurations.getInstance().isSoundMuted())
        {
            car.GetComponent<AudioListener>().enabled = false;
            car.GetComponent<AudioSource>().enabled = false;

        }


    }

    // Update is called once per frame
    void Update () {
        if(Time.timeScale == 1)
        {
            moveCar();
            if (!car.GetComponent<AudioSource>().enabled && !Configurations.getInstance().isSoundMuted())
            {
                car.GetComponent<AudioSource>().enabled = true;
                car.GetComponent<AudioSource>().Play();
            }
        }
            

        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 1)
        {
            Time.timeScale = 0;
            car.GetComponent<AudioSource>().Pause();

            car.GetComponent<AudioSource>().enabled = false;
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        
        }

        if (Input.GetButtonDown("Jump"))
            jump();

        if (Input.GetKeyDown(KeyCode.C))
            changeCamera();

    }


    private void moveCar()
    {

        // move right and left
        if (Input.GetButtonDown("Horizontal") || Input.acceleration.x != 0)
        {

            float t = Input.GetAxis("Horizontal");
            if (t < 0 || Input.acceleration.x < 0)
            {
                if (car.transform.position.x > -4)
                {
                    transform.Translate(-4, 0, 0);
                }
                else
                {
                    if (!Configurations.getInstance().isSoundMuted())
                        AudioSource.PlayClipAtPoint(crashClip, car.transform.position);
                }
            }

            if (t > 0 || Input.acceleration.x > 0)
            {
                if (car.transform.position.x < 4)
                {
                    transform.Translate(4, 0, 0);
                }
                else
                {
                    if (!Configurations.getInstance().isSoundMuted())
                         AudioSource.PlayClipAtPoint(crashClip, car.transform.position);
                }
            }
                              
         }
 
            // move two floors in the negative z direction
            floor1.transform.Translate(0, 0, -speed*Time.deltaTime);
            floor2.transform.Translate(0, 0, -speed*Time.deltaTime);

    }


    public void OnTriggerEnter(Collider c)
    {

        if (currFloor == 1 && c.gameObject.name.Equals("Floor2"))
        {
            foreach (Transform child in floor1.transform)
            {

                if (child.tag.Equals("wall"))
                {
                    continue;
                }
                Destroy(child.gameObject);
            }
            floor1.transform.position = new Vector3(0, 0, 20);
            floor1Obj.createGameObjectsOnFloor();
            currFloor = 2;
            
        }

        if (currFloor == 2 && c.gameObject.name.Equals("Floor1"))
        {
           
            foreach (Transform child in floor2.transform)
            {
                if (child.tag.Equals("wall"))
                    continue;

                Destroy(child.gameObject);
            }
            floor2.transform.position = new Vector3(0, 0, 20);
            floor2Obj.createGameObjectsOnFloor();
            currFloor = 1;
        }

        if (c.gameObject.name.Equals("Laser Radar(Clone)"))
        {
            Destroy(c.gameObject);
            if (!Configurations.getInstance().isSoundMuted())
                AudioSource.PlayClipAtPoint(laserClip, c.gameObject.transform.position);
            // decrease score
            if (score > 50)
            {
                speed *= 1- (50 * 1) / score;
            }
            else
            {
                speed *= 0.8f;
            }

            if (speed == 0)
                speed = 0.5f;

            score -= 50;
            if (score < 0)
                score = 0;

 
            scoreText.text = "Score = " + score;

        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("Coin(Clone)"))
        {
            if (!Configurations.getInstance().isSoundMuted())
                AudioSource.PlayClipAtPoint(coinClip, collision.gameObject.transform.position);

            Destroy(collision.gameObject);
            score += 10;
            scoreCount++;
            if (scoreCount == 5)
            {
                scoreCount = 0;

                // increase speed
                if (score > 100)
                {
                    speed *= ((10 * 1) / score) + 1;
                }
                else
                {
                    speed *= 1.1f;
                }
            }
            scoreText.text = "Score = " + score;

            return;
        }

        if (collision.gameObject.name.Equals("Obstacle(Clone)"))
        {
            if (!Configurations.getInstance().isSoundMuted())
                AudioSource.PlayClipAtPoint(crashClip, collision.gameObject.transform.position);

            SceneManager.LoadScene("GameOver");
            return;
        }

    }


    public void pause()
    {
        if (!wasBtnPauseClicked)
        {
            Time.timeScale = 0;
            car.GetComponent<AudioSource>().Pause();
            car.GetComponent<AudioSource>().enabled = false;
            AudioSource.PlayClipAtPoint(pauseClip, gameObject.transform.position);
            SceneManager.LoadScene("Pause", LoadSceneMode.Additive);

            wasBtnPauseClicked = true;
        }
        else
        {
            car.GetComponent<AudioSource>().enabled = true;
            car.GetComponent<AudioSource>().Play();
            SceneManager.UnloadSceneAsync("Pause");
            //Time.timeScale = 1;

            wasBtnPauseClicked = false;
        }
    }
       

    public void jump()
    {
        if (car.transform.position.y >= 0.5 && car.transform.position.y < 0.6)
        {
            car.transform.Translate(0, 2, 1);
            car.GetComponent<Rigidbody>().AddForce(new Vector3(0, -2 * speed, 0), ForceMode.Impulse);
            if (car.transform.position.y < 0.5)
                car.transform.position = new Vector3(car.transform.position.x, 0.5f, car.transform.position.z);

            if (!Configurations.getInstance().isSoundMuted())
                AudioSource.PlayClipAtPoint(jumpClip, car.transform.position);
        }
    }

    public void changeCamera()
    {
        if (currCamera.Equals(thirdCameraPerson))
        {
            currCamera = firstCameraPerson;
            thirdCameraPerson.gameObject.SetActive(false);
            firstCameraPerson.gameObject.SetActive(true);
        }
        else
        {
            currCamera = thirdCameraPerson;
            thirdCameraPerson.gameObject.SetActive(true);
            firstCameraPerson.gameObject.SetActive(false);
        }
    }

}


