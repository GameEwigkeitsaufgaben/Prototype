using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayInPlace : MonoBehaviour
{
    bool started = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActivateOverlay(bool active)
    {
        gameObject.SetActive(active);

        if (started) return;
        
        started = true;
        gameObject.SetActive(false);
        
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
