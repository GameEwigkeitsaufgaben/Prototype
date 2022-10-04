using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSoleTwoStatic : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;

    // Start is called before the first frame update
    void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);    
    }
}
