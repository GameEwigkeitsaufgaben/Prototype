using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohlehobelManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameData.cavePosX = 0.12937f;
        GameData.cavePosY = -176.2351f;
        GameData.cavePosZ = 0.46835f;
        GameData.sohleToReload = (int)CurrentStop.Sohle3;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
