using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float BallSpeed = 10f;
    public float BallSpeedMultiplier = 2f;
    //public GameObject GlowBallObj = null;
    public float BigSizeMultiplier = 3f;
    //public Material Mat_NormalBall = null;
    //public Material Mat_GLowBall = null;

   // private int counter = 0;
    private GameManager gameManagerCache = null;
    private Rigidbody rbBall = null;
    private GameObject cacheGameobject = null;
    private Transform cacheTransform = null;

    //private Vector3 OriginalScale = Vector3.zero;
    //private Vector3 BigScale = Vector3.zero;

    //private float Originalspeed = 0f;
    //private float fastspeed = 0f;

    // Start is called before the first frame update
    void Awake()
    {
        rbBall = GetComponent<Rigidbody>();
        cacheGameobject = this.gameObject;
        cacheTransform = this.transform;
        gameManagerCache = GameManager.Instance;
        
        if(gameManagerCache)
        {
            gameManagerCache.RegisterBall();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rbBall != null)
        {
            Vector3 ballVelocity = rbBall.velocity;

            if(Mathf.Abs(ballVelocity.y)<3f)
            {
                if(ballVelocity.y<0f)
                {
                    ballVelocity.y = -3f;
                }
                else
                {
                    ballVelocity.y = 3f;
                }

                rbBall.velocity = ballVelocity;
            }

            if(transform.position.y<-5f)
            {
                if (gameManagerCache)
                    gameManagerCache.UnRegisterBall();

                Destroy(gameObject);
            }
        }


    }


    private void LateUpdate()
    {
        if(gameManagerCache)
        {
            rbBall.velocity = rbBall.velocity.normalized * BallSpeed;
        }

    }
}
