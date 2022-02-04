using System.Collections.Generic;
using UnityEngine;

public class CaveSpeechManger : MonoBehaviour
{
    public SoTalkingList audiosEntryArea, audiosEntryAreaTriggerSchacht,
        audiosSole1,
        audiosSole2,
        audiosSole3WPBahnsteig, audiosSole3WPBewetterung, audiosSole3WPCave, audiosSole3WPOVMine,
        audiosTrainRide,
        ausiosMuseumHistoryMining, audiosMuseumMinerEquipment, audiosMuseumInfo, audiosMuseumCarbonification;

    public SpeechBubble spEnya, spDad, spGeorg, spMuseumGuide;
    //fortesting
    public bool playEntryArea, playSchacht,
        playSole1, 
        playSole2, 
        playSole3WPBahnsteig;

    SpeechList speakEntryArea, speakSchacht, speakSole1, speakSole2;

    SpeechList currentList = null;
    private List<AudioSource> listAudioSrcs = new List<AudioSource>();
    private AudioSource mySrc;

    // Start is called before the first frame update
    void Start()
    {
        mySrc = gameObject.AddComponent<AudioSource>();

        speakEntryArea = gameObject.AddComponent<SpeechList>();
        speakEntryArea.SetUpList(audiosEntryArea, mySrc);

        speakSchacht = gameObject.AddComponent<SpeechList>();
        speakSchacht.SetUpList(audiosEntryAreaTriggerSchacht, mySrc);

        speakSole1 = gameObject.AddComponent<SpeechList>();
        speakSole1.SetUpList(audiosSole1, mySrc);

        speakSole2 = gameObject.AddComponent<SpeechList>();
        speakSole2.SetUpList(audiosSole2, mySrc);

        DisableAllSpeechlists();
    }

    void DisableAllSpeechlists()
    {
        speakEntryArea.enabled = false;
        speakSchacht.enabled = false;
        speakSole1.enabled = false;
        speakSole2.enabled = false;
    }

    void EnableListAndPlayAll(SpeechList list)
    {
        DisableAllSpeechlists();

        if (mySrc.isPlaying)
        {
            mySrc.Stop();
        }

        list.enabled = true;
        list.PlayAll();
    }


    void Update()
    {
        if (playEntryArea)
        {
            currentList = speakEntryArea;
            playEntryArea = false;
        }
        else if (playSchacht)
        {
            currentList = speakSchacht;
            playSchacht = false;
        }
        else if (playSole1)
        {
            currentList = speakSole1;
            playSole1 = false;
        }
        else if (playSole2)
        {
            currentList = speakSole2;
            playSole2 = false;
        }

        if(currentList != null)
        {
            if (mySrc.isPlaying) mySrc.Stop();

            DisableAllSpeechlists();
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }
        else
        {
            Debug.Log("current list ist null");
        }

        spDad.gameObject.SetActive(GameData.bubbleOnDad);
        spEnya.gameObject.SetActive(GameData.bubbleOnEnvy);
        spGeorg.gameObject.SetActive(GameData.bubbleOnGeorg);
    }
}
