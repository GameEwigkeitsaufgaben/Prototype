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
    //public bool isMinerDone;
    //public bool isMythDone;
    //public bool isCoalifictionDone;
    //public bool isEarthHistroyDone;

    public Button btnExitMuseum;
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
        museumDoneSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        runtimeDataCh1.CheckInteraction117Done();
        
        if (!museumDoneSet && runtimeDataCh1.interaction117Done)
        {
            speechManagerCh1.playMuseumOutro = true;
            museumDoneSet = true;
            walkingGroup.MoveToWaypoint((int)MuseumWaypoints.WPExitMuseum0);
        }

        if (speechManagerCh1.IsTalkingListFinished(GameData.NameTLMuseumInfoArrival))
        {
            SetInMuseumGroup();
        }

        if (speechManagerCh1.IsTalkingListFinished(GameData.NameTLMuseumOutro))
        {
            switchScene.SwitchToChapter1withOverlay(GameData.NameOverlay117); 
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
}
