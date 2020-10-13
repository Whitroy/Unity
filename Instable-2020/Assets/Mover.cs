using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;

    public enum Movement { horizontal,vertical,slide }

    public Movement movement = Movement.vertical;
    private void Start()
    {

    }
    private void Update()
    {
        if (movement == Movement.vertical)
            gameObject.transform.Translate(Vector3.up * Mathf.Cos(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
        else if (movement == Movement.horizontal)
            gameObject.transform.Translate(Vector3.right * Mathf.Cos(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
        else
            gameObject.transform.Translate(new Vector2(-Mathf.Cos(Mathf.PI/4), Mathf.Sin(Mathf.PI / 4)) * Mathf.Cos(Time.timeSinceLevelLoad) * speed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Knife") || collision.gameObject.CompareTag("Bullet") || collision.gameObject.CompareTag("Virus"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("Pipe"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }
}
