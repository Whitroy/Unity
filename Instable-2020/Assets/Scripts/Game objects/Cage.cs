using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public List<Zombie> Zombies;
    bool isSpawned = false, isdestroied = false;

    private void OnBecameVisible()
    {
        SpawnZombie();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Hero") && !isdestroied)
        {
            isdestroied = true;
            Destroy(gameObject.transform.GetChild(2).gameObject);
        }
    }


    void SpawnZombie()
    {
        if (!isSpawned)
        {
            Instantiate<Zombie>(Zombies[Random.Range(0, Zombies.Count)], new Vector2(transform.position.x, 3.74f), Quaternion.identity);
            isSpawned = true;
        }

    }
}
