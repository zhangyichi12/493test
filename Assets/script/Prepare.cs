using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prepare : MonoBehaviour {

    public string[] colors = { "pink", "yellow", "green", "blue" };
    public GUISkin theSkin;
    public AudioSource ding;
    private bool canStart = false;
    private float countDown = 10f;

    void Awake()
    {
        // Give you (the player) a random name.
        PhotonNetwork.playerName = "Player" + Random.Range(1, 999);
        
        // This flag enables all the players go to the Play scene at the same time.
        PhotonNetwork.automaticallySyncScene = true;
        
        // Connect to the server.
        PhotonNetwork.ConnectUsingSettings("1.0");

    }

    void Update()
    {
        if (PhotonNetwork.room != null)
        {
            if (PhotonNetwork.playerList.Length == PhotonNetwork.room.maxPlayers)
            {
                canStart = true;
                countDown -= Time.deltaTime;
            }
        }

        if (countDown < 0)
        {
            ding.Play();
			Application.LoadLevel("play");
        }
    }

    void OnGUI()
    {
        GUI.skin = theSkin;
        GUILayout.BeginArea(new Rect(Screen.width / 2 - 550/2, Screen.height / 2 - 200, 550, 800));
        GUILayout.BeginVertical();

        if (canStart)
        {
            GUILayout.Label("The game will start in " + Mathf.CeilToInt(countDown) + " seconds", "label");
        }
        else
        {
            GUILayout.Label("Waiting for other players...", "label");
        }

        GUILayout.Label("( " + PhotonNetwork.playerList.Length + " / " + PhotonNetwork.room.maxPlayers + " )", "label");
        GUILayout.Space(32);
        GUILayout.Label("You");
        PhotonNetwork.playerName = GUILayout.TextField(PhotonNetwork.playerName);
        GUILayout.Space(32);
        GUILayout.Label("Other Players");
        foreach (PhotonPlayer player in PhotonNetwork.otherPlayers)
        {
            GUILayout.Label(player.name);
        }
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void OnJoinedLobby()
    {
        // Join a random room.
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        PhotonNetwork.CreateRoom("", new RoomOptions() { maxPlayers = 4 }, null);
    }

//    void OnJoinedRoom()
//    {
//        ExitGames.Client.Photon.Hashtable info = new ExitGames.Client.Photon.Hashtable();
//        info.Add(colors[PhotonNetwork.room.playerCount - 1], PhotonNetwork.room.playerCount - 1);
//        PhotonNetwork.player.SetCustomProperties(info);
//    }


	private void JoinGame(RoomInfo room)
	{
		PhotonNetwork.JoinRoom (room.name);
	}
	
	
	
	void OnJoinedRoom()
	{
		ExitGames.Client.Photon.Hashtable info = new ExitGames.Client.Photon.Hashtable();
		info.Add ("color", PhotonNetwork.room.playerCount-1);
		PhotonNetwork.player.SetCustomProperties (info);
	}
}
