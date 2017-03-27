using UnityEngine;
using System.Collections;

public class KeluarGame : MonoBehaviour {

	public AudioSource ButtonSound;

	public void KeluarDariGame(){
		AudioSource buttonSound = ButtonSound.GetComponent<AudioSource> ();
		buttonSound.PlayOneShot (buttonSound.clip);
		Application.Quit ();
	}
}
