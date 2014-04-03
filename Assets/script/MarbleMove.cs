using UnityEngine;
using System.Collections;

public class MarbleMove : MonoBehaviour {
	
	private float force = 10f;
	
	public void setDes(Vector3 des) {
		Vector2 dir = new Vector2 (des.x-transform.position.x, des.y-transform.position.y);
		this.rigidbody2D.AddForce (dir * dir.magnitude * force);
	}
	
	void onCollision2D(GameObject go) {

	}
}
