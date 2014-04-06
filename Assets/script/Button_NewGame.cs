using UnityEngine;
using System.Collections;

public class Button_NewGame : MonoBehaviour {
		void OnGUI(){
			if (GUI.Button (new Rect (285, 255, 80, 18), "New Game")) {
				Application.LoadLevel(1);
			}
		}
}
