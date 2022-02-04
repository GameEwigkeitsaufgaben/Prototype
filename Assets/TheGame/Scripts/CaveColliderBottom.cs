using UnityEngine;
using UnityEngine.UI;

public class CaveColliderBottom : MonoBehaviour
{
    public Cave cave;
    public SprechblaseController sprechblaseController;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(" .." + other.name);

        if (other.name == "TriggerEinstieg")
        {
            cave.StopCave();
            cave.currentStop = CurrentStop.Einstieg;
            GameData.currentStopSohle = (int)cave.currentStop;
            sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<AudioSource>().clip);
            sprechblaseController.sprechblaseDad.SetSprechblaseInNotPlayedMode();

            sprechblaseController.sprechblaseGabi.gameObject.SetActive(false);
            
            cave.liftBtns[0].GetComponent<CaveButton>().feedbackObject.SetActive(true);
            cave.liftBtns[0].gameObject.SetActive(true);
            cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
            
        }

        if (other.name == "TriggerSohle1" && cave.targetStop == CurrentStop.Sohle1)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle1;
            GameData.currentStopSohle = (int)cave.currentStop;

            other.GetComponent<LiftSohleOne>().PlayEnvironmentalSound();

            sprechblaseController.sprechblaseGabi.SetAudioClip(other.GetComponent<LiftSohleOne>().clip1);
            sprechblaseController.sprechblaseGabi.SetSprechblaseInPlayingMode();

            sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<LiftSohleOne>().clip2);
            sprechblaseController.sprechblaseDad.SetSprechblaseInNotPlayedMode();
            sprechblaseController.sprechblaseDad.gameObject.SetActive(true);


            //StartCoroutine(sprechblaseController.PlayNextAudio("spdad", 10f, other.GetComponent<LiftSohleOne>().clip2));
        }

        if (other.name == "TriggerSohle2" && cave.targetStop == CurrentStop.Sohle2)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle2;
            GameData.currentStopSohle = (int)cave.currentStop;
            other.GetComponent<LiftSohleTwo>().PlayAudio();
        }
        if (other.name == "TriggerSohle3" && cave.targetStop == CurrentStop.Sohle3)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle3;
            GameData.currentStopSohle = (int)cave.currentStop;
            if (!GameData.sohle3IntroPlayedOnce)
            {
                other.GetComponent<LiftSohleThree>().PlayAudio();
                other.GetComponent<LiftSohleThree>().StartTrain();
                GameData.sohle3IntroPlayedOnce = true;
            }
        }
        if (other.name == "TriggerAudioGabi" && (cave.moveDirection ==  CaveMovement.MoveDown))
        {
            sprechblaseController.sprechblaseGabi.SetAudioClip(other.GetComponent<AudioSource>().clip);
            sprechblaseController.sprechblaseGabi.gameObject.SetActive(true);
            sprechblaseController.sprechblaseGabi.SetSprechblaseInPlayingMode();
        }
        if (other.name == "TriggerAudioDad" && (cave.moveDirection == CaveMovement.MoveDown))
        {
            sprechblaseController.sprechblaseGabi.gameObject.SetActive(false);
            sprechblaseController.sprechblaseDad.gameObject.SetActive(true);
            sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<AudioSource>().clip);
            sprechblaseController.sprechblaseDad.SetSprechblaseInPlayingMode();
        }
    }

    //public Cave cave;
    //public SprechblaseController sprechblaseController;

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log(" .." + other.name);

    //    if (other.name == "TriggerEinstieg")
    //    {
    //        cave.StopCave();
    //        cave.currentStop = CurrentStop.Einstieg;
    //        GameData.currentStopSohle = (int)cave.currentStop;
    //        sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<AudioSource>().clip);
    //        sprechblaseController.sprechblaseDad.SetSprechblaseInNotPlayedMode();

    //        sprechblaseController.sprechblaseGabi.gameObject.SetActive(false);
            
    //        cave.liftBtns[0].GetComponent<CaveButton>().feedbackObject.SetActive(true);
    //        cave.liftBtns[0].gameObject.SetActive(true);
    //        cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
            
    //    }

    //    if (other.name == "TriggerSohle1" && cave.targetStop == CurrentStop.Sohle1)
    //    {
    //        Debug.Log("Stop Cave");
    //        cave.StopCave();
    //        cave.currentStop = CurrentStop.Sohle1;
    //        GameData.currentStopSohle = (int)cave.currentStop;

    //        other.GetComponent<LiftSohleOne>().PlayEnvironmentalSound();

    //        sprechblaseController.sprechblaseGabi.SetAudioClip(other.GetComponent<LiftSohleOne>().clip1);
    //        sprechblaseController.sprechblaseGabi.SetSprechblaseInPlayingMode();

    //        sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<LiftSohleOne>().clip2);
    //        sprechblaseController.sprechblaseDad.SetSprechblaseInNotPlayedMode();
    //        sprechblaseController.sprechblaseDad.gameObject.SetActive(true);
                       
    //    }

    //    if (other.name == "TriggerSohle2" && cave.targetStop == CurrentStop.Sohle2)
    //    {
    //        Debug.Log("Stop Cave");
    //        cave.StopCave();
    //        cave.currentStop = CurrentStop.Sohle2;
    //        GameData.currentStopSohle = (int)cave.currentStop;
    //        other.GetComponent<LiftSohleTwo>().PlayAudio();
    //    }
    //    if (other.name == "TriggerSohle3" && cave.targetStop == CurrentStop.Sohle3)
    //    {
    //        Debug.Log("Stop Cave");
    //        cave.StopCave();
    //        cave.currentStop = CurrentStop.Sohle3;
    //        GameData.currentStopSohle = (int)cave.currentStop;
    //        if (!GameData.sohle3IntroPlayedOnce)
    //        {
    //            other.GetComponent<LiftSohleThree>().PlayAudio();
    //            other.GetComponent<LiftSohleThree>().StartTrain();
    //            GameData.sohle3IntroPlayedOnce = true;
    //        }
    //    }
    //    if (other.name == "TriggerAudioGabi" && cave.moveDirecton < 0)
    //    {
    //        sprechblaseController.sprechblaseGabi.SetAudioClip(other.GetComponent<AudioSource>().clip);
    //        sprechblaseController.sprechblaseGabi.gameObject.SetActive(true);
    //        sprechblaseController.sprechblaseGabi.SetSprechblaseInPlayingMode();
    //    }
    //    if (other.name == "TriggerAudioDad" && cave.moveDirecton < 0)
    //    {
    //        sprechblaseController.sprechblaseGabi.gameObject.SetActive(false);
    //        sprechblaseController.sprechblaseDad.gameObject.SetActive(true);
    //        sprechblaseController.sprechblaseDad.SetAudioClip(other.GetComponent<AudioSource>().clip);
    //        sprechblaseController.sprechblaseDad.SetSprechblaseInPlayingMode();
    //    }
    //}

}
