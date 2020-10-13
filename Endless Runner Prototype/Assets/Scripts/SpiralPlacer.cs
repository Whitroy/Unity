using UnityEngine;

public class SpiralPlacer : PipeItemGenerator
{
    public PipeItem[] ItemPrefabs;
    public override void GenerateItems(Pipe pipe)
    {
        float angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        float start = (Random.Range(0, pipe.torusSegmentCount) + 0.5f);
        float direction = Random.value < 0.5f ? 1f : -1f;
        for (int i= 0; i < pipe.CurveSegmentCount; i++)
        {
            PipeItem item = Instantiate<PipeItem>(ItemPrefabs[Random.Range(0, ItemPrefabs.Length)]);
            float pipeRotation = (start +i*direction) * 360f / pipe.torusSegmentCount;
            item.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}
