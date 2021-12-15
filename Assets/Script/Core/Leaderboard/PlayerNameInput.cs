using TMPro;
using UnityEngine;

public class PlayerNameInput : MonoBehaviour
{
    string playerInitials;

    public int myScore;
    [SerializeField] TMP_InputField inputField;
    LeaderboardUpdate leaderboardUpdate;

    void Awake()
    {
        leaderboardUpdate = FindObjectOfType<LeaderboardUpdate>();
    }

    private void Start()
    {
        inputField.text = "";
        inputField.ActivateInputField();
        inputField.Select();
    }

    public string GetPlayerInput()
    {
        playerInitials = inputField.text;
        return playerInitials;
    }
}
