using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {
	private RoomInfo[] 		roomList;



	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("alpha 1.0");
		PhotonNetwork.automaticallySyncScene = true;
	}



	void Update()
	{
		if (PhotonNetwork.room != null) 
		{
			if (PhotonNetwork.room.playerCount == PhotonNetwork.room.maxPlayers)
			{
				Application.LoadLevel(2);
			}
		}
	}



	void OnGUI()
	{
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

		if (GUI.Button(new Rect(100, 100, 250, 100), "Create Two Person Game"))
			CreateGame(2);

		if (GUI.Button (new Rect (100, 250, 250, 100), "Get Available Two Person Game"))
			GetAvailableGame (2);
		
		if (roomList != null)
		{
			int count = 0;
			for (int i = 0; i < roomList.Length; i++)
			{
				if (roomList[i].maxPlayers == 2)
				{
					string roomName = roomList[i].playerCount.ToString() + "/2";

					if (GUI.Button(new Rect(400, 100+(110*count), 200, 100), roomName))
						JoinGame(roomList[i]);
					count ++;
				}
			}
		}
	}
	
	
	
	private void CreateGame(int playerNum)
	{
		PhotonNetwork.CreateRoom (null, true, true, playerNum);
	}
	
	private void GetAvailableGame(int playerNum)
	{
		roomList = PhotonNetwork.GetRoomList ();
	}

	private void JoinGame(RoomInfo room)
	{
		PhotonNetwork.JoinRoom (room.name);
	}



	void OnJoinedRoom()
	{
		ExitGames.Client.Photon.Hashtable info = new ExitGames.Client.Photon.Hashtable();
		info.Add ("color", PhotonNetwork.room.playerCount);
		PhotonNetwork.player.SetCustomProperties (info);
	}
}
