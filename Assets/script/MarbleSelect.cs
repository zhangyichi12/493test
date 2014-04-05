using UnityEngine;
using System.Collections;

public class MarbleSelect : MonoBehaviour {

	private GameObject 	selectedMarble = null;
	
	void Update () {
		if (!MarblePlace.dragable) {
			if (Input.GetMouseButtonDown(0)) {
				selectedMarble = SelectMarbelByMousePos();
				if (selectedMarble != null) {
					selectedMarble.GetComponent<MarbleMove>().selected = true;
				}
			}

			if (Input.GetMouseButtonUp (0)) {
				selectedMarble = null;
			}
		}
	}


	
	#region helperFunc
	public static GameObject SelectMarbelByMousePos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit != null && hit.collider != null && hit.rigidbody != null) {
			GameObject go = hit.rigidbody.gameObject;
			if (go.tag.Equals("marble")) {
				return go;
			}
		}
		return null;
	}

	private float GetDragDistance() {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return (pos - selectedMarble.transform.position).magnitude;
	}
	#endregion
}
