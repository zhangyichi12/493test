using UnityEngine;
using System.Collections;

public class MarbleState : MonoBehaviour {

	public bool moveable = true;
	public bool dragable = true;


	void Start() {
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<MarbleState> ().enabled = false;
	}

	void Update() {
		if (ProcessControl.gameStarts && !moveable && rigidbody2D.velocity.magnitude <= 0.5f) 
		{
			// dead
			gameObject.GetComponent<Animator>().enabled = true;
			rigidbody2D.isKinematic = true;
		}
	}



	void OnTriggerEnter2D(Collider2D collision) 
	{
		if (collision.gameObject.tag == "placeMargin")
		{
			dragable = false;
		}

		if (ProcessControl.gameStarts) 
		{
			moveable = true;
			ProcessControl.leftMoveableMarble [int.Parse (gameObject.name [0].ToString ())]++;
		}
	}
	
	void OnTriggerExit2D(Collider2D collision) 
	{
		dragable = false;

		if (ProcessControl.gameStarts) 
		{
			moveable = false;
			ProcessControl.leftMoveableMarble[int.Parse(gameObject.name[0].ToString())]--;
		}
	}
}
