using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform targetTransform=null;
    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gameManager.GetGameState() != GameState.GameOver && targetTransform.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, targetTransform.position.y,transform.position.z);
        }
    }
}
