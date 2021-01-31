using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    private AudioSource Music;

    float MusicLength;

    float Timer = 0;

    public RectTransform ContinueUI;

    private void Start()
    {
        Music = GetComponent<AudioSource>();
        MusicLength = Music.clip.length;
    }

    void Update()
    {
        Timer += Time.deltaTime;

        ContinueUI.gameObject.SetActive(Timer >= MusicLength);

        if(Timer < MusicLength)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
