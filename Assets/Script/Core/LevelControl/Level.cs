using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    [SerializeField] int levelNumberFactor = 0;

    int breakableBlocks;
    int winBlocks;
    bool levelEnded = false;
    bool playerWon;

    GameObject[] lastBalls;
    GameObject[] lastPowerUps;
    GameObject[] lastBlocks;
    
    [SerializeField] AudioClip youLoseSound;

    SceneLoader sceneloader;
    GameObject powerUpCondition;
    TextControl uiTextControl;
    GameSession levelIndex;

    private void Awake()
    {
        uiTextControl = FindObjectOfType<TextControl>();
    }

    private void Start()
    {

        if (FindObjectOfType<MusicPlayer>())
        {
            FindObjectOfType<MusicPlayer>().ChangeSong();
        }
        else { Debug.Log("there is no musicplayer here. did you started from main menu?"); }

        sceneloader = FindObjectOfType<SceneLoader>();

        // change level number
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - levelNumberFactor;
        uiTextControl.ChangeLevelNumber(currentSceneIndex);

    }

    public bool LevelEnded() { return levelEnded; }

    // START LOSE/WIN PATHS
    // LOSE PATH :-(
    public void LosePath()
    {
        playerWon = false;

        FindObjectOfType<GameSession>().UpdateFinalScore(false);

        // send level index to gamesession for restart level purposes
        levelIndex = FindObjectOfType<GameSession>();
        levelIndex.LastLevelIndex(SceneManager.GetActiveScene().buildIndex);

        AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position, PlayerPrefsController.GetMusicVolume());
        uiTextControl.ChangeStatus("Game Over!!");

        StartCoroutine(PauseBeforeNextScene());

        DestroyThings();
        levelEnded = true;

    }

    // WIN PATH!! :-)
    public IEnumerator WinnerPath()
    {
        playerWon = true;

        FindObjectOfType<GameSession>().UpdateFinalScore(true);
        // FindObjectOfType<GameSession>().SceneManagement();
        levelEnded = true; //bloqueia o menu         
        StopThings();
        DestroyThings();
        yield return StartCoroutine(DestroyLastBlocks());
        uiTextControl.ChangeStatus("You Win!!");
        StartCoroutine(PauseBeforeNextScene());  //pausa antes de saltar para próxima scene
    }

    private void StopThings()
    {
        FindObjectOfType<Paddle>().StopLasers();
    }

    private void DestroyThings() //evitar várias coisas
    {
        Destroy(FindObjectOfType<LoseColider>()); //Destroi LoseColider quando ganha

        //destruir lasers
        GameObject[] lastLasersShots = GameObject.FindGameObjectsWithTag("Laser");
        foreach (GameObject laser in lastLasersShots)
        {
            Destroy(laser);
        }

        //destruir power ups, evitar bug quando contato com paddle após vitória
        lastPowerUps = GameObject.FindGameObjectsWithTag("PowerUp");
        for (var i = 0; i < lastPowerUps.Length; i++)
        {
            Destroy(lastPowerUps[i]);
        }

        //Destruir a bola. Evitar contagem de pontos quando vitoria ou derrota com blocos sobrandos
        lastBalls = GameObject.FindGameObjectsWithTag("Ball");
        for (var i = 0; i < lastBalls.Length; i++) //Destruir todas as bolas. Evitar contagem de pontos quando vitoria com blocos sobrandos
        {
            Destroy(lastBalls[i]);
        }

    }

    //destroi blocos remanescentes após vitoria
    private IEnumerator DestroyLastBlocks()
    {
        //destruir blocos não quebrados, porque sim
        lastBlocks = GameObject.FindGameObjectsWithTag("Breakable");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));

        lastBlocks = GameObject.FindGameObjectsWithTag("Unbreakable");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));

        lastBlocks = GameObject.FindGameObjectsWithTag("WinBlock");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));
    }

    IEnumerator DestroyBlocks(GameObject[] lastBlocks)
    {

        int totalBlock = lastBlocks.Length;
        List<GameObject> blocksToBeDestroyed = new List<GameObject>();
        /*
                foreach (GameObject block in lastBlocks)
                {
                    blocksToBeDestroyed.Add(block);
                }
                int multipleExplosion = blocksToBeDestroyed.Count;
                while (blocksToBeDestroyed.Count > 0)
                {

                    for (var i = 0; i < blocksToBeDestroyed.Count; i++)
                    {

                        yield return new WaitForSecondsRealtime(0.1f);
                        for (var j = 0; j < multipleExplosion; j++)
                        {
                            if (blocksToBeDestroyed[j] != null)
                            {
                                //print(blocksToBeDestroyed.Count);
                                blocksToBeDestroyed[j].GetComponent<Block>().DestroyBlockFromOutside();
                                blocksToBeDestroyed.RemoveAt(j); // indice fica nulo, ai é removido           
                            }
                        }
                    }
                    multipleExplosion -= 1;
                }
                */

        //explosão que funciona
        foreach (GameObject blockToDestroyed in lastBlocks)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            if (blockToDestroyed != null)
            {
                blockToDestroyed.GetComponent<Block>().DestroyBlockWinLevel();
            }
        }
    }

    IEnumerator PauseBeforeNextScene()
    {
        yield return new WaitForSecondsRealtime(3f);
        if (playerWon) { sceneloader.LoadNextLevel(); }
        else { SceneManager.LoadScene("Leaderboard"); }
    }
    //fim caminhos!!
}
