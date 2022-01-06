using UnityEngine;

public class CaveColliderBottom : MonoBehaviour
{
    public Cave cave;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerEinstieg")
        {
            cave.StopCave();
            cave.currentStop = CurrentStop.Einstieg;
        }
        Debug.Log(" .." +other.name);
        if (other.name == "TriggerSohle1" && cave.targetStop == CurrentStop.Sohle1)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle1;

            other.GetComponent<LiftSohleOne>().PlayAudio();

        }
        if (other.name == "TriggerSohle2" && cave.targetStop == CurrentStop.Sohle2)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle2;
            other.GetComponent<LiftSohleTwo>().PlayAudio();
        }
        if (other.name == "TriggerSohle3" && cave.targetStop == CurrentStop.Sohle3)
        {
            Debug.Log("Stop Cave");
            cave.StopCave();
            cave.currentStop = CurrentStop.Sohle3;
            if (!GameData.sohle3IntroPlayedOnce)
            {
                other.GetComponent<LiftSohleThree>().PlayAudio();
                other.GetComponent<LiftSohleThree>().StartTrain();
                GameData.sohle3IntroPlayedOnce = true;
            }
        }
        if (other.name == "TriggerAudioGabi" && cave.moveDirecton < 0)
        {
            other.GetComponent<AudioSource>().Play();
        }
        if (other.name == "TriggerAudioDad" && cave.moveDirecton < 0)
        {
            other.GetComponent<AudioSource>().Play();
        }
    }
}
