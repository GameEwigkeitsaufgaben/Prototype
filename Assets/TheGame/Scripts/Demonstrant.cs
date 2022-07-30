using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demonstrant : MonoBehaviour
{
    public Image speechBubble;
    public AudioClip audioClip;
    public bool gehoert;

    // Start is called before the first frame update
    void Start()
    {
        gehoert = false;
    }
}
