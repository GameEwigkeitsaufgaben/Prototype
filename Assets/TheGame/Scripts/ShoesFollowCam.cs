using UnityEngine;

public class ShoesFollowCam : MonoBehaviour
{
    public Camera myCam;
    public GameObject shoes;

    void Update()
    {
       Vector3 rotationThisObj = gameObject.transform.localEulerAngles;
       gameObject.transform.localRotation = Quaternion.Euler(rotationThisObj.x, myCam.transform.localEulerAngles.y, rotationThisObj.z);
    }
}
