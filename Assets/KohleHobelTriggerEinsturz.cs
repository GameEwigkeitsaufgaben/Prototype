using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohleHobelTriggerEinsturz : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;
    private GameObject verbruch;

    public void Einsturz()
    {
        verbruch = Instantiate(prefab, position.position,Quaternion.identity);
    }

    public void DestroyVerbruch()
    {
        if (verbruch != null)
        {
            Destroy(verbruch);
        }
    }
}
