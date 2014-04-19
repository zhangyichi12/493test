using UnityEngine;
using System.Collections;

public class ProcessControl : MonoBehaviour 
{
	public static bool  gameStarts = false;

	public static int 	whoseTurn = 0;
	public static bool  canTakeInput = false;
	public static float playTimer = 30f;
	public static float maxPlayTime = 30f;

	public static int[] leftMoveableMarble = {4, 4, 4, 4};



	private int winner;



	void FixedUpdate()
	{
		if (gameStarts)
		{
			if (ifAllBallsStop())
			{
				playTimer -= Time.fixedDeltaTime;

				if (whoseTurn == (int)PhotonNetwork.player.customProperties ["color"]) 
				{
					canTakeInput = true;
				} 
				else
				{
					canTakeInput = false;
				}
			}
			else 
			{
				playTimer = maxPlayTime;

				canTakeInput = false;
			}
		}
	}



	private bool ifAllBallsStop()
	{
		GameObject[] marbles = GameObject.FindGameObjectsWithTag("marble");
		for(int i = 0; i < marbles.Length; i++)
		{
			if (marbles[i].rigidbody2D.velocity.magnitude > 0.1f
			    || marbles[i].rigidbody2D.angularVelocity > 0.1f)
			{
				return false;
			}
		}

		if (ifGameEnds ()) 
		{
			PhotonNetwork.LeaveRoom();
			Application.LoadLevel(3);		
		}

		return true;
	}

	private bool ifGameEnds()
	{
		int count = 0;

		for (int i = 0; i < ProcessControl.leftMoveableMarble.Length; i++)
		{
			if (ProcessControl.leftMoveableMarble[i] > 0)
			{
				count++;
				winner = i;
			}

			if (count > 1)
				return false;
		}
		return true;
	}
}
