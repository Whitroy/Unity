using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Score : MonoBehaviour
{
    public Text T_Score = null;
    public Text Highscore = null;

    public GameObject GameOver = null;
    public GameObject restart = null;

    public static bool isdie = false;

    public static int Totalscore = 0;
    public static bool ishighscore = false;

    private void Awake()
    {
        SetScore(PlayerPrefs.GetInt("HighScore"));
        //PlayerPrefs.SetInt("HighScore", 1);
    }

    public void AddScore()
    {
        Totalscore++;
        T_Score.text = "SCORE : " + Totalscore;
    }
    public void SetScore(int H_S)
    {
        Highscore.text = "HIGH SCORE : " + H_S;
    }

    public void SetHighScore()
    {
        ishighscore = false;
        if(PlayerPrefs.GetInt("HighScore")<Totalscore)
        {
            PlayerPrefs.SetInt("HighScore", Totalscore);
            SetScore(PlayerPrefs.GetInt("HighScore"));
            Totalscore = 0;
        }
    }

    private void Update()
    {
        if (isdie)
        {

            if (Input.GetKeyDown(KeyCode.S) && !Bird.canrestart)
            {
                    Score.Totalscore = 0;
                    Score.ishighscore = false;
                    Application.LoadLevel(Application.loadedLevel);
                
            }
            
        }

        if(Totalscore>PlayerPrefs.GetInt("HighScore"))
            Highscore.text = "HIGH SCORE : " + Totalscore;

    }
}
