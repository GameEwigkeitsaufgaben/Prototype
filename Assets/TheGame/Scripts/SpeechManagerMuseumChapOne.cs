using System.Collections;
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

    private AudioSource mySrc;

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
        mySrc = gameObject.AddComponent<AudioSource>();

        speakMuseumInfoArrival = gameObject.AddComponent<SpeechList>();
        speakMuseumInfoArrival.SetUpList(audiosMuseumInfoArrival, mySrc);
        mySpeechDict.Add(speakMuseumInfoArrival.listName, speakMuseumInfoArrival);

        speakMinerEquipment = gameObject.AddComponent<SpeechList>();
        speakMinerEquipment.SetUpList(audiosMuseumMinerEquipment, mySrc);
        mySpeechDict.Add(speakMinerEquipment.listName, speakMinerEquipment);

        speakMuseumHistoryCarbon = gameObject.AddComponent<SpeechList>();
        speakMuseumHistoryCarbon.SetUpList(audiosMuseumHistoryCarbon, mySrc);
        mySpeechDict.Add(speakMuseumHistoryCarbon.listName, speakMuseumHistoryCarbon);

        speakMuseumHistoryMining = gameObject.AddComponent<SpeechList>();
        speakMuseumHistoryMining.SetUpList(audiosMuseumHistoryMining, mySrc);
        mySpeechDict.Add(speakMuseumHistoryMining.listName, speakMuseumHistoryMining);

        speakMuseumCoalification = gameObject.AddComponent<SpeechList>();
        speakMuseumCoalification.SetUpList(audiosMuseumCoalification, mySrc);
        mySpeechDict.Add(speakMuseumCoalification.listName, speakMuseumCoalification);

        speakMuseumOutro = gameObject.AddComponent<SpeechList>();
        speakMuseumOutro.SetUpList(audiosMuseumOutro, mySrc);
        mySpeechDict.Add(speakMuseumOutro.listName, speakMuseumOutro);
    }

    public void LoadTalkingListsMuseum()
    {
        audiosMuseumInfoArrival = Resources.Load<SoTalkingList>(GameData.NameTLMuseumInfoArrival);
        audiosMuseumMinerEquipment = Resources.Load<SoTalkingList>(GameData.NameTLMuseumMinerEquipment);
        audiosMuseumHistoryCarbon = Resources.Load<SoTalkingList>(GameData.NameTLMuseumCarbonificationPeriod);
        audiosMuseumHistoryMining = Resources.Load<SoTalkingList>(GameData.NameTLMuseumHistoryMining);
        audiosMuseumCoalification = Resources.Load<SoTalkingList>(GameData.NameTLMuseumCoalification);
        audiosMuseumOutro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumOutro);
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

    //Generic Reset, Finished
    public void ResetFinished(string talkingListName)
    {
        mySpeechDict[talkingListName].finishedToogle = false;
    }

    public bool IsTalkingListFinished(string talkingListName)
    {
        return mySpeechDict[talkingListName].finishedToogle;
    }


    // Update is called once per frame
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
            mySpeechDict[GameData.NameTLMuseumInfoArrival].finishedToogle = false;
        }

        if (playMuseumInfoArrival)
        {
            playMuseumInfoArrival = false;
            currentList = mySpeechDict[GameData.NameTLMuseumInfoArrival];
        }
        else if (playMinerEquipment)
        {
            currentList = speakMinerEquipment;
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

        runtimeDataChapters.XX(currentList, mySrc, mySpeechDict);
    }
}
