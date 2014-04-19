using UnityEngine;
using System.Collections;

public class MarbleSelect : MonoBehaviour {

	private GameObject 	selectedMarble = null;

	private int			color;



	void Start() 
	{
		color = (int)PhotonNetwork.player.customProperties["color"];	

		gameObject.GetComponent<MarbleSelect> ().enabled = false;
	}



	void Update () 
	{
		if (ProcessControl.canTakeInput)
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				selectedMarble = SelectMarbelByMousePos();
				if (selectedMarble != null) 
				{
					if (selectedMarble.name[0].ToString() == color.ToString()) 
					{
						selectedMarble.GetComponent<MarbleMove>().selected = true;
					} 
					else 
					{
						PlayPageGUI.SetMes("You CANNOT move other players' marble.");
					}
				}
			}
			
			if (Input.GetMouseButtonUp (0)) 
			{
				selectedMarble = null;
			}
		}
		else
		{
			selectedMarble = null;
		}
	}


	
	#region helperFunc
	public static GameObject SelectMarbelByMousePos() 
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit != null && hit.collider != null && hit.rigidbody != null) 
		{
			GameObject go = hit.rigidbody.gameObject;
			if (go.tag.Equals("marble")) 
			{
				return go;
			}
		}
		return null;
	}

	private float GetDragDistance() 
	{
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		return (pos - selectedMarble.transform.position).magnitude;
	}
	#endregion
}
