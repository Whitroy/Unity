using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float BallInitialvelocity = 300f;
    Rigidbody RbBall = null;
    bool isBalllaunch = false;

    private void Awake()
    {
        RbBall = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (!isBalllaunch && Input.GetKeyDown(KeyCode.Space))
        {
            transform.parent = null;
            RbBall.isKinematic = false;
            RbBall.AddForce(BallInitialvelocity, BallInitialvelocity, 0f);
            isBalllaunch = true;

        }
    }
}
