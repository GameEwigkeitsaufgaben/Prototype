using UnityEngine;

public class LiftDoor : MonoBehaviour
{
    public Transform opendPosition;
    public float speed = 1f;
    public bool move = false;

    public bool doorOpened = false;
    private Vector3 targetPosition;
    private Vector3 closedPosition;
    

    private void Start()
    {
        closedPosition = gameObject.transform.position;
        targetPosition = closedPosition;
        
        gameObject.transform.position = opendPosition.transform.position;
        doorOpened = true;
        
        //Debug.Log(gameObject.name + " closed pos " + closedPosition);
        //Debug.Log(gameObject.name + " opened pos " + opendPosition.position);
        //Debug.Log(gameObject.name + " go pos " + transform.position);
        
        //Invoke("CloseDoor", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            if (transform.position == targetPosition)
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
        targetPosition = opendPosition.position;
    }
}
