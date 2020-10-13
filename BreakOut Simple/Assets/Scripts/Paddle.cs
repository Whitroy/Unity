using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    float PaddleSpeed = 0.8f;

    private void Update()
    {
        float x_pos = transform.position.x + (Input.GetAxis("Horizontal") * PaddleSpeed);
        x_pos = Mathf.Clamp(x_pos,-2.6f,3f);
        transform.position = new Vector3(x_pos, -0.74f, -3.08f);
    }
}
