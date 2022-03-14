using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MuseumMinerEquipmentItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public MinerEquipmentItem equipmentItem; //Enum
    public SnapetTo snapedTo; //Enum
    private GameObject miner; //set from Methodcall in Manager
    
    private GameObject itemOnTable; //this object where this script is attached to
    private Vector3 origPosTable;
    public GameObject correspondingItemOnMiner;
    
    public string desc;
    
    public bool solutionItemRound1 = false;
    public bool solutionItemRound2 = false;
    public bool solutionItemRound3 = false;
    
    private Canvas myCanvas;
    private RectTransform dragRectTransform;

    private bool snapToMiner;
    private Texture2D lampTexture; 


    // Start is called before the first frame update
    void Start()
    {
        itemOnTable = gameObject;
        origPosTable = itemOnTable.transform.position;
        lampTexture = GetComponent<Image>().sprite.texture;
        
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
        if (snapToMiner)
        {
            transform.position = correspondingItemOnMiner.transform.position;

            if (equipmentItem == MinerEquipmentItem.Lampe)
            {
                GetComponent<Image>().sprite.texture = lampTexture;
            }
        }
        else
        {
            transform.position = origPosTable;
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
            Debug.Log("Set it to miner");
            snapToMiner = true;
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
            snapToMiner = false;
        }
        else if (collision.name == "Scanarea")
        {
            Debug.Log("Set it to scanarea");
            collision.transform.parent.GetComponentInChildren<Text>().text = "";
        }
    }
}
