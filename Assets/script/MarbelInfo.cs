using UnityEngine;
using System.Collections;

public class MarbelInfo : MonoBehaviour {

	public bool moveable = true;
   
    public int playerID;

	void OnTriggerEnter2D(Collider2D collision) {
		moveable = true;
	}

	void OnTriggerExit2D(Collider2D collision) {
		moveable = false;
	}
}
