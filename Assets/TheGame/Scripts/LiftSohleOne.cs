using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftSohleOne : MonoBehaviour
{
    public AudioClip clip1, clip2;
    bool playNextAudio = false;

    public void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
        Invoke("PlayClip2", 15f);
    }

    private void PlayClip2()
    {
        GetComponent<AudioSource>().clip = clip2;
        GetComponent<AudioSource>().Play();
    }
}
