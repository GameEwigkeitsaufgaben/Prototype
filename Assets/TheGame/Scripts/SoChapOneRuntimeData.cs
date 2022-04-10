using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoChapOneRuntimeData")]
public class SoChapOneRuntimeData : ScriptableObject
{
    public Vector3 currentGroupPos = Vector3.zero; //-13.44922, 2.4, -2.670441
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;

    public float playerRotation; //needed for lookaroundMouse

}
