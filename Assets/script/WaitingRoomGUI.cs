using UnityEngine;
using System.Collections;

public class WaitingRoomGUI : MonoBehaviour {	

	private Vector2 enterButPos;
	private Vector2 enterButSize;



	void Start() {
		enterButPos = new Vector2 (Screen.width*0.58f, Screen.height*0.7f);
		enterButSize = new Vector2 (Screen.width*0.1f, Screen.height*0.1f);
	}



	void OnGUI(){
		if (GUI.Button(new Rect(enterButPos.x, enterButPos.y, enterButSize.x, enterButSize.y), "Enter")) {
			Application.LoadLevel(2);
		}
	}
}
