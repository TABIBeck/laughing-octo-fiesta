using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    Paused,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameState state = GameState.Playing;

    public static void PauseSlime() // pauses the game, called by pressing the in game pause button
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
            case GameState.Dead: // you can't pause or unpause while dead, but this button should be disabled in such a scenario anyway
                break;
            default:
                break;
        }
    }

    public static void StageSlime()
    {
        switch (state)
        {
            case GameState.Playing:
                UIManager.ShowStageText();
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                if (UIManager.IsStageTextUp()) // if this screen is already up and its button is pressed again, resume the game
                {
                    UIManager.HideStageText();
                    state = GameState.Playing;
                    Time.timeScale = 1f;
                } else // if this screen isn't up, it must be one of the others. Hide all other screens and pull up this one
                {
                    UIManager.HidePauseText();
                    UIManager.HideQuitText();
                    UIManager.ShowStageText();
                }
                break;
            case GameState.Dead: // if this is called, the player should be on the restart menu, which will close when we leave, and LoadLevel unpauses the game
                // for us too, so we can just call it and go
                LoadLevel("StageSelect");
                break;
            default:
                break;
        }
    }

    public static void QuitSlime()
    {
        switch (state)
        {
            case GameState.Playing:
                UIManager.ShowQuitText();
                state = GameState.Paused;
                Time.timeScale = 0f;
                break;
            case GameState.Paused:
                if (UIManager.IsQuitTextUp()) // if this screen is already up and its button is pressed again, resume the game
                {
                    UIManager.HideQuitText();
                    state = GameState.Playing;
                    Time.timeScale = 1f;
                }
                else // if this screen isn't up, it must be one of the others. Hide all other screens and pull up this one
                {
                    UIManager.HidePauseText();
                    UIManager.HideStageText();
                    UIManager.ShowQuitText();
                }
                break;
            case GameState.Dead: // if this is called, the player should be on the restart menu. Quitting from here doesn't really lose anything, and the player might
                // be rage quitting from here anyway, so best to make this as quick as possible and just quit without asking for secondary confirmation
                FullQuit();
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

    public static void ReloadLevel()
    {
        LoadLevel(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
