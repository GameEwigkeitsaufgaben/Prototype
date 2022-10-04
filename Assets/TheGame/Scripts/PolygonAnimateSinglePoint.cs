using UnityEngine;
using Shapes;

public class PolygonAnimateSinglePoint : MonoBehaviour
{
    public int index;
    public Polygon polygon;
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
        polygon.SetPointPosition(index, pos);
    }
}
