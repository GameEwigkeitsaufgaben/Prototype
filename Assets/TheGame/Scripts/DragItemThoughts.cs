using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public enum GWChanceType
{
    chance,
    nochance,
    neitherNor
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BehaviourButton))]
public class DragItemThoughts : MonoBehaviour, IEndDragHandler, IDragHandler
{
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;
    private ManagerGWChancen manager;
    private string myTag;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;

    public Vector3 origPos;
    public TMP_Text buzzword;
    public Image bubble, rahmen;
    public Button btnRahmen;

    public GameObject mySnapObj;
    public bool snaped = false;
    public bool dragging = false;
    public bool dragable = true;

    public GWChanceType type;

    private MouseChange mouse;
    [SerializeField] private bool isMouseUp = false;
    [SerializeField] private bool rightCollision = false;
    [SerializeField] private Collider2D enterCollision;
    [SerializeField] private AudioSource audioSrcDragDrop;

    public GameObject dropTargetChance, dropTargetNoChance, dropTargetNeitherNor;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        sfx = runtimeDataChapters.LoadSfx();
    }

    void Start()
    {
        myDragRectTransform = GetComponent<RectTransform>();
        mouse = GetComponent<MouseChange>();
        manager = FindObjectOfType<ManagerGWChancen>();
        btnRahmen = rahmen.gameObject.GetComponent<Button>();
        rahmen.enabled = false;
        bubble.enabled = true;
        btnRahmen.enabled = false;
        buzzword = GetComponentInChildren<TMP_Text>();
        buzzword.gameObject.SetActive(true);

        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();
        //origPos = gameObject.transform.position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.tag = "DragItem";
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (snaped) return;
        if (!dragable) return;

        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;

        if (!rightCollision) return;

        switch (type)
        {
            case GWChanceType.chance:
                ChangeSceneBehaviour(dropTargetChance);
                PlayChance();
                break;
            case GWChanceType.nochance:
                PlayNoChance();
                ChangeSceneBehaviour(dropTargetNoChance);
                break;
            case GWChanceType.neitherNor:
                ChangeSceneBehaviour(dropTargetNeitherNor);
                PlayNeitherNor();
                break;
        }

        audioSrcDragDrop.clip = sfx.dropSfx;
        audioSrcDragDrop.Play();

    }

    private void OnMouseDown()
    {
        if (mouse == null) return;
        mouse.MouseDown();
    }

    private void OnMouseUp()
    {
        if (mouse == null) return;
        mouse.MouseUp();
    }

    public void ReplayTalkingList()
    {
        //ChangeSceneBehaviour(null);
        manager.animator.enabled = false;
        manager.MirrorBergbauvertreter(true);
        manager.PauseDragAllDragItems(true);

        PlayChance();
        PlayNoChance();
        PlayNeitherNor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        enterCollision = collision;
        if (collision.name == "DropTargetImgChance" && type == GWChanceType.chance) rightCollision = true;
        else if (collision.name == "DropTargetImgNoChance" && type == GWChanceType.nochance) rightCollision = true;
        else if (collision.name == "DropTargetImgNeitherNor" && type == GWChanceType.neitherNor) rightCollision = true;
        else rightCollision = false;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (enterCollision == null) return;

        if (collision.name != enterCollision.name) return;
        
        rightCollision = false;
        enterCollision = null;
    }

    private void PlayNeitherNor()
    {
        if (gameObject.name == "DragSourceSauberesGrubenwasser")
        {
            manager.speechManager.playSauberesGW = true;
            manager.currentTL = GameData.NameCH3TLSauberesGW;
        }
    }

    private void PlayNoChance()
    {
        if (gameObject.name == "DragSourcePumpspeicher")
        {
            manager.speechManager.playPumpspeicherkraftwerke = true;
            manager.currentTL = GameData.NameCH3TLPumpspeicherkraftwerke;
        }
        else if (gameObject.name == "DragSourceLagerstaette")
        {
            manager.speechManager.playLagerstaette = true;
            manager.currentTL = GameData.NameCH3TLLagerstaette;
        }
    }

    private void PlayChance()
    {
        if (gameObject.name == "DragSourceGeothermie")
        {
            manager.speechManager.playGeothermie = true;
            manager.currentTL = GameData.NameCH3TLGeothermie;
        }
        else if (gameObject.name == "DragSourceRohstoffquelle")
        {
            manager.speechManager.playRohstoffquelle = true;
            manager.currentTL = GameData.NameCH3TLRohstoffquelle;
        }
        else if (gameObject.name == "DragSourceEntlastungFluss")
        {
            manager.speechManager.playEntlastungFluesse = true;
            manager.currentTL = GameData.NameCH3TLEntlastungFluesse;
        }
        else if (gameObject.name == "DragSourceWenigGrubenwasser")
        {
            manager.speechManager.playWenigerGW = true;
            manager.currentTL = GameData.NameCH3TLWenigerGW;
        }
    }

    private void ChangeSceneBehaviour(GameObject dropTarget)
    {
        gameObject.transform.SetParent(dropTarget.transform);
        snaped = true;
        gameObject.GetComponent<Image>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<BehaviourButton>().enabled = false;

        btnRahmen.enabled = true;
        rahmen.enabled = true;
        bubble.enabled = false;
        gameObject.tag = "Untagged";
        manager.animator.enabled = false;
        manager.MirrorBergbauvertreter(true);
        manager.PauseDragAllDragItems(true);
    }

    private void Update()
    {
        if (snaped) return;
        if (dragging) return;

        if (rightCollision) return;

        if (Vector3.Distance(gameObject.transform.localPosition, origPos) < 0.1f) return;
        
        gameObject.transform.localPosition = origPos;
        Debug.Log("SET BACK");
       
    }
}
