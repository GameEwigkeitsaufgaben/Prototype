using UnityEngine;
using Shapes;

public class PolylineAnimate : MonoBehaviour
{
    public Polyline start;
    public Polyline move;
    public Polyline end;
    [Range(0,1)]
    public float t;

    private void Start()
    {
       // Debug.Log("PolylineAnimatePoint ist auf : " + gameObject.name);
    }

    private void OnValidate()
    {
        for (int i = 0; i < start.points.Count; i++)
        {
            Vector3 pos = Vector3.Lerp(start.points[i].point, end.points[i].point, t);
            move.SetPointPosition(i, pos);
        }
    }



    private void Update()
    {
        for (int i = 0; i < start.points.Count; i++)
        {
            Vector3 posNew = Vector3.Lerp(start.points[i].point, end.points[i].point, t);
            move.SetPointPosition(i, posNew);
        }
    }
}
