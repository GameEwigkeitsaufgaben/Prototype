//The SpeechmanagerChpTree, SpeechList and SpeechBubble script are esentially linked!!
//

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeopleInScene))]
public class SpeechManagerChapThree : MonoBehaviour
{
    public SoTalkingList tlDemo, tlGrubenwasser, tlPumpstandorte, tlPumpAufbau, tlMonitoring, 
        tlEntlastungFluesse, tlGeothermie, tlLagerstaette, 
        tlPumpspeicherkraftwerke, tlRohstoffquelle, tlSauberesGW, tlWenigerGW, 
        tlPolder;
    public bool playDemo, playGrubenwasser, playPumpstandorte, playPumpAufbau, playMonitoring,
        playEntlastungFluesse, playGeothermie, playLagerstaette,
        playPumpspeicherkraftwerke, playRohstoffquelle, playSauberesGW, playWenigerGW,
        playPolder;
    private GameObject dad, georg, enya, bergbauvertreter1, bergbauvertreter2;

    private SpeechList speakDemo, speakGrubenwasser, speakPumpstandorte, speakPumpAufbau, speakMonitoring,
        speakEntlastungFluesse, speakGeothermie, speakLagerstaette,
        speakPumpspeicherkraftwerke, speakRohstoffquelle, speakSauberesGW, speakWenigerGW,
        speakPolder;
    private Dictionary<string, SpeechList> speechDict = new Dictionary<string, SpeechList>();

    private AudioSource audioSrc;
    private SpeechList currentList = null;
    private SpeechBubble spBerbauvertreter1 = null, spBerbauvertreter2 = null, spDad = null, spEnya = null, spGeorg = null;
    public ManagerGrubenwasserhaltungAufbau manager;

    private void Awake()
    {
        tlDemo = Resources.Load<SoTalkingList>(GameData.NameCH3TLDemo);
        tlGrubenwasser = Resources.Load<SoTalkingList>(GameData.NameCH3TLGrubenwasser);
        tlPumpstandorte = Resources.Load<SoTalkingList>(GameData.NameCH3TLPumpenstandorte);
        tlPumpAufbau = Resources.Load<SoTalkingList>(GameData.NameCH3TLPumpenAufbau);
        tlMonitoring = Resources.Load<SoTalkingList>(GameData.NameCH3TLMonitoring);
        tlEntlastungFluesse = Resources.Load<SoTalkingList>(GameData.NameCH3TLEntlastungFluesse);
        tlGeothermie = Resources.Load<SoTalkingList>(GameData.NameCH3TLGeothermie);
        tlLagerstaette = Resources.Load<SoTalkingList>(GameData.NameCH3TLLagerstaette);
        tlPumpspeicherkraftwerke = Resources.Load<SoTalkingList>(GameData.NameCH3TLPumpspeicherkraftwerke);
        tlRohstoffquelle = Resources.Load<SoTalkingList>(GameData.NameCH3TLRohstoffquelle);
        tlSauberesGW = Resources.Load<SoTalkingList>(GameData.NameCH3TLSauberesGW);
        tlWenigerGW = Resources.Load<SoTalkingList>(GameData.NameCH3TLWenigerGW);

        //tlChancenAnstieg = Resources.Load<SoTalkingList>(GameData.NameCH3TLChancenAnstieg);
        tlPolder = Resources.Load<SoTalkingList>(GameData.NameCH3TLPolder);

        bergbauvertreter1 = GetComponent<PeopleInScene>().bergbauvertreter1;
        bergbauvertreter2 = GetComponent<PeopleInScene>().bergbauvertreter2;


        dad = GetComponent<PeopleInScene>().dad;
        enya = GetComponent<PeopleInScene>().enya;
        georg = GetComponent<PeopleInScene>().georg;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();

        AddToDict(speakDemo, tlDemo);
        AddToDict(speakGrubenwasser, tlGrubenwasser);
        AddToDict(speakPumpstandorte, tlPumpstandorte);
        AddToDict(speakPumpAufbau, tlPumpAufbau);
        AddToDict(speakMonitoring, tlMonitoring);
        AddToDict(speakEntlastungFluesse, tlEntlastungFluesse);
        AddToDict(speakGeothermie, tlGeothermie);
        AddToDict(speakLagerstaette, tlLagerstaette);
        AddToDict(speakPumpspeicherkraftwerke, tlPumpspeicherkraftwerke);
        AddToDict(speakRohstoffquelle, tlRohstoffquelle);
        AddToDict(speakSauberesGW, tlSauberesGW);
        AddToDict(speakWenigerGW, tlWenigerGW);
        AddToDict(speakPolder, tlPolder);

        if (bergbauvertreter1 != null) spBerbauvertreter1 = bergbauvertreter1.GetComponentInChildren<SpeechBubble>(true);
        if (bergbauvertreter2 != null) spBerbauvertreter2 = bergbauvertreter2.GetComponentInChildren<SpeechBubble>(true);
        if (dad != null) spDad = dad.GetComponentInChildren<SpeechBubble>(true);
        if (enya != null) spEnya = enya.GetComponentInChildren<SpeechBubble>(true);
        if (georg != null) spGeorg = georg.GetComponentInChildren<SpeechBubble>(true);
    }

    private void AddToDict(SpeechList speechList, SoTalkingList tl)
    {
        speechList = gameObject.AddComponent<SpeechList>();
        speechList.SetUpList(tl, audioSrc);
        speechDict.Add(speechList.listName, speechList);
    }

    //Generic Reset, Finished
    public void ResetFinished(string talkingListName)
    {
        speechDict[talkingListName].finishedToogle = false;
    }

    public bool IsTalkingListFinished(string talkingListName)
    {
        return speechDict[talkingListName].finishedToogle;
    }

    private void DisableAllSpeechlists()
    {
        foreach (var slist in speechDict)
        {
            slist.Value.finishedToogle = false;
        }
    }

    void Update()
    {
        if (playGrubenwasser)
        {
            currentList = speechDict[GameData.NameCH3TLGrubenwasser];
            playGrubenwasser = false;
        }
        else if (playPumpAufbau)
        {
            currentList = speechDict[GameData.NameCH3TLPumpenAufbau];
            playPumpAufbau = false;
        }
        else if (playPumpstandorte)
        {
            currentList = speechDict[GameData.NameCH3TLPumpenstandorte];
            playPumpstandorte = false;
        }
        else if (playEntlastungFluesse)
        {
            currentList = speechDict[GameData.NameCH3TLEntlastungFluesse];
            playEntlastungFluesse = false;
        }
        else if (playGeothermie)
        {
            currentList = speechDict[GameData.NameCH3TLGeothermie];
            playGeothermie = false;
        }
        else if (playLagerstaette)
        {
            currentList = speechDict[GameData.NameCH3TLLagerstaette];
            playLagerstaette = false;
        }
        else if (playPumpspeicherkraftwerke)
        {
            currentList = speechDict[GameData.NameCH3TLPumpspeicherkraftwerke];
            playPumpspeicherkraftwerke = false;
        }        
        else if (playSauberesGW)
        {
            currentList = speechDict[GameData.NameCH3TLSauberesGW];
            playSauberesGW = false;
        }
        else if (playWenigerGW)
        {
            currentList = speechDict[GameData.NameCH3TLWenigerGW];
            playWenigerGW = false;
        }
        else if (playRohstoffquelle)
        {
            currentList = speechDict[GameData.NameCH3TLRohstoffquelle];
            playRohstoffquelle = false;
        }
        else if (playPolder)
        {
            currentList = speechDict[GameData.NameCH3TLPolder];
            playPolder = false;
        }
        else if (playMonitoring)
        {
            currentList = speechDict[GameData.NameCH3TLMonitoring];
            playMonitoring = false;
        }
        if (currentList != null)
        {
            if (audioSrc.isPlaying) audioSrc.Stop();

            DisableAllSpeechlists();
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }

        if(spBerbauvertreter1 != null) spBerbauvertreter1.gameObject.SetActive(GameData.bubbleOnBergbauvertreter);
        if(spBerbauvertreter2 != null) spBerbauvertreter2.gameObject.SetActive(GameData.bubbleOnBergbauvertreter);
        if(spDad != null) spDad.gameObject.SetActive(GameData.bubbleOnDad);
        if(spEnya != null) spEnya.gameObject.SetActive(GameData.bubbleOnEnvy);
        if (spGeorg != null) spGeorg.gameObject.SetActive(GameData.bubbleOnGeorg);
    }
}
