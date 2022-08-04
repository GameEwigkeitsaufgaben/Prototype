using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TurmDragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;

    public GameObject mySnapObj;
    public bool snaped = false;

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == mySnapObj.name)
        {
            Debug.Log("SNAP");
            if (snaped) return;

            gameObject.transform.SetParent(mySnapObj.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
            snaped = true;
        }
        Debug.Log("Trigger "+collision.name );
    }
}
