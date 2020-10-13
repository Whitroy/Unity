using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlatformManager : MonoBehaviour
{
    public Transform Prefab;
    public Vector3 StartPosition;
    public int No_of_obj;
    public float recycleoffset;
    public Vector3 Minsize;
    public Vector3 Maxsize;
    public Vector3 MinGap;
    public Vector3 MaxGap;
    public float MinY;
    public float MaxY;
    public Material[] platform_material;
    public PhysicMaterial[] platform_physicMaterial;
    public Booster BoosterPrefab;

    private Vector3 NextPosition;
    private Queue<Transform> ObjectQueue;

    private void Start()
    {
        GameEventManager.GameStart += GameStart;
        GameEventManager.GameOver += GameOver;

        ObjectQueue = new Queue<Transform>(No_of_obj);

        for (int i = 0; i < No_of_obj; i++)
        {
            ObjectQueue.Enqueue((Transform)Instantiate(Prefab, new Vector3(0f, 0f, 100f), Quaternion.identity));
        }

        enabled = true;
    }

    private void GameStart()
    {
        NextPosition = StartPosition;

        for (int i = 0; i < No_of_obj; i++)
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

        Vector3 Scale = new Vector3(Random.Range(Minsize.x, Maxsize.x), Random.Range(Minsize.y, Maxsize.y), Random.Range(Minsize.z, Maxsize.z));
        Vector3 Gap = new Vector3(Random.Range(MinGap.x, MaxGap.x), Random.Range(MinGap.y, MaxGap.y), Random.Range(MinGap.z, MaxGap.z));

        int MaterialIndex = Random.Range(0, platform_material.Length);

        Vector3 position = NextPosition;
        position.x += Scale.x * 0.5f;
        position.y += Scale.y * 0.5f;
        BoosterPrefab.SpawnIfAvailable(position);

        Transform obj = ObjectQueue.Dequeue();
        obj.localScale = Scale;
        obj.localPosition = position;

        obj.GetComponent<Renderer>().material = platform_material[MaterialIndex];
        obj.GetComponent<BoxCollider>().material = platform_physicMaterial[MaterialIndex];

        NextPosition.x+= Scale.x;
        ObjectQueue.Enqueue(obj);

        NextPosition += Gap;

        if (NextPosition.y > MaxY)
            NextPosition.y = MaxY - MaxGap.x;
        else if (NextPosition.y < MinY)
            NextPosition.y = MaxGap.y + MinY;

    }
    private void Update()
    {
      
        if (ObjectQueue.Peek().localPosition.x+recycleoffset<Runner.distancetravelled)
        {
            Recycle();
        }
    }
}
