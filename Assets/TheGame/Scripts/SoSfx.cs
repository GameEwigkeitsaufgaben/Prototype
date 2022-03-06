using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SfxConfig")]
public class SoSfx : ScriptableObject
{
    public AudioClip instaMenuBGmusicLoop;//AudioSource on PostManager in Scene ch01scene01-00-instaMenu
    public AudioClip coalmineVerschub, coalmineZecheWind, coalmineConveyorBelt;
    public AudioClip coalmineCaveMoveDoors, coalmineMoveCave, coalmineWindInTunnel;
    public AudioClip coalmineWorkingMachinesMetal, caolmineLader;
    public AudioClip caolmineSplashingWater;
    public AudioClip coalmineIncomingTrain, coalmineDepartingTrain, coalmineMovingTrain;
    public AudioClip coalmineLWC, coalmineMoveShields, coalmineCoalToConveyorBeltFalling, coalmineBreakingRock;

    public AudioClip museumBGMurmur;

    public AudioClip correctAnswer, incorrectAnswer, btnClick;

    private Dictionary<string, AudioSource> playingSourcesLoop = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioSource> playingSourcesOneShot = new Dictionary<string, AudioSource>();


    public void PlayClip(GameObject go, AudioClip clip)
    {
        go.GetComponent<AudioSource>().clip = clip;

        if (go.GetComponent<AudioSource>().loop)
        {
            if (!playingSourcesLoop.ContainsKey (go.GetComponent<AudioSource>().clip.name))
            playingSourcesLoop.Add(go.GetComponent<AudioSource>().clip.name, go.GetComponent<AudioSource>());
        }
        else
        {
            if (!playingSourcesOneShot.ContainsKey(go.GetComponent<AudioSource>().clip.name))
                playingSourcesOneShot.Add(go.GetComponent<AudioSource>().clip.name, go.GetComponent<AudioSource>());
        }

        go.GetComponent<AudioSource>().Play();
    }

    public void PlayClip(AudioSource audioSource, AudioClip clip)
    {
        audioSource.clip = clip;

        if (audioSource.loop)
        {
            if (!playingSourcesLoop.ContainsKey(audioSource.clip.name))
                playingSourcesLoop.Add(audioSource.clip.name, audioSource);
        }
        else
        {
            if (!playingSourcesOneShot.ContainsKey(audioSource.clip.name))
                playingSourcesOneShot.Add(audioSource.clip.name, audioSource);
        }

        Debug.Log("00 play audio: " +audioSource.name);
        audioSource.Play();
    }

    public void PlayClip(AudioClip clip)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            playingSourcesLoop[clip.name].Play();
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].Play();
        }
        else
        {
            Debug.LogError("Key not in loop or oneshot list");
        }
    }
    public bool IsInstaBGMusicPlaying()
    {
        return playingSourcesLoop[instaMenuBGmusicLoop.name].isPlaying;
    }

    public void StopClip(AudioClip clip)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            playingSourcesLoop[clip.name].Stop();
            //playingSourcesLoop.Remove(clip.name);
            Debug.Log(clip.name + "in loop list");
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].Stop();
            //playingSourcesOneShot.Remove(clip.name);
            Debug.Log(clip.name + "in oneshot list");
        }
        else
        {
            Debug.Log(clip.name + "not in loop or oneshot list");
        }
    }

    public void PauseClip(AudioClip clip)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            playingSourcesLoop[clip.name].Pause();
            //playingSourcesLoop.Remove(clip.name);
            Debug.Log(clip.name + "in loop list");
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].Pause();
            //playingSourcesOneShot.Remove(clip.name);
            Debug.Log(clip.name + "in oneshot list");
        }
        else
        {
            Debug.Log(clip.name + "not in loop or oneshot list");
        }
    }

    public void ReduceVolume(AudioClip clip, float value)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            playingSourcesLoop[clip.name].volume -= value;
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].volume -= value;
        }
        else
        {
            Debug.LogError(clip.name + "not in loop or oneshot list");
        }
    }

    public void IncreaseVolume(AudioClip clip, float value)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            playingSourcesLoop[clip.name].volume += value;
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].volume += value;
        }
        else
        {
            Debug.LogError(clip.name + "not in loop or oneshot list");
        }
    }
}
