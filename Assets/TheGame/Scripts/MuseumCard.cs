using UnityEngine;
using UnityEngine.UI;

public class MuseumCard : MonoBehaviour
{
    public bool cardFaceDown = true;
    
    [SerializeField] private Image myContentImg, mySolutionImg, overallBgImg;
    [SerializeField] private Text myStatement;
    [SerializeField] private Sprite helperSprite;

    [SerializeField] private Canvas myParentCanvas;
    private SoMuseumConfig myConfig;
    public SoMuseumCard myResource;

    private void Awake()
    {
        myConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
    }

    public void PopulateElements()
    {
        myStatement.text = myResource.statement;
    }

    public void SetDefaults()
    {
        AssignChildElements();
        PopulateElements();
        cardFaceDown = myResource.cardFaceDown;

        Debug.Log("cdf " + cardFaceDown + " "+ gameObject.name);
        myContentImg.sprite = myResource.mySprite;

        if (cardFaceDown)
        {
            SetCardFaceDown();
        }
        else
        {
            SetCardFaceUp();
        }

        mySolutionImg.gameObject.SetActive(false);
    }

    public bool IsStatementTrue()
    {
        return myResource.statementTrue;
    }

    public void SetCardFaceDown()
    {
        //Debug.Log()
        //myImage.sprite = myConfig.memoryBackside;
        Debug.Log("Content img is assigned " + (myContentImg != null));
        Debug.Log("Content img is assigned " + (myStatement != null));
        Debug.Log("Content img is assigned " + (overallBgImg != null));
        Debug.Log("myResource is assigned " + (myResource != null));

        myContentImg.gameObject.SetActive(false);
        myStatement.gameObject.SetActive(false);
        overallBgImg.color = Color.black;
    }

    private void AssignChildElements()
    {
        Transform[] tmpObjs = gameObject.GetComponentsInChildren<Transform>();
        
        if (myParentCanvas == null)
        {
            myParentCanvas = gameObject.transform.parent.transform.parent.GetComponent<Canvas>();
            Debug.Log("MyCanvas " +myParentCanvas.name);
        }

        foreach (Transform i in tmpObjs)
        {
            Debug.Log(i.name);
            if (i.GetComponent<Text>() != null)
            {
                myStatement = i.GetComponent<Text>();
                Debug.Log(i.name + " set statatement component");
            }
            if (i.GetComponent<Image>() != null)
            {
                if (i.GetComponent<Image>().name == "MarkSolution")
                {
                    mySolutionImg = i.GetComponent<Image>();
                    Debug.Log(i.name + " set imgSolution component");
                }
                else if (i.GetComponent<Image>().name == "ContentImage")
                {
                    myContentImg = i.GetComponent<Image>();
                    Debug.Log(i.name + " set imgContentImage component");
                }
                else if (i.GetComponent<Image>().name == "FaceUpBgImage")
                {
                    overallBgImg = i.GetComponent<Image>();
                    Debug.Log(i.name + " set faceupimg component");
                }
            }
        }
    }

    public void TurnCard()
    {
        cardFaceDown = myResource.cardFaceDown;

        if (cardFaceDown)
        {
            SetCardFaceDown();
        }
        else
        {
            SetCardFaceUp();
        }
    }

    public void MarkRightSolution()
    {
        mySolutionImg.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        Debug.Log("on down!!!");
        FlipCard();
    }

    public void FlipCard()
    {
        cardFaceDown = !cardFaceDown;

        if (cardFaceDown)
        {
            SetCardFaceDown();
        }
        else
        {
            SetCardFaceUp();
        }
    }

    private void SetCardFaceUp()
    {
        myContentImg.gameObject.SetActive(true);
        overallBgImg.color = Color.white;
        //myContentImg.sprite = helperSprite;
        myStatement.gameObject.SetActive(true);
    }
}