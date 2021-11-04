using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{

    [SerializeField] Slider volumeSlider;
    float defaultMusicVolume = 0.5f;
    [SerializeField] Slider sfxSlider;
    float defaultSFX = 0.5f;

    void Start()
    {
        volumeSlider.value = PlayerPrefsController.GetMasterVolume();
        sfxSlider.value = PlayerPrefsController.GetSfxVolume();
    }

    void Update()
    {

        if (FindObjectOfType<MusicPlayer>())
        {
            var musicPlayer = FindObjectOfType<MusicPlayer>();

            if (musicPlayer)
            {
                musicPlayer.SetVolume(volumeSlider.value);
            }
            else
            {
                Debug.LogWarning("No music player found...did you start from splash screen?");
            }
        }

    }
    public void SaveAndExit()
    {
        PlayerPrefsController.SetMasterVolume(volumeSlider.value);
        PlayerPrefsController.SetSfxVolume(sfxSlider.value);
        //FindObjectOfType<LevelLoad>().LoadStartMenu();
    }
    public float SfxVolumeLevel()
    {
        return sfxSlider.value;
    }

    public void SetDefaults()
    {
        volumeSlider.value = defaultMusicVolume;
        sfxSlider.value = defaultSFX;
    }

}
