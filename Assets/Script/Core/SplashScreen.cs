using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void LoadMainMenu()
    {
        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }

    public void StartSong()
    {
        AudioSource myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Play();
    }
}
