using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsController : MonoBehaviour
{

    const string MASTER_VOLUME_KEY = "master volume";
    const string SFX_VOLUME_KEY = "sfx volume";

    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;

    const float SFX_MIN = 0f;
    const float SFX_MAX = 1f;

    const float SFX_Default = 0.5f;
    const float Music_Default = 0.5f;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Music_Default);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SFX_Default);
        }
    }

    public bool CheckKeys()
    {
        if (!PlayerPrefs.HasKey(MASTER_VOLUME_KEY))
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, Music_Default);
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, SFX_Default);
            return false;
        }
        return true;
    }

    public static void SaveMusicVolume(float musicVolume)
    {
        if (musicVolume >= MIN_VOLUME && musicVolume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, musicVolume); // SetFloat(string name key, float value)
        }
        else { Debug.LogError("Master volume is out of range"); }
    }

    public static void SaveSfxVolume(float sfxVolume)
    {
        if (sfxVolume >= SFX_MIN && sfxVolume <= SFX_MAX)
        {
            PlayerPrefs.SetFloat(SFX_VOLUME_KEY, sfxVolume);
        }
        else { Debug.LogError("Sfx volume is out of range"); }
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    }

    public static float GetSfxVolume()
    {
        return PlayerPrefs.GetFloat(SFX_VOLUME_KEY);
    }

}
