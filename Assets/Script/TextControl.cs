using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextControl : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI numberBlocks;
    [SerializeField] TextMeshProUGUI gameStatusText;
    [SerializeField] GameObject gameStatusObject;
    [SerializeField] TextMeshProUGUI levelNumber;
    [SerializeField] TextMeshProUGUI ballLivesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI totalScoreText;

    private void Start()
    {
        gameStatusObject.SetActive(false);
    }

    public void ChangeLifeNumber(int ballLives) { ballLivesText.text = ("Lives: \n" + ballLives); }

    public void ChangeStatus(string statusText)
    {
        gameStatusObject.gameObject.SetActive(true);
        gameStatusText.text = statusText;
    }

    public void ChangeLevelNumber(float currentSceneIndex) { levelNumber.text = ("Level \n") + (currentSceneIndex - 1).ToString(); }

    public void ChangeNumberBlocksUi(int breakableBlocks) { numberBlocks.text = ("Blocks Left: \n") + breakableBlocks.ToString(); }

    public void ChangeScoreText(int score) { scoreText.text = ("Score: \n") + score; }

    public void ChangeTotalScoreText(int totalScore) { totalScoreText.text = ("Total Score: \n") + totalScore; }

}
