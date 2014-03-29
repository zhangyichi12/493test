using UnityEngine;
using System.Collections;

public class MarbleSelect : MonoBehaviour {

	private GameObject selectedMarble = null;
	
	
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			this.selectedMarble = SelectMarbelByMousePos();
		}
		if (Input.GetMouseButtonUp (0)) {
			if (selectedMarble != null) {
				selectedMarble.GetComponent<marbleMove>().setDes(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			}
		}
	}
	
	
	
	#region helperFunc
	private GameObject SelectMarbelByMousePos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit != null && hit.collider != null) {
			return hit.rigidbody.gameObject;
		}
		return null;
	}
	#endregion
}
