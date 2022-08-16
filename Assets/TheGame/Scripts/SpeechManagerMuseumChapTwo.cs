using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeopleInScene))]
public class SpeechManagerMuseumChapTwo : MonoBehaviour
{

    //https://www.youtube.com/watch?v=Wu46UAVlFL4 Subtitles?;

    public SoTalkingList 
        tlMuseumIntroGrundwasser, 
        tlMuseumTVGrundwasserIntro, 
        tlMuseumTVGrundwasserOutro, 
        tlMuseumFliessPfadIntro, 
        tlMuseumExitZeche;

    public bool 
        playMuseumGWIntro, 
        playMuseumGWTVIntro,
        playMuseumGWTVOutro,
        playMuseumFliesspfadIntro, 
        playMuseumExitZeche;
    
    private AudioSource audioSrc;

    public bool resetFin;

    private SpeechList 
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

    public void LoadTalkingListsMuseum()
    {
        tlMuseumIntroGrundwasser = Resources.Load<SoTalkingList>(GameData.NameTLMuseumGrundwasserIntro);
        tlMuseumTVGrundwasserIntro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumIntroTV);
        tlMuseumTVGrundwasserOutro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumOutroTV);
        tlMuseumFliessPfadIntro = Resources.Load<SoTalkingList>(GameData.NameTLMuseumIntroFliesspfad);
        tlMuseumExitZeche = Resources.Load<SoTalkingList>(GameData.NameTLMuseumOutroExitZeche);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrc = gameObject.AddComponent<AudioSource>();

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

    public bool IsMusuemGWTVIntroFinished()
    {
        return speechDict[GameData.NameTLMuseumIntroTV].finishedToogle;
    }

    public void ResetMusuemGWTVIntro()
    {
        speechDict[GameData.NameTLMuseumIntroTV].finishedToogle = false;
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
                Debug.Log(slist.Key+ " "+ slist.Value.finishedToogle);
            }
        }
        
        if (playMuseumGWIntro)
        {
            currentList = speechDict[GameData.NameTLMuseumGrundwasserIntro];
            playMuseumGWIntro = false;
        }
        else if (playMuseumGWTVIntro)
        {
            currentList = speechDict[GameData.NameTLMuseumIntroTV];
            playMuseumGWTVIntro = false;
        }
        else if (playMuseumGWTVOutro)
        {
            currentList = speechDict[GameData.NameTLMuseumOutroTV];
            playMuseumGWTVOutro = false;
        }
        else if (playMuseumFliesspfadIntro)
        {
            currentList = speechDict[GameData.NameTLMuseumIntroFliesspfad];
            playMuseumFliesspfadIntro = false;
        }
        else if (playMuseumExitZeche)
        {
            currentList = speechDict[GameData.NameTLMuseumOutroExitZeche];
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
        
    }
}