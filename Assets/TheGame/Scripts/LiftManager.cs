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

    bool introPlayedOneTime = false;

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
            if (doorLeft.GetComponent<LiftDoor>().doorOpened)
            {
                doorLeft.GetComponent<LiftDoor>().CloseDoor();
                doorRight.GetComponent<LiftDoor>().CloseDoor();
            }
            else
            {
                doorLeft.GetComponent<LiftDoor>().OpenDoor();
                doorRight.GetComponent<LiftDoor>().OpenDoor();
            }
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

    public void StartDriving(int meters)
    {
        //GameObject go = Instantiate(prefabStollenVertical);
        //go.transform.position = new Vector3(4.6f, -10.7f, -6.8f);
        //go.GetComponent<Stollen>().StartMove();
    }

    private void Update()
    {
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
