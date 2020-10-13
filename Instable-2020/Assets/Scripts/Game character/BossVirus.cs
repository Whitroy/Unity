using UnityEngine;

public class BossVirus : MonoBehaviour
{
    private Rigidbody2D rbVirus;
    private Vector3 Launchdirection;
    bool islaunch = false;
    private void Start()
    {
        rbVirus = GetComponent<Rigidbody2D>();
    }

    public void direction(Vector3 dir)
    {
        islaunch = true;
        Launchdirection = dir;
    }

    private void FixedUpdate()
    {
        if (islaunch)
        {
            rbVirus.AddForce(Launchdirection * 2f, ForceMode2D.Impulse);
            islaunch = false;
        }
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") || collision.gameObject.CompareTag("Range"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            if(FindObjectOfType<Hero>())
                FindObjectOfType<Hero>().Kills++;
            Destroy(gameObject);
        }
    }
}
