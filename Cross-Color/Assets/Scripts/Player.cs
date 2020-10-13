using UnityEngine;
public class Player : MonoBehaviour
{
    private Rigidbody2D playerRb = null;

    private SpriteRenderer spriteRenderer = null;

    private string CurrentColor = "";

    float deathpoint = -5f;

    [SerializeField]
    private float jumpForce = 10f;

    [Header("Colors :- ")]
    public Color Cyan;
    public Color Pink;
    public Color Purple;
    public Color Yellow;


    float changeAfter=1.5f;

    float timer = 0f;

    float score = 0f;
    float Highscore = 0f;
    // Start is called before the first frame update
    void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb.isKinematic = true;

    }

    private void Start()
    {
        ChangeColor(RandomColor());
        if (PlayerPrefs.HasKey("HighScore"))
        {
            Highscore = PlayerPrefs.GetInt("HighScore");
        }
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerPrefs.SetInt("Score", (int)score);
        if (GameManager.gameManager.GetGameState() == GameState.Play)
        {
            if (Highscore < score)
            {
                PlayerPrefs.SetInt("HighScore", (int)score);
                Highscore = score;
                PlayerPrefs.Save();
            }
            playerRb.isKinematic = false;
            if (score< Mathf.Abs(transform.position.y))
            {
                score = Mathf.Abs(transform.position.y);
            }
            if (transform.position.y < deathpoint)
            {
                GameOver();
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                playerRb.velocity = Vector2.up * jumpForce;
                AudioManager._instance.Play("Tap");
            }
        }
        else
        {
            playerRb.isKinematic = true;
            if (timer > changeAfter)
            {
                ChangeColor(RandomColor());
                timer = 0f;
            }
            timer += Time.deltaTime;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="ColorChanger")
        {
            ChangeColor(RandomColor());
            AudioManager._instance.Play("ChangeColor");
            Destroy(collision.gameObject);
            deathpoint = collision.gameObject.transform.position.y - 8f;
            return;
        }
        if(collision.gameObject.name != CurrentColor)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        playerRb.velocity = Vector2.zero;
        transform.position = new Vector2(0f, -2.5f);
        deathpoint = -5f;
        GameManager.gameManager.SetGameState(GameState.GameOver);
        playerRb.isKinematic = true;
        score = 0f;
    }

    int RandomColor()
    {
        return Random.Range(0, 4);
    }

    void ChangeColor(int color)
    {
        switch(color)
        {
            case 0:
                spriteRenderer.color = new Color(Cyan.r,Cyan.g,Cyan.b);
                CurrentColor = "Cyan";
                break;

            case 1:
                spriteRenderer.color = new Color(Pink.r, Pink.g, Pink.b);
                CurrentColor = "Pink";
                break;
            case 2:
                spriteRenderer.color = new Color(Yellow.r, Yellow.g, Yellow.b);
                CurrentColor = "Yellow";
                break;
            case 3:
                spriteRenderer.color = new Color(Purple.r, Purple.g, Purple.b);
                CurrentColor = "Purple";
                break;
        }
    }
}
