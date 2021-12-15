using UnityEngine;

public class NumberBlockControl : MonoBehaviour
{

    [SerializeField] int specialBlockPoints;
    [SerializeField] int ordinaryBlockPoints;
    [SerializeField] int indestructibleBlockPoints;

    int breakableBlocksCount;
    int specialBlocksCount;
    int indestructibleBlocksCount;

    ScoreControl scoreControl;
    Level levelControl;
    TextControl numberBlocksText;

    private void Awake()
    {
        numberBlocksText = FindObjectOfType<TextControl>();
        levelControl = GetComponent<Level>();
        scoreControl = GetComponent<ScoreControl>();
    }

    public void CountBlocks(string blockTag) // chamado pelos blocos, conta total blocos
    {
        if (blockTag == "Breakable") { breakableBlocksCount++; }
        else if (blockTag == "WinBlock") { specialBlocksCount++; }
        else { indestructibleBlocksCount++; }

        int totalBlocks = breakableBlocksCount + specialBlocksCount + indestructibleBlocksCount;
        numberBlocksText.ChangeNumberBlocksUi(totalBlocks);

    }

    public void DestroyBlockCount(string blockTag)
    {
        
        switch (blockTag)
        {
            case "WinBlock":
                scoreControl.AddScore(specialBlockPoints);
                specialBlocksCount--;
                break;
            case "Breakable":
                scoreControl.AddScore(ordinaryBlockPoints);
                breakableBlocksCount--;                
                break;
            default:
                scoreControl.AddScore(indestructibleBlockPoints);
                indestructibleBlocksCount--;
                break;
        }

        int totalBlocks = breakableBlocksCount + specialBlocksCount + indestructibleBlocksCount;
        numberBlocksText.ChangeNumberBlocksUi(totalBlocks);

        if (specialBlocksCount == 0 || breakableBlocksCount == 0 && specialBlocksCount == 0)
        {
            StartCoroutine(levelControl.WinnerPath());
        }
    }

}
