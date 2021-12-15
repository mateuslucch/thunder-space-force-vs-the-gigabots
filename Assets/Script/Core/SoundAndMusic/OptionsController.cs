using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;

    [SerializeField] Slider sfxSlider;

    MusicPlayer musicPlayer = null;
    [SerializeField] PlayerPrefsController playerPrefs;

    void Start()
    {

        if (playerPrefs == null) { playerPrefs = FindObjectOfType<PlayerPrefsController>(); }

        if (!playerPrefs.CheckKeys()) // return true if 
        {
            volumeSlider.value = PlayerPrefsController.GetMusicVolume();
            sfxSlider.value = PlayerPrefsController.GetSfxVolume();
            Debug.Log("Volume keys not found. Creating new ones.");
        }

        if (FindObjectOfType<MusicPlayer>())
        {
            musicPlayer = FindObjectOfType<MusicPlayer>();

            if (musicPlayer) { musicPlayer.SetVolume(volumeSlider.value); }

            else { Debug.LogWarning("No music player found...did you start from splash screen?"); }
        }        

        // get volume values from playerprefs
        volumeSlider.value = PlayerPrefsController.GetMusicVolume();
        sfxSlider.value = PlayerPrefsController.GetSfxVolume();

        // detect if volume slider value changed
        volumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        sfxSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });

    }

    public void SaveConfigAndExit()
    {
        PlayerPrefsController.SaveMusicVolume(volumeSlider.value);
        PlayerPrefsController.SaveSfxVolume(sfxSlider.value);
    }

    public float GetSfxVolume() { return sfxSlider.value; }

    public float GetMusicVolume() { return volumeSlider.value; }

    public void ValueChangeCheck()
    {
        PlayerPrefsController.SaveMusicVolume(volumeSlider.value);
        PlayerPrefsController.SaveSfxVolume(sfxSlider.value);
        musicPlayer.SetVolume(volumeSlider.value);
    }
}
