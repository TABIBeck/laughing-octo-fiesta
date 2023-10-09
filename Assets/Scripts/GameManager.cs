using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameState state = GameState.Playing;

    private void Start()
    {
        /*UIManager.CreateMenu();*/
    }
    void Update()
    {
    }

    public void PauseSlime() // pauses the game, called by pressing the in game pause button
    {
        switch (state)
        {
            case GameState.Playing:
                UIManager.ShowPauseText();
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                UIManager.HidePauseText();
                state = GameState.Playing;
                Time.timeScale = 1f;
                break;
            default:
                break;
        }
    }

    public void StageSlime()
    {
        switch (state)
        {
            case GameState.Playing:
                UIManager.ShowStageText();
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                if ((UIManager.quitText.activeSelf == false) & (UIManager.pauseText.activeSelf == false)) // if neither of hte other screens is up, this one
                    // must be the one making the game pause, so resume the game
                {
                    UIManager.HideStageText();
                    state = GameState.Playing;
                    Time.timeScale = 1f;
                } else // if one of the other screens is up, hide all other screens and pull up this one
                {
                    UIManager.HidePauseText();
                    UIManager.HideQuitText();
                    UIManager.ShowStageText();
                }
                break;
            default:
                break;
        }
    }

    public void QuitSlime()
    {
        switch (state)
        {
            case GameState.Playing:
                UIManager.ShowQuitText();
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                if ((UIManager.stageText.activeSelf == false) & (UIManager.pauseText.activeSelf == false)) // if neither of the other screens is up, this one
                                                                                                          // must be the one making the game pause, so resume the game
                {
                    UIManager.HideQuitText();
                    state = GameState.Playing;
                    Time.timeScale = 1f;
                }
                else // if one of the other screens is up, hide all other screens and pull up this one
                {
                    UIManager.HidePauseText();
                    UIManager.HideStageText();
                    UIManager.ShowQuitText();
                }
                break;
            default:
                break;
        }
    }

    public static void FullQuit() // while quit slime pulls up a quit confirm, this is the function that you can select from that confirm to actually quit
    {
        Application.Quit();
    }

    public static void LoadLevel(string sceneName)
    {
        state = GameState.Playing; // this can be called from pause menus, so important to unpause the game if doing so
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
