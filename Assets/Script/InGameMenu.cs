using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    Level levelControl;
    [SerializeField] GameObject inGameMenu;

    void Start()
    {
        levelControl = FindObjectOfType<Level>();
        inGameMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("escape")) // open menu
        {
            if (!inGameMenu.activeSelf && levelControl.LevelEnded() == false)
            {
                OpenGameMenu();
            }
            else { ReturnToGame(); }
        }
    }

    public void OpenGameMenu()
    {
        FindObjectOfType<PaddleMove>().PauseShip(true);
        FindObjectOfType<GameSession>().GamePause(true);
        inGameMenu.SetActive(true);
    }

    public void ReturnToGame() // call in a button
    {
        FindObjectOfType<OptionsController>().SaveConfigAndExit(); // save sound config changes to playerprefab
        FindObjectOfType<PaddleMove>().PauseShip(false);
        FindObjectOfType<GameSession>().GamePause(false);
        inGameMenu.SetActive(false);
    }
}
