using UnityEngine;

public class LiftSohleThree : MonoBehaviour
{
    public AudioClip track1Young, track2dad, track3Young, track4dad, track5Young, track6dad;
    public AudioClip environmentalSoundTrain;
    public AudioSource young1, dad2, young3, dad4, young5, dad6, envtrain;
    public Train train;

    // Start is called before the first frame update
    void Start()
    {
        envtrain = gameObject.AddComponent<AudioSource>();
        envtrain.clip = environmentalSoundTrain;
        young1 = gameObject.AddComponent<AudioSource>();
        young1.playOnAwake = false;
        young1.clip = track1Young;
        dad2 = gameObject.AddComponent<AudioSource>();
        dad2.playOnAwake = false;
        dad2.clip = track2dad;
        young3 = gameObject.AddComponent<AudioSource>();
        young3.playOnAwake = false;
        young3.clip = track3Young;
        dad4 = gameObject.AddComponent<AudioSource>();
        dad4.playOnAwake = false;
        dad4.clip = track4dad;
        young5 = gameObject.AddComponent<AudioSource>();
        young5.playOnAwake = false;
        young5.clip = track5Young;
        dad6 = gameObject.AddComponent<AudioSource>();
        dad6.playOnAwake = false;
        dad6.clip = track6dad;
    }

    public void StartTrain()
    {
        Invoke("SetTrainMove", dad2.clip.length + young1.clip.length + young3.clip.length);
    }

    private void SetTrainMove()
    {
        Debug.Log("sollte funken train start!!");
        train.StartTrainMoving();
    }

    public void PlayAudio()
    {
        young1.Play();
        dad2.PlayDelayed(young1.clip.length);
        young3.PlayDelayed(dad2.clip.length + young1.clip.length);
        dad4.PlayDelayed(dad2.clip.length + young1.clip.length + young3.clip.length);
        young5.PlayDelayed(dad4.clip.length + dad2.clip.length + young1.clip.length + young3.clip.length);
        dad6.PlayDelayed(young5.clip.length + dad4.clip.length + dad2.clip.length + young1.clip.length + young3.clip.length);
    }

}
