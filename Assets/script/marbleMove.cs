using UnityEngine;
using System.Collections;

public class MarbleMove : MonoBehaviour {

	public bool 		selected = false;

	public Texture2D	circleArrow;
	public Texture2D	emptyBar;
	public Texture2D	fullBar;
	
	private float		pixelToUnit = 100f;
	private float		circleArrowSize = 150f;
	private float		forceArrowLength = 100f;
	private float		forceArrowWidth = 40f;
	
	private float 		forceMax = 1500f;
	private float 		dragMaxLength = 3f;
	private float 		forcePercentage = 0f;
	
	private float		rotateAngle = 0f;

	void Update() {
		if (Input.GetMouseButtonUp(0)) {
			if (selected && gameObject.GetComponent<MarbelInfo>().moveable) {
				// shoot marble
				if (gameObject != MarbleSelect.SelectMarbelByMousePos()) {
					Vector3	releasePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					Vector2 dir = new Vector2 (transform.position.x-releasePos.x, 
					                           transform.position.y-releasePos.y);
					rigidbody2D.AddForce (dir / dir.magnitude * forceMax * forcePercentage);
                    StartCoroutine(changeShotVal());
				}
			}
			// no longer selected
			selected = false;
		}
	}



	void OnGUI () {		
		if (Input.GetMouseButton(0)) {
			if (selected && gameObject.GetComponent<MarbelInfo>().moveable) {	
				Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				mousePos.z = 0f;
				rotateAngle = Mathf.Rad2Deg * 
					Mathf.Asin((mousePos.y - transform.position.y)
					           /(mousePos - transform.position).magnitude);
				if (mousePos.x > transform.position.x) {
					rotateAngle = Mathf.Sign(mousePos.y-transform.position.y)*180 - rotateAngle;
				} 
				
				//draw circle direction arrow
				Vector3 leftTopPos = new Vector3 
					(transform.position.x - circleArrow.width/2/pixelToUnit,
					 transform.position.y + circleArrow.height/2/pixelToUnit,
					 transform.position.z);				
				Vector3 screenPos = Camera.main.WorldToScreenPoint(leftTopPos);	
				screenPos.y = Screen.height - screenPos.y;
				
				Rect guiBox = new Rect(screenPos.x, screenPos.y, circleArrowSize, circleArrowSize);
				GUIUtility.RotateAroundPivot (rotateAngle, guiBox.center); 
				GUI.DrawTexture(guiBox, circleArrow);
				
				//draw force bar
				leftTopPos = new Vector3 
					(transform.position.x + renderer.bounds.size.x/2,
					 transform.position.y + fullBar.height/2/pixelToUnit,
					 transform.position.z);				
				screenPos = Camera.main.WorldToScreenPoint(leftTopPos);	
				screenPos.y = Screen.height - screenPos.y;
				
				guiBox = new Rect(screenPos.x, screenPos.y, forceArrowLength, forceArrowWidth);
				GUI.BeginGroup(guiBox);
				GUI.DrawTexture(new Rect(0, 0, forceArrowLength, forceArrowWidth), emptyBar);
				GUI.EndGroup();
				
				forcePercentage = (mousePos-transform.position).magnitude-renderer.bounds.size.x/2;
				forcePercentage = Mathf.Min (forcePercentage / dragMaxLength, 1f);
				
				guiBox = new Rect(screenPos.x, screenPos.y, 
				                  forcePercentage*forceArrowLength, forceArrowWidth);
				GUI.BeginGroup(guiBox);
				GUI.DrawTexture(new Rect(0, 0, forceArrowLength, forceArrowWidth), fullBar);
				GUI.EndGroup();
			}
		}
	}

    public IEnumerator changeShotVal()
    {
        yield return new WaitForSeconds(1);
        GUITest.playerHasShot = true;
    }
}
