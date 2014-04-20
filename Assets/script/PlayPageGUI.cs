using UnityEngine;
using System.Collections;

public class PlayPageGUI: MonoBehaviour {

	public Texture2D 	playerProfile;
	public Texture2D 	highLight;
	public Texture2D 	emptyTimeBar;
	public Texture2D 	fullTimeBar;
	public Texture2D playerYellow;
	public Texture2D playerGreen;
	public Texture2D playerPink;
	public Texture2D playerBlue;
	public GUIStyle     playerNameFont;

	public static float coolTime = 10f;

	private float    	timePercentage = 0f;
	private string[] playerName = {"", "", "", ""};

	int figureLength=90;
	int figureHeight=90;

	int timeBarLength=80;
	int timeBarHeight=20;

	int highLightLength=70;
	int highLightWidth=70;

	static string 	notifyMes;
	static float 	notifyTimer = 3f;



	public static void SetMes(string s) 
	{
		notifyTimer = 3f;
		notifyMes = s;

	}



	void Awake () 
	{
				Debug.Log ("start");
				StartCoroutine (prepareTimeCalculate ());
		Debug.Log (PhotonNetwork.playerList.Length);
		for (int i=0; i<PhotonNetwork.playerList.Length; i++) 
		        {
			Debug.Log (PhotonNetwork.playerList[i].name);
					playerName [i] = PhotonNetwork.playerList[i].name;
			//playerName[i] = "player";
						Debug.Log (playerName [i]);
				}
		}

	void Update () 
	{
		if (ProcessControl.gameStarts) 
		{
			if (ProcessControl.playTimer >= 0.0f) 
			{
				//finshOp();
			} 
			else 
			{
				ProcessControl.whoseTurn = (ProcessControl.whoseTurn+1) % PhotonNetwork.room.playerCount;
				ProcessControl.playTimer = ProcessControl.maxPlayTime;
			}
		}
	
	}

	void OnGUI()
	{
		Vector2 figurePosition = new Vector2 (Screen.width/2-430, Screen.height / 6-30);

		if (coolTime >= 0) {
			GUI.Label (new Rect (Screen.width/2-timeBarLength/2, Screen.height/11, 400, 50), "The Game Will Begin in " + coolTime.ToString () + " Seconds");
		}

		//timer
		if (coolTime < 0) {
			ProcessControl.gameStarts = true;

			if (ProcessControl.playTimer < ProcessControl.maxPlayTime)
			{
				timePercentage = ProcessControl.playTimer / ProcessControl.maxPlayTime;
				Rect guiBox = new Rect (Screen.width/2-timeBarLength/2, Screen.height/10, timeBarLength, timeBarHeight);
				GUI.BeginGroup (guiBox);
				GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarHeight), emptyTimeBar);
				GUI.EndGroup ();
				guiBox = new Rect (Screen.width/2-timeBarLength/2, Screen.height/10, timeBarLength * timePercentage, timeBarHeight);
				GUI.BeginGroup (guiBox);
				GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarHeight), fullTimeBar);
				GUI.EndGroup ();
									
				switch (ProcessControl.whoseTurn) 
				{
				case 0:
					GUI.Label (new Rect (figurePosition.x, figurePosition.y, highLightLength,highLightWidth), highLight);
					break;
				case 1:

					GUI.Label (new Rect (Screen.width-figurePosition.x+300, figurePosition.y,highLightLength,highLightWidth), highLight);
					break;

				case 2:
					GUI.Label (new Rect (figurePosition.x, Screen.height-figurePosition.y-figureHeight, highLightLength,highLightWidth), highLight);
					break;

				case 3:
					GUI.Label (new Rect (Screen.width-figurePosition.x-figureLength, Screen.height-figurePosition.y-figureHeight, highLightLength,highLightWidth), highLight);
					break;
				default:
					Debug.Log ("Bug happens");
					break;
				}	
			}
		}

		// player profile
		//GUIStyle fontStyle = new GUIStyle ();
		//fontStyle.fontSize = 12;


		
		GUI.Label (new Rect (figurePosition.x, figurePosition.y, figureLength, figureHeight), playerYellow);
		GUI.Label (new Rect (figurePosition.x, figurePosition.x+20, 30, 20), playerName[0], playerNameFont);
		
		
		GUI.Label (new Rect (Screen.width-figurePosition.x-figureLength+35, figurePosition.y, figureLength, figureHeight), playerGreen);
		GUI.Label (new Rect (Screen.width-figurePosition.x-figureLength, figurePosition.y+20, 30, 20), playerName[1], playerNameFont);
		
		GUI.Label (new Rect (figurePosition.x, Screen.height-figurePosition.y-figureHeight, figureLength, figureHeight), playerPink);
		GUI.Label (new Rect (figurePosition.x, Screen.height-figurePosition.y-figureHeight+20, 30, 20), playerName[2], playerNameFont);

		GUI.Label (new Rect (Screen.width-figurePosition.x-figureLength+35, Screen.height-figurePosition.y-figureHeight, figureLength, figureHeight), playerBlue);
		GUI.Label (new Rect (Screen.width-figurePosition.x-figureLength, Screen.height-figurePosition.y-figureHeight+20, 30, 20), playerName[3], playerNameFont);

		


		// notifymessage
		if (notifyTimer >= 0f) {
			notifyTimer -= Time.deltaTime;
			GUI.Label (new Rect (Screen.width/2-100, 5, 400, 30), notifyMes);
		}
    }

	public IEnumerator prepareTimeCalculate()
	{
		while (coolTime >= 0)
		{
			coolTime--;
			yield return new WaitForSeconds(1);   
		}
	}
}
