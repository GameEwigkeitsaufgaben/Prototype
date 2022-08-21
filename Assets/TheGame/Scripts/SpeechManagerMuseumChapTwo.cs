using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeopleInScene))]
public class SpeechManagerMuseumChapTwo : MonoBehaviour
{
    //https://www.youtube.com/watch?v=Wu46UAVlFL4 Subtitles?;

    public SoTalkingList
        tlSecSilent,
        tlMuseumIntroGrundwasser, 
        tlMuseumTVGrundwasserIntro, 
        tlMuseumTVGrundwasserOutro, 
        tlMuseumFliessPfadIntro, 
        tlMuseumExitZeche;

    public bool 
        playSecSilent,
        playMuseumGWIntro, 
        playMuseumGWTVIntro,
        playMuseumGWTVOutro,
        playMuseumFliesspfadIntro, 
        playMuseumExitZeche;
    
    private AudioSource audioSrc;

    public bool resetFin;

    private SpeechList 
        speakSilent,
        speakMuseumGWIntro, 
        speakMuseumGWTVIntro, 
        speakMuseumGWTVOutro, 
        speakMuseumFliesspfadIntro, 
        speakMuseumExitZeche;
   
    private Dictionary<string, SpeechList> speechDict = new Dictionary<string, SpeechList>();

    public bool showDictEntries;

    private SpeechList currentList = null;
    private SoChaptersRuntimeData runtimeDataChapters;

    private GameObject
        dad,
        georg,
        enya,
        guide;

    private SpeechBubble 
        spDad = null, 
        spEnya = null, 
        spGeorg = null,
        spGuide = null;

    private void Awake()
    {
        LoadTalkingListsMuseum();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    public bool IsAudioSRCPlaying()
    {
        return audioSrc.isPlaying;
    }

    public void LoadTalkingListsMuseum()
    {
        tlSecSilent = Resources.Load<SoTalkingList>(GameData.NameTLSecSilent);
        tlMuseumIntroGrundwasser = Resources.Load<SoTalkingList>(GameData.NameCH2TLMuseumGrundwasserIntro);
        tlMuseumTVGrundwasserIntro = Resources.Load<SoTalkingList>(GameData.NameCH2TLMuseumIntroTV);
        tlMuseumTVGrundwasserOutro = Resources.Load<SoTalkingList>(GameData.NameCH2TLMuseumOutroTV);
        tlMuseumFliessPfadIntro = Resources.Load<SoTalkingList>(GameData.NameCH2TLMuseumIntroFliesspfad);
        tlMuseumExitZeche = Resources.Load<SoTalkingList>(GameData.NameCH2TLMuseumOutroExitZeche);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();

        AddToDict(speakSilent, tlSecSilent);
        AddToDict(speakMuseumGWIntro, tlMuseumIntroGrundwasser);
        AddToDict(speakMuseumGWTVIntro, tlMuseumTVGrundwasserIntro);
        AddToDict(speakMuseumGWTVOutro, tlMuseumTVGrundwasserOutro);
        AddToDict(speakMuseumFliesspfadIntro, tlMuseumFliessPfadIntro);
        AddToDict(speakMuseumExitZeche, tlMuseumExitZeche);

        //dad = GetComponent<PeopleInScene>().dad;
        enya = GetComponent<PeopleInScene>().enya;
        georg = GetComponent<PeopleInScene>().georg;
        guide = GetComponent<PeopleInScene>().guide;
        if (guide != null) spGuide = guide.GetComponentInChildren<SpeechBubble>(true);
        if (enya != null) spEnya = enya.GetComponentInChildren<SpeechBubble>(true);
        if (georg != null) spGeorg = georg.GetComponentInChildren<SpeechBubble>(true);
    }

    private void AddToDict(SpeechList speechList, SoTalkingList tl)
    {
        speechList = gameObject.AddComponent<SpeechList>();
        speechList.SetUpList(tl, audioSrc);
        speechDict.Add(speechList.listName, speechList);
    }

    public void StopSpeaking()
    {
        audioSrc.Stop();

        foreach (var i in speechDict)
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
        speechDict[talkingListName].finishedToogle = false;
    }

    public bool IsTalkingListFinished(string talkingListName)
    {
        return speechDict[talkingListName].finishedToogle;
    }

    public float GetTalkingListOverallTimeInSec(string talkingListName)
    {
        return speechDict[talkingListName].GetTalkingListLentghSec();
    }

    public bool IsMusuemGWTVIntroFinished()
    {
        return speechDict[GameData.NameCH2TLMuseumIntroTV].finishedToogle;
    }

    public void ResetMusuemGWTVIntro()
    {
        speechDict[GameData.NameCH2TLMuseumIntroTV].finishedToogle = false;
    }

    //--------------Outro
    public bool IsMuseumOutroFinished()
    {
        return speechDict[GameData.NameTLMuseumOutro].finishedToogle;
    }

    public void ResetMuseumOutro()
    {
        speechDict[GameData.NameTLMuseumOutro].finishedToogle = false;
    }

    void Update()
    {
        if (showDictEntries)
        {
            foreach(var slist in speechDict)
            {
                Debug.Log(slist.Key+ " " + slist.Value.finishedToogle);
            }
        }

        if (playSecSilent)
        {
            currentList = speechDict[GameData.NameTLSecSilent];
            playSecSilent = false;
        }
        else if (playMuseumGWIntro)
        {
            currentList = speechDict[GameData.NameCH2TLMuseumGrundwasserIntro];
            playMuseumGWIntro = false;
        }
        else if (playMuseumGWTVIntro)
        {
            currentList = speechDict[GameData.NameCH2TLMuseumIntroTV];
            playMuseumGWTVIntro = false;
        }
        else if (playMuseumGWTVOutro)
        {
            currentList = speechDict[GameData.NameCH2TLMuseumOutroTV];
            playMuseumGWTVOutro = false;
        }
        else if (playMuseumFliesspfadIntro)
        {
            currentList = speechDict[GameData.NameCH2TLMuseumIntroFliesspfad];
            playMuseumFliesspfadIntro = false;
        }
        else if (playMuseumExitZeche)
        {
            currentList = speechDict[GameData.NameCH2TLMuseumOutroExitZeche];
            playMuseumExitZeche = false;
        }

        if (currentList != null)
        {
            if (audioSrc.isPlaying) audioSrc.Stop();

            runtimeDataChapters.DisableAllSpeechlists(speechDict);
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }

        if (spGuide != null) spGuide.gameObject.SetActive(GameData.bubbleOnMuseumGuide); 
        if (spEnya != null) spEnya.gameObject.SetActive(GameData.bubbleOnEnya);
    }
}
