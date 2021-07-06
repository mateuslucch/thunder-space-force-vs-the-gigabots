using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master volume"; //transforma string em constante(acho), facilita uso de string
    const string SFX_VOLUME_KEY = "sfx volume";

    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    
    const float SFX_MIN = 0f;
    const float SFX_MAX = 1f;

    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {            
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }
    public static void SetSfxVolume(float volume)
    {
        if (volume >= SFX_MIN && volume <= SFX_MAX)
        {            
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Sfx volume is out of range");
        }
    }
    public static float GetMasterVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }
    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

}
