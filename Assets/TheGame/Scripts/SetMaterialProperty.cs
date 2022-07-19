using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMaterialProperty : MonoBehaviour
{
    public GameObject test;
    [Range(0,1)]
    public float value;

    // Update is called once per frame
    void Update()
    {
        test.GetComponent<Image>().material.SetFloat("_ClipThreshold", value);
    }
}
