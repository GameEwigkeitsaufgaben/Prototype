using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinerItem : MonoBehaviour
{
    public GameObject itemBase, itemIcon, IconLabel;
    public GameObject miner;
    public string itemDesc;

    public float scaleOnMiner = 1.0f;
    public float scaleOnBase = 1.0f;
    public Vector2 iconPosOnMiner;
    private Vector3 iconPosOnBase;
    public bool snapedToBase, snapedToMiner;

    private void Start()
    {
        miner = GameObject.FindGameObjectWithTag("Miner");
        iconPosOnBase = itemIcon.transform.localPosition;
        Debug.Log("I am the parent: " + this + "; my Icon is " + itemIcon.name + "with transform " + itemIcon.transform.localPosition);
        snapedToBase = true;
        snapedToBase = false;
    }

    public void SetIconParentToMiner()
    {
        itemIcon.transform.SetParent(miner.transform);
        itemIcon.GetComponent<RectTransform>().anchoredPosition = iconPosOnMiner;

        if(itemIcon.name == "IconSuit")
        {
            itemIcon.GetComponent<RectTransform>().localScale = new Vector3(scaleOnMiner,scaleOnMiner);
        }
    }

    public void SetIconParentToBase()
    {
        Debug.Log("Set Icon to Base");
        itemIcon.transform.SetParent(gameObject.transform);
        itemIcon.GetComponent<RectTransform>().anchoredPosition = new Vector2(iconPosOnBase.x, iconPosOnBase.y);
        itemIcon.GetComponent<RectTransform>().localScale = new Vector3(scaleOnBase, scaleOnBase);
    }

    public void SetSizeSuit()
    {
        itemIcon.GetComponent<RectTransform>().localScale = new Vector3(scaleOnBase, scaleOnBase);
    }
}
