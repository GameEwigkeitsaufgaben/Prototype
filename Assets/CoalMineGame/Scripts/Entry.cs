using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : MonoBehaviour
{
    public GameObject post;
    public GameObject overlay;

    private OverlayType overlayType; // Enum

    public void OpenOverlay()
    {
        post.GetComponent<Post>().SetButtonFunctionInteractable(false);
        overlay.SetActive(true);
    }

    public void CloseOverlay()
    {
        post.GetComponent<Post>().SetButtonFunctionInteractable(true);
        overlay.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
