using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NamaPlayer : MonoBehaviour {

	public static string namaPlayer = "player_1";
	public Text placeholderInput;
	public Text inputNama;

	// Use this for initialization
	void Start () {
		string nama_awal = SystemInfo.deviceName;
		nama_awal = (nama_awal.Length < 3) ? "PLAYER" + nama_awal : nama_awal;
		nama_awal = (nama_awal.Length > 12) ? nama_awal.Substring(0, 12) : nama_awal;
		namaPlayer = (PlayerPrefs.GetString ("Nama Player").Length < 3) ? nama_awal : PlayerPrefs.GetString ("Nama Player");
		placeholderInput.text = namaPlayer;
		inputNama.text = namaPlayer;
		PlayerPrefs.SetString ("Nama Player", namaPlayer);
		PlayerPrefs.Save ();
	}

	public void SimpanNamaPlayer(){
		namaPlayer = inputNama.text;
		namaPlayer = (namaPlayer.Length < 3) ? "PLAYER" + namaPlayer : namaPlayer;
		PlayerPrefs.SetString ("Nama Player", namaPlayer);
		PlayerPrefs.Save ();
	}
}
