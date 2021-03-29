using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{

    [SerializeField] AudioClip[] musicList;
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

        //musicVolumeConfig = FindObjectOfType<GameConfigObject>();

        myAudioSource = GetComponent<AudioSource>();
        myAudioSource.volume = PlayerPrefsController.GetMasterVolume(); //valor do volume da musica
        ChangeSong();

        //PlayMusic();

        //musicVolume = musicVolumeConfig.MusicVolume();
    }

    public void ChangeSong() //altera musica, chamado no start e quando troca de fase
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //pega valor da cena
        if (currentSceneIndex == 0) //igual zero é menu inical
        {

            singleMusic = currentSceneIndex;
            PlayMusic();
        }
        else
        {
            singleMusic = Random.Range(1, musicList.Length);
            PlayMusic();
        }

    }
    public void MainMenuSong()
    {
        singleMusic = 0;
        PlayMusic();
    }

    private void PlayMusic()
    {

        myAudioSource.loop = true;
        myAudioSource.clip = musicList[singleMusic];
        myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        // myAudioSource.volume = musicConfig.MusicVolume();
        /*myAudioSource.volume = musicVolume;
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log(musicMute);
            Debug.Log("key m pressed");
            if (musicMute != false)
            {
                musicVolume = musicVolumeConfig.MusicVolume();
                musicMute = false;
            }
            else
            {
                musicVolume = 0f;
                musicMute = true;
            }
        }
        */
    }

    public void SetVolume(float volume) //do glitchgarden
    {
        myAudioSource.volume = volume;
    }


}
