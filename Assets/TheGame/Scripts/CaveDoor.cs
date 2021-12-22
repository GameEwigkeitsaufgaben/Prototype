using UnityEngine;

public class CaveDoor : MonoBehaviour
{
    public Transform opendPosition;
    public float speed = 1f;
    public bool move = false;

    public bool doorOpened = false;
    private Vector3 targetPosition;
    private Vector3 closedPosition;
    

    private void Start()
    {
        closedPosition = gameObject.transform.localPosition;
        targetPosition = closedPosition;
        
        gameObject.transform.localPosition = opendPosition.transform.localPosition;
        doorOpened = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
            
            if (transform.localPosition == targetPosition)
            {
                move = false;
            }
        }
    }
    public void CloseDoor()
    {
        move = true;
        doorOpened = false;
        targetPosition = closedPosition;
    }

    public void OpenDoor()
    {
        move = true;
        doorOpened = true;
        targetPosition = opendPosition.localPosition;
    }
}
