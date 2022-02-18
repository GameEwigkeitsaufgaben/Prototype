using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohlehobelKillzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kohle")
        {
            Destroy(other.gameObject);
        }
    }
}
