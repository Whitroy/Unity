using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeItem : MonoBehaviour
{
    private Transform rotator;
    private void Awake()
    {
        rotator = transform.GetChild(0);
    }

    public void Position(Pipe pipe,float curveRotation,float ringRotation)
    {
        transform.SetParent(pipe.transform, false);
        transform.localRotation = Quaternion.Euler(0f, 0f, -curveRotation);
        rotator.localPosition = new Vector3(0f, pipe.CurveRadius);
        rotator.localRotation = Quaternion.Euler(ringRotation, 0f, 0f);
    }
}
