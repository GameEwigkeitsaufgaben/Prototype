using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MyDragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform dragRectTransform;
    private CanvasGroup canvasGroup;

    private GameObject myParent;
    private MinerItem myMinerItem;
    private MinerGameData minerData;

    bool snapIconToBase = true;
   

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; //important to set for drag!!!
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //important when using screen space
        
        if(eventData.pointerDrag.name == "IconSuit")
        {
            myMinerItem.SetSizeSuit();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnDragEnd");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (snapIconToBase)
        {
            myMinerItem.SetIconParentToBase();
            myMinerItem.snapedToMiner = false;
            if (!myMinerItem.snapedToBase)
            {
                minerData.IncrementMinerItems();
                myMinerItem.snapedToBase = true;
            }
        }
        else
        {
            myMinerItem.SetIconParentToMiner();
            myMinerItem.snapedToBase = false;
            
            if (!myMinerItem.snapedToMiner)
            {
                minerData.DecrementMinerItems();
                myMinerItem.snapedToMiner = true;
            }
        }

        minerData.UpdateMinNumberItemText();
        minerData.PlaySnapSound();

    }

    private void Awake()
    {
        dragRectTransform = GetComponent<RectTransform>(); 
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        myParent = gameObject.transform.parent.gameObject;
        Debug.Log("this is " + this.name + " my Parent is "+ myParent);
        myMinerItem = myParent.GetComponent<MinerItem>();
        minerData = GameObject.FindObjectOfType<MinerGameData>();
        canvas = FindObjectOfType<Canvas>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name);
        Debug.Log(myMinerItem.name);

        if (myMinerItem.itemBase.gameObject.name == collision.gameObject.name)
        {
            snapIconToBase = true;
        }
        else if(myMinerItem.miner.gameObject.name == collision.gameObject.name)
        {
            snapIconToBase = false;
        }
        else
        {
            if(gameObject.transform.parent.gameObject.name == myMinerItem.miner.gameObject.name)
            {
                snapIconToBase = false;
            }
            else
            {
                snapIconToBase = true;
            }
        }

        if(collision.name == "BaseInfo")
        {
            minerData.SetInfoText(myMinerItem.itemDesc);
        }
    }
}
