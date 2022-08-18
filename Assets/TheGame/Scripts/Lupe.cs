using UnityEngine;
using UnityEngine.UI;
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
    public Image lupeBg;
    public Canvas myParentCanvas;
    //public GameObject h2oAnimObj, fes2AnimObj;
    
    int origSize;
    float scaleFactor = 4f;
    Vector3 origPosLupeCirc, origPosLupeHandle;
    RectTransform myDragRectTransform;
    SoChapTwoRuntimeData runtimeDataCh02;
    ManagerSauresWasser manager;
    public Animator animator;

    private SoConfigChapter2 configCh2;

    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        manager = FindObjectOfType<ManagerSauresWasser>();
        configCh2 = Resources.Load<SoConfigChapter2>(GameData.NameConfigCh2);
        
    }

    private void Start()
    {
        origSize = 100;
        origPosLupeCirc = LupeCirc.GetComponent<RectTransform>().localPosition;
        origPosLupeHandle = LupeHandle.GetComponent<RectTransform>().localPosition;
        myDragRectTransform = gameObject.GetComponent<RectTransform>();
    }


    public void EnlargeLupeHandle(float factor)
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
        //LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().localPosition = origPosLupeHandle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == h2o)
        {
            EnlargeLupeHandle(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Wasser);
            manager.SetMolekuelFound(SauresWasserTrigger.Wasser);
            lupeBg.sprite = configCh2.lupeBgH2O;
            lupeBg.color = configCh2.lupeBGH2oColor;
            animator.SetTrigger("rain");
            runtimeDataCh02.h2oFound = true;
        }
        else if (collision.name == fe)
        {
            EnlargeLupeHandle(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Austritt);
            manager.SetMolekuelFound(SauresWasserTrigger.Austritt);
            animator.SetTrigger("oxi2");
            lupeBg.sprite = configCh2.lupeBGOxi2;
            lupeBg.color = configCh2.lupeBGOxi2Color;
            runtimeDataCh02.feFound = true;
        }
        else if (collision.name == fes2)
        {
            EnlargeLupeHandle(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
            manager.SetMolekuelFound(SauresWasserTrigger.Pyrit);
            runtimeDataCh02.fes2Found = true;
            lupeBg.sprite = configCh2.lupeBGOxi1;
            lupeBg.color = configCh2.lupeBGOxi1Color;
            //fes2AnimObj.SetActive(true);
            animator.SetTrigger("oxi1");
        }
        else if(collision.name == o2)
        {
            EnlargeLupeHandle(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Schacht);
            manager.SetMolekuelFound(SauresWasserTrigger.Schacht);
            runtimeDataCh02.o2Found = true;
            lupeBg.sprite = configCh2.lupeBGO2;
            lupeBg.color = configCh2.lupeBGO2Color;
            animator.SetTrigger("o2");
        }
        else if (collision.name == so4)
        {
            EnlargeLupeHandle(4f);
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
            runtimeDataCh02.so4Found = true;
        }
        else if (collision.name == h)
        {
            EnlargeLupeHandle(4f);
            runtimeDataCh02.hFound = true;
            manager.SetTextToTrigger(SauresWasserTrigger.Pyrit);
        }
        else if (collision.name == h2so4)
        {
            EnlargeLupeHandle(4f);
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
            animator.SetTrigger("idle");
        }
        else if (collision.name == fes2)
        {
            ShrinkToOrigLupe();
            //fes2AnimObj.SetActive(false);
            animator.SetTrigger("idle");
        }
        else if (collision.name == fe)
        {
            ShrinkToOrigLupe();
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
