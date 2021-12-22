using UnityEngine;
using UnityEngine.UI;

public class LiftManager : MonoBehaviour
{
    public Cave cave;

    public AudioSource src11621Dad;
    public Button[] liftBtns;
    public Button exitScene;

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
            if (cave.caveDoorsClosed)
            {
                cave.OpenDoors();
            }
            else
            {
                cave.CloseDoors();
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

        if (cave.caveDoorsClosed)
        {
            GoToSohle1();
        }
        else
        {
            cave.CloseDoors();
            Invoke("GoToSohle1", 2f);
        }
    }

    private void GoToSohle1()
    {
        StartMoving();
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
