using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerangMusuh : Photon.MonoBehaviour {

	public GameObject partikelKena;
	public GameObject partikelMati;
	public string TextStatusAktifitas = "Text Aktifitas";

	public string tagTembakan;
	public string tagLedakan;
	public float lamaMati = 3f;

	public int nilaiTembakan = 10;
	public int nilaiLedakan = 30;

	public int nyawaMusuh = 50;

	Text statusAktifitas;

	private bool MatiUdah = false;
	private Vector3 syncPosMusuh = Vector3.zero;
	private Quaternion syncRotMusuh = Quaternion.identity;

	// Use this for initialization
	void Start () {
		partikelKena.SetActive (false);
		partikelMati.SetActive (false);
		statusAktifitas = GameObject.Find (TextStatusAktifitas).GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (nyawaMusuh < 0) {
			nyawaMusuh = 0;
		}

		if (nyawaMusuh == 0 && MatiUdah == false) {
			photonView.RPC ("MusuhMati", PhotonTargets.AllBuffered);
			statusAktifitas.text += "<color=maroon>musuh: "+ gameObject.name + " kalah </color>\n";
		}
	}

	[PunRPC]
	IEnumerator MusuhMati(){
		partikelMati.SetActive (true);
		MatiUdah = true;
		yield return new WaitForSeconds (lamaMati);
		PhotonNetwork.Destroy (gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag (tagTembakan)) {
			nyawaMusuh -= nilaiTembakan;
			partikelKena.SetActive (true);
			Debug.Log ("musuh menyentuh tembakan");
		}

		if (other.gameObject.CompareTag (tagLedakan)) {
			nyawaMusuh -= nilaiLedakan;
			partikelKena.SetActive (true);
			Debug.Log ("musuh menyentuh tembakan");
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag (tagTembakan)) {
			partikelKena.SetActive (false);
		}

		if (other.gameObject.CompareTag (tagLedakan)) {
			partikelKena.SetActive (false);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
	{ 
		if (stream.isWriting) 
		{ 
			stream.SendNext(transform.position); 
			stream.SendNext(transform.rotation);
			stream.SendNext (partikelMati.activeInHierarchy);
			stream.SendNext (partikelKena.activeInHierarchy);
			stream.SendNext (gameObject.activeInHierarchy);
			stream.SendNext (nyawaMusuh);
		} 
		else
		{ 
			syncPosMusuh = (Vector3)stream.ReceiveNext(); 
			syncRotMusuh = (Quaternion)stream.ReceiveNext();
			partikelMati.SetActive((bool)stream.ReceiveNext());
			partikelKena.SetActive((bool)stream.ReceiveNext());
			gameObject.SetActive((bool)stream.ReceiveNext());
			nyawaMusuh = (int)stream.ReceiveNext ();
		} 
	}
}
