//This script is responsible for to trigger at a coalmine stop depending on the stop and the direction the cave is reaching the stop.

using UnityEngine;

public class CaveColliderBottom : MonoBehaviour
{
    private const string TriggerEntryArea = "TriggerStopEntryArea";
    private const string TriggerSole1 = "TriggerStopSole1";
    private const string TriggerSole2 = "TriggerStopSole2";
    private const string TriggerSole3 = "TriggerStopSole3";
    private const string TriggerTalkNextStopS1 = "TriggerTalkNextStopS1";
    private const string TriggerTalkSpeedCave = "TriggerTalkSpeedCave";

    public Cave cave;
    public CoalmineSpeechManger speechManger;

    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == TriggerEntryArea)
        {
            cave.InitReachedStop(CoalmineStop.EntryArea);
            if (runtimeData.revisitEntryArea)
            {
                //es fehlt noch ein Audio noch nicht alle Stationen besucht, bitte wieder runterfahren. 
                //Es kann gleich weitergeschalten werden, noch keine Prüfung ob text fertig geprochen.
                speechManger.playEntryAreaRevisit = true;
            }
            else
            {
                speechManger.playEntryArea = true;
                runtimeData.revisitEntryArea = true;
            }
        }
        else if (other.name == TriggerSole1 && cave.targetStop == CoalmineStop.Sole1)
             {
                cave.InitReachedStop(CoalmineStop.Sole1);
                speechManger.playSole1 = true;
             }
        else if (other.name == TriggerTalkNextStopS1 && cave.moveDirection == CaveMovement.MoveDown)
        {
            speechManger.playSchachtS1 = true;
        }
        else if (other.name == TriggerTalkSpeedCave && cave.moveDirection == CaveMovement.MoveDown)
             {
                speechManger.playSchacht = true;
             }
        else if (other.name == TriggerSole2 && cave.targetStop == CoalmineStop.Sole2)
             {
                cave.InitReachedStop(CoalmineStop.Sole2);
                speechManger.playSole2 = true;
             }
        else if (other.name == TriggerSole3 && cave.targetStop == CoalmineStop.Sole3)
             {
                cave.InitReachedStop(CoalmineStop.Sole3);

                speechManger.playSole3WPCave = (runtimeData.currentCoalmineStop == CoalmineStop.Sole3) ? true : false;
             }
    }
}
