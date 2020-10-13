using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Claw : MonoBehaviour
{
    public Transform origin = null;
    public Turret turretscript = null;
    public const int Claw_speed = 4;
    private Vector3 target;

    LineRenderer Claw_line;

    public int Gem_points = 100;

    GameObject childobject = null; // to retrace objects from target to origin

    public ScoreManager scoremanager_script = null;

    bool isRetracing = false;
    bool isGemHit = false;

    private void Awake()
    {
        Claw_line = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        float actual_Speed = Claw_speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, actual_Speed);
        Claw_line.SetPosition(0, origin.position);
        Claw_line.SetPosition(1, transform.position);

        if(transform.position==origin.position && isRetracing && turretscript!=null)
        {
            turretscript.CollectedObject();

            if(isGemHit)
            {
                if(scoremanager_script!=null)
                {
                    scoremanager_script.AddPoints(Gem_points);
                }

                isGemHit = false;
            }

            if (childobject != null)
            {
                Destroy(childobject);
            }

            gameObject.SetActive(false);
            isRetracing = false;
        }
    }

    public void ClawTarget(Vector3 targetPos)
    {
        target = targetPos;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!isRetracing)
        {
            isRetracing = true;
            target = origin.position;

            if (other.gameObject.CompareTag("Gem"))
                isGemHit = true;

            if (!other.gameObject.CompareTag("Barrier") )
            {
                childobject = other.gameObject;
                childobject.transform.SetParent(this.transform);
            }
        }

    }
}
