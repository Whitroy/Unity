using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            AudioManager.instance.Play("CheckPoint");
            Destroy(gameObject.GetComponent<BoxCollider2D>());

        }
    }
}
