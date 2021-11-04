using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class LeaderboardUpdate : MonoBehaviour
{
    RecordContent playerRecord;
    LeaderboarList leaderboard;

    public string leaderboardFileName = "/leaderboard.json";
    //public string path = "Assets/Resources/leaderboard.json";

    private void Start()
    {
        /*
         if (!Directory.Exists(path))
         {
             Directory.CreateDirectory(path);
         }
         */
    }

    public void UpdateLeaderboard(int currentScore)
    {
        RecordContent playerRecord = new RecordContent();

        string path = Application.dataPath + "/Resources/leaderboard.json";

        string jsonString = File.ReadAllText(path);//armazena json em uma string...
        //playerRecord = JsonUtility.FromJson<RecordContent>(jsonString); //...converte para classe RecordContent
        leaderboard = JsonUtility.FromJson<LeaderboarList>(jsonString); //...converte para classe RecordContent

        print(jsonString.Length);
        
        leaderboard.ToString();

        print("firstScore: " + leaderboard);

        playerRecord.name = ("player");
        playerRecord.score = currentScore;

        print("lastScore:" + playerRecord.score);

        print(leaderboard);

        jsonString = JsonUtility.ToJson(playerRecord); //reconverte playerRecords para json
        File.WriteAllText(path, jsonString); //..depois salva em arquivo (caminho,variavel em formato json)

    }

    [System.Serializable]
    public class LeaderboarList
    {
        public string[] leaderboard;
    }

    [System.Serializable]
    public class RecordContent
    {
        public string name;
        public int score;
    }

}

