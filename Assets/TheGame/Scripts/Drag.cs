using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class Drag : MonoBehaviour, IDragHandler
{
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;

    void Start()
    {
        myDragRectTransform = this.GetComponent<RectTransform>();
        myParentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor;
    }

}
