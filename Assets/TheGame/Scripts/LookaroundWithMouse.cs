using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LookaroundWithMouse : MonoBehaviour
{
    //Follow player
    public GameObject player;
    private Vector3 offset;

    //Mousecontrol
    public float speedH = 2.0f;
    public float speedV = 2.0f;

    private float yaw = 90f; //Da Main Cam um 180 degree gedreht ist. 
    private float pitch = 0.0f;

    bool mouseDown = false;
    bool setYaw = true;

    private void OnEnable()
    {

    }

    private void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {

        //transform.position = player.transform.position + offset;

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

    public void SetPlayerBodyRotation(float yaw)
    {
        transform.parent.transform.localRotation = Quaternion.Euler(0f, yaw , 0f);
        this.yaw = yaw;
        Debug.Log("set player bodypos");
    }
}
