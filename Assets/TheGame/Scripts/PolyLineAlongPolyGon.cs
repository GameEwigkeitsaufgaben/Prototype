using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PolyLineAlongPolyGon : MonoBehaviour
{
    public Polyline line;
    public Polygon poly;
    public int[] ids;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ids.Length; i++)
        {
            Vector3 pos = new Vector3(poly.points[ids[i]].x, poly.points[ids[i]].y,0);
            line.AddPoint(pos);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < ids.Length; i++)
        {
            Vector3 pos = new Vector3(poly.points[ids[i]].x, poly.points[ids[i]].y, 0);
            line.SetPointPosition(i,pos);
        }
    }
}
