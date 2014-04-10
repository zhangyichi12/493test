using UnityEngine;
using System.Collections;

public class EnterPageGUI : MonoBehaviour {

	private Vector2 newGameButSize;
	private Vector2 newGameButPos;



	void Start() {
		newGameButSize = new Vector2(Screen.width*0.1f, Screen.height*0.06f);
		newGameButPos = new Vector2(Screen.width*0.45f, Screen.height*0.67f);
	}



	void OnGUI(){
		if (GUI.Button(new Rect(newGameButPos.x, newGameButPos.y, newGameButSize.x, newGameButSize.y), "New Game")) {
			Application.LoadLevel(1);
		}
	}
}
