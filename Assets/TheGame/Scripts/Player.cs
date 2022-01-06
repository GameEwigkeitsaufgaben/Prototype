using UnityEngine;

public class Player : MonoBehaviour
{
    public Camera mainCam;
    public GameObject ankerObjToFollow;
    public bool followAnker = true;

    private Vector3 offsetToAnkerObj;
    public GameObject targetObj = null;
    public float speed;

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

    public void MovePlayerToTargetObj()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetObj.transform.position, speed * Time.deltaTime);
        Debug.Log("new position player: " + transform.position);
        if(Vector3.Distance(transform.position, targetObj.transform.position) < 0.01)
        {
            targetObj = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (followAnker)
        {
            transform.position = ankerObjToFollow.transform.position + offsetToAnkerObj;
        }

        if(targetObj != null)
        {
            followAnker = false;
            MovePlayerToTargetObj();
            Debug.Log("move player");
        }
        
    }
}
