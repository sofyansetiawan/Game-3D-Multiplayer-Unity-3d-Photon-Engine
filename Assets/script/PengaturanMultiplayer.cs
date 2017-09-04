using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PengaturanMultiplayer : MonoBehaviour
{

    public int maksimumPlayerPerRoom = 5;
    public int maksimumRoom = 4;
    public string namaRoom = "Room";
	public string versiKonek = "1.0";

    TypedLobby lobby1 = new TypedLobby("Lobby1", LobbyType.Default);
    // Use this for initialization
    void Start()
    {
		if (!PhotonNetwork.connected) {
			PhotonNetwork.ConnectUsingSettings (versiKonek);
		}
    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<AmbilRoom.Room> MendapatkanDaftarRoom(int maksimumRoom, int maksimumPlayerPerRoom)
    {
        List<string> namaSemuaRoom = new List<string>();
        for (int i = 1; i <= maksimumRoom; i++)
        {
            namaSemuaRoom.Add(namaRoom + " " + i);
        }

        List<AmbilRoom.Room> rooms = new List<AmbilRoom.Room>();
        foreach (RoomInfo infoRoom in PhotonNetwork.GetRoomList())
        {
            rooms.Add(new AmbilRoom.Room(infoRoom));
        }
        List<string> namaRoomTersedia = rooms.Select(room => room.namaRoom).ToList();
        foreach (string namaRoom in namaSemuaRoom.Except(namaRoomTersedia))
        {
            rooms.Add(new AmbilRoom.Room(namaRoom, (byte)maksimumPlayerPerRoom, 0));
        }
        return rooms;
    }

    public void GabungRoom(string namaRoom, string namaPlayer)
    {
        if (namaPlayer.Length <= 0 || namaRoom.Length <= 0)
        {
            return;
        }
        RoomOptions opsiRoom = new RoomOptions()
        {
            isVisible = true,
            maxPlayers = (byte)maksimumPlayerPerRoom
        };
        PhotonNetwork.player.name = namaPlayer;
        PhotonNetwork.JoinOrCreateRoom(namaRoom, opsiRoom, lobby1);
    }

    void OnCreatedRoom()
    {
        Debug.Log("Room sudah dibuat");
    }

    void OnConnectedToPhoton()
    {
        Debug.Log("Terkoneksi ke Photon");
    }

    void OnJoinedLobby()
    {
        Debug.Log("Bergabung ke Lobby");
    }

    void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(lobby1);
        Debug.Log("Terkoneksi ke MasterServer");
    }

    void OnJoinedRoom()
    {
        Debug.Log("Bergabung Ke Room : " + PhotonNetwork.room.Name);
    }

    void OnPhotonCreateRoomFailed()
    {
        Debug.Log("Gagal Membuat Room");
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("Terputus dengan Photon");
    }

    public void MeninggalkanRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    void OnReceivedRoomListUpdate()
    {
        Debug.Log("Ada Perubahan Daftar Room");
        GameObjectHelper.SendMessageToAll("SaatPerubahanDaftarRoom", MendapatkanDaftarRoom(maksimumRoom, maksimumPlayerPerRoom));
    }
}
