﻿using UnityEngine;
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

    private void Awake()
    {
        myConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        //myResource = Resources.Load<SoMuseumConfig>(GameData.NameMuseumCard);
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
        overallBgImg.color = GameColors.defaultInteractionColorNormal;
        overallBgImg.sprite = myConfig.memoryBackside;
        //myContentImg.sprite = myConfig.memoryBackside;
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
            if (i.GetComponent<TMP_Text>() != null)
            {
                myStatement = i.GetComponent<TMP_Text>();
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

    private void OnMouseEnter()
    {
        Debug.Log("Mouse Enter in Card " + gameObject.name);
        gameObject.GetComponent<MouseChange>().MouseEnter();
        overallBgImg.color = GameColors.defaultInteractionColorHighlighted;
    }


    private void OnMouseExit()
    {
        Debug.Log("Mouse Exit in Card " + gameObject.name);
        gameObject.GetComponent<MouseChange>().MouseExit();
        overallBgImg.color = GameColors.defaultInteractionColorNormal;
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

    public void SetCardFaceUp()
    {
        myContentImg.gameObject.SetActive(true);
        overallBgImg.color = GameColors.defaultInteractionColorNormal;
        overallBgImg.sprite = null;
        //myContentImg.sprite = ;
        myStatement.gameObject.SetActive(true);
    }


}