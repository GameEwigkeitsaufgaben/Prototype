using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PostItDrag : MonoBehaviour
{
    public string myText = "";
    public bool statementTrue = false;
    public Sprite mySprite;
    private Image myImage, mySolutionImg;
    private Text myStatement;
    private Canvas myParentCanvas;
    private RectTransform dragRectTransform;
    private bool correctSelection;
    private Vector3 origPos;
    private SoMuseumConfig myConfig;
    public bool backsideUp = true;

    private void Start()
    {
        Transform[] tmpObjs = gameObject.GetComponentsInChildren<Transform>();
        myConfig = Resources.Load<SoMuseumConfig>("ConfigMuseum");

        dragRectTransform = GetComponent<RectTransform>();
        
        if (myParentCanvas == null)
        {
            myParentCanvas = gameObject.transform.parent.transform.parent.GetComponent<Canvas>();
        }
        
        foreach(Transform i in tmpObjs)
        {
            if (i.GetComponent<Text>() != null)
            {
                myStatement = i.GetComponent<Text>();
                myStatement.gameObject.SetActive(false);
            }
            if (i.GetComponent<Image>() != null)
            {
                if (i.GetComponent<Image>().name == "BgSolution")
                {
                    mySolutionImg = i.GetComponent<Image>();
                    mySolutionImg.gameObject.SetActive(false);
                }
                else if (i.GetComponent<Image>().name == "BgImg")
                {
                    myImage = i.GetComponent<Image>();
                    mySprite = myImage.sprite;
                    myImage.sprite = myConfig.memoryBackside;
                    
                }
            }
        }

        //FlipCard();
        //myImage.sprite = myConfig.memoryBackside;
        //myStatement.gameObject.SetActive(false);

        Debug.Log("setup img: " + myImage.sprite.ToString());
    }
    public void MarkRightSolution()
    {
        mySolutionImg.gameObject.SetActive(true);
    }

    public void FlipCard()
    {
        backsideUp = !backsideUp;

        if (backsideUp)
        {
            Debug.Log("img null -------- " + myImage);
            myImage.sprite = myConfig.memoryBackside;
            myStatement.gameObject.SetActive(false);
        }
        else
        {
            myImage.sprite = mySprite;
            myStatement.gameObject.SetActive(true);
        }


    }

    private void OnMouseDown()
    {
        Debug.Log("on down!!!");
        FlipCard();
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log(collision.name);
       
    //    if (collision.name == "Wahr")
    //    {
    //        Debug.Log("wahr");
    //        correctSelection = (statementTrue) ? true : false;
    //    }
        
    //    if (collision.name == "Falsch")
    //    {
    //        Debug.Log("falsch");
    //        correctSelection = (statementTrue) ? false : true;
    //    }
    //}

    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnDragBegin");
    //    origPos = gameObject.transform.position;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnDragEnd");
    //    if (!correctSelection)
    //    {
    //        transform.position = origPos;
    //    }
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    dragRectTransform.anchoredPosition += eventData.delta / myParentCanvas.scaleFactor; //important when using screen space
    //}
}
