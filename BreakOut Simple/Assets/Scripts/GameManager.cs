using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PfBrick = null;
    public GameObject Paddle = null;

    GameObject ClonePaddle = null;

    public GameObject DeathParticle = null;
    public GameObject YouWon = null;
    public GameObject GameOver = null;
    public Text Lives = null;

    public int lives;
    public int No_of_Bricks = 24;
    public int Reset_Delay;

    public static GameManager Instance = null;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(Instance);

        Lives.text = "Lives : " + lives;

        AddBricks();

        AddPaddle();
    }

    void AddBricks()
    {
        if(PfBrick!=null)
            Instantiate<GameObject>(PfBrick, new Vector3(0f, 2f, 0f), Quaternion.identity);
    }

    void AddPaddle()
    {
        if (Paddle != null)
            ClonePaddle = Instantiate<GameObject>(Paddle, new Vector3(0.24f, -0.74f, -3.0802f),Quaternion.identity) as GameObject;
    }

    void CheckGameOver()
    {
        if (lives < 1)
        {
            if(GameOver!=null)
            {
                GameOver.SetActive(true);
                //Time.timeScale = 0.25f;
                Invoke("Reset", Reset_Delay);
            }
        }
        if(No_of_Bricks<1)
        {
            if (YouWon != null)
            {
                YouWon.SetActive(true);
                Time.timeScale = 0.25f;
                Invoke("Reset", Reset_Delay);
            }
        }
    }

    private void Reset()
    {
        Time.timeScale = 1f;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Looselive()
    {
        lives--;
        if(Lives!=null)
            Lives.text = "Lives : " + lives;

        if(DeathParticle!=null)
        {
            Instantiate<GameObject>(DeathParticle,ClonePaddle.transform.position,Quaternion.identity);
            
        }

        Destroy(ClonePaddle.gameObject);
        CheckGameOver();
        AddPaddle();
    }
    public void DestroyBrick()
    {
        No_of_Bricks--;
        CheckGameOver();
    }
}
