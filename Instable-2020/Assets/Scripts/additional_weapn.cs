using UnityEngine;
public class additional_weapn : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Hero"))
        {
            FindObjectOfType<Hero>().Knife += 5;
            Destroy(gameObject);
        }
    }
}
