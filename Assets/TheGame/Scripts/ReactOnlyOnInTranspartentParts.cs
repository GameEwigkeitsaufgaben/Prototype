//https://stackoverflow.com/questions/64936167/setting-a-buttons-alphahittestminimumtreshold-results-into-no-call-to-button-on
//React only on intransparent parts of an image, link on top and deactivate crunch compress!
using UnityEngine;
using UnityEngine.UI;

public class ReactOnlyOnInTranspartentParts : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Image>().alphaHitTestMinimumThreshold = 0.001f;
    }
}
