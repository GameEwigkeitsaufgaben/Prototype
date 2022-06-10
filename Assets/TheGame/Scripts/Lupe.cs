using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lupe : MonoBehaviour
{
    public GameObject LupeCirc, LupeHandle;

    int origSize;
    float scaleFactor = 4f;
    Vector3 origPosLupeCirc, origPosLupeHandle;

    private void Start()
    {
        origSize = 100;
        origPosLupeCirc = LupeCirc.GetComponent<RectTransform>().localPosition;
        origPosLupeHandle = LupeHandle.GetComponent<RectTransform>().localPosition;
    }


    public void EnlargeLupe(float factor)
    {
        float newSize = origSize * factor;
        LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, newSize);
        LupeHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(newSize, newSize);
        LupeHandle.GetComponent<RectTransform>().localPosition = new Vector3(
            LupeCirc.GetComponent<RectTransform>().localPosition.x + (newSize/2),
            LupeCirc.GetComponent<RectTransform>().localPosition.y - (newSize/2), 
            0f);
    }

    public void ShrinkToOrigLupe()
    {
        float newSize = origSize;
        LupeCirc.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().sizeDelta = new Vector2(origSize, origSize);
        LupeHandle.GetComponent<RectTransform>().localPosition = origPosLupeHandle;
    }

}
