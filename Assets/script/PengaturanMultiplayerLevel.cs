using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class PengaturanMultiplayerLevel : Photon.PunBehaviour {

	public Text namaPlayer;
	public Text statusAktifitas;
	public Text namaRoom;
	public Text skorPlayer;
	public Text jumlahPlayer;
	public Text kalahSkor;
	public Text kalahNamaPlayer;
	public Text jumlahMusuh;
	public Text teksDaftarMenangPlayer;

	public Button tombolKeluarLevel;
	Button TombolKeluarLevel;

	public Transform player;
	public string tagPlayer = "Player";
	public string namaSceneKeluar;
	public string tagMusuh = "Musuh";
	public string sceneSelanjutnya;

	public GameObject panelMenang;
	public GameObject panelKalah;
	public GameObject panelPesan;

	public float lamaPanelMenang = 10f;
	private int skor = 0;
	private int jumlahMusuhTotal = 3;
	private bool sudahMenang = false;

	public float radiusPlayer = 7f;

	PhotonView pv;

	// Use this for initialization
	void Start () {
		pv = this.GetComponent<PhotonView>();

		if (PhotonNetwork.connected) {
			MulaiPlayer ();
			statusAktifitas.text += "<color=yellow>Mulai Bergabung</color>\n";
			PhotonNetwork.player.SetScore (0);
		}

		if (pv.isMine) {
			skor = (int)PhotonNetwork.player.GetScore ();
			namaPlayer.text = PhotonNetwork.player.NickName;
			kalahNamaPlayer.text = "<color=lime>" + PhotonNetwork.player.NickName + "</color>";
		}

		teksDaftarMenangPlayer.text = "";

		panelMenang.SetActive (false);
		panelPesan.SetActive (false);

		skorPlayer.text = "Skor " + skor.ToString();
		kalahSkor.text = "<color=cyan>Skor " + skor.ToString() + "</color>";
		namaRoom.text = PhotonNetwork.room.Name;

		TombolKeluarLevel = tombolKeluarLevel.GetComponent<Button> ();
		TombolKeluarLevel.onClick.AddListener (() => MeninggalkanRoom());
	}

	// Update is called once per frame
	void Update () {
		if (!PhotonNetwork.connected) {
			PhotonNetwork.offlineMode = true;
		}

		jumlahMusuhTotal = GameObject.FindGameObjectsWithTag (tagMusuh).Length;
		jumlahPlayer.text = PhotonNetwork.countOfPlayersInRooms.ToString() + " Player";
		jumlahMusuh.text = jumlahMusuhTotal.ToString () + " Musuh";
		skor = (int)PhotonNetwork.player.GetScore ();

		if (pv.isMine) {
			kalahSkor.text = "<color=cyan>Skor " + skor.ToString () + "</color>";
			skorPlayer.text = "Skor " + skor.ToString ();
		}

		if (jumlahMusuhTotal <= 0 && sudahMenang == false) {
			pv.RPC("KamuMenang", PhotonTargets.AllBuffered);
			if (panelMenang.activeInHierarchy) {
				List<PhotonPlayer> players = PhotonNetwork.playerList.OrderByDescending(p => p.GetScore()).ToList ();
				foreach (var player in players) {
					teksDaftarMenangPlayer.text += "Player : <b><color=lime>" + player.NickName + "</color></b> , Skor : <b><color=cyan>" + player.GetScore ().ToString () + "</color></b> \n";
				}
			}
		}
	}

	void OnLeftRoom(){
		pv.RPC ("KirimPesan", PhotonTargets.All , "<color=red>" + PhotonNetwork.player.NickName + " Meninggalkan Room </color>\n");
		Scene sceneIni = SceneManager.GetActiveScene ();
		if (sceneIni.name != namaSceneKeluar){
			PhotonNetwork.LoadLevel (namaSceneKeluar);
		}
	}

	public void MeninggalkanRoom()
	{
		if (PhotonNetwork.connected) {
			PhotonNetwork.LeaveRoom ();
			PhotonNetwork.LeaveLobby ();
			PhotonNetwork.DestroyPlayerObjects (PhotonNetwork.player.ID);
		}
	}

	void MulaiPlayer(){
		int id1 = PhotonNetwork.AllocateViewID();
		Vector3 posisiRandom = new Vector3(Random.Range(-radiusPlayer, radiusPlayer), 0f, Random.Range(-radiusPlayer, radiusPlayer));
		pv.RPC ("SpawnPlayer", PhotonTargets.All , posisiRandom, transform.rotation, id1);
		pv.RPC ("KirimPesan", PhotonTargets.All , "<color=cyan>" + PhotonNetwork.player.NickName + " Bergabung </color>\n");
	}

	[PunRPC]
	void SpawnPlayer(Vector3 posisi, Quaternion rotasi, int id1){
		if (PhotonNetwork.connected) {
			Transform Player = Instantiate (player, posisi, rotasi) as Transform;
			Player.name = player.name;
			PhotonView[] nViews = Player.GetComponentsInChildren<PhotonView> (); 
			nViews [0].viewID = id1;
		}
	}

	[PunRPC]
	void KirimPesan (string pesan) {
		statusAktifitas.text += pesan;
	}

	[PunRPC]
	IEnumerator KamuMenang () {
		sudahMenang = true;
		panelKalah.SetActive (false);
		panelPesan.SetActive (false);
		panelMenang.SetActive (true);
		yield return new WaitForSeconds(lamaPanelMenang);
		panelKalah.SetActive (false);
		panelPesan.SetActive (true);
		panelMenang.SetActive (false);
		yield return new WaitForSeconds(lamaPanelMenang);
		Scene sceneIni = SceneManager.GetActiveScene ();
		if (sceneIni.name != sceneSelanjutnya){
			PhotonNetwork.LoadLevel (sceneSelanjutnya);
		}
	}
}