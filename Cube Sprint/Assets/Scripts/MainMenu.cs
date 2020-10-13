using UnityEngine.UI;
using UnityEngine;
using System;

public class MainMenu : MonoBehaviour
{
    public Text Instruction;
    public Text Score;
    public Text GameOverText;
    public Button PlayButton;
    public Button InstructionButton;
    public Button QuitButton;
    public Button BackButton;
    public Button Restart;
    public Text NewHS;

    void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        Instruction.enabled = false;
        BackButton.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        PlayButton.gameObject.SetActive(false);
        InstructionButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
        Restart.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }

    private void GameStart()
    {
        gameObject.SetActive(false);
    }

    public void OnClickPlayButton()
    {
        GameEventManager.TriggerGameStart();
    }

    public void OnClickInstruction()
    {
        Instruction.enabled = true;
        PlayButton.gameObject.SetActive(false);
        InstructionButton.gameObject.SetActive(false);
        QuitButton.gameObject.SetActive(false);
        BackButton.gameObject.SetActive(true);
    }
    
    public void OnClickBackButton()
    {
        Instruction.enabled = false;
        PlayButton.gameObject.SetActive(true);
        InstructionButton.gameObject.SetActive(true);
        QuitButton.gameObject.SetActive(true);
        BackButton.gameObject.SetActive(false);
        Restart.gameObject.SetActive(false);
        Score.enabled = false;
        NewHS.enabled = false;
        GameOverText.enabled = false;
    }

    public void OnClickRestart()
    {
        gameObject.SetActive(false);
        GameEventManager.TriggerGameStart();
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}
