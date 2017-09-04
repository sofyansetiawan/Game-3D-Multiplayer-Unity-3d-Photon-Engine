using UnityEngine;
using System.Collections;

public class KameraPlayer : MonoBehaviour
{
	Transform target;
	Transform targetNamaPlayer;
	public string namaPlayer = "PlayerSaya";
	public string namaPlayerText = "PlayerNama";
	public float lembam = 1;

	private bool kameraSudah = false; 

	Vector3 offset;

	void Start ()
	{
		if (GameObject.Find (namaPlayer)) {
			target = GameObject.Find (namaPlayer).GetComponent<Transform> ();
			targetNamaPlayer = GameObject.Find (namaPlayer + "/" + namaPlayerText).GetComponent<Transform> ();
			if (kameraSudah == false) {
				offset = transform.position - target.position;
				kameraSudah = true;
			}
		} else {
			Debug.Log ("target kamera pada player tidak ditemukan");
		}
	}

	void Update(){
		if (GameObject.Find (namaPlayer)) {
			target = GameObject.Find (namaPlayer).GetComponent<Transform> ();
			targetNamaPlayer = GameObject.Find (namaPlayer + "/" + namaPlayerText).GetComponent<Transform> ();
			if (kameraSudah == false) {
				offset = transform.position - target.position;
				kameraSudah = true;
			}
		} else {
			Debug.Log ("target kamera pada player tidak ditemukan");
		}
	}

	void LateUpdate ()
	{
		if (target) {
			Vector3 posisiIngin = target.transform.position + offset;
			Vector3 posisi = Vector3.Lerp (transform.position, posisiIngin, Time.deltaTime * lembam);
			transform.position = posisi;
			transform.LookAt (target.transform.position);
		}

		if (targetNamaPlayer) {
			var n = transform.position - targetNamaPlayer.transform.position; 
			var newRotation = Quaternion.LookRotation (n) * Quaternion.Euler (0, 180, 0);
			targetNamaPlayer.transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, Time.deltaTime * 1f);
		}
	}
}