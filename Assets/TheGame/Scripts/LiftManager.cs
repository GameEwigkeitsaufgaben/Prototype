using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftManager : MonoBehaviour
{
    public GameObject prefabStollenVertical;
    public GameObject cabine;

    public void Eg()
    {
        Debug.Log("EG ---------------");
    }

    public void PlayAudioDadEGIntro(AudioSource src)
    {
        src.Play();
    }

    public void SohleOne()
    {
        Debug.Log("SO1 ---------------");
        cabine.GetComponent<LiftCaveShake>().StartShake();
    }

    public void SohleTwo()
    {
        Debug.Log("SO2 ---------------");
        cabine.GetComponent<LiftCaveShake>().StopShake();
    }

    public void SohleThree()
    {
        Debug.Log("SO3 ---------------");
    }

    public void StartDriving(int meters)
    {
        GameObject go = Instantiate(prefabStollenVertical);
        go.transform.position = new Vector3(4.6f, -10.7f, -6.8f);
        //go.GetComponent<Stollen>().StartMove();
        
    }
}
