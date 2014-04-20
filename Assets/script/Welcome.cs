using UnityEngine;
using System.Collections;

public class Welcome : MonoBehaviour {

    public GUISkin theSkin;
    public AudioSource click;

    void OnGUI()
    {
        GUI.skin = theSkin;

        if (GUI.Button(new Rect(Screen.width / 2 - 121 / 2, Screen.height / 2 - 53 / 2, 121, 53), "START", "button"))
        {
            click.Play();
            Application.LoadLevel("Prepare");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 121 / 2, Screen.height / 2 + 53 / 2 + 26, 121, 53), "RULES", "button"))
        {
            click.Play();
            Application.LoadLevel("Help");
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 121 / 2, Screen.height / 2 + 159 / 2 + 53, 121, 53), "LEAVE", "button"))
        {
            click.Play();
            Application.Quit();
        }
    }
}
