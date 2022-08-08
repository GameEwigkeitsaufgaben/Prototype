using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BehaviourButton))]
public class DragItemRiver : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameObject dropTarget;
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;
    public Vector3 origPos;

    public GameObject mySnapObj;
    public bool snaped = false;
    public bool dragging = false;

    // Start is called before the first frame update
    void Start()
    {
        myDragRectTransform = GetComponent<RectTransform>();

        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();
        origPos = gameObject.transform.position;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (snaped) return;

        Debug.Log("Drag");
        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
        dragging = true;
    }

    private void OnMouseUp()
    {
        if (snaped) return;

        gameObject.transform.position = gameObject.GetComponent<DragTurmItem>().origPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        dragging = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == mySnapObj.name)
        {
            Debug.Log("SNAP");
            if (snaped) return;

            gameObject.transform.SetParent(mySnapObj.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
            snaped = true;
        }
        Debug.Log("Trigger " + collision.name);
    }
}