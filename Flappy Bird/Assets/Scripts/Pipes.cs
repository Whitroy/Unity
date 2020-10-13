using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        if (pos.x < -10)
            Destroy(gameObject);

        if(!Score.isdie)
        {
            pos.x -= 0.07f;
            transform.position = pos;
        }

    }
}
