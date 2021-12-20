using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : MonoBehaviour
{



    // Update is called once per frame
    void Update()
    {
        if (GameData.moveCave)
        {
            transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("liftfahrt " + other.name);

        if (other.name == "TriggerSohle1")
        {
            GameData.moveCave = false;
        }
    }
}
