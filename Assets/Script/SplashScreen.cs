using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void StartMenu()
    {        
        FindObjectOfType<SceneLoader>().LoadMainMenu();        
    }
    public void StartSong()
    {
        //FindObjectOfType<MusicPlayer>().SplashScreenSong();
        AudioSource myAudioSource = GetComponent<AudioSource>();
        myAudioSource.Play();
    }
}
