using UnityEngine;
using UnityEngine.UI;

public enum CoalmineStop
{
    Outside,
    EntryArea,
    Sole1,
    Sole2,
    Sole3,
}

public enum CaveButtonsState
{
    Disabled,
    ToEnable,
    Enabled,
    ToDisable
}

public enum CaveMovement
{
    MoveDown = -1,
    MoveUp = 1,
    OnHold = 0,
}

public class Cave : MonoBehaviour
{
    public CaveColliderBottom caveTriggerBottom;

    public float caveSpeed = 2.0f;

    public GameObject doorLeft;
    public GameObject doorRight;
    public bool caveDoorsClosed = false;

    public Button[] liftBtns;

    public CoalmineStop currentStop;
    public CoalmineStop targetStop;
    public CaveMovement moveDirection;

    public AudioSource doorsMovingSrc;
    public AudioSource liftMovingSrc;

    private void Start()
    {
        currentStop = CoalmineStop.EntryArea;
        EnableButtons(true);
    }

    public void InitReachedStop(CoalmineStop reachedStop)
    {
        StopCave();
        currentStop = reachedStop;
        GameData.currentStopSohle = (int)currentStop;
    }

    public void StoreCavePosition()
    {
        GameData.cavePosX = transform.position.x;
        GameData.cavePosY = transform.position.y;
        GameData.cavePosZ = transform.position.z;
    }

    public void MoveTo(CoalmineStop tStop)
    {
        GameData.moveCave = true;
        targetStop = tStop;
        moveDirection = GetMoveDirection();
        gameObject.GetComponent<CaveShake>().StartShake();
        liftMovingSrc.Play();
    }

    private CaveMovement GetMoveDirection()
    {
        CaveMovement tmp = CaveMovement.OnHold;

        switch (currentStop)
        {
            case CoalmineStop.EntryArea:
                    tmp = CaveMovement.MoveDown;
                    break;
            case CoalmineStop.Sole1:
                if (targetStop == CoalmineStop.EntryArea) tmp = CaveMovement.MoveUp;
                if ((targetStop == CoalmineStop.Sole2) || (targetStop == CoalmineStop.Sole3)) tmp = CaveMovement.MoveDown;
                break;
            case CoalmineStop.Sole2:
                if (targetStop == CoalmineStop.EntryArea || (targetStop == CoalmineStop.Sole1)) tmp = CaveMovement.MoveUp;
                if (targetStop == CoalmineStop.Sole3) tmp = CaveMovement.MoveDown;
                break;
            case CoalmineStop.Sole3:
                    tmp = CaveMovement.MoveUp;
                    break;
        }

        return tmp;
    }

    public void CloseDoors()
    {
        doorLeft.GetComponent<CaveDoor>().CloseDoor();
        doorRight.GetComponent<CaveDoor>().CloseDoor();
        caveDoorsClosed = true;
        if (!doorsMovingSrc.isPlaying)
        {
            doorsMovingSrc.Play();
        }
    }

    public void EnableButtons(bool enableButtons)
    {
        foreach (Button y in liftBtns)
        {
            y.interactable = enableButtons;
        }

        GameData.liftBtnsEnabled = enableButtons;
    }

    public void OpenDoors()
    {
        doorLeft.GetComponent<CaveDoor>().OpenDoor();
        doorRight.GetComponent<CaveDoor>().OpenDoor();
        caveDoorsClosed = false;
        {
            doorsMovingSrc.Play();
        }
    }

    public void StopCave()
    {
        GameData.moveCave = false;
        gameObject.GetComponent<CaveShake>().StopShake();
        liftMovingSrc.Stop();
    }

    public void ReloadSohle3AsCurrent()
    {
        currentStop = targetStop = CoalmineStop.Sole3;
        var tempPos = new Vector3(GameData.cavePosX, GameData.cavePosY, GameData.cavePosZ);
        Debug.Log(tempPos);
        gameObject.transform.position = tempPos;
        StopCave();
        OpenDoors();
    }

    void Update()
    {
        if (GameData.moveCave)
        {
            //Debug.Log((int)moveDirection + " movedir");
            GameData.moveDirection = (int)moveDirection;
            transform.position += new Vector3(0, (int)moveDirection * caveSpeed * Time.deltaTime, 0);
        }

        if (GameData.liftBtnsEnabled)
        {
            EnableButtons(true);
        }
        else
        {
            EnableButtons(false);
        }
    }
}
