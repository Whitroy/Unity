using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpwaning : MonoBehaviour
{
    public GameObject Pipe = null;
    Vector3 pos;

    private void Start()
    {
        if(Pipe!=null)
            pos = Pipe.transform.position;
            InvokeRepeating("SpawnPipe", 2, 2);
        
    }
void SpawnPipe()
{
    float random_yaxis = Random.Range(-2f, 2.6f);
    pos.y = random_yaxis;
    if (!Score.isdie)
    {
        Instantiate<GameObject>(Pipe, pos, Quaternion.identity);
    }
}
}
