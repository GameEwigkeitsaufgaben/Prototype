using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MuseumCard : MonoBehaviour
{
    public bool cardFaceDown = true;
    
    [SerializeField] private Image myContentImg, mySolutionImg, overallBgImg;
    [SerializeField] private TMP_Text myStatement;
    [SerializeField] private Sprite helperSprite;

    [SerializeField] private Canvas myParentCanvas;
    private SoMuseumConfig myConfig;
    public SoMuseumCard myResource;
    bool setup;

    private AudioSource cardAudio;

    private void Awake()
    {
        setup = true;
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

    //public void TurnCard()
    //{
    //    cardFaceDown = myResource.cardFaceDown;

    //    if (cardFaceDown)
    //    {
    //        SetCardFaceDown();
    //    }
    //    else
    //    {
    //        SetCardFaceUp();
    //    }
    //}


    public bool IsStatementTrue()
    {
        return myResource.statementTrue;
    }

    public void SetCardFaceDown()
    {
        myContentImg.gameObject.SetActive(false);
        myStatement.gameObject.SetActive(false);
        //overallBgImg.color = GameColors.defaultInteractionColorNormal;
        overallBgImg.sprite = myConfig.memoryBackside;
        if (setup) return;
        cardAudio.Play();
    }

    private void AssignChildElements()
    {
        Transform[] tmpObjs = gameObject.GetComponentsInChildren<Transform>();
        
        if (myParentCanvas == null)
        {
            myParentCanvas = gameObject.transform.parent.transform.parent.GetComponent<Canvas>();
        }

        foreach (Transform i in tmpObjs)
        {
            if (i.GetComponent<TMP_Text>() != null)
            {
                myStatement = i.GetComponent<TMP_Text>();
            }
            if (i.GetComponent<Image>() != null)
            {
                if (i.GetComponent<Image>().name == "MarkSolution")
                {
                    mySolutionImg = i.GetComponent<Image>();
                }
                else if (i.GetComponent<Image>().name == "ContentImage")
                {
                    myContentImg = i.GetComponent<Image>();
                }
                else if (i.GetComponent<Image>().name == "FaceUpBgImage")
                {
                    overallBgImg = i.GetComponent<Image>();
                }
            }
        }

        cardAudio = myParentCanvas.GetComponent<AudioSource>();
        cardAudio.playOnAwake = false;
        cardAudio.clip = myConfig.sfxFlipCard;
    }

    public void MarkRightSolution()
    {
        mySolutionImg.gameObject.SetActive(true);
    }

    private void OnMouseDown()
    {
        FlipCard();
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<MouseChange>().MouseEnter();
        //overallBgImg.color = GameColors.defaultInteractionColorHighlighted;
    }


    private void OnMouseExit()
    {
        gameObject.GetComponent<MouseChange>().MouseExit();
        //overallBgImg.color = GameColors.defaultInteractionColorNormal;
    }

    public void FlipCard()
    {
        setup = false;
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

    public void SetCardFaceUp()
    {
        myContentImg.gameObject.SetActive(true);
        //overallBgImg.color = GameColors.defaultInteractionColorNormal;
        overallBgImg.sprite = null;
        //myContentImg.sprite = ;
        myStatement.gameObject.SetActive(true);
        if (setup) return;
        cardAudio.Play();
    }
}