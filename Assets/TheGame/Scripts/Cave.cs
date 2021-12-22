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
            transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
        }
    }

    public void StopCave()
    {
        GameData.moveCave = false;
    }
}
