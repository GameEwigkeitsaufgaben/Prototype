using UnityEngine;
using UnityEngine.EventSystems;

public class DragTurmItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;

    public Vector3 origPos;
    [SerializeField] private  AudioSource audioSrcDragDrop;

    public GameObject mySnapObj;
    public bool snaped = false;
    public bool dragging = false;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        sfx = runtimeDataChapters.LoadSfx();
    }

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
        if (snaped) return;

        audioSrcDragDrop.clip = sfx.pinDrag;
        audioSrcDragDrop.Play();
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
        if(collision.name == mySnapObj.name)
        {
            if (snaped) return;

            gameObject.transform.SetParent(mySnapObj.transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.parent.GetComponent<RectTransform>().sizeDelta = gameObject.GetComponent<RectTransform>().sizeDelta;
            snaped = true;

            audioSrcDragDrop.clip = sfx.pinDrop;
            audioSrcDragDrop.Play();
        }
    }
}
