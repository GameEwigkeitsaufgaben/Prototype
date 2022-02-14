using UnityEngine;
using UnityEngine.UI;

public class CaveColliderBottom : MonoBehaviour
{
    private const string TriggerEntryArea = "TriggerEntryArea";
    private const string TriggerSole1 = "TriggerSole1";
    private const string TriggerSole2 = "TriggerSole2";
    private const string TriggerSole3 = "TriggerSohle3";
    private const string TriggerTunnel = "TriggerSchacht";

    public Cave cave;
    public CaveSpeechManger speechManger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" .." + other.name);

        if (other.name == TriggerEntryArea)
        {
            cave.StopCave();
            cave.currentStop = CurrentStop.Einstieg;
            GameData.currentStopSohle = (int)cave.currentStop;
            speechManger.playEntryArea = true;
            cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
            // es fehlt noch ein Audio bitte den Ausgang nehmen! wenn wir wieder da sind. 
            //Es kann gleich weitergeschalten werden, noch keine Prüfung ob text fertig geprochen.
        }
        else if(other.name == TriggerSole1 && cave.targetStop == CurrentStop.Sohle1)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle1;
            GameData.currentStopSohle = (int)cave.currentStop;
            speechManger.playSole1 = true;
            //StartCoroutine(sprechblaseController.PlayNextAudio("spdad", 10f, other.GetComponent<LiftSohleOne>().clip2));
        }
        else if (other.name == TriggerTunnel && cave.moveDirection == CaveMovement.MoveDown)
        {
            speechManger.playSchacht = true;
        }
        else if (other.name == TriggerSole2 && cave.targetStop == CurrentStop.Sohle2)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle2;
            GameData.currentStopSohle = (int)cave.currentStop;
            speechManger.playSole2 = true;
        }
        else if (other.name == TriggerSole3 && cave.targetStop == CurrentStop.Sohle3)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle3;
            GameData.currentStopSohle = (int)cave.currentStop;
            speechManger.playSole3WPCave = true;
            //if (!GameData.sohle3IntroPlayedOnce)
            //{
            //    other.GetComponent<LiftSohleThree>().PlayAudio();
            //    other.GetComponent<LiftSohleThree>().StartTrain();
            //    GameData.sohle3IntroPlayedOnce = true;
            //}
        }
        
    }

}
