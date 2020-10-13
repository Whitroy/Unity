using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{
    public int points = 0;
    public float Weights = 100f;
    public GameObject NxtObject = null;
    public AudioClip ballhitsound = null;
    public float AudioVolume = 5f;

    private GameManager cacheGameManager = null;
    private GameObject cachegameobject = null;
    private Transform cacheTransform = null;
    
    // Start is called before the first frame update
    void Start()
    {
        cacheGameManager = GameManager.Instance;
        cachegameobject = this.gameObject;
        cacheTransform = this.transform;
        
        InvokeRepeating("WallMovement", 0f, 0.1f);
    }

    // Update is called once per frame
    void WallMovement()
    {
        if (cacheGameManager.Currentstate == GameState.Playing)
            cacheTransform.position += new Vector3(0f, -cacheGameManager.Maxwallspeed, 0f) * Time.deltaTime;
    }

    private void Update()
    {
        if(cacheGameManager.Currentstate ==GameState.Playing)
        {
            if(cacheTransform.position.y<0.5f)
            {
                cacheGameManager.EndGame();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(cacheGameManager.Currentstate==GameState.Playing)
        {
            killBrick();

            if(NxtObject!=null)
            {
                Instantiate<GameObject>(NxtObject, cacheTransform.position, cacheTransform.rotation);
            }
        }
    }

    private void killBrick()
    {

        if(ballhitsound!=null)
        {
            AudioSource.PlayClipAtPoint(ballhitsound, cacheTransform.position, AudioVolume);
        }

        cacheGameManager.AddPoints(points);

        Destroy(cachegameobject);
    }
}
