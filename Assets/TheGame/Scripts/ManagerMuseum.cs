using UnityEngine;
using UnityEngine.UI;

public enum SoundMuseum
{
    Showroom,
    Overlay,
    None
}

public class ManagerMuseum : MonoBehaviour
{
    public Button btnExitMuseum, btnReplayTalkingList;
    public SpeechManagerMuseumChapOne speechManagerCh1;
    public MuseumPlayer walkingGroup;
    public SwitchSceneManager switchScene;

    private bool startOutro;
    private Image btnExitImage;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChaptersRuntimeData runtimeDataChapters;
    private bool museumDoneSet;
    public GameObject characterDad, characterGuide, waitingGuide;

    private AudioSource audioSrcBGMusic;
   
    public MuseumOverlay overlay;

    private void Awake()
    {
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    // Start is called before the first frame update
    void Start()
    {
        walkingGroup.SetCharcters(characterDad, characterGuide, waitingGuide);
        audioSrcBGMusic = gameObject.GetComponent<AudioSource>();
        runtimeDataCh1.soundSettingMuseum = SoundMuseum.Showroom;

        if(runtimeDataCh1.currentMuseumWaypoint != MuseumWaypoints.WP0)
        {
            SetInMuseumGroup();
        }
       
        btnExitImage = btnExitMuseum.GetComponent<Image>();
        btnExitImage.gameObject.GetComponent<Button>().interactable = false;
        btnReplayTalkingList.gameObject.SetActive(false);
        museumDoneSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!overlay.OverlayActive())
        {
            if(!btnReplayTalkingList.gameObject.activeSelf)
            {
                switch (walkingGroup.currentWP)
                {
                    case MuseumWaypoints.WPInfo:
                        if (runtimeDataCh1.replayInfoPointMuseum) btnReplayTalkingList.gameObject.SetActive(true);
                        break;
                    case MuseumWaypoints.WPBergmann:
                        if (runtimeDataCh1.replayMinerEquipment) btnReplayTalkingList.gameObject.SetActive(true);
                        break;
                    case MuseumWaypoints.WPInkohlung:
                        if (runtimeDataCh1.replayCoalification) btnReplayTalkingList.gameObject.SetActive(true);
                        break;
                    case MuseumWaypoints.WPMythos:
                        if (runtimeDataCh1.replayHistoryMining) btnReplayTalkingList.gameObject.SetActive(true);
                        break;
                    case MuseumWaypoints.WPWelt:
                        if (runtimeDataCh1.replayWorld) btnReplayTalkingList.gameObject.SetActive(true);
                        break;
                }
            }

            if (speechManagerCh1.IsTalkingListFinished(GameData.NameCH1TLMuseumOutro))
            {
                switchScene.SwitchToChapter1withOverlay(GameData.NameOverlay117);
            }

        }
        else
        {
            if(btnReplayTalkingList.gameObject.activeSelf) btnReplayTalkingList.gameObject.SetActive(false);

            if (speechManagerCh1.IsTalkingListFinished(GameData.NameCH1TLMuseumInfoArrival))
            {
                SetInMuseumGroup();
            }
        }

        runtimeDataCh1.CheckInteraction117Done();
        
        if (!museumDoneSet && runtimeDataCh1.interaction117Done)
        {
            speechManagerCh1.playMuseumOutro = true;
            museumDoneSet = true;
            walkingGroup.MoveToWaypoint((int)MuseumWaypoints.WPExitMuseum0);
        }

        if (startOutro)
        {
            startOutro = false;
        }

        switch (runtimeDataCh1.soundSettingMuseum)
        {
            case SoundMuseum.Showroom:
                PlayAdjustedBGMusic(0.5f);
                break;
            case SoundMuseum.Overlay:
                PlayAdjustedBGMusic(0.2f);
                break;
            case SoundMuseum.None:
                audioSrcBGMusic.Stop();
                break;
        }
    }

    private void SetInMuseumGroup()
    {
        characterDad.gameObject.SetActive(false);
        characterGuide.gameObject.SetActive(true);
        waitingGuide.gameObject.SetActive(false);
    }

    private void PlayAdjustedBGMusic(float volume)
    {
        if (!audioSrcBGMusic.isPlaying) audioSrcBGMusic.Play();
        if (audioSrcBGMusic.volume != volume) audioSrcBGMusic.volume = volume;
    }

    public void ReplayTalkingList()
    {
        btnReplayTalkingList.gameObject.SetActive(false);
        overlay.ActivateOverlay(walkingGroup.currentWP);
    }
}
