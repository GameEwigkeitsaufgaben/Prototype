using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MuseumMinerEquipmentItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private const string COMiner = "MinerImg";
    private const string plainTextEmptyString = "";
    private const string plainTextNoDescription = "Keine Beschreibung vorhanden";
    private GameObject miner = null; //set in onTrigger
    private Canvas myParentCanvas;
    private RectTransform myDragRectTransform;
    
    //private Text uiDescItems;

    private bool positionChanged = false;
    private SoMinerEquipment myConifg;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;


    private AudioSource myAudioSrc;

    //private GameObject itemOnTable; //this object where this script is attached to
    private Vector3 origPosOnTable;
    
    public GameObject correspondingItemOnMiner;
    
    public string individualDesc;
    public bool solutionItemRound1 = false;
    public bool solutionItemRound2 = false;
    public bool solutionItemRound3 = false;
    public GameObject dragObjParent, dragObjDefaultParent, orderTopParent, parentTable;
    public ManagerMuseumMinerEquipment myManager;
    public bool isDragableInRound;
    public bool isCurrentlyDragging;
    public MinerEquipmentItem equipmentItem; //Enum
    public SnapetTo snapedTo; //Enum
    public SnapetTo previous;
    public GameObject particles;

    public TMP_Text uiTextTooltip; //set for every item in managermuseumminerequipment

    string descHelmet = "Der Helm schützt vor Kopfverletzungen durch herabfallenden Einbauten oder in Stollen mit geringer Höhe.";
    string descLamp = "Das Geleucht dient zum Ausleuchten der Stollen, um nicht mit Gegenständen oder Einbauten zusammenzustoßen.";
    string descMask = "Die Atemschutzmaske verhindert, dass krankmachender Kohlenstaub in großen Mengen eingeatmet wird.";
    string descFootware = "Die Sicherheitsschuhe verhindern Fußverletzungen durch herabfallende Gegenstände.";
    string descCard = "Die Stechkarte sorgt dafür, dass man weiß, wer im Bergwerk unterwegs ist und die Grubenwehr helfen kann, wenn jemand vermisst wird.";
    string descGlasses = "Die Schutzbrille schützt die Augen vor allem vor Staub, aber auch vor Augenverletzungen durch Gegenstände im Stollen.";
    string descShinGuard = "Die Schienbeinschoner schützen vor allem in Engstellen und niederen Stollenstrecken, die man nicht aufrecht durchlaufen kann.";
    string descSuit = "Der weiße Bergmannsanzug, aus Baumwolle, kann nicht elektrostatisch aufgeladen werden und ist bequem.";
    string descScarf = "Das Halstuch kann zur Not als Mundschutz dienen und ist angenehm zu tragen.";
    string descGloves = "Die Handschuhe schützen die Hände bei schweren Arbeiten vor Blasen und Schwielen.";

    private MuseumHandschuhe handschuhe;

    private void Awake()
    {
        myManager = FindObjectOfType<ManagerMuseumMinerEquipment>();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        sfx = runtimeDataChapters.LoadSfx();
    }

    private void Start()
    {
        
        origPosOnTable = gameObject.transform.position; //every instance of class is reference type and any instance of structure is value type.
        isDragableInRound = true;
        
        myConifg = Resources.Load<SoMinerEquipment>(GameData.NameConfigMinerEquiment);
        myDragRectTransform = GetComponent<RectTransform>();

        dragObjDefaultParent = gameObject.transform.parent.transform.gameObject;

        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();
        
        previous = snapedTo;
        SetupDescription();
        ChooseSprite();
        myAudioSrc = gameObject.AddComponent<AudioSource>();
        myAudioSrc.volume = 0.4f;

        if(MinerEquipmentItem.Handschuhe == equipmentItem)
        {
            handschuhe = gameObject.transform.parent.GetComponent<MuseumHandschuhe>();
        }
    }

    public void EnableParticles(bool enable)
    {
        particles.gameObject.SetActive(enable);
    }

    private void SetupDescription()
    {

        switch (equipmentItem)
        {
            case MinerEquipmentItem.Anzug:
                individualDesc = descSuit;
                break;
            case MinerEquipmentItem.Atemmaske:
                individualDesc = descMask;
                break;
            case MinerEquipmentItem.Halstuch:
                individualDesc = descScarf;
                break;
            case MinerEquipmentItem.Handschuhe:
                individualDesc = descGloves;
                break;
            case MinerEquipmentItem.Helm:
                individualDesc = descHelmet;
                break;
            case MinerEquipmentItem.Lampe:
                individualDesc = descLamp;
                break;
            case MinerEquipmentItem.Schienbeinschuetzer:
                individualDesc = descShinGuard;
                break;
            case MinerEquipmentItem.Schutzbrille:
                individualDesc = descGlasses;
                break;
            case MinerEquipmentItem.Sicherheitsschuhe:
                individualDesc = descFootware;
                break;
            case MinerEquipmentItem.Stechkarte:
                individualDesc = descCard;
                break;
            default:
                individualDesc = plainTextNoDescription;
                break;
        }
    }

    public void ChooseSprite()
    {
        //https://stackoverflow.com/questions/44471568/how-to-calculate-sizedelta-in-recttransform
        switch (equipmentItem)
        {
            case MinerEquipmentItem.Lampe:
                if (snapedTo == SnapetTo.Miner)
                {
                    GetComponent<Image>().sprite = myConifg.lampMiner;
                    GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 219.9f); //needed for drag and drop to opitmize drag area
                }
                else
                {
                    GetComponent<Image>().sprite = myConifg.lampTable;
                    GetComponent<RectTransform>().sizeDelta = new Vector2(190f, 65f); //needed for drag and drop to opitmize drag area
                }
                break;
            case MinerEquipmentItem.Stechkarte:
                if (snapedTo == SnapetTo.Miner)
                {
                    GetComponent<Image>().sprite = myConifg.cardMiner;
                    GetComponent<RectTransform>().sizeDelta = new Vector2(158.71f, 125.2f); //needed for drag and drop to opitmize drag area
                }
                else
                {
                    GetComponent<Image>().sprite = myConifg.cardTable;
                    GetComponent<RectTransform>().sizeDelta = new Vector2(158.71f, 71f); //needed for drag and drop to opitmize drag area
                }
                break;
        }
    }

    public void ResetToTable()
    {
        snapedTo = previous = SnapetTo.Table;
        ChooseSprite();
        transform.position = origPosOnTable;
        gameObject.transform.SetParent(parentTable.transform);

    }

    public void ResetToMiner()
    {
        snapedTo = previous = SnapetTo.Miner;
        ChooseSprite();
        transform.position = correspondingItemOnMiner.transform.position;
    }

    public bool GetHasLocationChanged()
    {
        return (previous != snapedTo) ? true : false; //Check if there is any change in positioning of the item. 
    }

    public void SetMiner(GameObject miner)
    {
        this.miner = miner;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isDragableInRound) return;

        if (myManager.IsMaxItemsOnMinerReached() && snapedTo == SnapetTo.Table) return;

        isCurrentlyDragging = true;
        
        //gameObject.transform.parent = dragObjParent.transform;
        gameObject.transform.SetParent(dragObjParent.transform);
        myAudioSrc.clip = sfx.dragSfx;
        myAudioSrc.Play();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragableInRound) return;
        if (myManager.IsMaxItemsOnMinerReached() && snapedTo == SnapetTo.Table) return;

        isCurrentlyDragging = false;
        //gameObject.transform.parent = dragObjDefaultParent.transform;
        gameObject.transform.SetParent(dragObjParent.transform);

        positionChanged = GetHasLocationChanged();
        myAudioSrc.clip = sfx.dropSfx;
        myAudioSrc.Play();

        //Special: 2 different sprites for table and miner
        ChooseSprite();

        //Set transform for items, with special handschuhe
        if (snapedTo == SnapetTo.Miner)
        {
            transform.position = correspondingItemOnMiner.transform.position;

            //if item is one handschuh, also make changes for the other handschuh
            if (equipmentItem == MinerEquipmentItem.Handschuhe) handschuhe.ResetBothToMiner();
            
        }
        else if (snapedTo == SnapetTo.Table)
        {
            transform.position = origPosOnTable;

            if (equipmentItem == MinerEquipmentItem.Handschuhe) handschuhe.ResetBothToTable();
        }
        
        if (positionChanged)
        {
            previous = snapedTo;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragableInRound) return;
        if (myManager.IsMaxItemsOnMinerReached() && snapedTo == SnapetTo.Table) return;

        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
    }

    //Snapt to Miner if collision is detected, Snapet to Table if item exits collision miner Enter2D/Exit2D
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == COMiner)
        {
            snapedTo = SnapetTo.Miner;
            if (equipmentItem != MinerEquipmentItem.Handschuhe) myManager.itemsOnMiner++;
            else
            {
                if (gameObject.name.Contains("Links"))
                {
                    myManager.itemsOnMiner++;
                }
            }

            if (equipmentItem == MinerEquipmentItem.Handschuhe)
            {
                if(gameObject.transform.parent.GetComponent<MuseumHandschuhe>() != null)
                    gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetBothSnapedToMiner();
            }

            if (miner == null)
            {
                miner = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == COMiner)
        {
            snapedTo = SnapetTo.Table;
            if(equipmentItem != MinerEquipmentItem.Handschuhe) myManager.itemsOnMiner--;
            else
            {
                if (gameObject.name.Contains("Links"))
                {
                    myManager.itemsOnMiner--;
                }
            }
            if (equipmentItem == MinerEquipmentItem.Handschuhe)
            {

                if (gameObject.transform.parent.GetComponent<MuseumHandschuhe>() != null) 
                    gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetBothSnapedToTable();
            }
        }
    }

    public void ShowTooltip()
    {
       uiTextTooltip.text = individualDesc;
    }

    public void HideTooltip()
    {
        uiTextTooltip.text = plainTextEmptyString;
    }

    private void Update()
    {
        if (myManager.currentRound == EquipmentRound.Protection)
        {
            if (solutionItemRound1)
            {
                isDragableInRound = false;
                gameObject.transform.parent = orderTopParent.transform;
            }
        }
        else if (myManager.currentRound == EquipmentRound.SpecialTask)
        {
            if (solutionItemRound2)
            {
                isDragableInRound = false;
                gameObject.transform.parent = orderTopParent.transform;
            }
        }
    }
}
