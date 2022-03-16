using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuseumMinerEquipmentItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public MinerEquipmentItem equipmentItem; //Enum
    public SnapetTo snapedTo; //Enum
    public SnapetTo previous;
    private GameObject miner = null; //set in onTrigger
    
    private GameObject itemOnTable; //this object where this script is attached to
    public Vector3 origPosTable;
    public GameObject correspondingItemOnMiner;
    
    public string desc;
    
    public bool solutionItemRound1 = false;
    public bool solutionItemRound2 = false;
    public bool solutionItemRound3 = false;
    
    private Canvas myCanvas;
    private RectTransform dragRectTransform;

    private bool snapToggled = false;
    private Texture2D lampTexture;
    private SoMinerEquipment myConifg;

    //private GameObject handschuhLeft = null, handschuhRight = null;
    private ManagerMuseumMinerEquipment manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ManagerMuseumMinerEquipment>();
        itemOnTable = gameObject;
        origPosTable = itemOnTable.transform.position;
        lampTexture = GetComponent<Image>().sprite.texture;
        myConifg = Resources.Load<SoMinerEquipment>("ConfigMinerEquipment");

        //origTransformTable = Instantiate(itemOnTable.transform, itemOnTable.transform.position, itemOnTable.transform.rotation);

        //Get the parent Canvas Obj
        GameObject canvasItem = gameObject;
        while (canvasItem.GetComponent<Canvas>() == null)
        {
            Debug.Log("citem " + canvasItem.name);
            canvasItem = canvasItem.transform.parent.gameObject;
        }

        myCanvas = canvasItem.GetComponent<Canvas>();
        dragRectTransform = GetComponent<RectTransform>();
        previous = snapedTo;
    }
    public void SetMiner(GameObject miner)
    {
        miner = miner;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        Debug.Log("Event data  +++++++++++++++++++++ " + eventData.pointerEnter.name + " " + equipmentItem);

        snapToggled = (previous != snapedTo) ? true : false;

        //Special: 2 different sprites for table and miner
        switch (equipmentItem)
        {
            case MinerEquipmentItem.Lampe:
                if (snapedTo == SnapetTo.Miner) GetComponent<Image>().sprite = myConifg.lampMiner;
                else GetComponent<Image>().sprite = myConifg.lampTable;
                break;
            case MinerEquipmentItem.Stechkarte:
                if (snapedTo == SnapetTo.Miner) GetComponent<Image>().sprite = myConifg.cardMiner;
                else GetComponent<Image>().sprite = myConifg.cardTable;
                break;
        }

        //Set transform for items, with special handschuhe
        if (snapedTo == SnapetTo.Miner)
        {
            transform.position = correspondingItemOnMiner.transform.position;

            if (equipmentItem == MinerEquipmentItem.Handschuhe) gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetMinerPos();

            if (snapToggled) manager.itemsOnMiner++;
        }
        else if (snapedTo == SnapetTo.Table)
        {
            transform.position = origPosTable;

            if (equipmentItem == MinerEquipmentItem.Handschuhe) gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetTablePos();

            if (snapToggled) manager.itemsOnMiner--;
        }

        
        if (snapToggled)
        {
            previous = snapedTo;

            if(equipmentItem == MinerEquipmentItem.Handschuhe)
            {
                gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetToogleSameForBoth();
            }
        }
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        dragRectTransform.anchoredPosition += eventData.delta / myCanvas.scaleFactor; //important when using screen space
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MinerImg")
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
        else if (collision.name == "Scanarea")
        {
            Debug.Log("Set it to scanarea");
            collision.transform.parent.GetComponentInChildren<Text>().text = desc;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "MinerImg")
        {
            Debug.Log("Set it to table " + gameObject.name);
            snapedTo = SnapetTo.Table;
            if (equipmentItem == MinerEquipmentItem.Handschuhe)
            {
                gameObject.transform.parent.GetComponent<MuseumHandschuhe>().SetBothSnapedToTable();
            }
        }
        else if (collision.name == "Scanarea")
        {
            Debug.Log("Set it to scanarea");
            collision.transform.parent.GetComponentInChildren<Text>().text = "";
        }
    }
}
