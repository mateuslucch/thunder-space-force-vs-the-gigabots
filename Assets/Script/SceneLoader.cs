using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    void Update()
    { //USAR PARA TESTES
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            LoadPreviosScene();
        }
    }


    public void LoadNextScene() //Carrega próxima scene, em relação a atual
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1); //Posição da cena atual + 1 para abrir a próxima cena

    }

    public void LoadPreviosScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex - 1); //Posição da cena atual + 1 para abrir a próxima cena
    }

    public void LoadMainMenu() //Carregar menu inicial e reseta configs
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Start Menu"); //pode usar 0, cena inicial é numero 0 (NÃO MAIS, AGORA É 1)
        if (FindObjectOfType<MusicPlayer>()) //nao dar erro quando termina splashscreen (a mesma tenta chamar)
        {
            FindObjectOfType<MusicPlayer>().MainMenuSong();
        }
        if (FindObjectOfType<GameSession>() && FindObjectOfType<OptionsController>())
        {
            FindObjectOfType<GameSession>().ResetGame(); //vai buscar o ResetGame em GameStatus.cs!!!
            FindObjectOfType<OptionsController>().SaveAndExit(); //SALVA MUDANÇAS no optionscontroller
        }
    }

    public void LoadGameScene() //carrega primeira cena (primeiro level no caso)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        FindObjectOfType<GameSession>().ResetGame();
        FindObjectOfType<OptionsController>().SaveAndExit(); //SALVA MUDANÇAS no optionscontroller
        SceneManager.LoadScene("Level1"); //pode usar 1 aqui, cena inicial é numero 0(start menu)
    }

    public void RestartLevel()
    {
        FindObjectOfType<GameSession>().RestartLastLevel();
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void LoadLevelOne() //carrega primeira cena (primeiro level no caso)
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene("Level1"); //pode usar 1 aqui, cena inicial é numero 0(start menu)
        FindObjectOfType<OptionsController>().SaveAndExit();
    }

    public void ReturnToGame()
    {
        FindObjectOfType<OptionsController>().SaveAndExit(); //SALVA MUDANÇAS no optionscontroller
        FindObjectOfType<PaddleMove>().PaddleUnPause();
        FindObjectOfType<GameSession>().GameUnPause();
        FindObjectOfType<Level>().TurnMenuOff();
        FindObjectOfType<Ball>().GameNotPausedLaunch();
    }

}