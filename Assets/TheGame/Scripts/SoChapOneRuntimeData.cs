using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SoChapOneRuntimeData")]
public class SoChapOneRuntimeData : ScriptableObject
{
    public Vector3 currentGroupPos = Vector3.zero;
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;
}
