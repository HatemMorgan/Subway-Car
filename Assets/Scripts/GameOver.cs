using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

    public AudioClip crashClip;

    // Use this for initialization
    void Start () {
        if(!Configurations.getInstance().isSoundMuted())
             AudioSource.PlayClipAtPoint(crashClip, this.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
