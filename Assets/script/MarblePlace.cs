using UnityEngine;
using System.Collections;

public class MarblePlace : MonoBehaviour {

	private GameObject  dragMarble = null;
	private Vector3 	oriPos;

	private int			color; // 0~3

	private PhotonView 	pv;



	void Awake()
	{
		color = (int)PhotonNetwork.player.customProperties["color"];
		pv = gameObject.GetComponent<PhotonView> ();

		GameObject[] marblePos = GameObject.FindGameObjectsWithTag(color.ToString());
		for (int i = 0; i < marblePos.Length; i++) 
		{
			string name = color.ToString() + i.ToString();
			PhotonNetwork.Instantiate(name, marblePos[i].transform.position, Quaternion.identity, 0);
		}
	}



	void Update () {
		if (!ProcessControl.gameStarts)
		{
			if (dragMarble != null && !dragMarble.GetComponent<MarbleState>().dragable)
			{
				dragMarble.transform.position = oriPos;
				PlayPageGUI.SetMes("Marble CANNOT be placed outside scoring area.");
				dragMarble.GetComponent<MarbleState>().dragable = true;
				dragMarble = null;
			}

			if (Input.GetMouseButtonDown(0)) 
			{
				dragMarble = MarbleSelect.SelectMarbelByMousePos();
				if (dragMarble != null) {
					if (dragMarble.name[0].ToString() == color.ToString()) 
					{
						oriPos = dragMarble.transform.position;
					} 
					else
					{
						PlayPageGUI.SetMes("CANNOT move other players' marbles.");
						dragMarble = null;
					}
				}
			}
			
			if (Input.GetMouseButton(0) && dragMarble != null) 
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pos.z = 0f;
				dragMarble.transform.position = pos;
			}
			
			if (Input.GetMouseButtonUp (0) && dragMarble != null) 
			{
				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pos.z = 0f;
				pv.RPC ("PlaceMarbleto", PhotonTargets.All, dragMarble.name, pos);
				dragMarble = null;
			}
		}
		else 
		{
			gameObject.GetComponent<MarblePlace>().enabled = false;

			GameObject[] margin = GameObject.FindGameObjectsWithTag("placeMargin");
			for (int i = 0; i < margin.Length; i++)
			{
				Destroy(margin[i]);
			}

			GameObject[] marble = GameObject.FindGameObjectsWithTag("marble"); 
			for (int i = 0; i < marble.Length; i++)
			{
				marble[i].GetComponent<MarbleState>().enabled = true;
				if (marble[i].name[0].ToString() == color.ToString())
				{
					marble[i].GetComponent<MarbleSelect> ().enabled = true;
					marble[i].GetComponent<MarbleMove> ().enabled = true;
				}
			}
		}
	}



	
	[RPC] void PlaceMarbleto(string name, Vector3 pos)
	{
		GameObject.Find (name).transform.position = pos;
	}
}
