using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SeranganPlayer : Photon.PunBehaviour {

	GameObject JurusLaser;
	GameObject JurusBomb;
	GameObject JurusKecepatan;
	GameObject panelKalah;

	public string jurusLaser;
	public GameObject posisiShot;
	public GameObject tembakanLaser;
	public float lamaAktifLaser = 3f;
	public float lamaMati = 3f;

	public string jurusBomb;
	public GameObject ObyekBom;
	public float lamaAktifBomb = 2f;
	private bool BombUdah = false;
	public string TagLedakan = "Ledakan";

	public string jurusKecepatan;
	public float KecepatanBaru = 6f;
	public float kecepatanAwal = 2f;
	public GameObject partikelKecepatan;
	public GameObject partikelSerang;
	public GameObject partikelMati;

	public KeyCode keyboardKeyTembak = KeyCode.G;
	public KeyCode keyboardKeyLedakan = KeyCode.H;
	public KeyCode keyboardKeyKecepatan = KeyCode.J;

	public float nilaiSeranganMusuh = 10f;
	public float nyawaPlayer = 100f;
	public string slider = "Slider Nyawa";
	public string TagMusuh = "Musuh";
	public string TextStatusAktifitas = "Text Aktifitas";

	public string panelkalah = "Panel Kalah";
	public TextMesh textNamaPlayer;
	private Vector3 syncPosPlayer = Vector3.zero;
	private Quaternion syncRotPlayer = Quaternion.identity;
	private Animator animatorPlayer;

	public bool sudahKalah = false;

	GerakanPlayer gerakanPlayer;
	Slider sliderNyawa;
	Text statusAktifitas;

	PhotonView pv;


	// Use this for initialization
	void Start () 
	{
		pv = this.GetComponent<PhotonView> ();
		gerakanPlayer = transform.GetComponent<GerakanPlayer> ();
		sliderNyawa = GameObject.Find (slider).GetComponent<Slider> ();
		statusAktifitas = GameObject.Find (TextStatusAktifitas).GetComponent<Text> ();
		panelKalah = GameObject.Find (panelkalah);
		panelKalah.SetActive (false);

		JurusLaser = GameObject.Find (jurusLaser);
		JurusBomb = GameObject.Find (jurusBomb);
		JurusKecepatan = GameObject.Find (jurusKecepatan);

		if (pv.isMine) {
			JurusLaser.SetActive(true);
			JurusBomb.SetActive(false);
			JurusKecepatan.SetActive(false);

			partikelKecepatan.SetActive(false);
			partikelSerang.SetActive(false);
			partikelMati.SetActive(false);
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (pv.isMine) {
			if (Input.GetKeyDown (keyboardKeyTembak)) {
				StartCoroutine("TembakLaser");
				JurusLaser.SetActive (true);
				JurusBomb.SetActive (false);
				JurusKecepatan.SetActive (false);
			}

			if (Input.GetKeyDown (keyboardKeyLedakan) && BombUdah == false) {
				StartCoroutine("LedakanPlayer");
				JurusLaser.SetActive (false);
				JurusBomb.SetActive (true);
				JurusKecepatan.SetActive (false);
			}

			if (Input.GetKeyDown (keyboardKeyKecepatan)) {
				GerakCepat ();
				JurusLaser.SetActive (false);
				JurusBomb.SetActive (false);
				JurusKecepatan.SetActive (true);
			}

			if (sliderNyawa.value <= 0f) {
				sliderNyawa.value = 0f;
			}

			if (nyawaPlayer == 0f && sudahKalah == false) {
				StartCoroutine ("PlayerMati");
				pv.RPC ("statusMati", PhotonTargets.All);
			}
		}

		if (pv.isMine) {
			TextMesh textnamaPlayer = textNamaPlayer.GetComponent<TextMesh> ();
			textnamaPlayer.text = PhotonNetwork.player.NickName;
		}

		if (!pv.isMine) {
			TextMesh textnamaPlayer = textNamaPlayer.GetComponent<TextMesh> ();
			textnamaPlayer.text = pv.owner.NickName;
		}
	}
		
	IEnumerator TembakLaser(){
		GameObject laser = PhotonNetwork.Instantiate (tembakanLaser.name, posisiShot.transform.position, posisiShot.transform.rotation, 0) as GameObject;
		yield return new WaitForSeconds (lamaAktifLaser);
		PhotonNetwork.Destroy (laser);
		if (pv.isMine) {
			partikelKecepatan.SetActive (false);
			gerakanPlayer.m_moveSpeed = kecepatanAwal;
		}
	}
		
	IEnumerator LedakanPlayer()
	{
		GameObject bom = PhotonNetwork.Instantiate (ObyekBom.name, posisiShot.transform.position, posisiShot.transform.rotation, 0) as GameObject;
		if (pv.isMine) {
			gerakanPlayer.m_moveSpeed = 0f;
			BombUdah = true;
			yield return new WaitForSeconds (lamaAktifBomb);
		}
		PhotonNetwork.Destroy (bom);
		if (pv.isMine) {
			gerakanPlayer.m_moveSpeed = kecepatanAwal;
			BombUdah = false;
			partikelKecepatan.SetActive (false);
		}
	}
		
	IEnumerator PlayerMati()
	{
		partikelMati.SetActive (true);
		yield return new WaitForSeconds(lamaMati);
		partikelMati.SetActive (false);
		panelKalah.SetActive (true);
		StopCoroutine ("PlayerMati");
		gameObject.SetActive (false);
	}

	[PunRPC]
	void statusMati(){
		statusAktifitas.text += "<color=magenta>" + pv.owner.NickName + " Kalah </color> \n";
		sudahKalah = true;
	}
		
	void GerakCepat()
	{
		partikelKecepatan.SetActive (true);
		gerakanPlayer.m_moveSpeed = KecepatanBaru;
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag (TagMusuh)) {
			if (pv.isMine) {
				sliderNyawa.value -= nilaiSeranganMusuh;
				nyawaPlayer -= nilaiSeranganMusuh;
				partikelSerang.SetActive (true);
				Debug.Log ("player menyentuh musuh");
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag (TagMusuh)) {
			partikelSerang.SetActive (false);
		}
	}

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
	{ 
		if (stream.isWriting) 
		{ 
			stream.SendNext (transform.position); 
			stream.SendNext (transform.rotation);
			stream.SendNext (partikelMati.activeInHierarchy);
			stream.SendNext (partikelSerang.activeInHierarchy);
			stream.SendNext (partikelKecepatan.activeInHierarchy);
			stream.SendNext (nyawaPlayer);
		} 
		else
		{ 
			syncPosPlayer = (Vector3)stream.ReceiveNext(); 
			syncRotPlayer = (Quaternion)stream.ReceiveNext();
			partikelMati.SetActive((bool)stream.ReceiveNext());
			partikelSerang.SetActive((bool)stream.ReceiveNext());
			partikelKecepatan.SetActive((bool)stream.ReceiveNext());
			nyawaPlayer = (float)stream.ReceiveNext();
		} 
	}
}
