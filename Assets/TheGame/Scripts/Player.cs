using UnityEngine;

public class Player : MonoBehaviour
{
    private const string triggerPlayerInCave = "TriggerPlayerInCave";
    public Camera mainCam;
    public GameObject ankerObjToFollow;
    public bool followAnker = true;
    
    public GameObject myFirstplayerPos;

    private Vector3 offsetToAnkerObj;
    public GameObject targetObj = null;
    public float speed;
    
    public bool playerInCave = true;

    private void Start()
    {
            try
            {
                SetupPlayerOffsetToAnkerObj(ankerObjToFollow);
            }
            catch (System.Exception)
            {
                Debug.Log("No ANKER OBJ SET");
            }
    }

    public void SetPlayerRotation(float yaw, bool inclMainCamAdjustment)
    {
        mainCam.GetComponent<LookaroundWithMouse>().SetPlayerBodyRotation(yaw, inclMainCamAdjustment);
       
    }

    public void StorePlayerAtBahnsteigPositon()
    {
        GameData.playerPositonXatS3Bahnsteig = transform.position.x;
        GameData.playerPositonYatS3Bahnsteig = transform.position.y;
        GameData.playerPositonZatS3Bahnsteig = transform.position.z;
        Debug.Log("Write to gamedata players bahnsteig position: " + transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == triggerPlayerInCave)
        {
            playerInCave = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == triggerPlayerInCave)
        {
            playerInCave = false;
        }
    }

    public void SetTarget(GameObject target)
    {
        targetObj = target;
    }

    private void SetupPlayerOffsetToAnkerObj(GameObject ankerObj)
    {
        ankerObjToFollow = ankerObj;

        if (GameData.sohleToReload != (int)CoalmineStop.Sole3)
        {
            var tmpOffset = transform.position - ankerObj.transform.position;
            offsetToAnkerObj = tmpOffset;
            playerInCave = true;
            WritePlayerOffsetToAnkerObj();
        }
        else
        {
            offsetToAnkerObj = ReadPlayerOffsetToAnkerObj();
            playerInCave = false;
        }
    }

    private void WritePlayerOffsetToAnkerObj()
    {
        GameData.playerOffsetToAnkerObjX = offsetToAnkerObj.x;
        GameData.playerOffsetToAnkerObjY = offsetToAnkerObj.y;
        GameData.playerOffsetToAnkerObjZ = offsetToAnkerObj.z;
    }

    private Vector3 ReadPlayerOffsetToAnkerObj()
    {
        return new Vector3(GameData.playerOffsetToAnkerObjX,
            GameData.playerOffsetToAnkerObjY,
            GameData.playerOffsetToAnkerObjZ);
    }

    public  void RealoadPlayerAtS3Bahnsteig()
    {
        SetPlayerToS3ViewpointBahnsteigPosition();
        SetPlayerRotation(-64f, false);
    }

    public void ReloadPlayerAtS3Cave()
    {
        SetPlayerToAnkerPosition();
        SetPlayerRotation(0f, true);
    }

    public void SetPlayerToAnkerPosition()
    {
        transform.position = ankerObjToFollow.transform.position + ReadPlayerOffsetToAnkerObj();
    }

    public void SetPlayerToS3ViewpointBahnsteigPosition()
    {
        transform.position = new Vector3(
            GameData.playerPositonXatS3Bahnsteig, 
            GameData.playerPositonYatS3Bahnsteig, 
            GameData.playerPositonZatS3Bahnsteig);

        Debug.Log("Read Player Pos from Gamedata and set transform.postion (Bahnsteig): " + transform.position);
    }

    ////wird nicht mehr gebruacht Prüfen!!! Jetzt in Cave controller drinnen
    //public void MovePlayerToTargetObj()
    //{
    //    transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, speed * Time.deltaTime);
    //    Debug.Log("new position player: " + transform.position);
    //    if (Vector3.Distance(transform.position, targetObj.transform.position) < 0.01)
    //    {
    //        targetObj = null;
    //    }
    //}

    private void Update()
    {
        //Hinders the player to move the cave without the player being inside.
        if (playerInCave)
        {
            GameData.liftBtnsEnabled = true;
            if(GameData.currentStopSohle == (int)CoalmineStop.Sole3 && GameData.sohleToReload == (int)CoalmineStop.Sole3)
            {
                GameData.sohleToReload = (int)CoalmineStop.Unset;
            }
        }
        else
        {
            GameData.liftBtnsEnabled = false;
        }

        //Cam follows capsule Player when cave is moving
        if (followAnker && (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01Mine))
        {
            transform.position = ankerObjToFollow.transform.position + offsetToAnkerObj;
        }

    }
}
