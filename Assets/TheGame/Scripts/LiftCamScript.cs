using UnityEngine;

public class LiftCamScript : MonoBehaviour
{
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 180f; //Da Main Cam um 180 degree gedreht ist. 
    private float pitch = 0.0f;

    bool mouseDown = false;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown)
        {

            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
        
    }
}
