using UnityEngine;
using System.Text.RegularExpressions;

public class SpeechList : MonoBehaviour
{
    public string listName;
    public AudioClip[] clips;
    public AudioSource audioSrc;
    bool playAll;
    int currentIndex = 0;
    bool resetBubbles;
    public bool finishedToogle;

    bool enableBubbleEnya, enableBubbleDad, enableBubbleMuseumGuide, enableBubbleGeorg;

    public void SetUpList(SoTalkingList list, AudioSource src)
    {
        listName = list.listName;
        audioSrc = src;
        audioSrc.playOnAwake = false;
        clips = list.orderedListOfAudioClips;
        Debug.Log("set audio src " + audioSrc.name);
    }

    public bool isPlaying()
    {
        return playAll;
    }

    public float GetClipLength()
    {
        return audioSrc.clip.length;
    }

    public void StopList()
    {
        currentIndex = clips.Length;
    }

    public void PlayAll()
    {
        playAll = true;
        currentIndex = 0;
    }

    private void ResetFlags()
    {
        playAll = false;
        currentIndex = 0;
        finishedToogle = false;
        Debug.Log("Reset all flags in speechlist;");
    }

    void ResetSpeechBubbles()
    {
        GameData.bubbleOnEnvy = false;
        GameData.bubbleOnDad = false;
        GameData.bubbleOnGeorg = false;
        GameData.bubbleOnMuseumGuide = false;
    }

    void SetSpeechBubbleFlagCharcters()
    {
        GameData.bubbleOnEnvy = false;
        GameData.bubbleOnDad = false;
        GameData.bubbleOnGeorg = false;
        GameData.bubbleOnMuseumGuide = false;

        if (audioSrc.clip.name.Contains("e"))
        {
            GameData.bubbleOnEnvy = true;
        }
        else if (audioSrc.clip.name.Contains("v"))
        {
            GameData.bubbleOnDad = true;
        }
        else if (audioSrc.clip.name.Contains("g"))
        {
            GameData.bubbleOnGeorg = true;
        }
        else if (audioSrc.clip.name.Contains("m"))
        {
            GameData.bubbleOnMuseumGuide = true;
        }
    }

    void Update()
    {
        if (audioSrc == null) return;
        
        if (!audioSrc.isPlaying)
        {
            ResetSpeechBubbles();
        }

        if (finishedToogle)
        {
            ResetFlags();
        }

        if (playAll && !audioSrc.isPlaying)
        {
            finishedToogle = true;
            if (currentIndex >= clips.Length) return;

            finishedToogle = false;
            audioSrc.clip = clips[currentIndex];
            
            if (audioSrc.clip == null) return;

            audioSrc.Play();
            SetSpeechBubbleFlagCharcters();

            currentIndex++;
        }
    }
}
