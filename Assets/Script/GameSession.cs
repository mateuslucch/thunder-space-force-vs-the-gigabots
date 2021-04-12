using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    float realTimeSpeed;
    [SerializeField] int pointPerBlockDestroyed = 20;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] bool isAutoPlayEnabled;
    [SerializeField] int lastScene;


    //state variables
    int currentScore = 0;
    int totalScore = 0;

    [SerializeField] int currentSceneIndex;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //busca o valor da primeira scene quando gamesession é carregado
        realTimeSpeed = gameSpeed;
    }

    void Update()
    {
        Time.timeScale = realTimeSpeed;
    }

    public void GamePause()
    {
        realTimeSpeed = 0f;
    }
    public void GameUnPause()
    {
        realTimeSpeed = gameSpeed;
    }
    public void SceneManagement()
    {
        currentSceneIndex++;        
    }

    //showscore busca valor do score
    public int GetScore()
    {
        return currentScore;
    }

    //Processo contar Score
    public void AddToScore()
    {
        currentScore = currentScore + pointPerBlockDestroyed;
        //scoreText.text = ("Score: ") + currentScore.ToString();
    }

    public void TotalScore() //atualiza o score total se ganhar
    {
        totalScore = currentScore;
        //scoreText.text = ("Score: ") + totalScore.ToString();
    }

    public void UpdateScore()
    {
        //FindObjectOfType<ShowScore>().AddToScore(totalScore);
    }

    //processo resetar GameSession
    public void ResetGame() //o método é chamado no arquivo SceneLoader.cs, dependendo do botão que clica no jogo
    {
        Destroy(gameObject);
    }

    //Processo Autoplay testes
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    //Rollback the score and Restart the last level 
    public void RestartLastLevel()
    {

        SceneManager.LoadScene(currentSceneIndex);
        currentScore = totalScore; //rollback do score, só quando perde
        //scoreText.text = ("Score: ") + currentScore.ToString(); //atualiza o texto do score
    }
}