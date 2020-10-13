using UnityEngine;
using Random = UnityEngine.Random;

public class Booster : MonoBehaviour
{
    public Vector3 offset;
    public float recycleOffset;
    public Vector3 rotationVelocity;

    public float SpawnChance;

    private void Start()
    {
        GameEventManager.GameOver += GameOver;
        gameObject.SetActive(false);
    }

    public void SpawnIfAvailable(Vector3 position)
    {
        if (gameObject.activeSelf || SpawnChance<=Random.Range(0f,100f))
            return;

        transform.localPosition = position + offset;
        gameObject.SetActive(true);
    }

    private void GameOver()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (transform.localPosition.x + recycleOffset < Runner.distancetravelled)
        {
            gameObject.SetActive(false);
            return;
        }

        transform.Rotate(rotationVelocity * Time.deltaTime);
    }

    private void OnTriggerEnter()
    {
        Runner.AddBoost();
        gameObject.SetActive(false);
    }

}

