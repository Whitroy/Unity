using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    private Vector3 StartPos;
    public Transform target;
    public float speed;
    private bool moveUp;
    private void Start()
    {
        StartPos = transform.position;
        moveUp = true;
    }

    private void Update()
    {
        float step = speed * .025f; ;

        if (transform.position == target.position)
        {
            moveUp = false;
        }
        else if (transform.position == StartPos)
        {
            moveUp = true;
        }
        if (moveUp)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        }
        else
            transform.position = Vector3.MoveTowards(transform.position, StartPos, step);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Virus"))
        {
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Pipe"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
