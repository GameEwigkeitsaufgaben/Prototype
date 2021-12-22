using UnityEngine;

public class Cave : MonoBehaviour
{
    public GameObject doorLeft;
    public GameObject doorRight;

    public bool caveDoorsClosed = false;

    void Update()
    {
        if (GameData.moveCave)
        {
            transform.position += new Vector3(0, -2 * Time.deltaTime, 0);
        }
    }

    public void CloseDoors()
    {
        doorLeft.GetComponent<LiftDoor>().CloseDoor();
        doorRight.GetComponent<LiftDoor>().CloseDoor();
        caveDoorsClosed = true;
    }

    public void OpenDoors()
    {
        doorLeft.GetComponent<LiftDoor>().OpenDoor();
        doorRight.GetComponent<LiftDoor>().OpenDoor();
        caveDoorsClosed = false;
    }

    public void StopCave()
    {
        GameData.moveCave = false;
    }
}
