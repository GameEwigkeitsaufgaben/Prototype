using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;
    public Image graying;

    public SpeechManagerMuseum speechManager;
    public Button btnClose;
    public Button btnSkipIntro;

    private GameObject parentMaskPanel;

    bool playOverlay;
    private SoMuseumConfig configMuseum;

    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;

    private UnityAction openCarbonPeriodGame, openMinerEquipment, openCoalification, openHistoryMining;

    private void Awake()
    {
        configMuseum = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    private void Start()
    {
        parentMaskPanel = container.transform.parent.gameObject;
        
        gameObject.transform.localPosition = runtimeData.currentGroupPos;

        openCarbonPeriodGame += gameObject.GetComponent<SwitchSceneManager>().GoToWorld;
        openCoalification += gameObject.GetComponent<SwitchSceneManager>().GoToCoalification;
        openMinerEquipment += gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment;
        openHistoryMining += gameObject.GetComponent<SwitchSceneManager>().GoToMythos;
    }

    public void ActivateOverlay(MuseumWaypoints wp)
    {
        parentMaskPanel.SetActive(true);
        playOverlay = true;
        runtimeData.soundSettingMuseum = SoundMuseum.Overlay;
        bool showSkip = false;

        if (wp == MuseumWaypoints.WPInfo)
        {
            container.sprite = configMuseum.info;
            speechManager.playMuseumInfoArrival = true;
        }
        else if (wp == MuseumWaypoints.WPBergmann)
        {
            container.sprite = configMuseum.miner;
            speechManager.playMinerEquipment = true;
            btnSkipIntro.onClick.AddListener(openMinerEquipment);
            if (runtimeData.isMinerDone) showSkip = true;
        }
        else if (wp == MuseumWaypoints.WPWelt)
        {
            container.sprite = configMuseum.world;
            speechManager.playMuseumWorld = true;
            btnSkipIntro.onClick.AddListener(openCarbonPeriodGame);
            if (runtimeData.isCarbonificationPeriodDone) showSkip = true;
        }
        else if (wp == MuseumWaypoints.WPMythos)
        {
            container.sprite = configMuseum.myth;
            speechManager.playMuseumCoalHistory = true;
            btnSkipIntro.onClick.AddListener(openHistoryMining);
            if (runtimeData.isMythDone) showSkip = true;
        }
        else if (wp == MuseumWaypoints.WPInkohlung)
        {
            container.sprite = configMuseum.carbonification;
            speechManager.playMuseumCarbonification = true;
            btnSkipIntro.onClick.AddListener(openCoalification);
            if (runtimeData.isCoalifiationDone) showSkip = true;
        }
        else if(wp == MuseumWaypoints.WPTV)
        {
            container.sprite = configMuseum.tv;
            speechManager.playMuseumGWTVIntro = true;
        }
        else if (wp == MuseumWaypoints.WPFliesspfad)
        {
            container.sprite = configMuseum.fliesspfad;
            speechManager.playMuseumFliesspfadIntro = true;
        }

        if (showSkip) btnSkipIntro.gameObject.SetActive(true);
        
        btnClose.gameObject.SetActive(true);
        graying.gameObject.SetActive(true);
    }

    public void StopOverlay()
    {
        playOverlay = false;
        runtimeData.soundSettingMuseum = SoundMuseum.Showroom;
    }

    private void Update()
    {
        //playOverlay with will be set to true in ActivateOverlay(), speechmanager starts the audio, here is proved if audio is finished.
        if (speechManager.IsMusuemInfoIntroFinished() && playOverlay)
        {
            playOverlay = false;
            //https://forum.unity.com/threads/solved-scenemanager-loadscene-make-the-scene-darker-a-bug.542440/
        }

        if (playOverlay)
        {
            if (speechManager.IsMusuemMinerEquipmentFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
            }
            else if (speechManager.IsMusuemHistroyCarbonFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToWorld();
            }
            else if (speechManager.IsMusuemHistoryMiningFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToMythos();
            }
            else if (speechManager.IsMusuemCoalifictionFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToCoalification();
            }
            else if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumIntroTV))
            {
                gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
            }
            else if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumIntroFliesspfad))
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToFliesspfade();
            }
            else if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumOutroExitZeche))
            {
                gameObject.GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay("Overlay212");
            }
        }
        
        if (!playOverlay && parentMaskPanel.activeSelf)
        {
            parentMaskPanel.SetActive(false);
            graying.gameObject.SetActive(false);
            btnClose.gameObject.SetActive(false);
            btnSkipIntro.gameObject.SetActive(false);
            speechManager.StopSpeaking();
        }
    }
}
