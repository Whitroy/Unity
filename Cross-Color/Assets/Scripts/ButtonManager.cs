using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void PlayButton()
    {
        GameManager.gameManager.SetGameState(GameState.Play);
    }

    public void PauseButton()
    {
        GameManager.gameManager.SetGameState(GameState.Pause);
    }

    public void ResumeButton()
    {
        GameManager.gameManager.SetGameState(GameState.Resume);
    }

    public void ExitButton()
    {
        Debug.Log("Exit");
        GameManager.gameManager.SetGameState(GameState.Exit);
    }

    public void Sound()
    {
        Debug.Log("Change");
        GameManager.gameManager.ChangeSound();
    }
}
