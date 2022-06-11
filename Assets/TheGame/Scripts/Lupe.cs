using UnityEngine;
using UnityEngine.EventSystems;

public class Lupe : MonoBehaviour, IDragHandler
{
    private const string fes2 = "TriggerFeS2Floez";
    private const string h2o = "TriggerH2OinWasser";
    private const string o2 = "TriggerO2inStollen";
    private const string so4 = "TriggerSO4";
    private const string h = "TriggerH";
    private const string h2so4 = "TriggerH2So4";
    private const string fe = "TriggerFe";

    public GameObject LupeCirc, LupeHandle;
    public Canvas myParentCanvas;
    public GameObject h2oAnimObj, fes2AnimObj;
    
    int origSize;
    float scaleFactor = 4f;
    Vector3 origPosLupeCirc, origPosLupeHandle;
    RectTransform myDragRectTransform;
    SoChapTwoRuntimeData runtimeDataCh02;
    ManagerSauresWasser manager;
    public Animator animator;


    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        manager = FindObjectOfType<ManagerSauresWasser>();
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
        //LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, newSize);
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == h2o)
        {
            EnlargeLupe(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Wasser);
            manager.SetMolekuelFound(SauresWasserTrigger.Wasser);
            animator.SetTrigger("rain");
            runtimeDataCh02.h2oFound = true;
        }
        else if (collision.name == fe)
        {
            EnlargeLupe(4f);
            // manager.SetTextToTrigger(SauresWasserTrigger.Austritt);
            runtimeDataCh02.feFound = true;
        }
        else if (collision.name == fes2)
        {
            EnlargeLupe(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
            manager.SetMolekuelFound(SauresWasserTrigger.Pyrit);
            runtimeDataCh02.fes2Found = true;
            fes2AnimObj.SetActive(true);
            animator.SetTrigger("oxi1");
        }
        else if(collision.name == o2)
        {
            EnlargeLupe(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Schacht);
            runtimeDataCh02.o2Found = true;
        }
        else if (collision.name == so4)
        {
            EnlargeLupe(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
            runtimeDataCh02.so4Found = true;
        }
        else if (collision.name == h)
        {
            EnlargeLupe(4f);
            runtimeDataCh02.hFound = true;
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
        }
        else if (collision.name == h2so4)
        {
            EnlargeLupe(4f);
            runtimeDataCh02.h2So4Found = true;
            manager.SetTextToTrigger(SauresWasserTrigger.Austritt);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == h2o)
        {
            ShrinkToOrigLupe();
            animator.SetTrigger("idle");
        }
        else if (collision.name == o2)
        {
            ShrinkToOrigLupe();
        }
        else if (collision.name == fes2)
        {
            ShrinkToOrigLupe();
            fes2AnimObj.SetActive(false);
            animator.SetTrigger("idle");
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
