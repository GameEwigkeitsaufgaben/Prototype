using UnityEngine;
using UnityEngine.EventSystems;

public class Lupe : MonoBehaviour, IDragHandler
{
    private const string h2o = "TriggerH2OinWasser";
    private const string o2 = "TriggerO2inStollen";
    private const string h2so4 = "TriggerH2So4";
    public GameObject LupeCirc, LupeHandle;
    public Canvas myParentCanvas;
    
    int origSize;
    float scaleFactor = 4f;
    Vector3 origPosLupeCirc, origPosLupeHandle;
    RectTransform myDragRectTransform;
    SoChapTwoRuntimeData runtimeDataCh02;

    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
    }

    private void Start()
    {
        origSize = 100;
        origPosLupeCirc = LupeCirc.GetComponent<RectTransform>().localPosition;
        origPosLupeHandle = LupeHandle.GetComponent<RectTransform>().localPosition;
        myDragRectTransform = gameObject.GetComponent<RectTransform>();
    }


    public void EnlargeLupe(float factor)
    {
        float newSize = origSize * factor;
        LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, newSize);
        LupeHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, newSize);
        LupeHandle.GetComponent<RectTransform>().localPosition = new Vector3(
            LupeCirc.GetComponent<RectTransform>().localPosition.x + (newSize*0.68f),
            LupeCirc.GetComponent<RectTransform>().localPosition.y - (newSize*0.68f), 
            0f);
    }

    public void ShrinkToOrigLupe()
    {
        float newSize = origSize;
        LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().localPosition = origPosLupeHandle;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == h2o)
        {
            EnlargeLupe(4f);
            runtimeDataCh02.h2oFound = true;
        }
        else if(collision.name == o2)
        {
            EnlargeLupe(4f);
            runtimeDataCh02.o2Found = true;
        }
        else if (collision.name == h2so4)
        {
            EnlargeLupe(4f);
            runtimeDataCh02.h2So4Found = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == h2o)
        {
            ShrinkToOrigLupe();
        }
        else if (collision.name == o2)
        {
            ShrinkToOrigLupe();
        }
        else if (collision.name == h2so4)
        {
            ShrinkToOrigLupe();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
    }
}
