using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

    //parameters
    [SerializeField] TextMeshProUGUI numberBlocks;
    [SerializeField] TextMeshProUGUI gameStatus;
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] int levelNumberFactor = 0;

    int breakableBlocks;
    int winBlocks;
    bool levelEnded = false;

    //bolas
    [SerializeField] int ballLives = 3;
    [SerializeField] TextMeshProUGUI ballLivesText;
    int numberBalls = 1;

    //cached reference    
    SceneLoader sceneloader;
    GameObject powerUpCondition;

    //destruir apos vitoria ou derrota
    public GameObject[] lastBalls;
    public GameObject[] lastPowerUps;
    public GameObject[] lastBlocks;

    [SerializeField] GameConfig soundLevel;
    [SerializeField] AudioClip youLoseSound;
    [SerializeField] GameObject inGameMenu;

    private void Start()
    {
        if (FindObjectOfType<MusicPlayer>())
        {
            FindObjectOfType<MusicPlayer>().ChangeSong();
        }
        else { Debug.Log("there is no musicplayer here"); }

        gameStatus.gameObject.SetActive(false);
        sceneloader = FindObjectOfType<SceneLoader>();
        ballLivesText.text = ("Lives: \n" + ballLives);

        inGameMenu.SetActive(false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - levelNumberFactor;//captura numero da cena
        levelNumber.text = ("Level \n") + (currentSceneIndex - 1).ToString();//mostra numero da fase
    }

    private void Update()
    {
        if (levelEnded == false)
        {
            if (Input.GetKeyDown("escape")) //open menu
            {
                TurnMenuOn();
            }
        }
    }

    public void TurnMenuOn()
    {
        FindObjectOfType<PaddleMove>().PaddlePause();
        FindObjectOfType<GameSession>().GamePause();
        inGameMenu.SetActive(true);
    }

    public void TurnMenuOff()
    {
        inGameMenu.SetActive(false);
    }

    public void CountBlocks()
    {
        breakableBlocks++;
        numberBlocks.text = ("Blocks Left: \n") + breakableBlocks.ToString();
    }

    public void WinBlocks() //apenas conta os blocos que dao vitoria direta (independente se tem outros)
    {
        winBlocks++;
    }

    public void WinBlocksDestroyed() //desconta blocos especiais do total e do numero de especiais
    {
        winBlocks--;
        breakableBlocks--;
        Count();

        //VITÓRIA COM BLOCOS ESPECIAIS
        //vitoria se só um bloco especial for destruido
        StartCoroutine(WinnerPath());

        /* //vitoria se todos blocos especiais forem destruidos
        if (winBlocks <= 0)
        {
            StartCoroutine(WinnerPath());
        }
        */
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        Count();

        if (breakableBlocks <= 0)  //condição de vitória, caso todos os blocos são destruídos
        {
            if (winBlocks <= 0) //se não tiver essa condição, ele da vitória só com os quebraveis
            {
                StartCoroutine(WinnerPath());
            }
        }
    }

    //!!!!!!!IMPORTANTE, APRESENTA TODOS OS BLOCOS DESTRUTIVEIS, INDEPENDENTE DE SEREM ESPECIAIS OU NÃO
    private void Count() //apresenta no canvas o numero de blocos faltando, rodasempre que um bloco é destruido
    {
        numberBlocks.text = ("Blocks Left: \n") + breakableBlocks.ToString();
    }

    //GANHA VIDA EXTRA Do POWERUP!!
    public void ExtraLife()
    {
        ballLives++;
        ballLivesText.text = ("Lives: \n" + ballLives);
    }

    public int NumberLife()
    {
        return ballLives;
    }

    //extraballs!!!  e também EVITAR PERDA DE VIDA, QUANDO TIVER MULTIPLAS BOLAS, chamado pelo powerup
    public void AddBall()
    {
        numberBalls++;
    }

    public void ExtraBallsMethods()
    {
        if (numberBalls >= 2)
        {
            numberBalls--;
        }
        else
        {
            FindObjectOfType<Level>().LosePath();
        }
    }

    public int NumberBalls()
    {
        return numberBalls; //retorna o numero de bolas pra classe que precisar
    }
    //end extraBalls methods

    //INICIO CAMINHOS DERROTA/VITORIA
    //caminho da derrota :-(
    public void LosePath()
    {
        ballLives--;
        ballLivesText.text = ("Lives: \n" + ballLives);
        if (ballLives <= 0)
        {
            //FindObjectOfType<GameSession>().TotalScore();
            gameStatus.gameObject.SetActive(true);

            AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position, PlayerPrefsController.GetMasterVolume());

            gameStatus.text = ("Game Over!!");
            StartCoroutine(PausaLose());

            DestroyThings();
            levelEnded = true;
        }
    }

    IEnumerator PausaLose() //pausa antes de começar nova fase
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("Game Over");
    }

    //CAMINHO DA VITÓRIA!! :-)
    private IEnumerator WinnerPath()
    {
        FindObjectOfType<GameSession>().SceneManagement();
        FindObjectOfType<GameSession>().TotalScore();
        levelEnded = true; //bloqueia o menu         
        StopThings();
        DestroyThings();
        yield return StartCoroutine(DestroyLastBlocks());
        gameStatus.gameObject.SetActive(true);
        gameStatus.text = ("You Win!!");
        StartCoroutine(PausaWin());  //pausa antes de saltar para próxima scene
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
                blockToDestroyed.GetComponent<Block>().DestroyBlockFromOutside();
            }
        }


    }

    IEnumerator PausaWin()
    {
        yield return new WaitForSecondsRealtime(3f);
        sceneloader.LoadNextLevel(); //Roda o elemento LoadNextScene() do script SceneLoader.cs
    }
    //fim caminhos!!

}
