using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AmbilRoom
{
	public class Room
	{

		public byte maksimumJumlahPlayer = 0;
		public string namaRoom;
		public int jumlahPlayer = 0;

		public Room (string namaRoom2, byte maksimumJumlahPlayer2, int jumlahPlayer2)
		{
			maksimumJumlahPlayer = maksimumJumlahPlayer2;
			namaRoom = namaRoom2;
			jumlahPlayer = jumlahPlayer2;
		}

		public Room (RoomInfo room) : this(room.name, room.maxPlayers, room.playerCount)
		{

		}
	}
}

