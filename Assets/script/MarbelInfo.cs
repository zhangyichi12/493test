using UnityEngine;
using System.Collections;

public class MarbelInfo : MonoBehaviour {

	public bool moveable = true;


	public int playerID;



	void Update() {
		if (!moveable && rigidbody2D.velocity.magnitude == 0) {
			// dead
			rigidbody2D.isKinematic = true;
		}
	}



	//public void SetPlayerID(int id) {
	//	playerID = id;
	//}



	void OnTriggerEnter2D(Collider2D collision) {
		moveable = true;
	}

	void OnTriggerExit2D(Collider2D collision) {
		moveable = false;
	}
}
