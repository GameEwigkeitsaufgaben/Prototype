using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropFotoItem : MonoBehaviour
{
    public Vector3 posPick;
    public Vector3 posOnMiner;
    public bool snaped;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MinerNoHead")
        {
            if(!snaped)
            {
                Debug.Log("########################################");
                snaped = true;
            }
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "MinerNoHead")
        {
            if (snaped)
            {
                Debug.Log("########################################------------");
                snaped = false;
            }
        }
    }

    private void OnMouseUp()
    {
        if (!snaped)
        {
            gameObject.transform.localPosition = posPick;
        }
    }

}
