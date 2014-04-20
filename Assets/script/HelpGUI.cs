using UnityEngine;
using System.Collections;

public class HelpGUI : MonoBehaviour {

	public Texture2D close;
	public Texture2D go;
	public Texture2D left;
	public Texture2D right;
	public Texture2D first;
	public Texture2D second;
	public Texture2D third;
	public Texture2D pic1;
	public Texture2D pic2;
	public Texture2D pic3;

	public GUIStyle rightButton;
	public GUIStyle leftButton;
	public GUIStyle closeButton;
	public GUIStyle goButton;


	private Vector2 leftPosition;
	private Vector2 rightPosition;
	private Vector2 playButtonArea;
	private Vector2 closePosition;
	private Vector2 goPosition;
	private Vector2 closeSize;
	private Vector2 goSize;
	private Vector2 pageNum;
	private Vector2 picPosition;

	private string instruction;
	private string string1;
	private string string2;
	private string string3;
	private Rect picRect;
	private Rect pageRect;



	// Use this for initialization
	void Start () {
		string1 = "Drag your marble to place them on your desired position within your area.";
		string2 = "Click Marble and drag...";
		string3 = "Once you don't have marbles inside circle area, you lose the game";
		instruction = string1;
		playButtonArea=new Vector2 (30,30);
		leftPosition= new Vector2 (10, Screen.width/3);
		rightPosition = new Vector2 (Screen.width - leftPosition.x - playButtonArea.x, leftPosition.y);
		closeSize = new Vector2 (40, 40);
		goSize = new Vector2 (55, 55);
		closePosition=new Vector2(5,Screen.height-closeSize.y-10);
		goPosition = new Vector2 (Screen.width - closePosition.x - goSize.x, closePosition.y-5);
		picRect = new Rect (Screen.width / 2 - 80, Screen.height / 3, 160, Screen.height / 3);
		pageRect = new Rect (Screen.width / 2 - 40, Screen.height / 3 + 150, 120, 50);


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{

		GUI.Label(new Rect (Screen.width / 2 - 80, Screen.height / 3 + 100, 160, Screen.height / 3+50),instruction);
		if (instruction == string1) 
		{
			GUI.Label(picRect, pic1);
			GUI.Label (pageRect,first);
		}

		if (instruction == string2) 
		{
			GUI.Label(picRect, pic2);
			GUI.Label (pageRect,second);
		}

		if (instruction == string3) 
		{
			GUI.Label(picRect, pic3);
			GUI.Label (pageRect,third);
		}



		if (GUI.Button (new Rect(closePosition.x,closePosition.y,closeSize.x,closeSize.y),"",closeButton))
		{
		    Application.LoadLevel("Welcome");
		}

		if (GUI.Button (new Rect(goPosition.x,goPosition.y,goSize.x,goSize.y),"",goButton))
		{
			Application.LoadLevel("Prepare");
		}

		if(GUI.Button (new Rect (rightPosition.x,rightPosition.y,playButtonArea.x,playButtonArea.y),"",rightButton))
		{
			
			switch (instruction) 
			{
			case "Drag your marble to place them on your desired position within your area.":
				instruction="Click Marble and drag...";
				GUI.Label(new Rect (Screen.width/2-60, Screen.height/3, 120,Screen.height/3+80), pic2);
				break;
			case "Click Marble and drag...":
				instruction="Once you don't have marbles inside circle area, you lose the game";
				GUI.Label(new Rect (Screen.width/2-60, Screen.height/3, 120,Screen.height/3+80), pic3);
				break;
			case "Once you don't have marbles inside circle area, you lose the game":
				//				instruction="Once you don't have marbles inside circle area, you lose the game";
				//				GUI.Label(new Rect (Screen.width-60, Screen.height/3, 120,Screen.height/3+80), pic3);
				break;
			default:
				Debug.Log ("Bug happens");
				break;
				
				//			if(instruction == "Drag your marble to place them on your desired position within your area.")
				//				instruction = "Click Marble and drag...
				//			else if(instruction
				//				renderer.material.mainTexture = color1;
			}
		}

			if(GUI.Button (new Rect (leftPosition.x,leftPosition.y,playButtonArea.x,playButtonArea.y),"",leftButton))
		{

			switch (instruction) 
			{
			case "Drag your marble to place them on your desired position within your area.":
				
				break;
			case "Click Marble and drag...":
				instruction="Drag your marble to place them on your desired position within your area.";
				GUI.Label(new Rect (Screen.width/2-60, Screen.height/3, 120,Screen.height/3+80), pic1);
				break;
			case "Once you don't have marbles inside circle area, you lose the game":
				instruction="Click Marble and drag...";
				GUI.Label(new Rect (Screen.width/2-60, Screen.height/3, 120,Screen.height/3+80), pic2);
				break;
			default:
				Debug.Log ("Bug happens");
				break;

//			if(instruction == "Drag your marble to place them on your desired position within your area.")
//				instruction = "Click Marble and drag...
//			else if(instruction
//				renderer.material.mainTexture = color1;
		     }


	    }
}
}
