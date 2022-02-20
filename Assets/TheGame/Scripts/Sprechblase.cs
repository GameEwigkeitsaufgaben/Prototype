using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Sprechblase : MonoBehaviour
{
    public Button btnInteraction;
    public Sprite play, reload, talking;

    public bool isInteractable;

    private AudioSource audioSrc;
    private bool audioStarted = false;
    public bool PlayedOnceMode = false;

    public float GetClipLength()
    {
        return audioSrc.clip.length;
    }
   

    public void CreateAudioSource()
    {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }

    public void SetAudioClip(AudioClip audioClip)
    {
        audioSrc.clip = audioClip;
    }

    public AudioSource GetAudioSource()
    {
        return audioSrc;
    }

    public void SetSprechblaseInNotPlayedMode()
    {
        gameObject.SetActive(true);
        btnInteraction.GetComponent<Image>().sprite = play;
        btnInteraction.GetComponent<Button>().onClick.AddListener(ChangeButtonEventMethod);
    }

    void ChangeButtonEventMethod()
    {
        Debug.Log("Test button");
        if (audioSrc.isPlaying) return;

        //audioSrc.SetAudioClip(introDad);
        SetSprechblaseInPlayingMode();
    } 

    public void SetSprechblaseInPlayedOnceMode()
    {
        btnInteraction.GetComponent<Image>().sprite = reload;
    }

    public void SetSprechblaseInPlayingMode()
    {
        btnInteraction.GetComponent<Image>().sprite = talking;
        gameObject.SetActive(true);
        
        if (audioSrc.isPlaying) return;

        audioStarted = true;

        audioSrc.Play();
    }

    private void Update()
    {
       // Debug.Log(audioSrc.isPlaying + " started " + audioStarted);
        if(audioSrc != null) 
        {
            if(audioSrc.clip != null)
            {
                Debug.Log(audioSrc.clip.name);
            }
            
            Debug.Log(audioSrc.isPlaying + " started " + audioStarted);

            if (!audioSrc.isPlaying && audioStarted)
            {
                if ((CoalmineStop)GameData.currentStopSohle == CoalmineStop.EntryArea && !GameData.moveCave)
                {
                   // GameData.introPlayedOnce = true;
                    return;
                }

                gameObject.SetActive(false);
                audioStarted = false;
            }
     
        }
    }

}
