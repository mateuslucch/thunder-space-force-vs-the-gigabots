using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] musicList;
    [SerializeField] int firstLevelMusic = 2;
    [SerializeField] int mainMenuMusic = 1;
    
    AudioSource myAudioSource;
    int singleMusic;   

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
        myAudioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {                
        myAudioSource.volume = PlayerPrefsController.GetMusicVolume(); //valor do volume da musica de playerpref
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

    public void SetVolume(float musicVolume)
    {
        myAudioSource.volume = musicVolume;
    }

}
