using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public enum GWChanceType
{
    chance,
    nochance,
    neitherNor
}

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BehaviourButton))]
public class DragItemThoughts : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform myDragRectTransform;
    private Canvas myParentCanvas;
    private ManagerGWChancen manager;
    private string myTag;
    public Vector3 origPos;

    public GameObject mySnapObj;
    public bool snaped = false;
    public bool dragging = false;
    public bool dragable = true;

    public GWChanceType type;

    // Start is called before the first frame update
    void Start()
    {
        myDragRectTransform = GetComponent<RectTransform>();
        manager = FindObjectOfType<ManagerGWChancen>();
        //Get the parent Canvas Obj, for dragging mechanics - needed for scalefactor in differenct screen spaces! 
        GameObject tempCanvasItem = gameObject; //start with gameobject
        while (tempCanvasItem.GetComponent<Canvas>() == null)
        {
            tempCanvasItem = tempCanvasItem.transform.parent.gameObject;
        }
        myParentCanvas = tempCanvasItem.GetComponent<Canvas>();
        origPos = gameObject.transform.position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        gameObject.tag = "DragItem";
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (snaped) return;
        if (!dragable) return;

        Debug.Log("Drag");
        myDragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        dragging = false;
    }

    private void OnMouseDown()
    {
        GetComponent<MouseChange>().MouseDown();
    }

    private void OnMouseUp()
    {
        GetComponent<MouseChange>().MouseUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "DropTargetImgChance" && type == GWChanceType.chance)
        {
            ChangeSceneBehaviour(collision);

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
        else if (collision.name == "DropTargetImgNoChance" && type == GWChanceType.nochance)
        {
            ChangeSceneBehaviour(collision);

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
        else if (collision.name == "DropTargetImgNeitherNor" && type == GWChanceType.neitherNor)
        {
            ChangeSceneBehaviour(collision);

            if (gameObject.name == "DragSourceSauberesGrubenwasser")
            {
                manager.speechManager.playSauberesGW = true;
                manager.currentTL = GameData.NameCH3TLSauberesGW;
            }
        }

    }

    private void ChangeSceneBehaviour(Collider2D collision)
    {
        Debug.Log(collision.name);
        gameObject.transform.SetParent(collision.transform);
        gameObject.GetComponent<Image>().enabled = false;
        snaped = true;
        manager.animator.enabled = false;
        manager.MirrorBergbauvertreter(true);
        manager.PauseDragAllDragItems(true);
    }
}
