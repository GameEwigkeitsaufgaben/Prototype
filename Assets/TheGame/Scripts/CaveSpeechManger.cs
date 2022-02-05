using System.Collections.Generic;
using UnityEngine;

public class CaveSpeechManger : MonoBehaviour
{
    public SoTalkingList
        audiosEntryArea,
        audiosEntryAreaTriggerSchacht,
        audiosSole1,
        audiosSole2,
        audiosSole3WPBahnsteig, audiosSole3WPBewetterung, audiosSole3WPCave, audiosSole3WPOVMine,
        audiosTrainRide,
        audiosLongwallCutterBahnsteig, audioLongwallCutterLongwallCutter;
        //audiosMuseumHistoryMining, audiosMuseumMinerEquipment, audiosMuseumInfo, audiosMuseumCarbonification;

    public SpeechBubble spEnya, spDad, spGeorg, spMuseumGuide;
    //fortesting
    public bool
        playEntryArea,
        playSchacht,
        playSole1,
        playSole2,
        playSole3WPBahnsteig, playSole3WPBewetterung, playSole3WPCave, playSole3WPOVMine,
        playTrainRide,
        playLongwallCutterBahnsteig, playLongwallCutterLongwallCutter;
    //playMuseumInfo, playMuseumCarbonification, MinerEquipment, HistoryMining;

    SpeechList
        speakEntryArea,
        speakSchacht,
        speakSole1,
        speakSole2,
        speakBewetterung, speakSole3Bahnsteig, speakSole3Cave, speakSole3BoardOVmine,
        speakTrainRide,
        speakLongwallCutterBahnsteig, speakLongwallCutterLongwallCutter;
        //speakMuseumInfo, speakMuseumCarbonification, speakMinerEquipment, speakHistroyMining;

    SpeechList currentList = null;
    private List<AudioSource> listAudioSrcs = new List<AudioSource>();
    private List<SpeechList> mySpeechLists = new List<SpeechList>();
    private AudioSource mySrc;

    // Start is called before the first frame update
    void Start()
    {
        mySrc = gameObject.AddComponent<AudioSource>();

        speakEntryArea = gameObject.AddComponent<SpeechList>();
        speakEntryArea.SetUpList(audiosEntryArea, mySrc);
        mySpeechLists.Add(speakEntryArea);

        speakSchacht = gameObject.AddComponent<SpeechList>();
        speakSchacht.SetUpList(audiosEntryAreaTriggerSchacht, mySrc);
        mySpeechLists.Add(speakSchacht);

        speakSole1 = gameObject.AddComponent<SpeechList>();
        speakSole1.SetUpList(audiosSole1, mySrc);
        mySpeechLists.Add(speakSole1);

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

        speakTrainRide = gameObject.AddComponent<SpeechList>();
        speakTrainRide.SetUpList(audiosTrainRide, mySrc);
        mySpeechLists.Add(speakTrainRide);

        speakLongwallCutterBahnsteig = gameObject.AddComponent<SpeechList>();
        speakLongwallCutterBahnsteig.SetUpList(audiosLongwallCutterBahnsteig, mySrc);
        mySpeechLists.Add(speakLongwallCutterBahnsteig);

        speakLongwallCutterLongwallCutter = gameObject.AddComponent<SpeechList>();
        speakLongwallCutterLongwallCutter.SetUpList(audioLongwallCutterLongwallCutter, mySrc);
        mySpeechLists.Add(speakLongwallCutterLongwallCutter);

        //speakMuseumInfo = gameObject.AddComponent<SpeechList>();
        //speakMuseumInfo.SetUpList(audiosMuseumInfo, mySrc);
        //mySpeechLists.Add(speakMuseumInfo);

        //speakHistroyMining = gameObject.AddComponent<SpeechList>();
        //speakHistroyMining.SetUpList(audiosMuseumHistoryMining, mySrc);
        //mySpeechLists.Add(speakHistroyMining);

        //speakMuseumCarbonification = gameObject.AddComponent<SpeechList>();
        //speakMuseumCarbonification.SetUpList(audiosMuseumCarbonification, mySrc);
        //mySpeechLists.Add(speakMuseumCarbonification);

        //speakMinerEquipment = gameObject.AddComponent<SpeechList>();
        //speakMinerEquipment.SetUpList(audiosMuseumMinerEquipment, mySrc);
        //mySpeechLists.Add(speakMinerEquipment);

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


    void Update()
    {
        if (playEntryArea)
        {
            currentList = speakEntryArea;
            playEntryArea = false;
        }
        else if (playSchacht)
        {
            currentList = speakSchacht;
            playSchacht = false;
        }
        else if (playSole1)
        {
            currentList = speakSole1;
            playSole1 = false;
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
        else if (playTrainRide)
        {
            currentList = speakTrainRide;
            playTrainRide = false;
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
        else
        {
            Debug.Log("current list ist null");
        }

        spDad.gameObject.SetActive(GameData.bubbleOnDad);
        spEnya.gameObject.SetActive(GameData.bubbleOnEnvy);
        spGeorg.gameObject.SetActive(GameData.bubbleOnGeorg);
    }
}
