using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public ParticleSystem shape, trail, burst;
    private Player player;
    public float deathCountdown = -1f;
    private void Awake()
    {
        player = transform.root.GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(deathCountdown<0f)
        {
            shape.enableEmission = false;
            trail.enableEmission = false;
            burst.Emit(burst.maxParticles);
            deathCountdown = burst.startLifetime;
        }
    }

    private void Update()
    {
        if (deathCountdown >= 0f)
        {
            deathCountdown -= Time.deltaTime;
            if (deathCountdown <= 0f)
            {
                deathCountdown = -1f;
                shape.enableEmission = true;
                trail.enableEmission = true;
                player.Die();
            }
        }
    }
}
