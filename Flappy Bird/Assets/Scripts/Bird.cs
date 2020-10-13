using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    Rigidbody2D rbBird;

    public float force_value = 40f;
    bool isforce_apply = false;
    public static bool canrestart = false;

    public Score scoreobj = null;

    Animator bird_anim;

    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        Score.isdie = false;
        rbBird = GetComponent<Rigidbody2D>();
        bird_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Score.isdie)
        {
            if (!Score.ishighscore && scoreobj != null)
            {
                scoreobj.SetHighScore();
            }
        }

        if(!Score.isdie && Input.GetKeyDown(KeyCode.Space) && !isforce_apply)
        {
            isforce_apply = true;
        }
    }
    private void FixedUpdate()
    {
        if (isforce_apply)
        { 
            rbBird.AddForce(Vector3.down*force_value*Physics2D.gravity);
            isforce_apply = false;

        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (scoreobj != null)
            scoreobj.AddScore();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(DisplayGameOver());
        Score.isdie = true;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    private IEnumerator DisplayGameOver()
    {
        yield return new WaitForSeconds(2f);

        if (scoreobj.GameOver != null)
        {
            scoreobj.restart.SetActive(true);
            scoreobj.GameOver.SetActive(true);
        }

    }
}

