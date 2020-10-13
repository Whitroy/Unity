using UnityEngine;

public class Virus : MonoBehaviour
{
    private Rigidbody2D rbVirus;
    private Vector3 TargetPos;
    bool islaunch = false;
    private void Start()
    {
        rbVirus = GetComponent<Rigidbody2D>();
    }

    public void LaunchedForce(Vector3 Target)
    {
        islaunch = true;
        TargetPos = Target;
    }

    private void FixedUpdate()
    {
        if (islaunch)
        {
            rbVirus.AddForce(TargetPos, ForceMode2D.Impulse);
            islaunch = false;
        }

        if (transform.position.x > 5.5f || transform.position.x < -130f)
            Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("BackgroundSurface"))
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            AudioManager.instance.Play("Ground hit");
            if (FindObjectOfType<Hero>())
            {
                if (FindObjectOfType<Hero>().transform.position.x > transform.position.x)
                    rbVirus.AddForce(Vector2.right * 0.9f);
                else
                    rbVirus.AddForce(Vector2.left * 0.9f);
            }
            transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("BackgroundSurface"))
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
            AudioManager.instance.Play("Ground hit");
            if (FindObjectOfType<Hero>())
            {
                if (FindObjectOfType<Hero>().transform.position.x > transform.position.x)
                    rbVirus.AddForce(Vector2.right * 0.9f);
                else
                    rbVirus.AddForce(Vector2.left * 0.9f);
            }
            transform.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            transform.parent = null;
        }
        if (collision.gameObject.CompareTag("Knife"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
