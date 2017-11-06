using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour {

    public GameObject emptyGameObject;

    public Button btnCredit;
    public Button btnHowToPlay;
    public Button btnStart;
    public Button btnOptions;

    public Toggle toggleMute;


    public Text txtHowToPlay;
    public Text txtStartGame;
    public Text txtCredit;

    private bool expandedOptions;
    private bool wasBtnHowTopPlayClicked;
    private bool wasBtnCreditClicked;
    private bool wasMuteEnabled;

	// Use this for initialization
	void Start () {
        if(!Configurations.getInstance().isSoundMuted())
            emptyGameObject.GetComponent<AudioSource>().Play();
        
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale == 0)
            resume();
	}

    void OnDestroy()
    {
        if (!Configurations.getInstance().isSoundMuted()) 
             emptyGameObject.GetComponent<AudioSource>().Stop();

        if (Time.timeScale == 0)
            Time.timeScale = 1;

    }

    public void start()
    {
        emptyGameObject.GetComponent<AudioSource>().Stop();
        SceneManager.LoadScene("MainScene");
    }

    public void options()
    {
        if(!expandedOptions)
        {
             foreach(GameObject o in GameObject.FindGameObjectsWithTag("main"))
                 o.transform.Translate(0, 100, 0);

            CanvasGroup canvasGroup = btnHowToPlay.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            canvasGroup = btnCredit.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            canvasGroup = toggleMute.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;

            expandedOptions = true;

        }
        else
        {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag("main"))
                o.transform.Translate(0, -100, 0);

            CanvasGroup canvasGroup = btnHowToPlay.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;

            canvasGroup = btnCredit.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;

            canvasGroup = toggleMute.GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = false;

            expandedOptions = false;
        }


    }

    public void mute()
    {
        wasMuteEnabled = !wasMuteEnabled;
        Configurations config = Configurations.getInstance();
        config.setSoundMuted(wasMuteEnabled);
        if (wasMuteEnabled)
        {
            emptyGameObject.GetComponent<AudioSource>().Pause();
        }
        else
        {
            emptyGameObject.GetComponent<AudioSource>().Play();
        }

    }

    public void credit()
    {
        if (!wasBtnCreditClicked)
        {
            CanvasGroup canvaseGroup = txtStartGame.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnStart.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnOptions.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = toggleMute.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnHowToPlay.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            btnCredit.transform.Translate(-30,400, 0);

            txtCredit.GetComponent<CanvasGroup>().alpha = 1f;

            wasBtnCreditClicked = true;
        }
        else
        {
            CanvasGroup canvaseGroup = txtStartGame.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnStart.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnOptions.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = toggleMute.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnHowToPlay.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            btnCredit.transform.Translate(30, -400, 0);

            txtCredit.GetComponent<CanvasGroup>().alpha = 0f;

            wasBtnCreditClicked = false;
        }
    }

    public void howToPlay()
    {
        if (!wasBtnHowTopPlayClicked)
        {
            CanvasGroup canvaseGroup = txtStartGame.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnStart.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnOptions.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = toggleMute.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            canvaseGroup = btnCredit.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 0f;
            canvaseGroup.blocksRaycasts = false;

            btnHowToPlay.transform.Translate(-30, 350, 0);

            txtHowToPlay.GetComponent<CanvasGroup>().alpha = 1f;

            wasBtnHowTopPlayClicked = true;
        }
        else
        {
            CanvasGroup canvaseGroup = txtStartGame.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnStart.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnOptions.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = toggleMute.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            canvaseGroup = btnCredit.GetComponent<CanvasGroup>();
            canvaseGroup.alpha = 1f;
            canvaseGroup.blocksRaycasts = true;

            btnHowToPlay.transform.Translate(30, -350, 0);

            txtHowToPlay.GetComponent<CanvasGroup>().alpha = 0f;

            wasBtnHowTopPlayClicked = false;
        }

    }

    public void resume()
    {
        if(! Configurations.getInstance().isSoundMuted())
            emptyGameObject.GetComponent<AudioSource>().Stop();
        SceneManager.UnloadSceneAsync("Pause");
        Time.timeScale = 1;
    }

    public void restart()
    {
        if (!Configurations.getInstance().isSoundMuted())
               emptyGameObject.GetComponent<AudioSource>().Stop();

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync("Pause");
        }
        SceneManager.LoadScene("MainScene");
    }

    public void quit()
    {
        if (!Configurations.getInstance().isSoundMuted())
            emptyGameObject.GetComponent<AudioSource>().Stop();
        Application.Quit();

    }
}
