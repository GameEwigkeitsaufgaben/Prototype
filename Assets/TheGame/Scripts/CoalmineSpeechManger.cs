using System.Collections.Generic;
using UnityEngine;

public class CoalmineSpeechManger : MonoBehaviour
{
    public SpeechBubble spEnya, spDad, spGeorg;

    //fortesting
    public bool
        playCaveIntro,
        playEntryArea,
        playSchachtS1,
        playSchacht,
        playSole1, playSole1Vp,
        playSole2, playSole2Badewannen,
        playSole3WPBahnsteig, playSole3WPBewetterung, playSole3WPCave, playSole3WPOVMine,
        playTrainRideIn, playTrainRideOut,
        playLongwallCutterBahnsteig, playLongwallCutterLongwallCutter;

    private SpeechList
        speakCaveIntro,
        speakEntryArea,
        speakSchacht,
        speakSchachtS1,
        speakSole1, speakSole1Vp,
        speakSole2, speakSole2Badewannen,
        speakBewetterung, speakSole3Bahnsteig, speakSole3Cave, speakSole3BoardOVmine,
        speakTrainRideIn, speakTrainRideOut,
        speakLongwallCutterBahnsteig, speakLongwallCutterLongwallCutter;

    private SpeechList currentList = null;
    private List<AudioSource> listAudioSrcs = new List<AudioSource>();
    private List<SpeechList> mySpeechLists = new List<SpeechList>();
    private Dictionary<string, SpeechList> mySpeechDict = new Dictionary<string, SpeechList>();

    private AudioSource mySrc;

    [Header("Assigned at Runtime")]
    [SerializeField]
    private SoTalkingList
        audiosCaveIntro,
        audiosEntryArea,
        audiosEntryAreaTriggerSchacht, audiosTriggerSchachtS1,
        audiosSole1, audiosSole1Vp, 
        audiosSole2, audiosSole2Badewannen,
        audiosSole3WPBahnsteig, audiosSole3WPBewetterung, audiosSole3WPCave, audiosSole3WPOVMine,
        audiosTrainRideIn, audiosTrainRideOut,
        audiosLongwallCutterBahnsteig, audioLongwallCutterLongwallCutter;


    public void LoadTalkingListsMine()
    {
        audiosCaveIntro = Resources.Load<SoTalkingList>(GameData.NameTLMineIntro);
        audiosEntryArea = Resources.Load<SoTalkingList>(GameData.NameTLMineEACave);
        audiosTriggerSchachtS1 = Resources.Load<SoTalkingList>(GameData.NameTLMineTriggerSchachtS1);
        audiosEntryAreaTriggerSchacht = Resources.Load<SoTalkingList>(GameData.NameTLMineEATriggerSchacht);
        audiosSole1 = Resources.Load<SoTalkingList>(GameData.NameTLMineS1Cave);
        audiosSole1Vp = Resources.Load<SoTalkingList>(GameData.NameTLMineS1Vp);
        audiosSole2Badewannen = Resources.Load<SoTalkingList>(GameData.NameTLMineS2VpBadewanne);
        audiosSole2 = Resources.Load<SoTalkingList>(GameData.NameTLMineS2Cave);
        audiosSole3WPBahnsteig = Resources.Load<SoTalkingList>(GameData.NameTLMineS3Bahnsteig);
        audiosSole3WPBewetterung = Resources.Load<SoTalkingList>(GameData.NameTLMineS3Bewetterung);
        audiosSole3WPCave = Resources.Load<SoTalkingList>(GameData.NameTLMineS3Cave);
        audiosSole3WPOVMine = Resources.Load<SoTalkingList>(GameData.NameTLMineS3OvMine);
        audiosTrainRideIn = Resources.Load<SoTalkingList>(GameData.NameTLMineS3TrainrideIn);
        audiosTrainRideOut = Resources.Load<SoTalkingList>(GameData.NameTLMineS3TrainrideOut);
        audiosLongwallCutterBahnsteig = Resources.Load<SoTalkingList>(GameData.NameTLMineLWCBahnsteig);
        audioLongwallCutterLongwallCutter = Resources.Load<SoTalkingList>(GameData.NameTLMineLWCCutter);
    }

    private void Awake()
    {
        LoadTalkingListsMine();
    }

    // Start is called before the first frame update
    void Start()
    {
        mySrc = gameObject.AddComponent<AudioSource>();

        speakCaveIntro = gameObject.AddComponent<SpeechList>();
        speakCaveIntro.SetUpList(audiosCaveIntro, mySrc);
        mySpeechLists.Add(speakCaveIntro);

        speakEntryArea = gameObject.AddComponent<SpeechList>();
        speakEntryArea.SetUpList(audiosEntryArea, mySrc);
        mySpeechLists.Add(speakEntryArea);

        speakSchachtS1 = gameObject.AddComponent<SpeechList>();
        speakSchachtS1.SetUpList(audiosTriggerSchachtS1, mySrc);
        mySpeechLists.Add(speakSchachtS1);

        speakSchacht = gameObject.AddComponent<SpeechList>();
        speakSchacht.SetUpList(audiosEntryAreaTriggerSchacht, mySrc);
        mySpeechLists.Add(speakSchacht);

        speakSole1 = gameObject.AddComponent<SpeechList>();
        speakSole1.SetUpList(audiosSole1, mySrc);
        mySpeechLists.Add(speakSole1);

        speakSole1Vp = gameObject.AddComponent<SpeechList>();
        speakSole1Vp.SetUpList(audiosSole1Vp, mySrc);
        mySpeechLists.Add(speakSole1Vp);

        speakSole2Badewannen = gameObject.AddComponent<SpeechList>();
        speakSole2Badewannen.SetUpList(audiosSole2Badewannen, mySrc);
        mySpeechLists.Add(speakSole2Badewannen);

        speakSole2 = gameObject.AddComponent<SpeechList>();
        speakSole2.SetUpList(audiosSole2, mySrc);
        mySpeechLists.Add(speakSole2);

        speakSole3Bahnsteig = gameObject.AddComponent<SpeechList>();
        speakSole3Bahnsteig.SetUpList(audiosSole3WPBahnsteig, mySrc);
        mySpeechLists.Add(speakSole3Bahnsteig);

        speakBewetterung = gameObject.AddComponent<SpeechList>();
        speakBewetterung.SetUpList(audiosSole3WPBewetterung, mySrc);
        mySpeechLists.Add(speakBewetterung);

        speakSole3BoardOVmine = gameObject.AddComponent<SpeechList>();
        speakSole3BoardOVmine.SetUpList(audiosSole3WPOVMine, mySrc);
        mySpeechLists.Add(speakSole3BoardOVmine);

        speakSole3Cave = gameObject.AddComponent<SpeechList>();
        speakSole3Cave.SetUpList(audiosSole3WPCave, mySrc);
        mySpeechLists.Add(speakSole3Cave);

        speakTrainRideIn = gameObject.AddComponent<SpeechList>();
        speakTrainRideIn.SetUpList(audiosTrainRideIn, mySrc);
        mySpeechLists.Add(speakTrainRideIn);

        speakTrainRideOut = gameObject.AddComponent<SpeechList>();
        speakTrainRideOut.SetUpList(audiosTrainRideOut, mySrc);
        mySpeechLists.Add(speakTrainRideOut);

        speakLongwallCutterBahnsteig = gameObject.AddComponent<SpeechList>();
        speakLongwallCutterBahnsteig.SetUpList(audiosLongwallCutterBahnsteig, mySrc);
        mySpeechLists.Add(speakLongwallCutterBahnsteig);

        speakLongwallCutterLongwallCutter = gameObject.AddComponent<SpeechList>();
        speakLongwallCutterLongwallCutter.SetUpList(audioLongwallCutterLongwallCutter, mySrc);
        mySpeechLists.Add(speakLongwallCutterLongwallCutter);

        for (int i = 0; i < mySpeechLists.Count; i++)
        {
            mySpeechDict.Add(mySpeechLists[i].listName, mySpeechLists[i]);
        }

        DisableAllSpeechlists();
    }

    void DisableAllSpeechlists()
    {
        for(int i = 0; i<mySpeechLists.Count; i++)
        {
            mySpeechLists[i].enabled = false;
        }
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

    public bool IsMineS1CaveTalkingFinished()
    {
        return mySpeechDict[GameData.NameTLMineS1Cave].finishedToogle;
    }

    public bool IsMineS1VpTalkingFinished()
    {
        return mySpeechDict[GameData.NameTLMineS1Vp].finishedToogle;
    }

    public bool IsMineEATalkingFinished()
    {
        return mySpeechDict[GameData.NameTLMineEACave].finishedToogle;
    }

    public bool IsTrainRideInTalkingFinished()
    {
        return mySpeechDict[GameData.NameTLMineS3TrainrideIn].finishedToogle;
    }

    public bool IsTrainRideOutTalkingFinished()
    {
        return mySpeechDict["Audios100160CaveTrainRideOut"].finishedToogle;
        //return mySpeechLists[9].finished;
    }

    public bool IsLWCBahnsteigFinished()
    {
        return mySpeechLists[10].finishedToogle;
    }

    public void ToggleTrainRideOutTalkingFinished()
    {
        mySpeechDict["Audios100160CaveTrainRideOut"].finishedToogle = !mySpeechDict["Audios100160CaveTrainRideOut"].finishedToogle;
    }

    public void ToggleTrainRideInTalkingFinished()
    {
        mySpeechDict["Audios100160CaveTrainRideIn"].finishedToogle = !mySpeechDict["Audios100160CaveTrainRideIn"].finishedToogle;
    }

    void Update()
    {
        if (playCaveIntro)
        {
            currentList = speakCaveIntro;
            playCaveIntro = false;
        }
        else if (playEntryArea)
        {
            currentList = speakEntryArea;
            playEntryArea = false;
        }
        else if (playSchacht)
        {
            currentList = speakSchacht;
            playSchacht = false;
        }
        else if (playSchachtS1)
        {
            currentList = speakSchachtS1;
            playSchachtS1 = false;
        }
        else if (playSole1)
        {
            currentList = speakSole1;
            playSole1 = false;
        }

        else if (playSole1Vp)
        {
            currentList = speakSole1Vp;
            playSole1Vp = false;
        }

        else if (playSole2Badewannen)
        {
            currentList = speakSole2Badewannen;
            playSole2Badewannen = false;
        }
        else if (playSole2)
        {
            currentList = speakSole2;
            playSole2 = false;
        }
        else if (playSole3WPBahnsteig)
        {
            currentList = speakSole3Bahnsteig;
            playSole3WPBahnsteig = false;
        }
        else if (playSole3WPBewetterung)
        {
            currentList = speakBewetterung;
            playSole3WPBewetterung = false;
        }
        else if (playSole3WPCave)
        {
            currentList = speakSole3Cave;
            playSole3WPCave = false;
        }
        else if (playSole3WPOVMine)
        {
            currentList = speakSole3BoardOVmine;
            playSole3WPOVMine = false;
        }
        else if (playTrainRideIn)
        {
            currentList = speakTrainRideIn;
            playTrainRideIn = false;
        }
        else if (playTrainRideOut)
        {
            currentList = speakTrainRideOut;
            playTrainRideOut = false;
        }
        else if (playLongwallCutterBahnsteig)
        {
            currentList = speakLongwallCutterBahnsteig;
            playLongwallCutterBahnsteig = false;
        }
        else if (playLongwallCutterLongwallCutter)
        {
            currentList = speakLongwallCutterLongwallCutter;
            playLongwallCutterLongwallCutter = false;
        }

        if (currentList != null)
        {
            if (mySrc.isPlaying) mySrc.Stop();

            DisableAllSpeechlists();
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }

        spDad.gameObject.SetActive(GameData.bubbleOnDad);
        spEnya.gameObject.SetActive(GameData.bubbleOnEnvy);
        spGeorg.gameObject.SetActive(GameData.bubbleOnGeorg);
    }
}
