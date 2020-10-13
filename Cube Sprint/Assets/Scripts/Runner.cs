using UnityEngine.UI;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public static float distancetravelled;

    private bool touchingPlatform;
    public Vector3 jumpvelocity;
    public float Acceleration;
    public float GameOverCondition;
    public Vector3 BoostVelocity;
    private Rigidbody rbrunner;
    private Vector3 startPosition;
    private static int boost;
    public Text BoostValue;
    public Text distanceTravel;
    public Text HighScoreValue;
    public Text NewHighScore;
    public Text FinalScore;
    float HighScore,temp;

    private void Awake()
    {
        rbrunner = GetComponent<Rigidbody>();
        HighScore= temp = PlayerPrefs.GetFloat("High Score");
        NewHighScore.enabled = false;
        FinalScore.enabled = false;
    }
    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;
        startPosition = transform.localPosition;
        GetComponent<Renderer>().enabled = false;
        rbrunner.isKinematic = true;
        enabled = false;
    }

    private void GameStart()
    {
        boost = 0;
        distancetravelled = 0f;
        transform.localPosition = startPosition;
        GetComponent<Renderer>().enabled = true;
        rbrunner.isKinematic = false;
        HighScoreValue.text = ((int)HighScore).ToString();
        enabled = true;
    }

    private void GameOver()
    {
        GetComponent<Renderer>().enabled = false;
        rbrunner.isKinematic = true;
        PlayerPrefs.SetInt("High Score", (int)HighScore);
        if(temp<HighScore && distancetravelled>temp)
        {
            NewHighScore.text = "New High Score : " + ((int)HighScore).ToString();
            NewHighScore.enabled = true;
            FinalScore.enabled = false;
        }
        else
        {
            FinalScore.text = "Your Score : " + ((int)Runner.distancetravelled).ToString();
            FinalScore.enabled = true;
            NewHighScore.enabled = false;
        }
        enabled = false;
        temp = HighScore;
    }

    private void Update()
    {
        if (Input.touchCount>0)
        {
            if (touchingPlatform)
            {
                rbrunner.AddForce(jumpvelocity, ForceMode.VelocityChange);
                touchingPlatform = false;
            }
            else if(boost>0)
            {
                rbrunner.AddForce(BoostVelocity, ForceMode.VelocityChange);
                boost--;
            }
        }
        distancetravelled = transform.localPosition.x;
        
        if(transform.localPosition.y<GameOverCondition)
        {
            GameEventManager.TriggerGameOver();
        }

        BoostValue.text =boost.ToString();
        distanceTravel.text =((int)distancetravelled).ToString();
        if(HighScore<distancetravelled)
        {
            HighScore = distancetravelled;
            HighScoreValue.text = ((int)HighScore).ToString();
        }
    }

    private void FixedUpdate()
    {
        if (touchingPlatform)
        {
            rbrunner.AddForce(Acceleration,0f,0f, ForceMode.Acceleration);
        }
    }

    private void OnCollisionEnter()
    {
        touchingPlatform = true;
    }
    private void OnCollisionExit()
    {
        touchingPlatform = false;
    }

    public static void AddBoost()
    {
        boost++;
    }
}
