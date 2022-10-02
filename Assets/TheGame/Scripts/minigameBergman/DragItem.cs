using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Text nbrMinerItems;
    [SerializeField] private Text infoText;
    [SerializeField] private Canvas canvas;
    private RectTransform dragRectTransform;
    private CanvasGroup canvasGroup;
    Vector2 lastPos;
    Vector2 dropPosition = Vector2.zero;
    MinerGameData minerData;

    private bool snap;
    private GameObject snapObject;
    //int maxNumberSnapedObjects = 3;

    private void Awake()
    {
        minerData = GameObject.FindObjectOfType<MinerGameData>();
        
        dragRectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        
        snapObject = null;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; //important to set for drag!!!
        lastPos = dragRectTransform.anchoredPosition;

    }

    public void OnDrag(PointerEventData eventData)
    {
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor; //important when using screen space
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnDragEnd");
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        if (snap)
        {
            //Vector2 dropPosition = Vector2.zero;
            if (snapObject.name == "Miner")
            {
                if (gameObject.name == "IconLamp")
                {
                    dropPosition = minerData.GetPosLampOnMiner();
                }
                else if (gameObject.name == "IconHelmet")
                {
                    dropPosition = minerData.GetPosHelmetOnMiner();
                }
                else if (gameObject.name == "IconCard")
                {
                    dropPosition = minerData.GetPosCardOnMiner();
                }
                else if(gameObject.name == "IconSuit")
                {
                    dropPosition = minerData.GetPosSuitOnMiner();
                }
                
            }
            else if(snapObject.name == "BaseCard" && gameObject.name == "IconCard")
            {
                dropPosition = new Vector2(60f, -60f);
            }
            else if (snapObject.name == "BaseHelmet" && gameObject.name == "IconHelmet")
            {
                dropPosition = new Vector2(60f, -60f);
            }
            else if (snapObject.name == "BaseLamp" && gameObject.name == "IconLamp")
            {
                dropPosition = new Vector2(60f, -60f);
            }

            else
            {
                dropPosition = lastPos;
            }

            //eventdata.pointerdrag.gameobject.transform.setparent(minerdata.getminertransform());
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = dropPosition;

            if (eventData.pointerDrag.gameObject.name == "IconSuit")
            {
                float myscale = 2.125f;
                eventData.pointerDrag.GetComponent<RectTransform>().localScale = new Vector3(myscale,myscale,myscale);
                Debug.Log(eventData.pointerDrag.gameObject.name + " " + eventData.pointerDrag.gameObject.transform);
            }

            snap = false;
            minerData.PlaySnapSound();
        }
        else
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = lastPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTERs " + gameObject.name + " ;trigger enter object" + collision.gameObject.name);
        Debug.Log("-------------------in Trigger " + (snapObject != null) + " " + collision.gameObject.name);

        snap = true;
        snapObject = collision.gameObject;

        if (collision.gameObject.name == "Miner")
        {
            minerData.DecrementMinerItems();
            nbrMinerItems.text = minerData.GetNbrMinerItems().ToString();
            gameObject.transform.SetParent(minerData.GetMinerTransform());
            dragRectTransform = GetComponent<RectTransform>();
        }
        else if (snapObject.name == "BaseInfo")
        {
            if (gameObject.name == "IconCard")
            {
                infoText.text = minerData.GetInfoCard();
                
            }
            if (gameObject.name == "IconHelmet")
            {
                infoText.text = minerData.GetInfoHelmet();
            }
            if (gameObject.name == "IconLamp")
            {
                infoText.text = minerData.GetInfoLamp();
               
            }
        }

     }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("EXIT " + gameObject.name + " ;trigger exit " + collision.gameObject.name);
       
        
        if((snapObject != null) && (snapObject.gameObject.name == collision.gameObject.name))
        {
            snap = false;
            snapObject = null;
        }
        
        if(collision.gameObject.name == "Miner")
        {
            minerData.IncrementMinerItems();
            nbrMinerItems.text = minerData.GetNbrMinerItems().ToString();

            if(gameObject.name == "IconLamp")
            {
                gameObject.transform.SetParent(minerData.GetLampTransform());
                dropPosition = minerData.GetPosLampOnMiner();
            }
            else if (gameObject.name == "IconSuit")
            {
                gameObject.transform.SetParent(minerData.GetSuitTransform());
                dropPosition = minerData.GetPosSuitOnMiner();
            }
            else if (gameObject.name == "IconCard")
            {
                gameObject.transform.SetParent(minerData.GetCardTransform());
                dropPosition = minerData.GetPosCardOnMiner();
            }
            else if (gameObject.name == "IconHelmet")
            {
                gameObject.transform.SetParent(minerData.GetHelmetTransform());
                dropPosition = minerData.GetPosHelmetOnMiner();
            }
            dragRectTransform = GetComponent<RectTransform>();
        }
        else if (collision.gameObject.name == "BaseInfo")
        {
                infoText.text = "";
        }

    }
}
