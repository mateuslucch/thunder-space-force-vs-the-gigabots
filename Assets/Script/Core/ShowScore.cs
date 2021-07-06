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
        //gameSession = FindObjectOfType<GameSession>();

    }

    void Update()
    {
        scoreText.text = ("Score: ") + FindObjectOfType<GameSession>().GetScore().ToString();
    }

}
