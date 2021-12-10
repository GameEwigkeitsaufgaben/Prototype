using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum OverlayChildType
{
    IMAGE, TYPEICON, DESCRIPTION, TAGS,
}

public class Overlay : MonoBehaviour
{
    private Transform[] allOverlayChildren;
    private const int OVERLAYIMAGE = 3; 
    private const int OVERLAYTYPEICON = 4;
    private const int OVERLAYDESCRIPTION = 6;
    private const int OVERLAYTAGS = 7;

    private PostData postData;
    private GameIcons icons;

    private void Start()
    {
        icons = Resources.Load<GameIcons>("Icons");
    }

    private void SetUpOverlay()
    {
        allOverlayChildren = gameObject.transform.GetComponentsInChildren<Transform>(true); //inclusive inactive elements
                                                                                            ShowOverlayChildrenInArray();

        allOverlayChildren[OVERLAYDESCRIPTION].gameObject.GetComponent<Text>().text = postData.postDescription;
        allOverlayChildren[OVERLAYTAGS].gameObject.GetComponent<Text>().text = postData.postTags;
        allOverlayChildren[OVERLAYIMAGE].gameObject.GetComponent<Image>().sprite = postData.postSprite;
        
        

        if(postData.overlayType == OverlayType.IMAGE)
        {
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().interactable = false;
            ColorBlock ab = allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors;
            ab.disabledColor = Color.white;
            allOverlayChildren[OVERLAYIMAGE].GetComponent<Button>().colors = ab;
        }
        else
        {
            allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().sprite = postData.GetIcon();
        }

        

        Debug.Log(gameObject.name + "has icon " + allOverlayChildren[OVERLAYTYPEICON].gameObject.GetComponent<Image>().sprite.name);
        Debug.Log(gameObject.name + "image icon " + allOverlayChildren[OVERLAYIMAGE].gameObject.GetComponent<Image>().sprite.name);
        Debug.Log("Overlay created" + gameObject.name);
    }

    public void SetOverlayData(PostData data)
    {
        postData = data;
        SetUpOverlay();
    }

    public void ShowOverlayChildrenInArray()
    {
        int index = 0;
        foreach(Transform f in allOverlayChildren)
        {
            Debug.Log("obj at "+index+": " + f.gameObject.name);
            index++;
        }
    }

    public void CloseOverlay()
    {
        gameObject.SetActive(false);
    }
}
