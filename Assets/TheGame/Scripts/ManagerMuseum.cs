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
    public SpeechManagerMuseum speechManager;
    public MuseumPlayer walkingGroup;
    public SwitchSceneManager switchScene;

    private bool startOutro;
    private Image btnExitImage;
    private SoChapOneRuntimeData runtimeData;
    private bool museumDoneSet;
    public GameObject characterDad, characterGuide, waitingGuide;

    private AudioSource audioSrcBGMusic;

    // Start is called before the first frame update
    void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        walkingGroup.SetCharcters(characterDad, characterGuide, waitingGuide);
        audioSrcBGMusic = gameObject.GetComponent<AudioSource>();
        runtimeData.soundSettingMuseum = SoundMuseum.Showroom;

        if(runtimeData.currentMuseumWaypoint != MuseumWaypoints.WP0)
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
        runtimeData.CheckInteraction117Done();
        
        if (!museumDoneSet && runtimeData.interaction117Done)
        {
            speechManager.playMuseumOutro = true;
            museumDoneSet = true;
            walkingGroup.MoveToWaypoint((int)MuseumWaypoints.WPExitMuseum0);
        }

        if (speechManager.IsMusuemInfoIntroFinished())
        {
            SetInMuseumGroup();
        }

        if (speechManager.IsMuseumOutroFinished())
        {
            switchScene.SwitchToChapter1withOverlay("Overlay117"); 
        }

        if (startOutro)
        {
            startOutro = false;
        }

        switch (runtimeData.soundSettingMuseum)
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
