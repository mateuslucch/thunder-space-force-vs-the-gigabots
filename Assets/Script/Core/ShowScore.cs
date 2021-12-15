using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI scoreText;
    
    private void OnEnable()
    {
        int score = ScoreValues.showScore;
        UpdateScore(score);
    }

    void Start()
    {
        int score = ScoreValues.showScore;
        UpdateScore(score);
    }

    public void UpdateScore(int score)
    {
        if (!scoreText)
        {
            print("there is no scoretext.");
            return;
        }
        scoreText.text = ("Score: \n") + score;
    }
}
