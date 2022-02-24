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

        if (GameScenes.ch01LongwallCutter == SwitchSceneManager.GetCurrentSceneName())
        {
            this.transform.position = myFirstplayerPos.transform.position;
        }
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
