using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateRenderOnCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Killzone")
        {
            this.GetComponent<Renderer>().enabled = false;
        }
    }
}
