using UnityEngine;

public class LiftSohleOne : MonoBehaviour
{
    public AudioClip clip1, clip2, clipEnvironment;
    //bool playNextAudio = false;

    private AudioSource srcEnvironment;
    private AudioSource srcTalk;

    private void Start()
    {
        srcTalk = GetComponent<AudioSource>();
        srcEnvironment = gameObject.AddComponent<AudioSource>();
        srcEnvironment.clip = clipEnvironment;
        srcEnvironment.loop = true;
        srcEnvironment.playOnAwake = false;
    }

    public void PlayEnvironmentalSound()
    {
        srcEnvironment.Play();
    }

    public void PlayAudio()
    {
        srcTalk.Play();
        Invoke("PlayClip2", 15f);
    }

    private void PlayClip2()
    {
        srcTalk.clip = clip2;
        srcTalk.Play();
    }
}
