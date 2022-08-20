using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;
    public Image graying;

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

    private UnityAction openCarbonPeriodGame, openMinerEquipment, openCoalification, openHistoryMining;

    private int chapter;

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
        
        parentMaskPanel = container.transform.parent.gameObject;
        
        gameObject.transform.localPosition = runtimeDataCh1.currentGroupPos;

        openCarbonPeriodGame += gameObject.GetComponent<SwitchSceneManager>().GoToWorld;
        openCoalification += gameObject.GetComponent<SwitchSceneManager>().GoToCoalification;
        openMinerEquipment += gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment;
        openHistoryMining += gameObject.GetComponent<SwitchSceneManager>().GoToMythos;
    }

    public void ActivateOverlay(MuseumWaypoints wp)
    {
        parentMaskPanel.SetActive(true);
        playOverlay = true;
        runtimeDataCh1.soundSettingMuseum = SoundMuseum.Overlay;
        bool showSkip = false;

        switch (chapter)
        {
            case 1:
                if (wp == MuseumWaypoints.WPInfo)
                {
                    container.sprite = configMuseum.info;
                    speechManagerChapOne.playMuseumInfoArrival = true;
                    Debug.Log("Info should be played!");
                }
                else if (wp == MuseumWaypoints.WPBergmann)
                {
                    container.sprite = configMuseum.miner;
                    speechManagerChapOne.playMinerEquipment = true;
                    btnSkipIntro.onClick.AddListener(openMinerEquipment);
                    if (runtimeDataCh1.isMinerDone) showSkip = true;
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
                    speechManagerChapTwo.playMuseumGWTVIntro = true;
                }
                else if (wp == MuseumWaypoints.WPFliesspfad)
                {
                    container.sprite = configMuseum.fliesspfad;
                    speechManagerChapTwo.playMuseumFliesspfadIntro = true;
                }
                break;
        }

        if (showSkip) btnSkipIntro.gameObject.SetActive(true);
        
        btnClose.gameObject.SetActive(true);
        graying.gameObject.SetActive(true);
    }

    public void StopOverlay()
    {
        playOverlay = false;
        runtimeDataCh1.soundSettingMuseum = SoundMuseum.Showroom;
    }

    private void Update()
    {
        //playOverlay with will be set to true in ActivateOverlay(), speechmanager starts the audio, here is proved if audio is finished.
        if ((chapter == 1) && speechManagerChapOne.IsTalkingListFinished(GameData.NameTLMuseumInfoArrival) && playOverlay)
        {
            playOverlay = false;
            //https://forum.unity.com/threads/solved-scenemanager-loadscene-make-the-scene-darker-a-bug.542440/
        }

        if (playOverlay)
        {
            switch (chapter)
            {
                case 1:
                    if (speechManagerChapOne.IsTalkingListFinished(GameData.NameTLMuseumMinerEquipment))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameTLMuseumCarbonificationPeriod))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToWorld();
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameTLMuseumHistoryMining))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToMythos();
                    }
                    else if (speechManagerChapOne.IsTalkingListFinished(GameData.NameTLMuseumCoalification))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToCoalification();
                    }
                    break;
                case 2:
                    if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameTLMuseumIntroTV))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
                        runtimeDataCh2.replayOverlay2121 = true; 
                    }
                    else if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameTLMuseumIntroFliesspfad))
                    {
                        gameObject.GetComponent<SwitchSceneManager>().GoToFliesspfade();
                    }
                    else if (speechManagerChapTwo.IsTalkingListFinished(GameData.NameTLMuseumOutroExitZeche))
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
