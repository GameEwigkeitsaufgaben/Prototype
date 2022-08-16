using System.Collections.Generic;
using UnityEngine;

public class SpeechManagerMuseumChapTwo : MonoBehaviour
{

    //https://www.youtube.com/watch?v=Wu46UAVlFL4 Subtitles?;

    public SoTalkingList audiosMuseumIntroGrundwasser, audiosMuseumTVGrundwasserIntro, audiosMuseumTVGrundwasserOutro, audiosMuseumFliessPfadIntro, audiosMuseumExitZeche;

    public bool playMuseumGWIntro, playMuseumGWTVIntro, playMuseumFliesspfadIntro, playMuseumExitZeche;
    
    private AudioSource mySrc;

    public bool resetFin;

    private SpeechList speakMuseumGWIntro, speakMuseumGWTVIntro, speakMuseumFliesspfadIntro, speakMuseumExitZeche;
   
    private Dictionary<string, SpeechList> mySpeechDict = new Dictionary<string, SpeechList>();

    public bool showDictEntries;

    private SpeechList currentList = null;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        LoadTalkingListsMuseum();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    public void LoadTalkingListsMuseum()
    {
        audiosMuseumIntroGrundwasser = Resources.Load<SoTalkingList>(GameData.NameTLMuseumGrundwasserIntro);
        audiosMuseumTVGrundwasserIntro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumIntroTV);
        audiosMuseumFliessPfadIntro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumIntroFliesspfad);
        audiosMuseumExitZeche = Resources.Load<SoTalkingList>(GameData.NameTLMuseumOutroExitZeche);
        //audiosMuseumTVGrundwasserOutroEnya, MuseumOutroGuide;
    }

    // Start is called before the first frame update
    void Start()
    {
        mySrc = gameObject.AddComponent<AudioSource>();

        speakMuseumGWIntro = gameObject.AddComponent<SpeechList>();
        speakMuseumGWIntro.SetUpList(audiosMuseumIntroGrundwasser, mySrc);
        mySpeechDict.Add(speakMuseumGWIntro.listName, speakMuseumGWIntro);

        speakMuseumGWTVIntro = gameObject.AddComponent<SpeechList>();
        speakMuseumGWTVIntro.SetUpList(audiosMuseumTVGrundwasserIntro, mySrc);
        mySpeechDict.Add(speakMuseumGWTVIntro.listName, speakMuseumGWTVIntro);

        speakMuseumFliesspfadIntro = gameObject.AddComponent<SpeechList>();
        speakMuseumFliesspfadIntro.SetUpList(audiosMuseumFliessPfadIntro, mySrc);
        mySpeechDict.Add(speakMuseumFliesspfadIntro.listName, speakMuseumFliesspfadIntro);

        speakMuseumExitZeche = gameObject.AddComponent<SpeechList>();
        speakMuseumExitZeche.SetUpList(audiosMuseumExitZeche, mySrc);
        mySpeechDict.Add(speakMuseumExitZeche.listName, speakMuseumExitZeche);
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

    public bool IsMusuemGWTVIntroFinished()
    {
        return mySpeechDict[GameData.NameTLMuseumIntroTV].finishedToogle;
    }

    public void ResetMusuemGWTVIntro()
    {
        mySpeechDict[GameData.NameTLMuseumIntroTV].finishedToogle = false;
    }

    //--------------Intro
    public bool IsMusuemInfoIntroFinished()
    {
        return mySpeechDict[GameData.NameTLMuseumInfoArrival].finishedToogle;
    }

    //public void ResetMusuemInfoIntro()
    //{
    //    mySpeechDict[GameData.NameTLMuseumInfoArrival].finishedToogle = false;
    //}


    //--------------Outro
    public bool IsMuseumOutroFinished()
    {
        return mySpeechDict[GameData.NameTLMuseumOutro].finishedToogle;
    }

    public void ResetMuseumOutro()
    {
        mySpeechDict[GameData.NameTLMuseumOutro].finishedToogle = false;
    }

    ////--------------Coalification
    //public bool IsMusuemCoalifictionFinished()
    //{
    //    return mySpeechDict[GameData.NameTLMuseumCoalification].finishedToogle;
    //}

    //public void ResetMusuemCoalification()
    //{
    //    mySpeechDict[GameData.NameTLMuseumCoalification].finishedToogle = false;
    //}

    ////--------------MinerEquipment
    //public bool IsMusuemMinerEquipmentFinished()
    //{
    //    return mySpeechDict[GameData.NameTLMuseumMinerEquipment].finishedToogle;
    //}

    //public void ResetMusuemMinerEquipment()
    //{
    //    mySpeechDict[GameData.NameTLMuseumMinerEquipment].finishedToogle = false;
    //}

    ////--------------HistroyCarbon
    //public bool IsMusuemHistroyCarbonFinished()
    //{
    //    return mySpeechDict[GameData.NameTLMuseumCarbonificationPeriod].finishedToogle;
    //}
    //public void ResetMusuemHistroyCarbon()
    //{
    //    mySpeechDict[GameData.NameTLMuseumCarbonificationPeriod].finishedToogle = false;
    //}

    ////-----------------HistoryMining
    //public bool IsMusuemHistoryMiningFinished()
    //{
    //    return mySpeechDict[GameData.NameTLMuseumHistoryMining].finishedToogle;
    //}

    //public void ResetMusuemHistoryMining()
    //{
    //    mySpeechDict[GameData.NameTLMuseumHistoryMining].finishedToogle = false;
    //}

    //void DisableAllSpeechlists()
    //{
    //    foreach(var slist in mySpeechDict)
    //    {
    //        slist.Value.finishedToogle = false;
    //    }
    //}

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
            mySpeechDict[GameData.NameTLMuseumInfoArrival].finishedToogle = false;
        }

        
        if (playMuseumGWIntro)
        {
            currentList = speakMuseumGWIntro;
            playMuseumGWIntro = false;
        }
        else if (playMuseumGWTVIntro)
        {
            currentList = speakMuseumGWTVIntro;
            playMuseumGWTVIntro = false;
        }
        else if (playMuseumFliesspfadIntro)
        {
            currentList = speakMuseumFliesspfadIntro;
            playMuseumFliesspfadIntro = false;
        }
        else if (playMuseumExitZeche)
        {
            currentList = speakMuseumExitZeche;
            playMuseumExitZeche = false;
        }

        runtimeDataChapters.XX(currentList, mySrc, mySpeechDict);

        //if (currentList != null)
        //{
        //    if (mySrc.isPlaying) mySrc.Stop();

        //    DisableAllSpeechlists();
        //    currentList.enabled = true;
        //    currentList.PlayAll();
        //    currentList = null;
        //}
    }
}
