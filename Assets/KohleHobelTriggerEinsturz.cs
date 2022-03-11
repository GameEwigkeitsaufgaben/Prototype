using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohleHobelTriggerEinsturz : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;
    public float delayDestruction;

    public void Einsturz()
    {
        GameObject clone = Instantiate(prefab, position.position,Quaternion.identity);
        Destroy(clone, delayDestruction);
    }
}
