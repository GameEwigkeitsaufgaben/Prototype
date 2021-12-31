using System;
using UnityEngine;

public enum CurrentStop
{
    Einstieg,
    Sohle1,
    Sohle2,
    Sohle3,
}

public class Cave : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;
    public bool caveDoorsClosed = false;
    
    public CurrentStop currentStop;
    public CurrentStop targetStop;
    public int moveDirecton;//-1 for moving down, +1 for moving up

    private void Start()
    {
        currentStop = CurrentStop.Einstieg;
    }

    void Update()
    {
        if (GameData.moveCave)
        {
            Debug.Log(moveDirecton + " movedir");
            transform.position += new Vector3(0, moveDirecton * 2 * Time.deltaTime, 0);
        }
    }

    public void MoveTo(CurrentStop tStop)
    {
        targetStop = tStop;
        moveDirecton = GetMoveDirection();
        GameData.moveCave = true;
        gameObject.GetComponent<CaveShake>().StartShake();

    }

    //public void MoveToShole1()
    //{
    //    targetStop = CurrentStop.Sohle1;
    //    moveDirecton = GetMoveDirection();
    //    GameData.moveCave = true;
    //    gameObject.GetComponent<CaveShake>().StartShake();  
    //}

    //public void MoveToEinstieg()
    //{
    //    targetStop = CurrentStop.Einstieg;
    //    moveDirecton = GetMoveDirection();
    //    GameData.moveCave = true;
    //    gameObject.GetComponent<CaveShake>().StartShake();
    //}

    private int GetMoveDirection()
    {
        int tmp = 0;

        switch (currentStop)
        {
            case CurrentStop.Einstieg:
                    tmp = -1;
                    break;
            case CurrentStop.Sohle1:
                if (targetStop == CurrentStop.Einstieg) tmp = 1;
                if ((targetStop == CurrentStop.Sohle2) || (targetStop == CurrentStop.Sohle3)) tmp = -1;
                break;
            case CurrentStop.Sohle2:
                if (targetStop == CurrentStop.Einstieg || (targetStop == CurrentStop.Sohle1)) tmp = 1;
                if (targetStop == CurrentStop.Sohle3) tmp = -1;
                break;
            case CurrentStop.Sohle3:
                    tmp = 1;
                    break;
        }

        return tmp;
    }

    public void CloseDoors()
    {
        doorLeft.GetComponent<CaveDoor>().CloseDoor();
        doorRight.GetComponent<CaveDoor>().CloseDoor();
        caveDoorsClosed = true;
    }

    public void OpenDoors()
    {
        doorLeft.GetComponent<CaveDoor>().OpenDoor();
        doorRight.GetComponent<CaveDoor>().OpenDoor();
        caveDoorsClosed = false;
    }

    public void StopCave()
    {
        GameData.moveCave = false;
        gameObject.GetComponent<CaveShake>().StopShake();
    }
}
