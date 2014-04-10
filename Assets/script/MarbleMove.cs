using UnityEngine;
using System.Collections;

public class MarbleMove : MonoBehaviour {

	public bool 		selected = false;

	public Texture2D	circleArrow;
	public Texture2D	emptyBar;
	public Texture2D	fullBar;
	
	private float		pixelToUnit = 50f;
	
	private float 		forceMax = 8000f;
	private float 		dragMaxLength = 6f;
	private float 		forcePercentage = 0f;
	
	private float		rotateAngle = 0f;

	private bool		moveable = true;



	void Start() {
		gameObject.GetComponent<Animator> ().enabled = false;
		gameObject.GetComponent<MarbleMove> ().enabled = false;
	}



	void Update() {
		if (!moveable && rigidbody2D.velocity.magnitude <= 0.5f) {
			// dead
			gameObject.GetComponent<Animator>().enabled = true;
			rigidbody2D.isKinematic = true;
		}
		
		if (Input.GetMouseButtonUp(0)) {
			if (selected && moveable) {
				// shoot marble
				if (gameObject != MarbleSelect.SelectMarbelByMousePos()) {
					Vector3	releasePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Vector2 dir = new Vector2 (transform.position.x-releasePos.x, 
					                           transform.position.y-releasePos.y);
					rigidbody2D.AddForce (dir / dir.magnitude * forceMax * forcePercentage);
				}
			}
			// no longer selected
			selected = false;
		}
	}



	void OnGUI () {		
		if (Input.GetMouseButton(0)) {
			if (selected) {	
				if (moveable) {
					Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					mousePos.z = 0f;
					rotateAngle = Mathf.Rad2Deg * 
						Mathf.Asin((mousePos.y - transform.position.y)
						           /(mousePos - transform.position).magnitude);
					if (mousePos.x > transform.position.x) {
						rotateAngle = Mathf.Sign(mousePos.y-transform.position.y)*180 - rotateAngle;
					} 

					//draw circle direction arrow
					Vector3 leftTopPos = 
						new Vector3 (transform.position.x - circleArrow.width/2/pixelToUnit,
						 			 transform.position.y + circleArrow.height/2/pixelToUnit,
									 transform.position.z);				
					Vector3 screenPos = Camera.main.WorldToScreenPoint(leftTopPos);	
					screenPos.y = Screen.height - screenPos.y;
					
					float size = (Camera.main.WorldToScreenPoint(new Vector3(circleArrow.width/pixelToUnit,0f,0f))
					              - Camera.main.WorldToScreenPoint(new Vector3(0f,0f,0f))).magnitude;
					Rect guiBox = new Rect(screenPos.x, screenPos.y, size, size);
					GUIUtility.RotateAroundPivot (rotateAngle, guiBox.center); 
					GUI.DrawTexture(guiBox, circleArrow);
					
					//draw force bar
					leftTopPos = new Vector3 
						(transform.position.x + renderer.bounds.size.x/2,
						 transform.position.y + fullBar.height/2/pixelToUnit,
						 transform.position.z);				
					screenPos = Camera.main.WorldToScreenPoint(leftTopPos);	
					screenPos.y = Screen.height - screenPos.y;

					float length = (Camera.main.WorldToScreenPoint(new Vector3(fullBar.width/pixelToUnit,0f,0f))
					                - Camera.main.WorldToScreenPoint(new Vector3(0f,0f,0f))).magnitude;
					float height = (Camera.main.WorldToScreenPoint(new Vector3(fullBar.height/pixelToUnit,0f,0f))
					                - Camera.main.WorldToScreenPoint(new Vector3(0f,0f,0f))).magnitude;
					guiBox = new Rect(screenPos.x, screenPos.y, length, height);
					GUI.BeginGroup(guiBox);
					GUI.DrawTexture(new Rect(0, 0, length, height), emptyBar);
					GUI.EndGroup();
					
					forcePercentage = (mousePos-transform.position).magnitude-renderer.bounds.size.x/2;
					forcePercentage = Mathf.Min (forcePercentage / dragMaxLength, 1f);
					
					guiBox = new Rect(screenPos.x, screenPos.y, 
					                  forcePercentage*length, height);
					GUI.BeginGroup(guiBox);
					GUI.DrawTexture(new Rect(0, 0, length, height), fullBar);
					GUI.EndGroup();
				} else {
					GUITest.SetMes("The marble is dead and CANNOT be moved.");
				}
			}
		}
	}



	void OnTriggerEnter2D(Collider2D collision) {
		moveable = true;
	}
	
	void OnTriggerExit2D(Collider2D collision) {
		moveable = false;
	}
}
