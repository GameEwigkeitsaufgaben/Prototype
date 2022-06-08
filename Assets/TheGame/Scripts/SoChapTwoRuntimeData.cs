using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoChapTwoRuntimeData")]
public class SoChapTwoRuntimeData : Runtime
{
    public MuseumWaypoints lastWP = MuseumWaypoints.None;
    public bool interactTVDone = false;

    private void OnEnable()
    {
        interactTVDone = false;
    }

}
