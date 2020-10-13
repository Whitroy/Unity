using UnityEngine;

public class Fire : MonoBehaviour
{
    Rigidbody2D rbFire;
    int direction;
    private SpriteRenderer spriteRendererFire;
    private void Awake()
    {
        rbFire = GetComponent<Rigidbody2D>();
        spriteRendererFire = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rbFire.velocity = new Vector2(direction * 8f, 0f);
        if (!FindObjectOfType<Hero>())
            Destroy(gameObject);
    }

    public void setdirection(bool isflip)
    {
        if (isflip)
            direction = 1;
        else
            direction = -1;
        spriteRendererFire.flipX = isflip;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Counter()
    {
        if (Mathf.Abs(Mathf.Abs(FindObjectOfType<Hero>().transform.position.x) - Mathf.Abs(transform.position.x)) < 1.5f)
        {
                Time.timeScale = 0.25f;
                direction *= -1;
                spriteRendererFire.flipX = !spriteRendererFire.flipX;
                Invoke("TimeNormal", 0.15f);
        }
    }
    void TimeNormal()
    {
        Time.timeScale = 1f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.gameObject.GetComponent<Collider2D>());
        }
    }
}
