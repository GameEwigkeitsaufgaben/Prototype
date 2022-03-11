using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohlehobelActivateMesh : MonoBehaviour
{
    public string meshTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == meshTag)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
