using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    //static float LastPlaybackPosition = 0f;

    //private AudioSource AudioSource;

    private void Awake()
    {
        var musicPlayers = FindObjectsOfType<MusicPlayer>();
        if(musicPlayers.Length > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    //// Start is called before the first frame update
    //void Start()
    //{
    //    AudioSource = GetComponent<AudioSource>();
    //    AudioSource.time = LastPlaybackPosition;
    //    AudioSource.Play();
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    LastPlaybackPosition = AudioSource.time;
    //}
}
