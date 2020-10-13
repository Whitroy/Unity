using UnityEngine.UI;
using UnityEngine;

public class Counter_Button : MonoBehaviour
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
            FindObjectOfType<Fire>().Counter();
        }
        catch
        {
            Debug.Log("Fire is not exist");
        }

    }
}
