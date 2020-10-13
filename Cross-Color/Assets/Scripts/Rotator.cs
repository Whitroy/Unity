using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rotationSpeed = Random.Range(100, 150);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, Time.deltaTime * rotationSpeed, Space.World);
    }
}
