using UnityEngine;
using System.Collections;

public class PindahPanel : MonoBehaviour {

	public AudioSource buttonSound;
	public GameObject PanelAwal;
	public GameObject PanelTujuan;

	public void GantiKePanelBaru(){
		buttonSound.PlayOneShot (buttonSound.clip);
		PanelAwal.SetActive (false);
		PanelTujuan.SetActive (true);
	}
}
