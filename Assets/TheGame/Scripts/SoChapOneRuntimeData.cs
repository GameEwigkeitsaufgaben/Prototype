using UnityEngine;

[CreateAssetMenu(menuName = "SoChapOneRuntimeData")]
public class SoChapOneRuntimeData : ScriptableObject
{
    //Museum
    public Vector3 currentGroupPos = Vector3.zero; //-13.44922, 2.4, -2.670441
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;

    //Lookaround cave
    public float playerRotation; //needed for lookaroundMouse
    public bool trainArrived;

    //GameProgress
    public bool isMinerDone, isMythDone, isCoalifiationDone, isCarbonificationPeriodDone;
    public bool sole1done, sole2done, sole3BewetterungDone, sole3GebaeudeDone, trainRideInDone, trainRideOutDone;
    public bool interaction116Done, interaction117done;
    
    //General Settings
    public bool musicOn = true;

    private void OnEnable()
    {
        Debug.Log("SO enableds");
        Debug.Log("deserilize");
        currentGroupPos = Vector3.zero;
        currentMuseumWaypoint = MuseumWaypoints.WP0;
        playerRotation = 0f;
        isMinerDone = isMythDone = isCarbonificationPeriodDone = isCoalifiationDone = false;
        sole1done = sole2done = sole3BewetterungDone = sole3GebaeudeDone = trainRideInDone = trainRideOutDone = false;
        interaction116Done = interaction117done = false;
        musicOn = true;
    }

    //public void OnAfterDeserialize()
    //{
    //    Debug.Log("deserilize");
    //    currentGroupPos = Vector3.zero;
    //    currentMuseumWaypoint = MuseumWaypoints.WP0;
    //    playerRotation = 0f;
    //    isMinerDone = isMythDone = isCarbonificationPeriodDone = isCoalifiationDone = false;
    //    sole1done = sole2done = sole3BewetterungDone = sole3GebaeudeDone = trainRideInDone = trainRideOutDone = false;
    //    interaction116Done = interaction117done = false;
    //    musicOn = true;
    //}

    //public void OnBeforeSerialize()
    //{
    //    throw new System.NotImplementedException();
    //}
}
