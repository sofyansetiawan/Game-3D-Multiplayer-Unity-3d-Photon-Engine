using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CekBacksound : MonoBehaviour {

	public string SourceBacksound;

	void Awake(){

		Toggle toggleBacksound = this.gameObject.GetComponent<Toggle> ();

		if(BacksoundFullscreen.BacksoundStatus == true){
			toggleBacksound.isOn = true;
		}else{
			toggleBacksound.isOn = false;
		}

		AudioSource backsound = GameObject.Find (SourceBacksound).GetComponent<AudioSource> ();

		if (toggleBacksound.isOn == true) {
			backsound.mute = false;
			BacksoundFullscreen.BacksoundStatus = true;
		} else {
			backsound.mute = true;
			BacksoundFullscreen.BacksoundStatus = false;
		}
	}

	public void BacksoundOnOff(){
		AudioSource backsound = GameObject.Find (SourceBacksound).GetComponent<AudioSource> ();

		if(backsound.mute == true){
			backsound.mute = false;
			BacksoundFullscreen.BacksoundStatus = true;
			Debug.Log ("Backsound : " +BacksoundFullscreen.BacksoundStatus);
		}else{
			backsound.mute = true;
			BacksoundFullscreen.BacksoundStatus = false;
			Debug.Log ("Backsound : " +BacksoundFullscreen.BacksoundStatus);
		}
	}
}
