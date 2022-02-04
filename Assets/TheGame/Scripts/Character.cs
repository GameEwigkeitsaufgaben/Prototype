using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    string name;
    Sprite characterSprite;
    float spriteScaleFactor;
    Vector3 characterLocalPosition;
    Vector3 characterLocalRotation;
    Camera mainCam;
    GameObject prefabCharacter;
    Image characterImage;
    Component[] character;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        character = GetComponentsInChildren<Transform>();
        foreach(Component a in character)
        {
            //Debug.Log("-------------------------------------------------" + a.gameObject.name);
        }
    }




}
