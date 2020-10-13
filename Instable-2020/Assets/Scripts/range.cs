using UnityEngine;
public class range : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bat") || collision.gameObject.CompareTag("Zombie") || collision.gameObject.CompareTag("Laser")
            || collision.gameObject.CompareTag("Scientist") || collision.gameObject.CompareTag("Tank") || collision.gameObject.CompareTag("Virus"))
        {
            Destroy(collision.gameObject);
        }
    }
}
