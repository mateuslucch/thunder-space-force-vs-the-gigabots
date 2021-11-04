using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] musicList;
    [SerializeField] int firstLevelMusic = 2;
    [SerializeField] int mainMenuMusic = 1;
    [SerializeField] GameConfig musicConfig;

    AudioSource myAudioSource;
    int singleMusic;
    float musicVolume;

    private void Awake()
    {
        int playerMusic = FindObjectsOfType<MusicPlayer>().Length;
        if (playerMusic > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = PlayerPrefsController.GetMasterVolume(); //valor do volume da musica
        ChangeSong();
    }

    public void ChangeSong() //altera musica, chamado no start e quando troca de fase
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            singleMusic = currentSceneIndex;
            PlayMusic();
        }
        else if (currentSceneIndex == 1)
        {
            singleMusic = mainMenuMusic;
            PlayMusic();
        }
        else
        {
            singleMusic = Random.Range(firstLevelMusic, musicList.Length);
            PlayMusic();
        }
    }

    public void MainMenuSong()
    {
        singleMusic = mainMenuMusic;
        PlayMusic();
    }

    private void PlayMusic()
    {
        if (SceneManager.GetActiveScene().name == "Splash Screen")
        {
            myAudioSource.loop = false;
        }
        else { myAudioSource.loop = true; }
        myAudioSource.clip = musicList[singleMusic];
        myAudioSource.Play();
    }

    public void SetVolume(float volume)
    {
        myAudioSource.volume = volume;
    }

}
