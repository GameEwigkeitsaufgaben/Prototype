//Control the cave with cavebuttons move to up and down between stops (soles1-3 and the entry area)
//Before starting to move to the target stop, the doors of the cave will be closed.
//if the current stop and the target stop is equivalent the doors will be opened or closed, no movement will be done.

using UnityEngine;

public class CaveMoveUpDownController : MonoBehaviour
{
    //Methodnames to be used in Invoke call
    private const string StartMovingToEA = "StartMovingToEntryArea";
    private const string StartMovingToS1 = "StartMovingSoleOne";
    private const string StartMovingToS2 = "StartMovingSoleTwo";
    private const string StartMovingToS3 = "StartMovingSoleThree";
    
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

    public bool CheckNextStopInvalid()
    {
        //next stop is invalid if the cave is moving, or an ohter sole is already chosen.
        Debug.Log(GameData.moveCave + " cstop " + cave.currentStop + " tstop " +cave.targetStop) ;
        return GameData.moveCave || (cave.currentStop != cave.targetStop);
    }

    public void GoToStop(CoalmineStop nextStop)
    {
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
                case CoalmineStop.EntryArea: StartMovingToEntryArea(); break;
                case CoalmineStop.Sole1: StartMovingSoleOne(); break;
                case CoalmineStop.Sole2: StartMovingSoleTwo(); break;
                case CoalmineStop.Sole3: StartMovingSoleThree(); break;
            }
        }
        else
        {
            cave.CloseDoors();
            switch (nextStop)
            {   //Calls GoToSpohle1 after 2 sec auf;
                case CoalmineStop.EntryArea: Invoke(StartMovingToEA, 2f); break;
                case CoalmineStop.Sole1: Invoke(StartMovingToS1, 2f); break;
                case CoalmineStop.Sole2: Invoke(StartMovingToS2, 2f); break;
                case CoalmineStop.Sole3: Invoke(StartMovingToS3, 2f); break;
            }
        }

        cave.liftBtns[GameData.GetCurrentStop(cave.currentStop)].GetComponent<CaveButton>().DisableButtonSelected();
    }

    public void StartMovingSoleOne()
    {
        cave.MoveTo(CoalmineStop.Sole1);
    }

    public void StartMovingToEntryArea()
    {
        cave.MoveTo(CoalmineStop.EntryArea);
    }

    public void StartMovingSoleTwo()
    {
        cave.MoveTo(CoalmineStop.Sole2);
    }
    public void StartMovingSoleThree()
    {
        cave.MoveTo(CoalmineStop.Sole3);
    }
}
