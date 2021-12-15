using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] GameObject exitButton = null;
    [SerializeField] GameObject startButton = null;
    [SerializeField] Vector2 startAnchorAlternativeX;
    [SerializeField] Vector2 startAnchorAlternativeY;

    int actualSceneIndex;

    private void Start()
    {
        actualSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // hide exit button if not windows
        if (exitButton != null)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                Vector2 offsetMin = new Vector2(startButton.GetComponent<RectTransform>().offsetMin.x, startButton.GetComponent<RectTransform>().offsetMin.y);
                Vector2 offsetMax = new Vector2(startButton.GetComponent<RectTransform>().offsetMax.x, startButton.GetComponent<RectTransform>().offsetMax.y);

                exitButton.SetActive(false);

                startButton.GetComponent<RectTransform>().anchorMin = new Vector2(startAnchorAlternativeX.x, startAnchorAlternativeY.x);
                startButton.GetComponent<RectTransform>().anchorMax = new Vector2(startAnchorAlternativeX.y, startAnchorAlternativeY.y);
                startButton.GetComponent<RectTransform>().offsetMin = new Vector2(offsetMin.x, offsetMin.y);
                startButton.GetComponent<RectTransform>().offsetMax = new Vector2(offsetMax.x, offsetMax.y);
            }

        }
        // end hide exit button
    }

#if UNITY_EDITOR
    void Update()
    {
        //USAR PARA TESTES
        //TROCA DE CENA USANDO L E K
        if (Input.GetKeyDown(KeyCode.L))
        {
            //LoadNextSceneIndex();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //LoadPreviosSceneIndex();
        }
    }

    public void LoadNextSceneIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadPreviosSceneIndex()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1);
    }
#endif

    public void LoadMainMenu()
    {        
        if (FindObjectOfType<GameSession>()) { FindObjectOfType<GameSession>().DestroyGameSession(); }
        if (FindObjectOfType<OptionsController>()) { FindObjectOfType<OptionsController>().SaveConfigAndExit(); }
        if (FindObjectOfType<MusicPlayer>()) { FindObjectOfType<MusicPlayer>().MainMenuSong(); }
        ScoreValues.totalWinScore = 0;
        ScoreValues.showScore = 0;
        SceneManager.LoadScene("Start Menu");
    }
    
    public void StartNewGame()
    {
        ScoreValues.totalWinScore = 0;
        ScoreValues.showScore = 0;        
        if (FindObjectOfType<OptionsController>()) { FindObjectOfType<OptionsController>().SaveConfigAndExit(); }

        SceneManager.LoadScene("Level1");
    }

    public void RestartGame()
    {
        if (FindObjectOfType<GameSession>()) { FindObjectOfType<GameSession>().DestroyGameSession(); }
        StartNewGame();
    }

    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartLevel()
    {
        int levelSceneIndex;
        levelSceneIndex = FindObjectOfType<GameSession>().RestartLastLevel();
        SceneManager.LoadScene(levelSceneIndex);
    }

    public void ManualScene() { SceneManager.LoadScene("Manual Scene"); }

    public void LeaderboardScene() { SceneManager.LoadScene("Leaderboard"); }

    public void QuitGame() { Application.Quit(); }

}