using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score = 0;
    int T_Time = 30;
    public Text Score = null;
    public Text Timer = null;
    const int clockspeed = 1;
    public GameObject You_Won = null;
    public GameObject Game_Over = null;

    private void Awake()
    {
        Score.text = "Score :" + score + "/ 500 ";
        InvokeRepeating("TimeDeduct", 0f, clockspeed);
    }

    void TimeDeduct()
    {
        Timer.text = "Timer : " + T_Time;
        CheckGameOver();
        T_Time--;
    }

    public void AddPoints(int points)
    {
        score += points;
        Score.text = "Score :" + score + "/ 500 ";

    }

    void CheckGameOver()
    {
        if (score >= 500 && Score != null)
        {
            Time.timeScale = 0;
            You_Won.SetActive(true);
        }
        else if ((score < 500 && T_Time<0 )&& Timer!=null )
        {
            Time.timeScale = 0;
            Game_Over.SetActive(true);
        }

    }
}
