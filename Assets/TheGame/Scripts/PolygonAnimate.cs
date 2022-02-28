using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PolygonAnimate : MonoBehaviour
{
    public Polygon start;
    public Polygon move;
    public Polygon end;

    public float t;

    private void OnValidate()
    {
        for (int i = 0; i < start.points.Count; i++)
        {
            Vector2 pos = Vector2.Lerp(start.points[i], end.points[i], t);
            move.SetPointPosition(i, pos);
        }
    }

    private void Update()
    {
        for (int i = 0; i < start.points.Count; i++)
        {
            Vector2 pos = Vector2.Lerp(start.points[i], end.points[i], t);
            move.SetPointPosition(i, pos);
        }
    }
}
