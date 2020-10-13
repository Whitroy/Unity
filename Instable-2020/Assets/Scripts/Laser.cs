using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody2D rblaser;
    int direction;
    Vector3 dir;
    private void Start()
    {
        rblaser = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(direction==2)
        {
            if(dir.x>0)
            {
                transform.rotation = Quaternion.Euler(0f,0f, dir.x * 8f);
            }
            else
                transform.rotation = Quaternion.Euler(0f, 180f, dir.x * 8f);
            rblaser.velocity = dir*10f;
        }
        else
            rblaser.velocity = new Vector2(direction * 5f, 0f);
    }

    public void setdirection(bool flipX)
    {
        if (flipX)
            direction = 1;
        else
            direction = -1;
    }

    public void setdirectionBoss(Vector3 d)
    {
        dir = d;
        direction = 2;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Knife"))
        {
            if (!collision.gameObject.CompareTag("Hero"))
            {
                AudioManager.instance.Play("Bullet Collision");
                Destroy(collision.gameObject);
            }
            Destroy(gameObject);
           
        }
        if (collision.gameObject.CompareTag("Pipe"))
            Destroy(gameObject);
    }
}
