using UnityEngine;

public class ResetHandle : MonoBehaviour
{
    [SerializeField] GameObject resetConfirmation;
    LeaderboardUpdate leaderboardUpdate;

    private void Awake()
    {
        resetConfirmation.SetActive(false);
    }
    private void Start() {
        leaderboardUpdate = GetComponent<LeaderboardUpdate>();
    }

    public void OpenResetConfirmation(bool confirmation) // open with reset button(true), close with no button(false)
    {
        resetConfirmation.SetActive(confirmation);
    }

    public void ResetScore() // open with yes button
    {
        leaderboardUpdate.GetComponent<LeaderboardUpdate>().ResetBoard();
        resetConfirmation.SetActive(false);
    }
}
