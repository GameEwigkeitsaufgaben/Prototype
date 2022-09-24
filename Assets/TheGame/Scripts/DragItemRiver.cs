using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BehaviourButton))]
public class DragItemRiver : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private SoChaptersRuntimeData runtimeDataChapter;
    private SoSfx sfx;
    public GameObject dropTarget;
    public AudioSource dragSfx;
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;
    public Vector3 origPos;

    public GameObject mySnapObj;
    public bool snaped = false;
    public bool dragging = false;

    void Start()
    {
        myDragRectTransform = GetComponent<RectTransform>();
        runtimeDataChapter = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        sfx = runtimeDataChapter.LoadSfx();

        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();
        origPos = gameObject.transform.position;

        dragSfx.clip = sfx.dropSfx;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (snaped) return;

        dragSfx.clip = sfx.mechanicBtnPress;
        dragSfx.Play();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (snaped) return;

        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
        dragging = true;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        if (snaped) return;

        gameObject.transform.position = origPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == mySnapObj.name)
        {
            if (snaped) return;
            
            dragSfx.clip = sfx.dropSfx;
            dragSfx.Play();
            
            gameObject.transform.SetParent(mySnapObj.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
            snaped = true;
        }
    }
}
