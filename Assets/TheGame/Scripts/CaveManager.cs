using UnityEngine;
using UnityEngine.UI;

public class CaveManager : MonoBehaviour
{
    public Cave cave;
    public Player player;
    public SwitchSceneManager switchScene;

    public Button exitScene;
    public SprechblaseController sprechblaseController;

    public GameObject triggerEinstieg;

    bool introPlayedOneTime = false;
    

    private void Start()
    {
        //triggerEinstieg.SetActive(false);
        GameData.scene1162LoadedOnce = true;
    }

    public void SetTargetForPlayer(GameObject obj)
    {
        player.SetTarget(obj);
    }

    private void OnEnable()
    {
        Debug.Log("In On Enable");
        Debug.Log("sohle to reload " + GameData.sohleToReload);

        if((CurrentStop)GameData.sohleToReload == CurrentStop.Sohle3)
        {
            cave.ReloadSohle3AsCurrent();
            player.SetPlayerToAnkerPosition();
        }
    }

    public void GoToEinstieg()
    {
        Debug.Log("Go To Einstieg  ---------------");
        if (GameData.moveCave) return;

        cave.targetStop = CurrentStop.Einstieg;
        cave.liftBtns[(int)cave.currentStop].GetComponent<CaveButton>().feedbackObject.SetActive(false);
        cave.liftBtns[(int)cave.targetStop].GetComponent<CaveButton>().feedbackObject.SetActive(true);

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
        }
        else if (cave.currentStop == CurrentStop.Sohle1)
        {
            GameData.moveCave = true;
            cave.MoveTo(CurrentStop.Einstieg);
            triggerEinstieg.SetActive(true);
        }
    }

    public void GoToSohle1()
    {
        Debug.Log("Go To SO1 ---------------");

        if (GameData.moveCave) return;

        cave.targetStop = CurrentStop.Sohle1;

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
            if(cave.currentStop == CurrentStop.Einstieg)
            {
                sprechblaseController.sprechblaseDad.gameObject.SetActive(false);
            }
        }

        cave.liftBtns[(int)cave.currentStop].GetComponent<CaveButton>().feedbackObject.SetActive(false);
        cave.liftBtns[(int)cave.targetStop].GetComponent<CaveButton>().feedbackObject.SetActive(true);

    }
    public void GoToSohle2()
    {
        Debug.Log("Go To SO2 ---------------");

        if (GameData.moveCave) return;

        cave.targetStop = CurrentStop.Sohle2;

        Debug.Log("WTF --------------- " + cave.currentStop);
        if (cave.currentStop == CurrentStop.Sohle2)
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
                StartMovingSohleTwo();
            }
            else
            {
                cave.CloseDoors();
                Invoke("StartMovingSohleTwo", 2f); //Ruft GoToSpohle1 after 2 sec auf;
            }
        }
        cave.liftBtns[(int)cave.currentStop].GetComponent<CaveButton>().feedbackObject.SetActive(false);
        cave.liftBtns[(int)cave.targetStop].GetComponent<CaveButton>().feedbackObject.SetActive(true);
    }

    public void GoToSohle3()
    {
        Debug.Log("Go To SO3 ---------------");

        if (GameData.moveCave) return;

        cave.targetStop = CurrentStop.Sohle3;

        Debug.Log("WTF --------------- " + cave.currentStop);
        if (cave.currentStop == CurrentStop.Sohle3)
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
                StartMovingSohleThree();
            }
            else
            {
                cave.CloseDoors();
                Invoke("StartMovingSohleThree", 2f); //Ruft GoToSpohle1 after 2 sec auf;
            }
        }

        cave.liftBtns[(int)cave.currentStop].GetComponent<CaveButton>().feedbackObject.SetActive(false);
        cave.liftBtns[(int)cave.targetStop].GetComponent<CaveButton>().feedbackObject.SetActive(true);
    }

    public void GoToSohle3Kohlehobel()
    {
        switchScene.SwitchSceneWithTransition(GameData.scene1165Blackscreen);
        GameData.sohleToReload = (int)CurrentStop.Sohle3;
        cave.StoreCavePosition();
        
    }

    public void StartMovingSohleOne()
    {
        cave.MoveTo(CurrentStop.Sohle1);
    }

    public void StartMovingSohleTwo()
    {
        cave.MoveTo(CurrentStop.Sohle2);
    }
    public void StartMovingSohleThree()
    {
        cave.MoveTo(CurrentStop.Sohle3);
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

    private void OnDestroy()
    {
        GameData.liftBtnsEnabled = false;
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
    }
}
