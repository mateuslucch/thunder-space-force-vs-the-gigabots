using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] musicList;
    [SerializeField] int firstLevelMusic = 2;
    [SerializeField] int mainMenuMusic = 2;
    AudioSource myAudioSource;
    int singleMusic;
    float musicVolume;

    [SerializeField] GameConfig musicConfig;

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
        DontDestroyOnLoad(this); //do glich garden, nao destroy o objeto quando troca cena
        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = PlayerPrefsController.GetMasterVolume(); //valor do volume da musica
        ChangeSong();
    }

    public void ChangeSong() //altera musica, chamado no start e quando troca de fase
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //pega valor da cena
        if (currentSceneIndex == 0) //igual zero é menu inical
        {
            singleMusic = currentSceneIndex;
            PlayMusic();
        }
        else if (currentSceneIndex == 1) //igual zero é menu inical
        {
            singleMusic = currentSceneIndex;
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

    public void SetVolume(float volume) //do glitchgarden
    {
        myAudioSource.volume = volume;
    }


}
