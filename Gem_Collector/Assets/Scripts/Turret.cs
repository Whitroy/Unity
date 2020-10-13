using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public Animator Gun_anim = null;
    public GameObject claw = null;
    bool isShooting = false;

    public bool IsShooting
    {
        get
        {
            return isShooting;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isShooting)
        {
            LaunchClaw();
        }
    }

    void LaunchClaw()
    {
        isShooting = true;

        if(Gun_anim!=null)
        {
            Gun_anim.speed = 0f;
        }

        RaycastHit hit;
        Vector3 down = transform.TransformDirection(Vector3.down);

        if(Physics.Raycast(transform.position,down, out hit,100f))
        {
            if(claw!=null)
            {
                claw.SetActive(true);
                claw.GetComponent<Claw>().ClawTarget(hit.point);
            }
        }

    }

    public void CollectedObject()
    {
        isShooting = false;

        if(Gun_anim!=null)
        {
            Gun_anim.speed = 1f;
        }
    }
}
