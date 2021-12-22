using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftManager : MonoBehaviour
{
    //public GameObject prefabStollenVertical;
    public GameObject cave;
    public GameObject doorLeft;
    public GameObject doorRight;

    public AudioSource src11621Dad;
    public Button[] liftBtns;
    public Button exitScene;

    bool introPlayedOneTime = false;
    bool caveDoorsClosed = false;

    private void Start()
    {
        foreach (Button y in liftBtns)
        {
            y.interactable = false;
        }
    }

    public void GoToEinstieg()
    {
        if (gameObject.GetComponent<SwitchSceneManager>().GetSceneName() == "Scene1162")
        {
            OpenCloseDoors();
        }
    }

    private void OpenCloseDoors()
    {
        if (doorLeft.GetComponent<LiftDoor>().doorOpened)
        {
            doorLeft.GetComponent<LiftDoor>().CloseDoor();
            doorRight.GetComponent<LiftDoor>().CloseDoor();
            caveDoorsClosed = true;
        }
        else
        {
            doorLeft.GetComponent<LiftDoor>().OpenDoor();
            doorRight.GetComponent<LiftDoor>().OpenDoor();
            caveDoorsClosed = false;
        }
    }

    public void PlayDaD1162Intro()
    {
        if (src11621Dad.isPlaying) return;

        src11621Dad.Play();

        introPlayedOneTime = true;

    }

    public void Eg()
    {
        Debug.Log("EG ---------------");
    }

    public void PlayAudioDadEGIntro(AudioSource src)
    {
        src.Play();
    }

    public void SohleOne()
    {
        Debug.Log("SO1 ---------------");

        if (caveDoorsClosed)
        {
            GoToSohle1();
        }
        else
        {
            OpenCloseDoors();
            Invoke("GoToSohle1", 2f);
        }
    }

    private void GoToSohle1()
    {
        StartMoving("sohle1");
        cave.GetComponent<LiftCaveShake>().StartShake();
    }

    public void SohleTwo()
    {
        Debug.Log("SO2 ---------------");
        cave.GetComponent<LiftCaveShake>().StopShake();
    }

    public void SohleThree()
    {
        Debug.Log("SO3 ---------------");
    }

    public void StartMoving(string destination) //"einstieg, sohle1, sohle2, sohle3"
    {
        GameData.moveCave = true;
        //GameObject go = Instantiate(prefabStollenVertical);
        //go.transform.position = new Vector3(4.6f, -10.7f, -6.8f);
        //go.GetComponent<Stollen>().StartMove();
    }

    private void Update()
    {
        if (caveDoorsClosed && exitScene.interactable)
        {
            exitScene.interactable = false;
        }

        if (!caveDoorsClosed && !exitScene.interactable)
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
