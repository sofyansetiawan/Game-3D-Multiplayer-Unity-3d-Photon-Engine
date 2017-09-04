using UnityEngine;
using System.Collections;

public class Tembakan : Photon.PunBehaviour {

	public float kecepatanLaser;
	private Vector3 syncPosShot = Vector3.zero;
	public string tagMusuh = "Musuh";
	public GameObject playerSaya;
	public int satuanSkor = 100;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (photonView.isMine) {
			transform.position += transform.forward * Time.deltaTime * kecepatanLaser;
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
	{ 
		if (stream.isWriting) 
		{ 
			stream.SendNext(transform.position);
		} 
		else
		{ 
			syncPosShot = (Vector3)stream.ReceiveNext(); 
		} 
	}

	void OnTriggerEnter(Collider other)
	{
		if(photonView.isMine)
		{
			if (other.gameObject.CompareTag (tagMusuh)) {
				PhotonNetwork.player.AddScore (satuanSkor);
			}
		}
	}
}
