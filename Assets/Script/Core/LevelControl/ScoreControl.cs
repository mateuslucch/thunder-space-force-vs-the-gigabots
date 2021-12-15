using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    int levelScore;
    int totalScore;
    TextControl scoreText;

    private void Awake()
    {
        scoreText = FindObjectOfType<TextControl>();
        scoreText.ChangeScoreText(levelScore);
        UpdateTotalScore();
    }

    public void AddScore(int score)
    {
        levelScore += score;
        scoreText.ChangeScoreText(levelScore);
    }

    public void UpdateTotalScore()
    {
        totalScore = ScoreValues.totalWinScore;
        scoreText.ChangeTotalScoreText(totalScore);
    }
    
    public int LevelScore() { return levelScore; }

}
