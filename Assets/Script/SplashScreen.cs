using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{
    public void StartMenu()
    {
        Debug.Log("Start Menu");
        FindObjectOfType<SceneLoader>().LoadMainMenu();
    }

}
