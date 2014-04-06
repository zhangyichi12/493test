using UnityEngine;
using System.Collections;

public class Button_Enter : MonoBehaviour {		
		void OnGUI(){
			if (GUI.Button (new Rect (395, 280, 80, 18), "Enter")) {
				Application.LoadLevel(2);
			}
		}
}
