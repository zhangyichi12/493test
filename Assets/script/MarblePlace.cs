using UnityEngine;
using System.Collections;

public class MarblePlace : MonoBehaviour {

	private GameObject  dragMarble = null;
	private Vector3 	oriPos;
	private bool		dragPosFine = true;

	void Update () {

		if (GUITest.coolTime >= 0f)
		{
			if (Input.GetMouseButtonDown(0)) {
				dragMarble = MarbleSelect.SelectMarbelByMousePos();
				if (dragMarble != null) {
					oriPos = dragMarble.transform.position;
				}
			}

			if (Input.GetMouseButton(0) && dragMarble != null) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pos.z = 0f;
				dragMarble.transform.position = pos;
			}
		
			if (Input.GetMouseButtonUp (0) && dragMarble != null) {
				if (!dragPosFine) {
					dragMarble.transform.position = oriPos;
					GUITest.SetMes("Marble CANNOT be placed outside scoring area.");
				}
				dragMarble = null;
			}
		}
		else 
		{
			gameObject.GetComponent<MarbleMove> ().enabled = true;
			gameObject.GetComponent<MarbleSelect> ().enabled = true;
			gameObject.GetComponent<MarblePlace>().enabled = false;
		}
	}



	void OnTriggerEnter2D(Collider2D collision) {
		dragPosFine = true;
	}
	
	void OnTriggerExit2D(Collider2D collision) {
		dragPosFine = false;
	}
}
