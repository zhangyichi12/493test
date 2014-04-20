using UnityEngine;
using System.Collections;

public class MusicControl : MonoBehaviour {

    public AudioClip bgm;

    void Awake() {
        GameObject go = GameObject.Find("BGM");
        if (go.audio.clip != bgm)
        {
            go.audio.clip = bgm;
            go.audio.Play();
        }
    }
}
