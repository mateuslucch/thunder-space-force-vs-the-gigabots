using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowScore : MonoBehaviour
{

    TextMeshProUGUI scoreText;
    GameSession gameSession;

    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        gameSession = FindObjectOfType<GameSession>();
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = ("Score: \n") + score;
        }
        else { print("There is no scorebox"); }
    }
}
