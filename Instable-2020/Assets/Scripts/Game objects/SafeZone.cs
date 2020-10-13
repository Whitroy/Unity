using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public GameObject OpenDoor;
    public GameObject CloseDoor;

    private void Start()
    {
        if (OpenDoor != null && CloseDoor != null)
        {
            OpenDoor.SetActive(false);
            CloseDoor.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") || collision.gameObject.CompareTag("Villager"))
        {
            OpenDoor.SetActive(true);
            CloseDoor.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OpenDoor.SetActive(false);
        CloseDoor.SetActive(true);
    }
}
