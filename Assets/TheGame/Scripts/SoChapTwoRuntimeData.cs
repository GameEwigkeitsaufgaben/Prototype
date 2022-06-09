using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoChapTwoRuntimeData")]
public class SoChapTwoRuntimeData : Runtime
{
    public MuseumWaypoints lastWP = MuseumWaypoints.None;
    public bool interactTVDone = false;
    public Vector3 groupPosition;

    public bool grundwasser212Done = false;

    private void OnEnable()
    {
        grundwasser212Done = false;
        groupPosition = new Vector3(12.03f, 2.61f, -4.28f);
        lastWP = MuseumWaypoints.None;
        interactTVDone = false;
    }

}
