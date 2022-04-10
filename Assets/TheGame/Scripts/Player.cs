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

    public void SetPlayerBodyRotation(float yaw)
    {
        mainCam.GetComponent<LookaroundWithMouse>().SetPlayerBodyRotation(yaw);
    }

    public void StorePlayerPos()
    {
        GameData.playerPosX = transform.position.x;
        GameData.playerPoxY = transform.position.y;
        GameData.playerPosZ = transform.position.z;
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
        var tmpOffset = transform.position - ankerObj.transform.position;

        offsetToAnkerObj = tmpOffset;

        WritePlayerOffsetToAnkerObj();
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

    public void SetPlayerToAnkerPosition()
    {
        transform.position = ankerObjToFollow.transform.position + ReadPlayerOffsetToAnkerObj();
    }

    public void SetPlayerPos()
    {
        transform.position = new Vector3(GameData.playerPosX, GameData.playerPoxY, GameData.playerPosZ);
        mainCam.GetComponent<LookaroundWithMouse>().SetPlayerBodyRotation(20f);
        //mainCam.transform.localRotation = Quaternion.Euler(0f,-60f,0f);
        Debug.Log("Player pos after reload: " + transform.position);
    }

    ////wird nicht mehr gebruacht Pr�fen!!! Jetzt in Cave controller drinnen
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
