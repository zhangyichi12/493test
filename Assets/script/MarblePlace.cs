using UnityEngine;
using System.Collections;

public class MarblePlace : MonoBehaviour {
	
	private GameObject dragMarble = null;

	void Update () {

		if (GUITest.coolTime >= 0f)
		{
			if (Input.GetMouseButtonDown(0)) {
				dragMarble = DragMarbelByMousePos();
			}

			if (Input.GetMouseButton(0) && dragMarble != null) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pos.z = 0f;
				dragMarble.transform.position = pos;
			}
		
			if (Input.GetMouseButtonUp (0)) {
				dragMarble = null;
			}
		}
	}

	#region helperFunc
	public static GameObject DragMarbelByMousePos() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
