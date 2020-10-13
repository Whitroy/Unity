using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public float MouseSenstivity = 0.8f;
    private Transform cacheTransform = null;
    private GameManager gameManagercache = null;
    public Color paddlecolor = Color.black;

    // Start is called before the first frame update
    void Start()
    {
        gameManagercache = GameManager.Instance;
        cacheTransform = this.transform;

        GetComponent<Renderer>().material.color = paddlecolor;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManagercache!=null && gameManagercache.Currentstate != GameState.GamePaused && gameManagercache.Currentstate==GameState.Playing)
        {
            float MouseDelta = Input.GetAxis("Mouse X") * MouseSenstivity;
            cacheTransform.position += new Vector3(MouseDelta,0f,0f);

            // TO DO : launch ball on left click

            if(Input.GetKeyDown(KeyCode.S))
            {
                gameManagercache.BallRelease();
            }

            if (cacheTransform.position.x >9.5f)
                cacheTransform.position=new Vector3(9.5f,cacheTransform.position.y,cacheTransform.position.z);
            else if (cacheTransform.position.x <-9.5f)
                cacheTransform.position = new Vector3(-9.5f, cacheTransform.position.y, cacheTransform.position.z);
        }

    }
}
