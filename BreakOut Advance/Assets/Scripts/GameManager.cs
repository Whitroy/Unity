using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    GetReady,
    Playing,
    GameOver,
    GamePaused
}
public class GameManager : MonoBehaviour
{
    public List<GameObject> BrickTable = null;
   // public Dictionary<string, Timer> TimeTable = null;
   // public List<GameObject> BonusBrickTable = null;

    public GameObject Pf_ball = null;
    public Transform ballSpawningPoint = null;

    public Text txtScore = null;
    public Text txtHighscore = null;
    public Text txtNoOfBall = null;
    public GameObject GameOver = null;
    public GameObject GetReady = null;
    public GameState Currentstate = GameState.GetReady;

    public float Maxwallspeed = 0.01f;

    private int currentscore = 0;
    private int highscore = 0;
    private int ballinplay = 0;
    private int ballcount = 6;

    private float[] BrickWeightTable = null;
   // private float[] BonusBrickWeightTable = null;
   // private int[] HalfRowTable = new int[5];
   // private float RowInterpolator = 5f;

    private static GameManager _instance = null;

    float startingcolpos;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = (GameManager)FindObjectOfType(typeof(GameManager));

            return _instance;
        }
    }


    //Awake
    void Awake()
    {

        if(Currentstate == GameState.GetReady && GetReady!=null)
        {
            GetReady.SetActive(true);
        }

        ConstructBrickWeightTable();

        if (BrickTable.Count > 0)
        {
            float rowintialpos = 12f;
            for (int row = 0; row < 5; row++)
            {
                startingcolpos = -8f;
                for (int col = 0; col < 18; col=col+2)
                {
                    float colpos = startingcolpos + col;
                    Instantiate<GameObject>(BrickTable[prob()], new Vector3(colpos,rowintialpos, 0f),BrickTable[0].transform.rotation);
                }
                rowintialpos -= 1.5f;
            }
        }

        // Add Bonus Brick
        InvokeRepeating("AddAdditionalBrickRow", 0f, 25f);

        //Score
        highscore = PlayerPrefs.GetInt("HIGH SCORE");
        currentscore = 0;
        if (txtScore != null)
            txtScore.text = "SCORE : " + currentscore;
        if (txtHighscore != null)
            txtHighscore.text = "HIGH SCORE : " + highscore;
    }


    //Construct Brick Weight Table
    void ConstructBrickWeightTable()
    {
        BrickWeightTable = new float[BrickTable.Count];

        float sum = 0f;

        for (int index = 0; index < BrickTable.Count; index++)
        {
            DestructibleItem Brick =BrickTable[index].GetComponent<DestructibleItem>(); 
            BrickWeightTable[index] = Brick.Weights;
            sum += Brick.Weights;

        }

        for (int index = 0; index < BrickTable.Count; index++)
        {
            BrickWeightTable[index] /= sum;
        }

    }

    // find which brick will instatiate
    int prob()
    {
        float randombrickindex = Random.value;
        float sum = 0f;
        for (int i=0;i<BrickTable.Count;i++)
        {
            sum += BrickWeightTable[i];
            if (randombrickindex <= sum)
                return i;
        }
        return 0;  
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GetReady.SetActive(false);
            Currentstate = GameState.Playing;

        }

        if (Currentstate == GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Restart();
            }
        }
        if (ballcount + ballinplay < 1)
        {
            EndGame();
            return;
        }
    }

    //AddPoints
    public void AddPoints(int points)
    {
        currentscore += points;
        if (txtScore != null)
        { 
            txtScore.text = "SCORE : " + currentscore;

            if(currentscore>highscore)
            {
                if (txtHighscore != null)
                {
                    highscore = currentscore;
                    txtHighscore.text = "HIGH SCORE : " + highscore;
                }
            }
        }

    }

    //EndGame
    public void EndGame()
    {
        if(Currentstate!=GameState.GameOver)
        Currentstate = GameState.GameOver;

        PlayerPrefs.SetInt("HIGH SCORE", highscore);

        if (GameOver != null)
            GameOver.SetActive(true);

    }


    void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    // Add Bonus Brick Row After one second
    void AddAdditionalBrickRow()
    {
        if (Currentstate == GameState.Playing && BrickTable.Count>0)
        {
            startingcolpos = -8f;
            for (int col = 0; col < 18; col = col + 2)
            {
                    float colpos = startingcolpos + col;
                    Instantiate<GameObject>(BrickTable[prob()], new Vector3(colpos, 12f, 0f), BrickTable[0].transform.rotation);
            }
        }
    }

    //Spawn Vall
    public void SpawnBall()
    {
        if(Pf_ball!=null && ballSpawningPoint!=null && Currentstate==GameState.Playing)
        {
            Instantiate<GameObject>(Pf_ball, ballSpawningPoint.position, Quaternion.identity);
        }
    }

    //Ball release
    public void BallRelease()
    {
        if (Currentstate == GameState.Playing && ballcount > 0 && ballSpawningPoint != null)
        {
            ballcount--;
            SpawnBall();

            if (txtNoOfBall != null)
                txtNoOfBall.text = "Balls : " + ballcount;
        }
    }

    public void RegisterBall()
    {
        ballinplay++;
    }

    public void UnRegisterBall()
    {
        ballinplay--;
    }
}
