using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public List<GameObject> Gems = new List<GameObject>();
    int random;
    Vector3 spawn_place;

    // Start is called before the first frame update
    void Start()
    {
        float x =0f;
        float j = 1.5f;
        float k = - 1.5f;
        for (int i=0;i<8;i++)
        {
            random = Random.Range(0, Gems.Count);
            if (i % 2 == 0)
            {
                x += j * i;
            }
            else
            {
                x += k * i;
            }
            spawn_place = new Vector3(x,Random.Range(-1.8f,0.5f),0f);
            Instantiate<GameObject>(Gems[random], spawn_place, Quaternion.identity);
        }
    }

}
