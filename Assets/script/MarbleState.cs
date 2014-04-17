using UnityEngine;
using System.Collections;

public class MarbleState : MonoBehaviour {

	private bool moveable = true;



	void Start() {
		gameObject.GetComponent<Animator> ().enabled = false;
	}

	void Update() {
		if (!moveable && rigidbody2D.velocity.magnitude <= 0.5f) {
			// dead
			gameObject.GetComponent<Animator>().enabled = true;
			rigidbody2D.isKinematic = true;
		}
	}



	void OnTriggerEnter2D(Collider2D collision) {
		moveable = true;
	}
	
	void OnTriggerExit2D(Collider2D collision) {
		moveable = false;
	}
}
