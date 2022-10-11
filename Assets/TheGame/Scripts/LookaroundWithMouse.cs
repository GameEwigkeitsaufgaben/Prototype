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
    bool s1outside = false, s2outside = false;

    public float helperYawLeft = 0.0f, helperYawRight= 0.0f;


    string activeScene = "";
    

    private void Start()
    {
        activeScene = SceneManager.GetActiveScene().name;
        if (activeScene == GameScenes.ch03Demo)
        {
            SetPlayerBodyRotation(0f, false);
        }

        if (activeScene == GameScenes.ch02gwReinigungAktiv)
        {
            SetPlayerBodyRotation(0f, false);
        }

        if (activeScene == GameScenes.ch02gwReinigungPassiv)
        {
            SetPlayerBodyRotation(-12f, false);
        }
    }

    public void SetS1OutsideLimits(bool inS1)
    {
        s1outside = inS1;
    }

    public void SetS2OutsideLimits(bool inS2)
    {
        s2outside = inS2;
    }

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

            Debug.Log(s1outside + "s1outside");

            if(activeScene == GameScenes.ch03Demo)
            {
                pitch = Mathf.Clamp(pitch, -12f, +55f);
            }
            else if (activeScene == GameScenes.ch02gwReinigungAktiv)
            {
                pitch = Mathf.Clamp(pitch, -12f, +55f);
                yaw = Mathf.Clamp(yaw, -40f, 55f);
            }
            else if (activeScene == GameScenes.ch02gwReinigungPassiv)
            {
                pitch = Mathf.Clamp(pitch, -12f, +55f);
                yaw = Mathf.Clamp(yaw, -40f, 7f);
            }
            else if (s1outside)
            {
                Debug.Log("in S1");
                pitch = Mathf.Clamp(pitch, -20f, +55f); ;
                yaw = Mathf.Clamp(yaw, -150f, 20f);
            }
            else if (s2outside)
            {
                Debug.Log("in S1");
                pitch = Mathf.Clamp(pitch, -8f, +55f); ;
                yaw = Mathf.Clamp(yaw, -150f, 66f);
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
    }
}
