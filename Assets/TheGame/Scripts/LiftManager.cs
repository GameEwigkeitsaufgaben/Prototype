using UnityEngine;
using UnityEngine.UI;

public class LiftManager : MonoBehaviour
{
    public Cave cave;

    public AudioSource src11621Dad;
    public Button[] liftBtns;
    public Button exitScene;

    public GameObject triggerEinstieg;

    bool introPlayedOneTime = false;

    private void Start()
    {
        triggerEinstieg.SetActive(false); 

        foreach (Button y in liftBtns)
        {
            y.interactable = false;
        }
    }

    public void GoToEinstieg()
    {
        Debug.Log("Go To Einstieg  ---------------");
        if (GameData.moveCave) return;

        cave.targetStop = CurrentStop.Einstieg;

        if (cave.currentStop == CurrentStop.Einstieg)
        {
            if (cave.caveDoorsClosed)
            {
                cave.OpenDoors();
            }
            else
            {
                cave.CloseDoors();
            }
        }else if (cave.currentStop == CurrentStop.Sohle1)
        {
            GameData.moveCave = true;
            cave.MoveToEinstieg();
            triggerEinstieg.SetActive(true);
        }
    }

    public void PlayDaD1162Intro()
    {
        if (src11621Dad.isPlaying) return;

        src11621Dad.Play();

        introPlayedOneTime = true;

    }

    public void PlayAudioDadEGIntro(AudioSource src)
    {
        src.Play();
    }

    public void GoToSohle1()
    {
        Debug.Log("Go To SO1 ---------------");
        
        if (GameData.moveCave) return;
        
        cave.targetStop = CurrentStop.Sohle1;

        Debug.Log("WTF --------------- " + cave.currentStop);
        if (cave.currentStop == CurrentStop.Sohle1)
        {
            if (cave.caveDoorsClosed)
            {
                cave.OpenDoors();
            }
            else
            {
                cave.CloseDoors();
            }
        }
        else
        {
            //Start Moving, after doors closed
            if (cave.caveDoorsClosed)
            {
                StartMovingSohleOne();
            }
            else
            {
                cave.CloseDoors();
                Invoke("StartMovingSohleOne", 2f); //Ruft GoToSpohle1 after 2 sec auf;
            }
        }
        
        
    }

    public void StartMovingSohleOne()
    {
        cave.MoveToShole1();
        cave.GetComponent<CaveShake>().StartShake();
    }

    public void SohleTwo()
    {
        Debug.Log("SO2 ---------------");
        cave.GetComponent<CaveShake>().StopShake();
    }

    public void SohleThree()
    {
        Debug.Log("SO3 ---------------");
    }

    public void StartMoving() //"einstieg, sohle1, sohle2, sohle3"
    {
        GameData.moveCave = true;
    }

    private void Update()
    {
        if (cave.caveDoorsClosed && exitScene.interactable)
        {
            exitScene.interactable = false;
        }

        if (!cave.caveDoorsClosed && !exitScene.interactable)
        {
            exitScene.interactable = true;
        }

        if (introPlayedOneTime  && !src11621Dad.isPlaying)
            GameData.liftIntroDadDone = true;

        if (GameData.liftIntroDadDone)
        {
            foreach (Button y in liftBtns)
            {
                y.interactable = true;
            }

            GameData.liftIntroDadDone = false;
        }
    }
}
