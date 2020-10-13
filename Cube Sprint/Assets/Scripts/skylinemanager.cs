using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class skylinemanager : MonoBehaviour
{
    public Transform Prefab;
    public Vector3 startPosition;
    public int no_of_objects;
    public float recycleoffset;
    public Vector3 Min_Scale;
    public Vector3 Max_Scale;

    private Vector3 nextPosition;
    private Queue<Transform> cube_transforms;

    private void Start()
    {
        cube_transforms = new Queue<Transform>();
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;

        for(int i=0;i<no_of_objects;i++)
        {
            cube_transforms.Enqueue((Transform)Instantiate(Prefab.transform,new Vector3(0f,0f,100f),Quaternion.identity));
        }

        enabled = false;
    }

    private void GameStart()
    {

        nextPosition = startPosition;

        for (int i = 0; i < no_of_objects; i++)
        {
            Recycle();
        }

        enabled = true;
    }

    private void GameOver()
    {
        enabled = false;
    }

    private void Recycle()
    {
        Vector3 Scale = new Vector3(Random.Range(Min_Scale.x, Max_Scale.x), Random.Range(Min_Scale.y, Max_Scale.y), Random.Range(Min_Scale.z, Max_Scale.z));
        Vector3 position = nextPosition;
        position.x += Scale.x*0.5f;
        position.y += Scale.y*0.5f;

        Transform obj=cube_transforms.Dequeue();
        obj.localScale = Scale;
        obj.localPosition = position;
        nextPosition.x += Scale.x;
        cube_transforms.Enqueue(obj);
    }

    private void Update()
    {
        if (cube_transforms.Peek().localPosition.x + recycleoffset < Runner.distancetravelled)
        {
            Recycle();
        }
    }
}
