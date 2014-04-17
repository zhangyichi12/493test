using UnityEngine;
using System.Collections;

public class PlayPageGUI: MonoBehaviour {

	public Texture2D playerProfile;
	public Texture2D highLight;
	public Texture2D emptyTimeBar;
	public Texture2D fullTimeBar;

	public static float coolTime = 10f;
	float maxTurnTime=20.0f;
	public bool gaming=false;
	public static int playerNo=0;
	public float ctime=20.0f;
	private float    timePercentage = 0f;

	int figureLength=50;
	int figureWidth=50;

	int timeBarLength=80;
	int timeBarWidth=10;

	int highLightLength=70;
	int highLightWidth=70;

	int time1;
	float Timer1 = 30;

	int playTime=3;
	bool highLight1;
	bool player1Turn;
	bool player2Turn;
	bool player3Turn;

	static string 	notifyMes;
	static float 	notifyTimer = 3f;



	public static void SetMes(string s) 
	{
		notifyTimer = 3f;
		notifyMes = s;
	}



	// Use this for initialization
	void Awake () {
		Debug.Log ("start");
		StartCoroutine (prepareTimeCalculate ());
		player1Turn=true;
		player1Turn=false;
		player1Turn=false;
	
	}
	
	// Update is called once per frame
	void Update () {
		if (gaming) {
			if (ctime >= 0.0f) {
				//finshOp();
			} else {
				playerNo = (playerNo + 1) % 2;
				ctime = 20.0f;
			}
		}
	
	}

	void FixedUpdate()
	{
		if(gaming)
			ctime-=Time.deltaTime;
	}

	void OnGUI()
	{
		if (coolTime >= 0) {
			GUI.Label (new Rect (10, 10, 400, 50), "The Game Will Begin in " + coolTime.ToString () + " Seconds");
		}


		if (coolTime < 0) {
			GameRun ();
			gaming = true;
			switch (playerNo) {
			case 0:
				Rect guiBox = new Rect (10, 130, timeBarLength, timeBarWidth);
				GUI.BeginGroup (guiBox);
				GUI.DrawTexture (new Rect (0, 0, timeBarLength, timeBarWidth), emptyTimeBar);
				GUI.EndGroup ();
				
				timePercentage = ctime / maxTurnTime;
				
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
				
				timePercentage = ctime / maxTurnTime;
//				Debug.Log ("ctime" + ctime);
				
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

		GUIStyle fontStyle = new GUIStyle ();
		fontStyle.fontSize = 12;
		
//		Debug.Log (Screen.height);
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


		if (player1Turn) {
			Debug.Log ("player1Turn");
			GUI.Label (new Rect (10, 50, 100, 100), highLight);
			GUI.Label (new Rect (10, 130, 400, 30), playTime.ToString () + "Seconds");
		}
		if (player2Turn) {
			GUI.Label (new Rect (10, 150, 100, 100), highLight);
		}
		if (player3Turn) {
			GUI.Label (new Rect (10, 250, 100, 100), highLight);
		}		


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


	public IEnumerator playTimeCalculate()
	{
		while (playTime >= 0)
		{
			playTime--;
			yield return new WaitForSeconds(1);   
		}
	}

	void GameRun(){
	//	Debug.Log ("Game Run");
		player1Turn = true;
		if (playTime>=0){
//		Debug.Log (playTime.ToString ()+"Seconds");
		StartCoroutine (playTimeCalculate ());
		}
		player1Turn = false;
		playTime = 3;

		}
}
