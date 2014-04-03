using UnityEngine;
using System.Collections;

public class MarbleSelect : MonoBehaviour {

	private GameObject 	selectedMarble = null;
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			selectedMarble = SelectMarbelByMousePos();
			selectedMarble.GetComponent<MarbleMove>().selected = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			selectedMarble = null;
		}
	}


	
	#region helperFunc
	public static GameObject SelectMarbelByMousePos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit != null && hit.collider != null) {
			return hit.rigidbody.gameObject;
		}
		return null;
	}

	private float GetDragDistance() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return (pos - selectedMarble.transform.position).magnitude;
	}
	#endregion
}
