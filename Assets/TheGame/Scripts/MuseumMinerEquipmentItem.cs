using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuseumMinerEquipmentItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private const string COScanarea = "Scanarea";
    private const string COMiner = "MinerImg";
    public MinerEquipmentItem equipmentItem; //Enum
    public SnapetTo snapedTo; //Enum
    public SnapetTo previous;
    private GameObject miner = null; //set in onTrigger
    
    //private GameObject itemOnTable; //this object where this script is attached to
    private Vector3 origPosOnTable;
    public GameObject correspondingItemOnMiner;
    
    public string individualDesc;
    
    public bool solutionItemRound1 = false;
    public bool solutionItemRound2 = false;
    public bool solutionItemRound3 = false;
    
    private Canvas myParentCanvas;
    private RectTransform myDragRectTransform;
    private Text uiDescItems;

    private bool positionChanged = false;
    //private Texture2D lampTexture;
    private SoMinerEquipment myConifg;

    //private GameObject handschuhLeft = null, handschuhRight = null;
    private ManagerMuseumMinerEquipment myManager;
    AudioSource myAudioSrc;

    public Text uiTextTooltip; //set for every item in managermuseumminerequipment

    string descHelmet = "Der Helm, schützt vor Kopfverletzungen durch herabfallenden Einbauten oder in Stollen mit geringer Höhe.";
    string descLamp = "Die Lampe, dient zum Ausleuchten der Stollen, um nicht mit Gegenständen oder Einbauten zusammenzustoßen.";
    string descMask = "Die Atemschutzmaske, verhindert, dass krankmachender Kohlenstaub in großen Mengen eingeatmet wird.";
    string descFootware = "Die Sicherheitsschuhe, verhindern Fußverletzungen durch herabfallende Gegenstände.";
    string descCard = "Die Stechkarte, sorgt dafür, dass man weiß, wer im Bergwerk unterwegs ist und die Grubenwehr helfen kann, wenn jemand vermisst wird.";
    string descGlasses = "Die Schutzbrille, schützt die Augen vor allem vor Staub, aber auch vor Augenverletzungen durch Gegenstände im Stollen.";
    string descShinGuard = "Die Schienbeinschützer, schützen vor allem in Engstellen und niederen Stollenstrecken, die man nicht aufrecht durchlaufen kann.";
    string descSuit = "Der weiße Bergmannsanzug, aus Baumwolle kann nicht elektrostatisch aufgeladen werden und ist bequem.";
    string descScarf = "Das Halstuch kann zur Not als Mundschutz dienen und ist angenehm zu tragen.";
    string descGloves = "Die Handschuhe schützen die Hände bei schweren Arbeiten vor Blasen und Schwielen.";

void Start()
    {
        myManager = FindObjectOfType<ManagerMuseumMinerEquipment>();
        origPosOnTable = gameObject.transform.position; //every instance of class is reference type and any instance of structure is value type.
        
        myConifg = Resources.Load<SoMinerEquipment>(GameData.ConfigMinerEquiment);
        myDragRectTransform = GetComponent<RectTransform>();


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
                individualDesc = "Kein Beschreibung vorhanden";
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
    }

    public void ResetToMiner()
    {
        snapedTo = previous = SnapetTo.Miner;
        ChooseSprite();
        transform.position = correspondingItemOnMiner.transform.position;
    }

    public bool GetHasPositionChanged()
    {
        return (previous != snapedTo) ? true : false; //Check if there is any change in positioning of the item. 
    }

    public void SetMiner(GameObject miner)
    {
        miner = miner;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        myAudioSrc.clip = myConifg.beginDrag;
        myAudioSrc.Play();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        Debug.Log("Event data  +++++++++++++++++++++ " + eventData.pointerEnter.name + " " + equipmentItem);

        positionChanged = GetHasPositionChanged();
        myAudioSrc.clip = myConifg.endDrag;
        myAudioSrc.Play();

        //Special: 2 different sprites for table and miner
        ChooseSprite();

        //Set transform for items, with special handschuhe
        if (snapedTo == SnapetTo.Miner)
        {
            transform.position = correspondingItemOnMiner.transform.position;

            if (positionChanged) myManager.itemsOnMiner++;

            //if item is one handschuh, also make changes for the other handschuh
            if (equipmentItem == MinerEquipmentItem.Handschuhe) gameObject.transform.parent.GetComponent<MuseumHandschuhe>().ResetBothToMiner();


        }
        else if (snapedTo == SnapetTo.Table)
        {
            transform.position = origPosOnTable;

            if (positionChanged) myManager.itemsOnMiner--;

            if (equipmentItem == MinerEquipmentItem.Handschuhe) gameObject.transform.parent.GetComponent<MuseumHandschuhe>().ResetBothToTable();
        }


        if (positionChanged)
        {
            previous = snapedTo;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == COMiner)
        {
            Debug.Log("Set it to miner " + gameObject.name);
            snapedTo = SnapetTo.Miner;
            
            if (equipmentItem == MinerEquipmentItem.Handschuhe)
            {
                gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetBothSnapedToMiner();
            }

            if (miner == null)
            {
                miner = collision.gameObject;
            }
        }
        else if (collision.name == COScanarea)
        {
            if (uiDescItems == null) uiDescItems = collision.transform.parent.transform.parent.GetComponentInChildren<Text>();

            Debug.Log("Set it to scanarea");
            uiDescItems.text = individualDesc;
        }
    }

    public void ShowTooltip()
    {
       uiTextTooltip.text = individualDesc;
    }

    public void HideTooltip()
    {
        uiTextTooltip.text = "";
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == COMiner)
        {
            Debug.Log("Set it to table " + gameObject.name);
            snapedTo = SnapetTo.Table;
            if (equipmentItem == MinerEquipmentItem.Handschuhe)
            {
                gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetBothSnapedToTable();
            }
        }
        else if (collision.name == COScanarea)
        {
            Debug.Log("Set it to scanarea ");
            uiDescItems.text = "";
        }
    }
}
