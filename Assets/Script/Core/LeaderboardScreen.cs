using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] scoreTable;
    [SerializeField] TextMeshProUGUI[] nameTable;

    private void Start()
    {
        for (var i = 0; i < scoreTable.Length; i++)
        {
            scoreTable[i].text = (i.ToString());
            nameTable[i].text = (i.ToString());
        }
    }
}
