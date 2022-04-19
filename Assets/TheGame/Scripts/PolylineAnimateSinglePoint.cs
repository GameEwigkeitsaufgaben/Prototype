using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shapes;

public class PolylineAnimateSinglePoint : MonoBehaviour
{
    public int index;
    public Polyline polyline;
    private RectTransform trans;

    private void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    private void OnValidate()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = new Vector2(trans.localPosition.x, trans.localPosition.y);
        polyline.SetPointPosition(index, pos);
    }
}
