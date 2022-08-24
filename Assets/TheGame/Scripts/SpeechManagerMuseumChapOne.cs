using System.Collections.Generic;
using UnityEngine;

public class SpeechManagerMuseumChapOne : MonoBehaviour
{
    public SoTalkingList 
        audiosMuseumInfoArrival, 
        audiosMuseumMinerEquipment, 
        audiosMuseumHistoryCarbon, 
        audiosMuseumHistoryMining, 
        audiosMuseumCoalification, 
        audiosMuseumOutro;
    
    public bool 
        playMuseumInfoArrival, 
        playMinerEquipment, 
        playMuseumWorld, 
        playMuseumCoalHistory, 
        playMuseumCarbonification, 
        playMuseumOutro;

    public bool resetFin;

    private AudioSource audioSrc;

    private SpeechList
        speakMuseumInfoArrival, 
        speakMinerEquipment, 
        speakMuseumHistoryCarbon, 
        speakMuseumHistoryMining, 
        speakMuseumCoalification, 
        speakMuseumOutro;

    private Dictionary<string, SpeechList> mySpeechDict = new Dictionary<string, SpeechList>();

    public bool showDictEntries;

    private SpeechList currentList = null;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        LoadTalkingListsMuseum();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();

        speakMuseumInfoArrival = gameObject.AddComponent<SpeechList>();
        speakMuseumInfoArrival.SetUpList(audiosMuseumInfoArrival, audioSrc);
        mySpeechDict.Add(speakMuseumInfoArrival.listName, speakMuseumInfoArrival);

        speakMinerEquipment = gameObject.AddComponent<SpeechList>();
        speakMinerEquipment.SetUpList(audiosMuseumMinerEquipment, audioSrc);
        mySpeechDict.Add(speakMinerEquipment.listName, speakMinerEquipment);

        speakMuseumHistoryCarbon = gameObject.AddComponent<SpeechList>();
        speakMuseumHistoryCarbon.SetUpList(audiosMuseumHistoryCarbon, audioSrc);
        mySpeechDict.Add(speakMuseumHistoryCarbon.listName, speakMuseumHistoryCarbon);

        speakMuseumHistoryMining = gameObject.AddComponent<SpeechList>();
        speakMuseumHistoryMining.SetUpList(audiosMuseumHistoryMining, audioSrc);
        mySpeechDict.Add(speakMuseumHistoryMining.listName, speakMuseumHistoryMining);

        speakMuseumCoalification = gameObject.AddComponent<SpeechList>();
        speakMuseumCoalification.SetUpList(audiosMuseumCoalification, audioSrc);
        mySpeechDict.Add(speakMuseumCoalification.listName, speakMuseumCoalification);

        speakMuseumOutro = gameObject.AddComponent<SpeechList>();
        speakMuseumOutro.SetUpList(audiosMuseumOutro, audioSrc);
        mySpeechDict.Add(speakMuseumOutro.listName, speakMuseumOutro);
    }

    public void LoadTalkingListsMuseum()
    {
        audiosMuseumInfoArrival = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumInfoArrival);
        audiosMuseumMinerEquipment = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumMinerEquipment);
        audiosMuseumHistoryCarbon = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumCarbonificationPeriod);
        audiosMuseumHistoryMining = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumHistoryMining);
        audiosMuseumCoalification = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumCoalification);
        audiosMuseumOutro = Resources.Load<SoTalkingList>(GameData.NameCH1TLMuseumOutro);
    }

    public float GetTalkingListOverallTimeInSec(string talkingListName)
    {
        return mySpeechDict[talkingListName].GetTalkingListLentghSec();
    }
    public void StopSpeaking()
    {
        audioSrc.Stop();

        foreach (var i in mySpeechDict)
        {
            if (i.Value.isPlaying())
            {
                i.Value.StopList();
            }
        }
    }

    //Generic Reset, Finished
    public void ResetFinished(string talkingListName)
    {
        mySpeechDict[talkingListName].finishedToogle = false;
    }

    public bool IsTalkingListFinished(string talkingListName)
    {
        return mySpeechDict[talkingListName].finishedToogle;
    }

    void Update()
    {
        if (showDictEntries)
        {
            foreach (var slist in mySpeechDict)
            {
                Debug.Log(slist.Key + " " + slist.Value.finishedToogle);
            }
        }

        if (resetFin)
        {
            mySpeechDict[GameData.NameCH1TLMuseumInfoArrival].finishedToogle = false;
        }

        if (playMuseumInfoArrival)
        {
            playMuseumInfoArrival = false;
            currentList = mySpeechDict[GameData.NameCH1TLMuseumInfoArrival];
        }
        else if (playMinerEquipment)
        {
            currentList = mySpeechDict[GameData.NameCH1TLMuseumMinerEquipment];
            playMinerEquipment = false;
        }
        else if (playMuseumWorld)
        {
            currentList = speakMuseumHistoryCarbon;
            playMuseumWorld = false;
        }
        else if (playMuseumCoalHistory)
        {
            currentList = speakMuseumHistoryMining;
            playMuseumCoalHistory = false;
        }
        else if (playMuseumCarbonification)
        {
            currentList = speakMuseumCoalification;
            playMuseumCarbonification = false;
        }
        else if (playMuseumOutro)
        {
            currentList = speakMuseumOutro;
            playMuseumOutro = false;
        }

        if (currentList != null)
        {
            if (audioSrc.isPlaying) audioSrc.Stop();

            runtimeDataChapters.DisableAllSpeechlists(mySpeechDict);
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }
    }
}
