using UnityEngine;
using System.Collections;

public class BacksoundFullscreen : MonoBehaviour {

	public string SourceBacksound;

	public static bool FullscreenStatus = true;
	public static bool BacksoundStatus = true;

	// Use this for initialization
	void Start () {
		Debug.Log ("Start Backsound : " + BacksoundStatus);
		Debug.Log ("Start Fullscreen : " + FullscreenStatus);

		AudioSource backsound = GameObject.Find (SourceBacksound).GetComponent<AudioSource> ();

		if(BacksoundFullscreen.BacksoundStatus == true){
			backsound.mute = false;
		}
		else{
			backsound.mute = true;
		}

		if(BacksoundFullscreen.FullscreenStatus == true){
			Screen.fullScreen = true;
		}
		else{
			Screen.fullScreen = false;
		}
	}
}
