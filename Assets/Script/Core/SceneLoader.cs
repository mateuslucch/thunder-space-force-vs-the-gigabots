using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int actualSceneIndex;

    private void Start()
    {
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update()
    {
        //USAR PARA TESTES
        //TROCA DE CENA USANDO L E K
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //LoadPreviosScene();
        }
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviosScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }

    public void LoadMainMenu()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Start Menu");

        if (FindObjectOfType<MusicPlayer>())
        {
            FindObjectOfType<MusicPlayer>().MainMenuSong();
        }
        if (FindObjectOfType<GameSession>() && FindObjectOfType<OptionsController>())
        {
            FindObjectOfType<GameSession>().ResetGame();
        }
    }

    public void LoadGameScene()
    {
        if (FindObjectOfType<GameSession>() && FindObjectOfType<OptionsController>())
        {
            FindObjectOfType<GameSession>().ResetGame();
            FindObjectOfType<OptionsController>().SaveAndExit();
        }
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        FindObjectOfType<GameSession>().ResetGame();
        if (FindObjectOfType<OptionsController>())
        {
            FindObjectOfType<OptionsController>().SaveAndExit();
        }
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevelOne()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene("Level1");
        FindObjectOfType<OptionsController>().SaveAndExit();
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartLevel(int levelSceneIndex)
    {
        SceneManager.LoadScene(levelSceneIndex);
        //call last scene
        FindObjectOfType<GameSession>().RestartLastLevel(); //reset score
    }

    public void ReturnSceneIndex()
    {

    }

    public void ReturnToGame()
    {
        FindObjectOfType<OptionsController>().SaveAndExit();
        FindObjectOfType<PaddleMove>().PaddleUnPause();
        FindObjectOfType<GameSession>().GameUnPause();
        FindObjectOfType<Level>().TurnMenuOff();
    }

    public void ManualScene()
    {
        SceneManager.LoadScene("Manual Scene");
    }

}