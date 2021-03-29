using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowScore : MonoBehaviour {

    TextMeshProUGUI scoreText;
    GameSession gameSession;

    // Use this for initialization
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
        //gameSession = FindObjectOfType<GameSession>();
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = ("Score: ") + FindObjectOfType<GameSession>().GetScore().ToString();
    }
        
}
