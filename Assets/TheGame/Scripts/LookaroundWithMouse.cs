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

    private float yaw = 90.0f; //Da Main Cam mit Body 90 Gragd gedreht ist. 
    private float pitch = 0.0f;

    bool mouseDown = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }

        if (mouseDown)
        {
            yaw += speedH * Input.GetAxis("Mouse X");
            pitch -= speedV * Input.GetAxis("Mouse Y");

            if(SceneManager.GetActiveScene().name == GameScenes.ch03Demo)
            {
                pitch = Mathf.Clamp(pitch, -12f, +55f);
            }
            else
            {
                pitch = Mathf.Clamp(pitch, -40f, +55f);
            }
            
            transform.localEulerAngles = new Vector3(pitch, yaw, 0.0f);
        }
    }

    public void SetPlayerBodyRotation(float yaw, bool inclMainCamOrientation)
    {
        transform.localEulerAngles = new Vector3(0.0f, yaw, 0.0f);

        this.yaw = yaw;
        Debug.Log("set player body rotation local euler angles");
    }
}
