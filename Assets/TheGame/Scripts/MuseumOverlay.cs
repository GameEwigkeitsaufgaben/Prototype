using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;
    public Image graying;
    public Image audioProgress, audioProgressBG;

    public SpeechManagerMuseumChapOne speechManagerChapOne;
    public SpeechManagerMuseumChapTwo speechManagerChapTwo;
    public Button btnClose;
    public Button btnSkipIntro;

    private GameObject parentMaskPanel;

    bool playOverlay;
    private SoMuseumConfig configMuseum;

    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChaptersRuntimeData runtimeDataChapters;

    private UnityAction stopIntroInfoPoint, openCarbonPeriodGame, openMinerEquipment, openCoalification, openHistoryMining;
    private UnityAction openTV, openFliesspfade; 

    private int chapter;
    AudioProgress audioP;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == GameScenes.ch01Museum)
        {
            chapter = 1;
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02Museum)
        {
            chapter = 2;
        }
            
        configMuseum = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);

        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
    }

    private void Start()
    {
        audioP = GetComponent<AudioProgress>();
        parentMaskPanel = container.transform.parent.gameObject;
        
        gameObject.transform.localPosition = runtimeDataCh1.currentGroupPos;

        stopIntroInfoPoint += StopOverlay;
        openCarbonPeriodGame += gameObject.GetComponent<SwitchSceneManager>().GoToWorld;
        openCoalification += gameObject.GetComponent<SwitchSceneManager>().GoToCoalification;
        openMinerEquipment += gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment;
        openHistoryMining += gameObject.GetComponent<SwitchSceneManager>().GoToMythos;
        openTV += gameObject.GetComponent<SwitchSceneManager>().GoToCh2MuseumTV;
        openFliesspfade += gameObject.GetComponent<SwitchSceneManager>().GoToFliesspfade;
        btnClose.gameObject.SetActive(false);
        btnSkipIntro.gameObject.SetActive(false);
        btnSkipIntro.interactable = false;
        

        ActivateAudioProgress(false);
    }

    

    public void ActivateAudioProgress(bool activate)
    {
        audioProgress.gameObject.SetActive(activate);
        audioProgressBG.gameObject.SetActive(activate);
    }

    public void ActivateOverlay(MuseumWaypoints wp)
    {
        parentMaskPanel.SetActive(true);
        btnSkipIntro.gameObject.SetActive(true);
        btnSkipIntro.interactable = false;
        graying.gameObject.SetActive(true);
        playOverlay = true;
        btnSkipIntro.onClick.RemoveAllListeners();

        bool showSkip = false;

        switch (chapter)
        {
            case 1:
                runtimeDataCh1.soundSettingMuseum = SoundMuseum.Overlay;

                if (wp == MuseumWaypoints.WPInfo)
                {
                    Debug.Log("Info should be played!");
                    audioProgressBG.gameObject.SetActive(true);
                    showSkip = runtimeDataCh1.replayInfoPointMuseum;
                    
                    container.sprite = configMuseum.info;
                    speechManagerChapOne.playMuseumInfoArrival = true;
                    btnSkipIntro.onClick.AddListener(stopIntroInfoPoint);
                    audioP.StartTimer(speechManagerChapOne.GetTalkingListOverallTimeInSec(GameData.NameCH1TLMuseumInfoArrival));
                    Debug.Log("---------------------END Info should be played!");

                }
                else if (wp == MuseumWaypoints.WPBergmann)
                {
                    Debug.Log("WPBEEEEEEEEEEEEEEERGMANN");
                    audioProgressBG.gameObject.SetActive(true);
                    Debug.Log("show skip " + runtimeDataCh1.replayMinerEquipment);
                    showSkip = runtimeDataCh1.replayMinerEquipment;
                    Debug.Log("show skip " + showSkip);

                    container.sprite = configMuseum.miner;
                    speechManagerChapOne.playMinerEquipment = true;
                    btnSkipIntro.onClick.AddListener(openMinerEquipment);
                    audioP.StartTimer(speechManagerChapOne.GetTalkingListOverallTimeInSec(GameData.NameCH1TLMuseumMinerEquipment));
                    //if (runtimeDataCh1.isMinerDone) showSkip = true;
                }
                else if (wp == MuseumWaypoints.WPWelt)
                {
                    container.sprite = configMuseum.world;
                    speechManagerChapOne.playMuseumWorld = true;
                    btnSkipIntro.onClick.AddListener(openCarbonPeriodGame);
                    if (runtimeDataCh1.isCarbonificationPeriodDone) showSkip = true;
                }
                else if (wp == MuseumWaypoints.WPMythos)
                {
                    container.sprite = configMuseum.myth;
                    speechManagerChapOne.playMuseumCoalHistory = true;
                    btnSkipIntro.onClick.AddListener(openHistoryMining);
                    if (runtimeDataCh1.isMythDone) showSkip = true;
                }
                else if (wp == MuseumWaypoints.WPInkohlung)
                {
                    container.sprite = configMuseum.carbonification;
                    speechManagerChapOne.playMuseumCarbonification = true;
                    btnSkipIntro.onClick.AddListener(openCoalification);
                    if (runtimeDataCh1.isCoalifiationDone) showSkip = true;
                }
                break;
            case 2:
                if (wp == MuseumWaypoints.WPTV)
                {
                    container.sprite = configMuseum.tv;
                    if (runtimeDataCh2.replayOverlay2122) showSkip = true;
                    speechManagerChapTwo.playMuseumGWTVIntro = true;
                    btnSkipIntro.onClick.AddListener(openTV);
                    audioP.StartTimer(speechManagerChapTwo.GetTalkingListOverallTimeInSec(GameData.NameCH2TLMuseumIntroTV));
                    audioProgressBG.gameObject.SetActive(true);
                }
                else if (wp == MuseumWaypoints.WPFliesspfad)
                {
                    container.sprite = configMuseum.fliesspfad;
                    if (runtimeDataCh2.replayOverlay2123) showSkip = true;
                    speechManagerChapTwo.playMuseumFliesspfadIntro = true;
                    btnSkipIntro.onClick.AddListener(openFliesspfade);
                    audioP.StartTimer(speechManagerChapTwo.GetTalkingListOverallTimeInSec(GameData.NameCH2TLMuseumIntroTV));
                    audioProgressBG.gameObject.SetActive(true);
                }
                break;
        }

        if (showSkip) btnSkipIntro.interactable = true;
    }

    public void StopOverlay()
    {
        playOverlay = false;
        runtimeDataCh1.soundSettingMuseum = SoundMuseum.Showroom;
        ActivateAudioProgress(false);
    }

    private void Update()
    {
        //playOverlay with will be set to true in ActivateOverlay(), speechmanager starts the audio, here is proved if audio is finished.
        if ((chapter == 1) && speechManagerChapOne.IsTalkingListFinished(GameData.NameCH1TLMuseumInfoArrival) && playOverlay)
        {
            playOverlay = false;
            ActivateAudioProgress(false);
            runtimeDataCh1.replayInfoPointMuseum = true;
            //https://forum.unity.com/threads/solved-scenemanager-loadscene-make-the-scene-darker-a-bug.542440/
        }

        if (playOverlay)
        {
            switch (chapter)
            {
                case 1:
                    if (speechManagerChapOne.IsTalkingListFinished(GameData.NameCH1TLMuseumMinerEquipment))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
                        runtimeDataCh1.replayMinerEquipment = true;
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameCH1TLMuseumCarbonificationPeriod))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToWorld();
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameCH1TLMuseumHistoryMining))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToMythos();
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameCH1TLMuseumCoalification))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToCoalification();
                    }
                    break;
                case 2:
                    if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameCH2TLMuseumIntroTV))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
                        runtimeDataCh2.replayOverlay2122 = true; 
                    }
                    else if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameCH2TLMuseumIntroFliesspfad))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToFliesspfade();
                        runtimeDataCh2.replayOverlay2123 = true;
                    }
                    else if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameCH2TLMuseumOutroExitZeche))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay("Overlay212");
                    }
                    break;
            }
           
        }
        
        if (!playOverlay && parentMaskPanel.activeSelf)
        {
            parentMaskPanel.SetActive(false);
            graying.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(false);
            btnSkipIntro.gameObject.SetActive(false);
            switch (chapter)
            {
                case 1:
                    speechManagerChapOne.StopSpeaking();
                    break;
                case 2:
                    speechManagerChapTwo.StopSpeaking();
                    break;
            }
        }
    }
}
