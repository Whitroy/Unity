using UnityEngine.UI;
using UnityEngine;

public class FireButton : MonoBehaviour
{
    void Start()
    {
        Button fire =GetComponent<Button>();

        if (fire == null)
            Debug.Log("Firing not working");

        fire.onClick.AddListener(Fire);
    }


    void Fire()
    {
        FindObjectOfType<Hero>().SpawnBullet();
    }
}
