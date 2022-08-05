//The SpeechmanagerChpTree, SpeechList and SpeechBubble script are esentially linked!!
//

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PeopleInScene))]
public class SpeechManagerChapThree : MonoBehaviour
{
    public SoTalkingList tlDemo, tlGrubenwasser, tlPumpstandorte, tlPumpAufbau,  tlMonitoring, tlChancenAnstieg, tlPolder;
    public bool playDemo, playGrubenwasser, playPumpstandorte, playPumpAufbau, playMonitoring, playChancenAnstieg, playPolder;
    private GameObject dad, enya, bergbauvertreter;

    private SpeechList speakDemo, speakGrubenwasser, speakPumpstandorte, speakPumpAufbau, speakMonitoring, speakChancenAnstieg, speakPolder;
    private Dictionary<string, SpeechList> speechDict = new Dictionary<string, SpeechList>();

    private AudioSource audioSrc;
    private SpeechList currentList = null;
    private SpeechBubble spBerbauvertreter, spDad, spEnya;
    public ManagerGrubenwasserhaltungAufbau manager;

    private void Awake()
    {
        tlDemo = Resources.Load<SoTalkingList>(GameData.NameCH3TLDemo);
        tlGrubenwasser = Resources.Load<SoTalkingList>(GameData.NameCH3TLGrubenwasser);
        tlPumpstandorte = Resources.Load<SoTalkingList>(GameData.NameCH3TLPumpenstandorte);
        tlPumpAufbau = Resources.Load<SoTalkingList>(GameData.NameCH3TLPumpenAufbau);
        tlMonitoring = Resources.Load<SoTalkingList>(GameData.NameCH3TLMonitoring);
        tlChancenAnstieg = Resources.Load<SoTalkingList>(GameData.NameCH3TLChancenAnstieg);
        tlPolder = Resources.Load<SoTalkingList>(GameData.NameCH3TLPolder);

        bergbauvertreter = GetComponent<PeopleInScene>().bergbauvertreter;
        dad = GetComponent<PeopleInScene>().dad;
        enya = GetComponent<PeopleInScene>().enya;
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
        AddToDict(speakChancenAnstieg, tlChancenAnstieg);
        AddToDict(speakPolder, tlPolder);

        spBerbauvertreter = bergbauvertreter.GetComponentInChildren<SpeechBubble>(true);
        spDad = dad.GetComponentInChildren<SpeechBubble>(true);
        spEnya = enya.GetComponentInChildren<SpeechBubble>(true);
    }

    private void AddToDict(SpeechList speechList, SoTalkingList tl)
    {
        speechList = gameObject.AddComponent<SpeechList>();
        Debug.Log(tl.listName);
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

    // Update is called once per frame
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

        if (currentList != null)
        {
            if (audioSrc.isPlaying) audioSrc.Stop();

            DisableAllSpeechlists();
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }

        if(spBerbauvertreter != null) spBerbauvertreter.gameObject.SetActive(GameData.bubbleOnBergbauvertreter);
        if(spDad != null) spDad.gameObject.SetActive(GameData.bubbleOnDad);
        if(spEnya != null) spEnya.gameObject.SetActive(GameData.bubbleOnEnvy);
    }
}
