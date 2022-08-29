using UnityEngine;
using UnityEngine.UI;

public enum CoalmineStop
{
    Unset = -1,
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

    public GameObject hideS2Top;

    //public AudioSource wind;
    //public AudioSource cbelt;
    public AudioSource liftMovingSrc;
    private SoSfx sfx;

    private SoChapOneRuntimeData runtimeData;

    private CaveDoor leftDoor, rightDoor;
    private AudioSource sfxLeftDoor, sfxRightDoor;
    private Vector3 tmpVector3;

    private void Awake()
    {
        Debug.Log("CAVE AWAKE");
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        leftDoor = doorLeft.GetComponent<CaveDoor>();
        rightDoor = doorRight.GetComponent<CaveDoor>();
        sfxLeftDoor = leftDoor.GetComponent<AudioSource>();
        sfxRightDoor = rightDoor.GetComponent<AudioSource>();
        tmpVector3 = Vector3.zero;
    }

    private void Start()
    {
       
        if(runtimeData.currentCoalmineStop == CoalmineStop.Unset)
        {
            //Default setting: current Sole is Entry Area;
            runtimeData.currentCoalmineStop = CoalmineStop.EntryArea;
            currentStop = targetStop = runtimeData.currentCoalmineStop;
            liftBtns[0].GetComponent<CaveButton>().isSelected = true;

            //move to manager?!
            //sfx.PlayClip(wind, sfx.coalmineWindInTunnel);
            //sfx.ReduceVolume(sfx.coalmineWindInTunnel, 0.7f);

            //sfx.PlayClip(cbelt, sfx.coalmineConveyorBelt);
            //Ssfx.ReduceVolume(sfx.coalmineConveyorBelt, 0.8f);

            liftMovingSrc.clip = sfx.coalmineMoveCave;
        }
    }

    public void InitReachedStop(CoalmineStop reachedStop)
    {
        StopCave();
        
        moveDirection = CaveMovement.OnHold;

        if (caveDoorsClosed) OpenDoors();
        
        runtimeData.currentCoalmineStop = currentStop = reachedStop;

        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2)
        {
            hideS2Top.SetActive(true);
        }
        else
        {
            hideS2Top.SetActive(false);
        }
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
        leftDoor.CloseDoorAnim();
        rightDoor.CloseDoorAnim();
        caveDoorsClosed = true;
        PlayDoorMoveSound();
    }

    public void OpenDoors()
    {
        leftDoor.OpenDoorAnim();
        rightDoor.OpenDoorAnim();
        caveDoorsClosed = false;
        PlayDoorMoveSound();
    }

    private void PlayDoorMoveSound()
    {
        if (!sfxRightDoor.isPlaying &&
                   !sfxLeftDoor.isPlaying)
        {
            leftDoor.PlayMoveSfx();
            rightDoor.PlayMoveSfx();
        }
    }

    public bool GoToNextStopValid()
    {
        return currentStop != targetStop;
    }

    public void SetAllButtonsInteractable(bool enableButtons)
    {
        foreach (Button y in liftBtns)
        {
            y.interactable = enableButtons;
        }

        if(runtimeData != null) runtimeData.liftBtnsAllEnabled = enableButtons;
    }

    public void StopCave()
    {
        GameData.moveCave = false;
        gameObject.GetComponent<CaveShake>().StopShake();
        liftMovingSrc.Stop();
    }

    //public void ReloadCaveAtSohle3()
    //{
    //    currentStop = targetStop = CoalmineStop.Sole3;
    //    var tempPosCavePos = new Vector3(GameData.cavePosX, GameData.cavePosY, GameData.cavePosZ);

    //    gameObject.transform.position = tempPosCavePos;
    //    StopCave();
    //    if (caveDoorsClosed)
    //    {
    //        OpenDoors();
    //    }
    //}

    private void FixedUpdate()
    {
        if (GameData.moveCave)
        {
            GameData.moveDirection = (int)moveDirection;
            // No shake! transform.position += new Vector3(0, (int)moveDirection * caveSpeed*0.01f, 0);
            float tmpY = (int)moveDirection * caveSpeed * Mathf.Clamp(Time.deltaTime, -0.006f, 0.006f);
            tmpVector3.Set(0f, tmpY, 0f);
            transform.position += tmpVector3;
            //transform.position += new Vector3(0, (int)moveDirection * caveSpeed * Mathf.Clamp(Time.deltaTime, -0.006f, 0.006f), 0);
            return;
        }
    }

    private void Update()
    {
        runtimeData.CheckInteraction116Done();

        //if (GameData.moveCave)
        //{
        //    GameData.moveDirection = (int)moveDirection;
        //    // No shake! transform.position += new Vector3(0, (int)moveDirection * caveSpeed*0.01f, 0);
        //    float tmpY = (int)moveDirection * caveSpeed * Mathf.Clamp(Time.deltaTime, -0.006f, 0.006f);
        //    tmpVector3.Set(0f, tmpY, 0f);
        //    transform.position += tmpVector3;
        //    //transform.position += new Vector3(0, (int)moveDirection * caveSpeed * Mathf.Clamp(Time.deltaTime, -0.006f, 0.006f), 0);
        //    return;
        //}

        if (!runtimeData.replayEntryArea) return;

        if (!runtimeData.playerInsideCave)
        {
            SetAllButtonsInteractable(false);
        }
        else
        {
            
            SetAllButtonsInteractable(true);
        }
    }
}
