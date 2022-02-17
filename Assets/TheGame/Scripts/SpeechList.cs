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
    public bool finished;

    bool enableBubbleEnya, enableBubbleDad, enableBubbleMuseumGuide, enableBubbleGeorg;

    public void SetUpList(SoTalkingList list, AudioSource src)
    {
        listName = list.listName;
        //audioSrc = gameObject.AddComponent<AudioSource>();
        //audioSrc.playOnAwake = false;
        audioSrc = src;
        audioSrc.playOnAwake = false;
        clips = list.orderedListOfAudioClips;
    }

    public float GetClipLength()
    {
        return audioSrc.clip.length;
    }

    public void PlayAll()
    {
        playAll = true;
        currentIndex = 0;
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

       // Debug.Log(audioSrc.clip.name + " bubble on e " + GameData.bubbleOnEnvy + " v " + GameData.bubbleOnDad+ " g " + GameData.bubbleOnGeorg);
    }

    // Update is called once per frame
    void Update()
    {
        if (audioSrc == null) return;
        
        //Debug.Log("bubble on audiosource playing " +audioSrc.isPlaying);
        if (!audioSrc.isPlaying)
        {
            ResetSpeechBubbles();
        }

        if (playAll && !audioSrc.isPlaying)
        {
            
            finished = true;
            if (currentIndex >= clips.Length) return;
            
            finished = false;
            audioSrc.clip = clips[currentIndex];
            
            if (audioSrc.clip == null) return;

            audioSrc.Play();
            SetSpeechBubbleFlagCharcters();

            currentIndex++;
        }
    }
}
