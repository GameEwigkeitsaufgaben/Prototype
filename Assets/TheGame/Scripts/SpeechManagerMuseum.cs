using System;
using System.Collections.Generic;
using UnityEngine;

public class SpeechManagerMuseum : MonoBehaviour
{
    private const string InfoIntro = "Audios100160MuseumInfoArrival";
    private const string ListAudioMinerEquipment = "Audios100160MuseumMinerEquipment";
    private const string ListWorld = "Audios100160MuseumCarbonifictionPeriod";
    private const string ListCoalHistory = "Audios100160MuseumHistoryMining";
    private const string ListCarbonification = "Audios100160MuseumHistoryCarbonification";
    private const string ListOutro = "Audios100170MuseumOutro";

    public SoTalkingList audiosMuseumInfoArrival, audiosMuseumMinerEquipment, audiosMuseumWorld, audiosCoalHistory, audiosMuseumCarbonification, audiosMuseumOutro;

    public bool playMuseumInfoArrival, playMinerEquipment, playMuseumWorld, playMuseumCoalHistory, playMuseumCarbonification, playMuseumOutro;
    
    private AudioSource mySrc;

    public bool resetFin;

    private SpeechList
       speakMuseumIntro, speakMinerEquipment, speakMuseumWorld, speakMuseumCoalHistory, speakMuseumCarbonification, speakMuseumOutro;
   

    private Dictionary<string, SpeechList> mySpeechDict = new Dictionary<string, SpeechList>();

    public bool showDictEntries;

    private SpeechList currentList = null;

    // Start is called before the first frame update
    void Start()
    {
        mySrc = gameObject.AddComponent<AudioSource>();

        speakMuseumIntro = gameObject.AddComponent<SpeechList>();
        speakMuseumIntro.SetUpList(audiosMuseumInfoArrival, mySrc);
        mySpeechDict.Add(speakMuseumIntro.listName, speakMuseumIntro);

        speakMinerEquipment = gameObject.AddComponent<SpeechList>();
        speakMinerEquipment.SetUpList(audiosMuseumMinerEquipment, mySrc);
        mySpeechDict.Add(speakMinerEquipment.listName, speakMinerEquipment);

        speakMuseumWorld = gameObject.AddComponent<SpeechList>();
        speakMuseumWorld.SetUpList(audiosMuseumWorld, mySrc);
        mySpeechDict.Add(speakMuseumWorld.listName, speakMuseumWorld);

        speakMuseumCoalHistory = gameObject.AddComponent<SpeechList>();
        speakMuseumCoalHistory.SetUpList(audiosCoalHistory, mySrc);
        mySpeechDict.Add(speakMuseumCoalHistory.listName, speakMuseumCoalHistory);

        speakMuseumCarbonification = gameObject.AddComponent<SpeechList>();
        speakMuseumCarbonification.SetUpList(audiosMuseumCarbonification, mySrc);
        mySpeechDict.Add(speakMuseumCarbonification.listName, speakMuseumCarbonification);

        speakMuseumOutro = gameObject.AddComponent<SpeechList>();
        speakMuseumOutro.SetUpList(audiosMuseumOutro, mySrc);
        mySpeechDict.Add(speakMuseumOutro.listName, speakMuseumOutro);
    }

    public void StopSpeaking()
    {
        mySrc.Stop();

        foreach (var i in mySpeechDict)
        {
            if (i.Value.isPlaying())
            {
                i.Value.StopList();
            }
        }

    }

    public bool IsMuseumOutroFinished()
    {
        return mySpeechDict[ListOutro].finishedToogle;
    }

    public bool IsMusuemCoalifictionFinished()
    {
        return mySpeechDict[ListCarbonification].finishedToogle;
    }

    public bool IsMusuemCoalHistoryFinished()
    {
        return mySpeechDict[ListCoalHistory].finishedToogle;
    }

    public bool IsMusuemWorldFinished()
    {
        return mySpeechDict[ListWorld].finishedToogle;
    }

    public bool IsMusuemInfoIntroFinished()
    {
        return mySpeechDict[InfoIntro].finishedToogle;
    }

    public void ResetMuseumOutro()
    {
        mySpeechDict[ListOutro].finishedToogle = false;
    }

    public void ResetMusuemCarbonifiction()
    {
        mySpeechDict[ListCarbonification].finishedToogle = false;
    }

    public void ResetMusuemCoalHistory()
    {
        mySpeechDict[ListCoalHistory].finishedToogle = false;
    }

    public void ResetMusuemInfoIntro()
    {
        mySpeechDict[InfoIntro].finishedToogle = false;
    }

    public void ResetMusuemWorld()
    {
        mySpeechDict[ListWorld].finishedToogle = false;
    }

    public void ResetMusuemMinerEquipment()
    {
        mySpeechDict[ListAudioMinerEquipment].finishedToogle = false;
    }

    public bool IsMusuemMinerEquipmentFinished()
    {
        return mySpeechDict[ListAudioMinerEquipment].finishedToogle;
    }

    void DisableAllSpeechlists()
    {
        foreach(var slist in mySpeechDict)
        {
            slist.Value.finishedToogle = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (showDictEntries)
        {
            foreach(var slist in mySpeechDict)
            {
                Debug.Log(slist.Key+ " "+ slist.Value.finishedToogle);
            }
        }

        if (resetFin)
        {
            mySpeechDict[InfoIntro].finishedToogle = false;
        }

        if (playMuseumInfoArrival)
        {
            playMuseumInfoArrival = false;
            currentList = mySpeechDict[InfoIntro];
        }
        else if (playMinerEquipment)
        {
            currentList = speakMinerEquipment;
            playMinerEquipment = false;
        }
        else if (playMuseumWorld)
        {
            currentList = speakMuseumWorld;
            playMuseumWorld = false;
        }
        else if (playMuseumCoalHistory)
        {
            currentList = speakMuseumCoalHistory;
            playMuseumCoalHistory = false;
        }
        else if (playMuseumCarbonification)
        {
            currentList = speakMuseumCarbonification;
            playMuseumCarbonification = false;
        }
        else if (playMuseumOutro)
        {
            currentList = speakMuseumOutro;
            playMuseumOutro = false;
        }

        if (currentList != null)
        {
            if (mySrc.isPlaying) mySrc.Stop();

            DisableAllSpeechlists();
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }
    }
}
