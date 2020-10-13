using UnityEngine.UI;
using UnityEngine;

public class GernadeButton : MonoBehaviour
{
    void Start()
    {
        Button Counter = GetComponent<Button>();

        if (Counter == null)
            Debug.Log("Counter is not working");

        Counter.onClick.AddListener(Fire);
    }


    void Fire()
    {
        try
        {
            FindObjectOfType<Hero>().GernadeThrow();
        }
        catch
        {
            Debug.Log("Can't throw Gernade");
        }

    }
}
