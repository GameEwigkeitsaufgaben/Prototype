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

    public GameObject instaAudioSrc;

    public Dictionary<string, AudioSource> playingSourcesLoop = new Dictionary<string, AudioSource>();
    public Dictionary<string, AudioSource> playingSourcesOneShot = new Dictionary<string, AudioSource>();

    public bool EntryAreaSfxStarted = false, sole1SfxStarted = false, sole2SfxStarted = false, sole3SfxStarted = false;

    public void PlayClip(GameObject goWithAudioSrc, AudioClip clip)
    {
        goWithAudioSrc.GetComponent<AudioSource>().clip = clip;

        if (goWithAudioSrc.GetComponent<AudioSource>().loop)
        {
            if (!playingSourcesLoop.ContainsKey (goWithAudioSrc.GetComponent<AudioSource>().clip.name))
            playingSourcesLoop.Add(goWithAudioSrc.GetComponent<AudioSource>().clip.name, goWithAudioSrc.GetComponent<AudioSource>());
        }
        else
        {
            if (!playingSourcesOneShot.ContainsKey(goWithAudioSrc.GetComponent<AudioSource>().clip.name))
                playingSourcesOneShot.Add(goWithAudioSrc.GetComponent<AudioSource>().clip.name, goWithAudioSrc.GetComponent<AudioSource>());
        }

        goWithAudioSrc.GetComponent<AudioSource>().Play();
    }

    public void PlaySfxSole2()
    {
        if (sole2SfxStarted) return;
        Debug.Log("++++++++++++++++++++++ " + sole2SfxStarted);
        //wind is further looping
        sole1SfxStarted = false;
        sole2SfxStarted = true;
        StopClip(caolmineLader);
        StopClip(coalmineWorkingMachinesMetal);

        PlayClip(caolmineSplashingWater);
        Debug.Log("Water is playing  " +playingSourcesLoop[caolmineSplashingWater.name].isPlaying);
    }

    public void PlayClipsInSole1Sfx()
    {
        Debug.Log("++++++++++++++++++++++ " + sole1SfxStarted);
        if (sole1SfxStarted) return;
        
        Debug.Log("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        sole1SfxStarted = true;

        //wind
        if (!playingSourcesLoop[coalmineWindInTunnel.name].isPlaying)
        {
            IncreaseVolume(coalmineWindInTunnel, 0.4f);
        }

        //baukipper
        if (!playingSourcesLoop[coalmineWorkingMachinesMetal.name].isPlaying)
        {
            PlayClip(coalmineWorkingMachinesMetal);
        }

        //lader
        if (!playingSourcesLoop[caolmineLader.name].isPlaying)
        {
            PlayClip(caolmineLader);
        }
    }

    public void PlayClip(AudioSource audioSource, AudioClip clip)
    {
        SetClipByAddToDict(audioSource, clip);

        Debug.Log("00 play audio: " + audioSource.name);
        audioSource.Play();
    }

    public void SetClipByAddToDict(AudioSource audioSource, AudioClip clip)
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
            Debug.Log(clip.name + "in loop list, method stop clip");
            playingSourcesLoop[clip.name].Stop();
            //playingSourcesLoop.Remove(clip.name);
           
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
        }
        else if (playingSourcesOneShot.ContainsKey(clip.name))
        {
            playingSourcesOneShot[clip.name].Pause();
        }
        else
        {
            Debug.LogError(clip.name + " not in loop or oneshot list");
        }
    }

    public void ReduceVolume(AudioClip clip, float value)
    {
        if (playingSourcesLoop.ContainsKey(clip.name))
        {
            //if(playingSourcesLoop[clip.name] == null)
            //{}

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
