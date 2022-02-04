//Move Cave with button up and down
//Close doors before moving
//if target == current stop open/close doors

using UnityEngine;

public class CaveMoveUpDownController : MonoBehaviour
{
    public Cave cave;

    void MoveCaveDoors()
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

    public void GoToSohle(int nextStopInt)
    {
        CurrentStop nextStop = (CurrentStop)nextStopInt;
        if (GameData.moveCave) return;
        if (cave.currentStop == nextStop)
        {
            MoveCaveDoors();
            return;
        }

        cave.targetStop = nextStop;

        if (cave.caveDoorsClosed)
        {
            switch (nextStop)
            {
                case CurrentStop.Einstieg: StartMovingToEinstieg(); break;
                case CurrentStop.Sohle1: StartMovingSohleOne(); break;
                case CurrentStop.Sohle2: StartMovingSohleTwo(); break;
                case CurrentStop.Sohle3: StartMovingSohleThree(); break;
            }
        }
        else
        {
            cave.CloseDoors();
            switch (nextStop)
            {   //Calls GoToSpohle1 after 2 sec auf;
                case CurrentStop.Einstieg: Invoke("StartMovingToEinstieg", 2f); break;
                case CurrentStop.Sohle1: Invoke("StartMovingSohleOne", 2f); break;
                case CurrentStop.Sohle2: Invoke("StartMovingSohleTwo", 2f); break;
                case CurrentStop.Sohle3: Invoke("StartMovingSohleThree", 2f); break;
            }
        }

        cave.liftBtns[(int)cave.currentStop].GetComponent<CaveButton>().feedbackObject.SetActive(false);
        cave.liftBtns[(int)cave.targetStop].GetComponent<CaveButton>().feedbackObject.SetActive(true);
    }

    public void StartMovingSohleOne()
    {
        cave.MoveTo(CurrentStop.Sohle1);
    }

    public void StartMovingToEinstieg()
    {
        cave.MoveTo(CurrentStop.Einstieg);
    }

    public void StartMovingSohleTwo()
    {
        cave.MoveTo(CurrentStop.Sohle2);
    }
    public void StartMovingSohleThree()
    {
        cave.MoveTo(CurrentStop.Sohle3);
    }
}
