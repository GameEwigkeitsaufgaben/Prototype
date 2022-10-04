using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropFotoItem : MonoBehaviour
{
    public Vector3 posPick;
    public Vector3 posOnMiner;
    public bool snaped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "MinerNoHead")
        {
            if(!snaped)
            {
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
