using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    static GameState gameState;

    public static GameManager gameManager=null;

    Object MainMenu,GameOver,Pause,GUI;

    static  bool sound = true;

    private void Start()
    {
        AudioManager._instance.Play("Music");
    }
    private void Awake()
    {
        Input.backButtonLeavesApp = true;
        if(gameManager == null)
        {
            gameManager = this;
            DontDestroyOnLoad(gameManager);
        }
        else
        {
            Destroy(this);
            return;
        }

        gameState = GameState.Menu;
        Action();
    }

    public void SetGameState(GameState state)
    {
        gameState = state;
        Action();
    }

    public GameState GetGameState()
    {
        return gameState;
    }

    private void Action()
    {
        switch (gameState)
        {
            case GameState.GameOver:
                Debug.Log("GameOver");
                AudioManager._instance.Play("Die");
                if(GameObject.FindGameObjectsWithTag("GameOver").Length<1)
                    GameOver = Object.Instantiate(Resources.Load("GameOver"));
                Camera.main.transform.position=new Vector3(0f, 0f, -10f);
                Object.Destroy(GUI);
                break;

            case GameState.Menu:
                Debug.Log("Menu");
                if (GameObject.FindGameObjectsWithTag("Menu").Length < 1)
                    MainMenu =Object.Instantiate(Resources.Load("Menu"));
                break;

            case GameState.Play:
                Debug.Log("Play");
                Object.Destroy(MainMenu);
                Object.Destroy(GameOver);
                if (GameObject.FindGameObjectsWithTag("GUI").Length < 1)
                    GUI = Object.Instantiate(Resources.Load("GUI"));
                break;

            case GameState.Pause:
                Object.Destroy(GUI);
                Debug.Log("Paused !");
                if (GameObject.FindGameObjectsWithTag("Pause").Length < 1)
                    Pause = Object.Instantiate(Resources.Load("Pause"));
                Time.timeScale = 0f;
                AudioManager._instance.Stop("Music");
                break;

            case GameState.Resume:
                Object.Destroy(Pause);
                Debug.Log("Resume");
                if (GameObject.FindGameObjectsWithTag("GUI").Length < 1)
                    GUI = Object.Instantiate(Resources.Load("GUI"));
                Time.timeScale = 1f;
                gameState=GameState.Play;
                AudioManager._instance.Play("Music");
                break;

            case GameState.Exit:
                Time.timeScale = 1f;
                #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
                #else
                    Application.Quit();
                #endif
                break;
        }
    }

    public void ChangeSound()
    {
        AudioManager._instance.VolumeControl(sound);
        sound = !sound;
    }
}

