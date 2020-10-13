using UnityEngine;
public class Additional_Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            FindObjectOfType<Hero>().Bullets +=5;
            Destroy(gameObject);
        }
    }
}
