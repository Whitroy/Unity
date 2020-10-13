using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    public delegate void GameEvent();

    public static event GameEvent GameStart;
    public static event GameEvent GameOver;

    public static void TriggerGameStart()
    {
        if (GameStart != null)
            GameStart();
    }

    public static void TriggerGameOver()
    {
        if (GameOver != null)
            GameOver();
    }
}
