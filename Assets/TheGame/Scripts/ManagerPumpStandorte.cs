using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum PumpenwerkStatus
{
    akitv,
    off,
    showAll
}

public class ManagerPumpStandorte : MonoBehaviour
{
    public Button btnNext, btnReplayTalkingList;
    public Toggle togglePumpwerke;

    public GameObject textWHBisher, textWHNeu;
    public Image lohberg, fuerstleopold, augusteVictoria, hausAden, walsum, prosterHaniel, concordia, amalie, zollverein, carolinenglueck, friedlicherNachbar, heinrich, roberMueser;
    public Dictionary<string, Image> pumpwerke;
    public Image pegelHigh, pegelLow;
    public Image lightReducedBetrieb, lightAllBetrieb;

    
    public bool isDone = false;
    public bool audioFinished = false;

    private SoGameColors gameColors;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataCh03;
    private SpeechManagerChapThree speechManager;
    Color32 colorInactive = new Color32(255, 255, 255, 50);

    private void Awake()
    {
        gameColors = Resources.Load<SoGameColors>(GameData.NameGameColors);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataCh03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        speechManager = GetComponent<SpeechManagerChapThree>();

        speechManager.playPumpstandorte = !runtimeDataCh03.replayTL3102;

        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh03.replayTL3102);
        togglePumpwerke.interactable = runtimeDataCh03.replayTL3102;
        btnNext.interactable = runtimeDataCh03.IsPostDone(ProgressChap3enum.Post3102);


        pumpwerke = new Dictionary<string, Image>();

        pumpwerke.Add(lohberg.name, lohberg);
        pumpwerke.Add(fuerstleopold.name, fuerstleopold);
        pumpwerke.Add(augusteVictoria.name, augusteVictoria);
        pumpwerke.Add(hausAden.name, hausAden);
        pumpwerke.Add(walsum.name, walsum);
        pumpwerke.Add(prosterHaniel.name, prosterHaniel);
        pumpwerke.Add(concordia.name, concordia);
        pumpwerke.Add(amalie.name, amalie);
        pumpwerke.Add(zollverein.name, zollverein);
        pumpwerke.Add(carolinenglueck.name, carolinenglueck);
        pumpwerke.Add(friedlicherNachbar.name, friedlicherNachbar);
        pumpwerke.Add(heinrich.name, heinrich);
        pumpwerke.Add(roberMueser.name, roberMueser);


        //MarkInactive();
        MarkAll();
        togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextInBetrieb.color = gameColors.gameGray;
        togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextAlleBetrieb.color = gameColors.gameRed;
        lightReducedBetrieb.color = gameColors.gameGray;
        pegelHigh.gameObject.SetActive(false);
        pegelLow.gameObject.SetActive(true);
        lightAllBetrieb.color = gameColors.gameRed;
        textWHBisher.SetActive(true);
        textWHNeu.SetActive(false);
    }

    public void TooglePumpwerke()
    {
        if (togglePumpwerke.GetComponent<Toggle>().isOn)
        {
            MarkActive();
            togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextInBetrieb.color = gameColors.gameRed;
            togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextAlleBetrieb.color = gameColors.gameGray;
            lightReducedBetrieb.color = gameColors.gameRed;
            lightAllBetrieb.color = gameColors.gameGray;
            pegelHigh.gameObject.SetActive(true);
            pegelLow.gameObject.SetActive(false);
            
            if (!btnNext.interactable)
            {
                btnNext.interactable = true;
                runtimeDataCh03.SetPostDone(ProgressChap3enum.Post3102);
            }
        }
        else
        {
            MarkAll();
            //MarkInactive();
            togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextInBetrieb.color = gameColors.gameGray;
            togglePumpwerke.gameObject.GetComponent<ToogleWasserhaltung>().btnTextAlleBetrieb.color = gameColors.gameRed;
            lightReducedBetrieb.color = gameColors.gameGray;
            lightAllBetrieb.color = gameColors.gameRed;
            pegelHigh.gameObject.SetActive(false);
            pegelLow.gameObject.SetActive(true);
        }
    }

    public void MarkActive()
    {
        Color markerColor = gameColors.gameRed;
        
        MarkAll(markerColor);

        fuerstleopold.color = augusteVictoria.color = 
        concordia.color = amalie.color = zollverein.color = carolinenglueck.color = colorInactive;

        Color showPumwerkNameColor = Color.black;

        //active pumpwerke
        lohberg.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        walsum.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        prosterHaniel.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        heinrich.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        friedlicherNachbar.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        roberMueser.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        hausAden.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        showPumwerkNameColor = colorInactive;
        //inactive pumpwerke
        fuerstleopold.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        augusteVictoria.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        concordia.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        amalie.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        zollverein.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        carolinenglueck.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        textWHBisher.SetActive(false);
        textWHNeu.SetActive(true);
    }

    public void MarkInactive()
    {
        Color markColor = gameColors.gameRed;

        MarkAll(colorInactive);
        fuerstleopold.color = augusteVictoria.color =
        concordia.color = amalie.color = zollverein.color = carolinenglueck.color = markColor;

        Color showPumwerkNameColor = colorInactive;

        lohberg.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        walsum.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        prosterHaniel.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        heinrich.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        friedlicherNachbar.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        roberMueser.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        hausAden.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        showPumwerkNameColor = Color.black;
        //inactive pumpwerke
        fuerstleopold.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        augusteVictoria.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        concordia.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        amalie.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        zollverein.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        carolinenglueck.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        textWHBisher.SetActive(false);
        textWHNeu.SetActive(true);
    }

    public void MarkAll()
    {
        MarkAll(Color.white);

        Color showPumwerkNameColor = Color.black;

        //active pumpwerke
        lohberg.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        walsum.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        prosterHaniel.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        heinrich.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        friedlicherNachbar.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        roberMueser.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        hausAden.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        showPumwerkNameColor = Color.black;
        //inactive pumpwerke
        fuerstleopold.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        augusteVictoria.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        concordia.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        amalie.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        zollverein.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;
        carolinenglueck.GetComponentInChildren<TMP_Text>().color = showPumwerkNameColor;

        textWHBisher.SetActive(true);
        textWHNeu.SetActive(false);
    }

    public void PlayTalkingList()
    {
        speechManager.playPumpstandorte = true;
    }

    public void MarkAll(Color color)
    {
        foreach(Image value in pumpwerke.Values)
        {
            value.color = color;
        }
    }

    public void Update()
    {
        if (!audioFinished)
        {
            audioFinished = speechManager.IsTalkingListFinished(GameData.NameCH3TLPumpenstandorte);
            runtimeDataCh03.replayTL3102 = audioFinished;

        }
        else
        {
            if (!btnReplayTalkingList.IsActive()) btnReplayTalkingList.gameObject.SetActive(runtimeDataCh03.replayTL3102);
            if (!togglePumpwerke.interactable) togglePumpwerke.interactable = true;
        }
    }
}