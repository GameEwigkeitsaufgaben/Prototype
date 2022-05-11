//This script is responsible for to trigger at a coalmine stop depending on the stop and the direction the cave is reaching the stop.

using UnityEngine;
using UnityEngine.UI;

public class CaveColliderBottom : MonoBehaviour
{
    private const string TriggerEntryArea = "TriggerStopEntryArea";
    private const string TriggerSole1 = "TriggerStopSole1";
    private const string TriggerSole2 = "TriggerStopSole2";
    private const string TriggerSole3 = "TriggerStopSole3";
    private const string TriggerTalkSpeedCave = "TriggerTalkSpeedCave";

    public Cave cave;
    public CoalmineSpeechManger speechManger;
    public bool revisitEntryArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == TriggerEntryArea && !revisitEntryArea)
        {
            //cave.InitReachedStop(CoalmineStop.EntryArea);
            speechManger.playEntryArea = true;
            revisitEntryArea = true;

            //cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
        }
        else if (other.name == TriggerEntryArea && revisitEntryArea)
             {
                cave.InitReachedStop(CoalmineStop.EntryArea);
                //es fehlt noch ein Audio bitte den Ausgang nehmen! wenn wir wieder da sind. 
                //Es kann gleich weitergeschalten werden, noch keine Prüfung ob text fertig geprochen.
             }
        else if (other.name == TriggerSole1 && cave.targetStop == CoalmineStop.Sole1)
             {
                cave.InitReachedStop(CoalmineStop.Sole1);
                speechManger.playSole1 = true;
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

                speechManger.playSole3WPCave = (GameData.currentStopSohle == (int)CoalmineStop.Sole3) ? true : false;
             }
    }
}
