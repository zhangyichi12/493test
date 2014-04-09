using UnityEngine;
using System.Collections;

public class GUITest : MonoBehaviour
{

    public Texture2D playerProfile;
    public Texture2D highLight;
    public Texture2D emptyTimeBar;
    public Texture2D fullTimeBar;

    public static float coolTime = 5;
    float ctime = 30.0f;
    float maxTurnTime = 30.0f;
    private float timePercentage;

    public bool gaming = false;
    public static bool isGameEndFlag = false;
    public static int playerNo = 0;

    public static bool playerHasShot = false;

    int figureLength = 50;
    int figureWidth = 50;

    int timeBarLength = 80;
    int timeBarWidth = 10;

    int highLightLength = 70;
    int highLightWidth = 70;

    // Update is called once per frame
    void Update()
    {
        if (gaming)
        {
            if (ctime > 0.0f)
            {
                if(isGameEnd())
                {
                    gaming = false;
                    isGameEndFlag = true;
                    return;
                }
                
                if (playerHasShot && ifAllBallsStop() || NoMarbles())
                {
                    ctime = 0f;
                }
            }
            else
            {
                playerHasShot = false;
                playerNo = (playerNo + 1) % 2;
                ctime = 30.0f;
            }
        }
    }

    void FixedUpdate()
    {
        if(gaming)
        {
            ctime -= Time.deltaTime;
        }
    }

    void OnGUI()
    {
        if (coolTime >= 0)
        {
            GUI.Label(new Rect(200, 10, 400, 50), "The Game Will Begin in " + coolTime.ToString() + " Seconds");

            if (GUI.Button(new Rect(600, 10, 50, 50), "Begin"))
            {
                StartCoroutine(prepareTimeCalculate());
            }
        }

        if (coolTime < 0)
        {
            switch (playerNo)
            {
                case 0:
                    Rect guiBox = new Rect(10, 130, timeBarLength, timeBarWidth);
                    GUI.BeginGroup(guiBox);
                    GUI.DrawTexture(new Rect(0, 0, timeBarLength, timeBarWidth), emptyTimeBar);
                    GUI.EndGroup();

                    timePercentage = ctime / maxTurnTime;

                    guiBox = new Rect(10, 130, timeBarLength * timePercentage, timeBarWidth);
                    GUI.BeginGroup(guiBox);
                    GUI.DrawTexture(new Rect(0, 0, timeBarLength, timeBarWidth), fullTimeBar);
                    GUI.EndGroup();
                    GUI.Label(new Rect(10, 55, highLightLength, highLightWidth), highLight);
                    break;
                case 1:
                    guiBox = new Rect(10, 230, timeBarLength, timeBarWidth);
                    GUI.BeginGroup(guiBox);
                    GUI.DrawTexture(new Rect(0, 0, timeBarLength, timeBarWidth), emptyTimeBar);
                    GUI.EndGroup();

                    timePercentage = ctime / maxTurnTime;

                    guiBox = new Rect(10, 230, timeBarLength * timePercentage, timeBarWidth);
                    GUI.BeginGroup(guiBox);
                    GUI.DrawTexture(new Rect(0, 0, timeBarLength, timeBarWidth), fullTimeBar);
                    GUI.EndGroup();
                    GUI.Label(new Rect(10, 155, highLightLength, highLightWidth), highLight);
                    break;
                default:
                    Debug.Log("Bug happens");
                    break;
            }
        }

        if(isGameEndFlag)
        {
            GUI.Label(new Rect(200, 10, 400, 50), "Player " + playerNo + " wins!");
        }

        GUIStyle fontStyle = new GUIStyle();
        fontStyle.fontSize = 12;

        GUILayout.BeginArea(new Rect(10, 10, Screen.width / 4, Screen.height));
        GUILayout.BeginVertical();

        GUI.Label(new Rect(10, 50, figureLength, figureWidth), playerProfile);
        GUI.Label(new Rect(10, 100, 30, 20), "Player 1", fontStyle);


        GUI.Label(new Rect(10, 150, figureLength, figureWidth), playerProfile);
        GUI.Label(new Rect(10, 200, 30, 20), "Player 2", fontStyle);

        GUI.Label(new Rect(10, 250, figureLength, figureWidth), playerProfile);
        GUI.Label(new Rect(10, 300, 30, 20), "Player 3", fontStyle);

        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    public IEnumerator prepareTimeCalculate()
    {
        while (coolTime >= 0)
        {
            coolTime--;
            yield return new WaitForSeconds(1);
        }
        gaming = true;
    }

    bool ifAllBallsStop()
    {
        GameObject[] marbles = GameObject.FindGameObjectsWithTag("marble");
        for(int i = 0; i < marbles.Length; i++)
        {
            Debug.Log(marbles[i].rigidbody2D.velocity);
            if (marbles[i].rigidbody2D.velocity.magnitude > 0.1f)
            {
                return false;
            }
        }
        return true;
    }

    bool isGameEnd()
    {
        GameObject[] marbles = GameObject.FindGameObjectsWithTag("marble");
        for(int i = 0; i < marbles.Length; i++)
        {
            if(marbles[i].GetComponent<MarbelInfo>().playerID != GUITest.playerNo)
            {
                if (marbles[i].GetComponent<MarbelInfo>().moveable || marbles[i].rigidbody2D.velocity.magnitude > 0.0f)
                    return false;
            }
        }
        return true;
    }

    bool NoMarbles()
    {
        GameObject[] marbles = GameObject.FindGameObjectsWithTag("marble");
        for (int i = 0; i < marbles.Length; i++)
        {
            if (marbles[i].GetComponent<MarbelInfo>().playerID == GUITest.playerNo)
            {
                if (marbles[i].GetComponent<MarbelInfo>().moveable)
                    return false;
            }
        }
        return true;
    }
}
