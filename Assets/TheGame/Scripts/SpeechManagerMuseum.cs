using System.Collections.Generic;
using UnityEngine;

public class SpeechManagerMuseum : MonoBehaviour
{
    private const string InfoIntro = "Audios100160MuseumInfoArrival";
    public SoTalkingList audiosMuseumInfoArrival, audiosMuseumMinerEquipment;
    //audiosMuseumHistoryMining, audiosMuseumMinerEquipment, audiosMuseumInfo, audiosMuseumCarbonification;

    public bool playMuseumInfoArrival, playMinerEquipment;
    //playMuseumInfo, playMuseumCarbonification, MinerEquipment, HistoryMining;

    private AudioSource mySrc;

    public bool resetFin;

    private SpeechList
       speakMuseumIntro, speakMinerEquipment;
    //speakMuseumInfo, speakMuseumCarbonification, speakMinerEquipment, speakHistroyMining;

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

        //speakMuseumInfo = gameObject.AddComponent<SpeechList>();
        //speakMuseumInfo.SetUpList(audiosMuseumInfo, mySrc);
        //mySpeechLists.Add(speakMuseumInfo);

        //speakHistroyMining = gameObject.AddComponent<SpeechList>();
        //speakHistroyMining.SetUpList(audiosMuseumHistoryMining, mySrc);
        //mySpeechLists.Add(speakHistroyMining);

        //speakMuseumCarbonification = gameObject.AddComponent<SpeechList>();
        //speakMuseumCarbonification.SetUpList(audiosMuseumCarbonification, mySrc);
        //mySpeechLists.Add(speakMuseumCarbonification);

    }

    public bool IsMusuemInfoIntroFinished()
    {
        Debug.Log("In Museum finished intro:  " + mySpeechDict[InfoIntro].finishedToogle + " " + mySpeechDict[InfoIntro].listName + mySpeechDict[InfoIntro].GetInstanceID());
        return mySpeechDict[InfoIntro].finishedToogle;
    }
    public void ResetMusuemInfoIntro()
    {
        mySpeechDict[InfoIntro].finishedToogle = false;
        Debug.Log("finished after reset " + mySpeechDict[InfoIntro].finishedToogle + " " +mySpeechDict[InfoIntro].listName + mySpeechDict[InfoIntro].GetInstanceID());
    }
    public void ResetMusuemMinerEquipment()
    {
        mySpeechDict["Audios100160MuseumMinerEquipment"].finishedToogle = false;
    }

    public bool IsMusuemMinerEquipmentFinished()
    {
        return mySpeechDict["Audios100160MuseumMinerEquipment"].finishedToogle;
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
