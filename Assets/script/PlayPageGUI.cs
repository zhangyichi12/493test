using UnityEngine;
using System.Collections;

public class PlayPageGUI: MonoBehaviour {

	public Texture2D 	playerProfile;
	public Texture2D 	highLight;
	public Texture2D 	emptyTimeBar;
	public Texture2D 	fullTimeBar;

	public static float coolTime = 10f;

	private float    	timePercentage = 0f;

	int figureLength=50;
	int figureWidth=50;

	int timeBarLength=80;
	int timeBarWidth=10;

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
		if (coolTime >= 0) {
			GUI.Label (new Rect (10, 10, 400, 50), "The Game Will Begin in " + coolTime.ToString () + " Seconds");
		}

		//timer
		if (coolTime < 0) {
			ProcessControl.gameStarts = true;

			if (ProcessControl.playTimer < ProcessControl.maxPlayTime)
			{
				timePercentage = ProcessControl.playTimer / ProcessControl.maxPlayTime;
				switch (ProcessControl.whoseTurn) 
				{
				case 0:
					Rect guiBox = new Rect (10, 130, timeBarLength, timeBarWidth);
					GUI.BeginGroup (guiBox);
					GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarWidth), emptyTimeBar);
					GUI.EndGroup ();
					
					guiBox = new Rect (10, 130, timeBarLength * timePercentage, timeBarWidth);
					GUI.BeginGroup (guiBox);
					GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarWidth), fullTimeBar);
					GUI.EndGroup ();
					GUI.Label (new Rect (10, 55, highLightLength,highLightWidth), highLight);
					break;
				case 1:
					guiBox = new Rect (10, 230, timeBarLength, timeBarWidth);
					GUI.BeginGroup (guiBox);
					GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarWidth), emptyTimeBar);
					GUI.EndGroup ();
					
					guiBox = new Rect (10, 230, timeBarLength * timePercentage, timeBarWidth);
					GUI.BeginGroup (guiBox);
					GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarWidth), fullTimeBar);
					GUI.EndGroup ();
					GUI.Label (new Rect (10, 155, highLightLength,highLightWidth), highLight);
					break;
				default:
					Debug.Log ("Bug happens");
					break;
				}	
			}
		}

		// player profile
		GUIStyle fontStyle = new GUIStyle ();
		fontStyle.fontSize = 12;

		GUILayout.BeginArea (new Rect (10, 10, Screen.width / 4, Screen.height));
		GUILayout.BeginVertical ();
		
		GUI.Label (new Rect (10, 50, figureLength, figureWidth), playerProfile);
		GUI.Label (new Rect (10, 100, 30, 20), "Player 1", fontStyle);
		
		
		GUI.Label (new Rect (10, 150, figureLength, figureWidth), playerProfile);
		GUI.Label (new Rect (10, 200, 30, 20), "Player 2", fontStyle);
		
		GUI.Label (new Rect (10, 250, figureLength, figureWidth), playerProfile);
		GUI.Label (new Rect (10, 300, 30, 20), "Player 3", fontStyle);		
		
		GUILayout.EndVertical ();
		GUILayout.EndArea ();

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
