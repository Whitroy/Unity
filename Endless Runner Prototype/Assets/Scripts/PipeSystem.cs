using System;
using UnityEngine;

public class PipeSystem : MonoBehaviour
{
    public Pipe PipePrefab;
    public int PipeCount;
    public int emptyPipeCount;

    private Pipe[] pipes;

    private void Awake()
    {
        pipes = new Pipe[PipeCount];

        for(int i=0;i<PipeCount;i++)
        {
            Pipe pipe = pipes[i] = Instantiate<Pipe>(PipePrefab);
            pipe.transform.SetParent(transform, false);
        }

    }

    public Pipe SetupFirstPipe()
    {
        for(int i=0;i<pipes.Length;i++)
        {
            Pipe pipe = pipes[i];
            pipe.Generate(i > emptyPipeCount);
            if(i>0)
            {
                pipe.AlignWith(pipes[i - 1]);
            }
        }
        AlignNxtPipwWithOrigin();
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

    public Pipe SetUpNextPipe()
    {
        shiftpipes();
        AlignNxtPipwWithOrigin();
        pipes[pipes.Length - 1].Generate();
        pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
        transform.localPosition = new Vector3(0f, -pipes[1].CurveRadius);
        return pipes[1];
    }

    private void AlignNxtPipwWithOrigin()
    {
        Transform AlignToOrigin = pipes[1].transform;
        for (int i=0;i<pipes.Length;i++)
        {
            if(i!=1)
                pipes[i].transform.SetParent(AlignToOrigin);
        }

        AlignToOrigin.localPosition = Vector3.zero;
        AlignToOrigin.localRotation = Quaternion.identity;

        for (int i = 0; i < pipes.Length; i++)
        {
            if(i!=1)
                pipes[i].transform.SetParent(transform);
        }
    }

    private void shiftpipes()
    {
        Pipe temp = pipes[0];
        for (int i = 1; i < pipes.Length; i++)
        {
            pipes[i - 1] = pipes[i];
        }
        pipes[pipes.Length - 1] = temp;
    }
}
