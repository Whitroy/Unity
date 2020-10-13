using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rbBullet;
    int direction;
    private void Start()
    {
        rbBullet = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rbBullet.velocity = new Vector2(direction * 5f, 0f);
        Destroy(gameObject, 10f);
    }

    public void setdirection(bool flipX)
    {
        if (flipX)
            direction = 1;
        else
            direction = -1;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Virus") || collision.gameObject.CompareTag("Laser"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Pipe"))
            Destroy(gameObject);
    }
}
