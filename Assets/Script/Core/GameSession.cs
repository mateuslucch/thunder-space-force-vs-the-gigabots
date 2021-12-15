using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    float realTimeSpeed;

    [SerializeField] int showScore; // show final score, use this only to show score (winnig or loose)
    [SerializeField] int totalWinScore; // total score only for winnigs and show in level UI
    [SerializeField] int levelScore; // score from level
    [SerializeField] int lastLevelIndex;

    int currentSceneIndex;
    ScoreControl scoreControl;

    private void Awake()
    {
        int gameSession = FindObjectsOfType<GameSession>().Length;
        if (gameSession > 1) { Destroy(gameObject); }
        else { DontDestroyOnLoad(gameObject); }
    }

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //busca o valor da primeira scene quando gamesession é carregado
        realTimeSpeed = gameSpeed;
        if (Time.timeScale != realTimeSpeed) { Time.timeScale = realTimeSpeed; }
    }

    public void GamePause(bool gamePaused)
    {
        if (gamePaused) { Time.timeScale = 0f; }
        else { Time.timeScale = realTimeSpeed; }
    }

    public void UpdateFinalScore(bool levelWin)
    {
        scoreControl = FindObjectOfType<ScoreControl>();
        levelScore = scoreControl.LevelScore();

        // increment totalwinscore if win
        if (levelWin)
        {
            totalWinScore += levelScore;
            showScore = totalWinScore;
        }
        // only show lose score if lose(preserv finalScore)
        else
        {
            showScore = totalWinScore + levelScore; // endgameScore here is last 
        }
        ScoreValues.totalWinScore = totalWinScore;
        ScoreValues.showScore = showScore;
    }

    public void LastLevelIndex(int index) { lastLevelIndex = index; }

    public int ResetWinScore() { return totalWinScore; }

    public int FinalScore() { return showScore; }

    // call in sceneloader when go to main menu
    public void DestroyGameSession() { Destroy(gameObject); }

    // Rollback the score and Restart the level
    public int RestartLastLevel()
    {
        showScore = totalWinScore; // rollback do score, só quando perde e reinicia fase        
        return lastLevelIndex;
    }
}
