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

    int breakableBlocks; //serializedfield for debugging purpose
    int winBlocks;
    bool levelEnded = false;

    //bolas
    [SerializeField] int ballLives = 3;
    [SerializeField] TextMeshProUGUI ballLivesText;
    float numberBalls = 1f;

    //powerup
    [SerializeField] float blocksToPowerUp = 2f;
    Block actualBlock;
    [SerializeField] public GameObject[] allGenericBlocks;
    float randomPowerup; //iguala com anotherRandomPowerUp, dependendo da condição
    float anotherRandomPowerup = 1f;  //varia de 1 a 2

    //cached reference
    //[SerializeField] GameObject powerUps;
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
        if (FindObjectOfType<MusicPlayer>()) // checking for tests
        {
            FindObjectOfType<MusicPlayer>().ChangeSong(); //troca de musica a cada fase nova
        }
        gameStatus.gameObject.SetActive(false); //esconde texto

        sceneloader = FindObjectOfType<SceneLoader>(); //busca o arquivo SceneLoader.cs e passa pro sceneloader
        FindObjectOfType<GameSession>().UpdateScore(); //nao ta sendo usado
        ballLivesText.text = ("Lives: " + ballLives); //atualiza texto de vidas

        //cursor mouse,USAR APENAS PARA DESKTOP!!!
        //CursorLocked();

        allGenericBlocks = GameObject.FindGameObjectsWithTag("Breakable");
        inGameMenu.SetActive(false);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex - levelNumberFactor;//captura numero da cena
        levelNumber.text = ("Level ") + (currentSceneIndex - 1).ToString();//mostra numero da fase
    }

    private static void CursorLocked() //USAR SÓ NO DESKTOP
    {
        //PARA DESBLOQUEAR NO BROWSER, SÓ IGNORAR ESSA PARTE
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        //FIM
    }

    private static void CursorUnlocked()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void Update()
    {
        if (levelEnded == false)
        {
            if (Input.GetKeyDown("escape")) //abre menu
            {
                TurnMenuOn();
            }
        }
    }

    public void TurnMenuOn()
    {
        FindObjectOfType<Ball>().GamePausedDontLaunch();
        FindObjectOfType<PaddleMove>().PaddlePause();
        FindObjectOfType<GameSession>().GamePause();
        inGameMenu.SetActive(true);
        CursorUnlocked();
    }

    public void TurnMenuOff()
    {
        inGameMenu.SetActive(false);
        CursorLocked();
    }

    public void CountBlocks() //Cada bloco tem o mesmo script. 
                              //Quando a fase inicia, o script é lido na mesma quantidade de blocos. Cada lida, acrescenta +1 na variável "breakableBlocks"
    {
        breakableBlocks++;
        numberBlocks.text = ("Blocks Left: ") + breakableBlocks.ToString();
    }

    public void WinBlocks() //apenas conta os blocos que dao vitoria direta (independente se tem outros)
    {
        winBlocks++;
    }
    //termina start

    public void WinBlocksDestroyed() //desconta blocos especiais do total e do numero de especiais
    {
        winBlocks--;
        breakableBlocks--;
        count();
        if (winBlocks <= 0) //condição de vitória, caso só os blocos de vitória são destruídos
        {
            StartCoroutine(WinnerPath());
        }
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        count();

        if (breakableBlocks <= 0)  //condição de vitória, caso todos os blocos são destruídos
        {
            if (winBlocks <= 0) //se não tiver essa condição, ele da vitória só com os quebraveis
            {
                StartCoroutine(WinnerPath());

            }
        }
    }

    //!!!!!!!IMPORTANTE, APRESENTA TODOS OS BLOCOS DESTRUTIVEIS, INDEPENDENTE DE SEREM ESPECIAIS OU NÃO
    private void count() //apresenta no canvas o numero de blocos faltando, rodasempre que um bloco é destruido
    {

        numberBlocks.text = ("Blocks Left: ") + breakableBlocks.ToString();
        //PARA TESTES
        //lança powerups em todos (menos o primeiro)
        /*
        allGenericBlocks = GameObject.FindGameObjectsWithTag("Breakable");
        for (var i = 0; i < allGenericBlocks.Length; i++)
        {
            actualBlock = allGenericBlocks[i].GetComponent<Block>();
            actualBlock.PowerUpTrue();

        }
        */

        //FINAL TESTES

        //processo para powerups

        blocksToPowerUp--;
        allGenericBlocks = GameObject.FindGameObjectsWithTag("Breakable");

        if (blocksToPowerUp <= 0f)
        {
            randomPowerup = Random.Range(anotherRandomPowerup, 2f);

            if (randomPowerup == 2f)
            {
                blocksToPowerUp = 3f;
                anotherRandomPowerup = 1f;
                for (var i = 0; i < allGenericBlocks.Length; i++)
                {
                    actualBlock = allGenericBlocks[i].GetComponent<Block>();
                    actualBlock.PowerUpTrue();
                }
            }
            anotherRandomPowerup = 2f;
        }
        else
        {
            for (var i = 0; i < allGenericBlocks.Length; i++)
            {
                actualBlock = allGenericBlocks[i].GetComponent<Block>();
                actualBlock.PowerUpFalse();
            }
        }
    }

    //GANHA VIDA EXTRA Do POWERUP!!
    public void ExtraLife()
    {
        ballLives++;
        ballLivesText.text = ("Lives: " + ballLives);
    }

    //extraballs!!! EVITAR PERDA DE VIDA, QUANDO TIVER MULTIPLAS BOLAS, chamado pelo powerup
    public void AddBall()//chamado pelo powerup
    {
        numberBalls++;
    }
    public void ExtraBallsMethods()//chamado pelo lose collider
    {
        if (numberBalls >= 2f)
        {
            numberBalls--;
        }
        else
        {
            FindObjectOfType<Level>().LosePath();
        }
    }
    //end extraBalls methods

    //INICIO CAMINHOS DERROTA/VITORIA
    //caminho da derrota :-(
    public void LosePath()
    {
        ballLives--;
        ballLivesText.text = ("Lives: " + ballLives);
        if (ballLives <= 0)
        {
            gameStatus.gameObject.SetActive(true);

            //ULTRAPASSADO//AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position,soundLevel.SfxVolume());
            AudioSource.PlayClipAtPoint(youLoseSound, Camera.main.transform.position, PlayerPrefsController.GetMasterVolume());

            gameStatus.text = ("Game Over!!");
            StartCoroutine(PausaLose());
            CursorUnlocked();

            DestroyThings();
            levelEnded = true;

        }
        else //reinicia
        {
            FindObjectOfType<Ball>().RestartBallToPaddle();
        }

    }
    IEnumerator PausaLose()
    {
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene("Game Over");
    }

    //CAMINHO DA VITÓRIA!! :-)
    private IEnumerator WinnerPath()
    {
        FindObjectOfType<GameSession>().SceneManagement();
        FindObjectOfType<GameSession>().TotalScore();
        gameStatus.gameObject.SetActive(true);
        gameStatus.text = ("You Win!!");
        levelEnded = true; //bloqueia o menu 
        CursorUnlocked();
        DestroyThings();
        yield return StartCoroutine(DestroyLastBlocks());
        StartCoroutine(PausaWin());  //tempo antes de saltar para próxima scene
    }

    private void DestroyThings()
    {
        Destroy(FindObjectOfType<LoseColider>()); //Destroi LoseColider quando ganha

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

    //destroi blocos após vitoria
    private IEnumerator DestroyLastBlocks()
    {
        //destruir blocos não quebrados, porque sim
        lastBlocks = GameObject.FindGameObjectsWithTag("Breakable");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));

        lastBlocks = GameObject.FindGameObjectsWithTag("WinBlock");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));

        lastBlocks = GameObject.FindGameObjectsWithTag("Unbreakable");
        yield return StartCoroutine(DestroyBlocks(lastBlocks));
    }

    IEnumerator DestroyBlocks(GameObject[] lastBlocks)
    {
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
        Debug.Log("começando nova fase");
        sceneloader.LoadNextScene(); //Roda o elemento LoadNextScene() do script SceneLoader.cs
    }
    //fim caminhos!!

}
