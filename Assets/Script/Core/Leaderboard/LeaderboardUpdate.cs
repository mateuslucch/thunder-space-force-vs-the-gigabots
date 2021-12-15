using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderboardUpdate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] allScoreText;
    [SerializeField] GameObject scoreTable;

    List<RecordData> recordTableData;
    int newScore;
    PlayerNameInput playerInputBox;
    SaveSystem saveSystem;

    private void Awake()
    {
        recordTableData = new List<RecordData>();
        playerInputBox = FindObjectOfType<PlayerNameInput>();
        saveSystem = GetComponent<SaveSystem>();
    }

    private void Start()
    {
        // load file
        recordTableData = saveSystem.LoadRecords(); // will return null if file dont exist
        // if file dont exist, create new
        if (recordTableData == null)
        {
            recordTableData = new List<RecordData>();
            CreateNewEmptyTable(recordTableData);
        }

        // clear last extra elements from data (if data > number of score texts)... happened once
        ClearExtrasElements();

        // add data to scoreboard and turn off
        scoreTable.SetActive(false);
        PrintScore(recordTableData);

        // get new score and check
        newScore = ScoreValues.showScore;
        CheckScore();
    }

    // check score
    private void CheckScore()
    {
        // check if score is bigger than last score
        if (newScore <= recordTableData[recordTableData.Count - 1].playerScore || newScore == 0)
        {               
            //turn off player input box
            playerInputBox.gameObject.SetActive(false);
            // turn on scoreboard
            scoreTable.SetActive(true);
            return;
        }
    }

    // get player name if input on (player beat last record)
    public void GetPlayerInitials()
    {
        // get initials from input        
        string name = playerInputBox.GetPlayerInput();
        // turn input box off
        playerInputBox.gameObject.SetActive(false);
        // turn on score table
        scoreTable.SetActive(true);
        // add score
        AddScore(name);

    }

    private void AddScore(string playerName)
    {
        // change last score for new       
        int recordSize = recordTableData.Count;

        // add new data
        RecordData tempData = new RecordData();
        tempData.playerName = playerName;
        tempData.playerScore = newScore;
        // add data last member
        recordTableData[recordTableData.Count - 1] = tempData;

        // sort data (bigger to small)
        SortData();

        // print saved data
        PrintScore(recordTableData);

        //save new        
        saveSystem.SaveRecord(recordTableData);
        // END
    }

    private void SortData()
    {
        for (var i = 0; i < recordTableData.Count; i++)
        {            
            for (var j = 0; j < recordTableData.Count - i; j++)
            {         
                if (j + 1 < recordTableData.Count)
                {
                    if (recordTableData[j].playerScore < recordTableData[j + 1].playerScore)
                    {
                        RecordData temp = new RecordData();
                        temp = recordTableData[j + 1];
                        recordTableData[j + 1] = recordTableData[j];
                        recordTableData[j] = temp;
                    }
                }
            }
        }
    }

    private void ClearExtrasElements()
    {
        if (recordTableData.Count > allScoreText.Length)
        {
            while (recordTableData.Count > allScoreText.Length)
            {
                print(recordTableData.Count);
                recordTableData.RemoveAt(recordTableData.Count - 1);
            }
        }
    }

    private void PrintScore(List<RecordData> saveDataList)
    {
        int i = 0;
        foreach (TextMeshProUGUI scoreText in allScoreText)
        {
            scoreText.text = ($"{i + 1}. {saveDataList[i].playerName}: {saveDataList[i].playerScore}");
            i++;
        }
    }

    public void ResetBoard()
    {
        List<RecordData> saveDataForReset = new List<RecordData>();
        CreateNewEmptyTable(saveDataForReset);
    }

    private void CreateNewEmptyTable(List<RecordData> saveDataForReset)
    {
        RecordData RecordData = new RecordData();
        RecordData.playerName = "___";
        RecordData.playerScore = 0000;
        foreach (TextMeshProUGUI scoreText in allScoreText)
        {
            saveDataForReset.Add(RecordData);
        }
        PrintScore(saveDataForReset);
        saveSystem.SaveRecord(saveDataForReset);
    }

}

