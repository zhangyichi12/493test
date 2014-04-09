using UnityEngine;
using System.Collections;

public class MarbleSelect : MonoBehaviour {

	private GameObject 	selectedMarble = null;
	
	void Update () {
		if (GUITest.coolTime < 0f && !GUITest.isGameEndFlag) {
			if (Input.GetMouseButtonDown(0)) {
				selectedMarble = SelectMarbelByMousePos();
				if (selectedMarble != null && selectedMarble.GetComponent<MarbelInfo>().playerID == GUITest.playerNo) {
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
		//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit.collider != null && hit.rigidbody != null) {
			GameObject go = hit.rigidbody.gameObject;
			if (go.tag.Equals("marble")) {
				return go;
			}
		}
		return null;
	}
	#endregion
}
