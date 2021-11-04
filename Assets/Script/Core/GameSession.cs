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
    ShowScore showScore;
    int currentScore = 0; //gameplay score
    int totalScore = 0;
    int startScore = 0; //score when starting level

    int currentSceneIndex;

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
        StartLevel();
    }

    void Update()
    {
        Time.timeScale = realTimeSpeed; //!!!!
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
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //currentSceneIndex++;
    }

    public void AddToScore()
    {
        currentScore = currentScore + pointPerBlockDestroyed;
        UpdateScore();
    }

    public void UpdateScore()
    {
        showScore.UpdateScore(currentScore);
    }

    public int GetScore()
    {
        return currentScore;
    }

    public void TotalScore() //atualiza o score total se ganhar
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalScore = currentScore;
        startScore = currentScore;
        currentScore = 0;
        //FindObjectOfType<LeaderboardUpdate>().UpdateLeaderboard(totalScore);
    }

    //processo "resetar" GameSession
    public void ResetGame() //o método é chamado no arquivo SceneLoader.cs, dependendo do botão que clica no jogo
    {
        Destroy(gameObject);
    }

    private void StartLevel()
    {
        currentScore = totalScore;
        showScore = FindObjectOfType<ShowScore>();
        showScore.UpdateScore(currentScore);
    }

    //Processo Autoplay testes
    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

    //Rollback the score and Restart the level
    public void RestartLastLevel()
    {
        FindObjectOfType<SceneLoader>().RestartLevel(lastScene);
        currentScore = startScore; //rollback do score, só quando perde        
    }
}
