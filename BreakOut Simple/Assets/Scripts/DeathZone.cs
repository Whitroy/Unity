using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider coll)
    {
        if(coll.gameObject.CompareTag("Ball"))
        GameManager.Instance.Looselive();
    }
}
