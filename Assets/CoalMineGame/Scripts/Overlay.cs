using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OverlayChildType
{
    DESCRIPTION, 
}

public class Overlay : MonoBehaviour
{
    private Transform[] allOverlayChildren;

    private void Start()
    {
        allOverlayChildren = gameObject.transform.GetComponentsInChildren<Transform>(true); //inclusive inactive elements

    }

    public void ShowOverlayChildrenInArray()
    {

    }
}
