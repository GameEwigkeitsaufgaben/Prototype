using UnityEngine;
using UnityEngine.UI;

public class KohlehobelManager : MonoBehaviour
{
    public Player myPlayer;
    public SwitchSceneManager switchScene;
    public CoalmineSpeechManger speechManager;
    public LongwallCutterWaypointManager lwcManager;
    public Character enya, georg, dad;
    public Button btnLwcViewpoint, btnLwcExit, btnReplayTalkingList;

    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    public AudioSource audioSrcBewetterung, audioSrcLwc, audioSrcBand, audioSrcZecheAtmo, audioSrcSchritte, audioSrcSchritte2, audioSrcWater;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();
        audioSrcSchritte2.clip = sfx.walkingGroupStones;
        audioSrcSchritte2.volume=0.4f;

        audioSrcWater.clip = sfx.rohreWasser;
        audioSrcWater.loop = true;
        audioSrcWater.Play();
    }

    public void PlayerSchritte()
    {
        if (!audioSrcSchritte.isPlaying)
        {
            audioSrcSchritte.Play();
            audioSrcSchritte2.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrcSchritte.clip = sfx.walkingGroup;
        audioSrcSchritte.volume = 0.34f;

        audioSrcBewetterung.clip = sfx.coalmineWindInTunnel;
        audioSrcBewetterung.playOnAwake = true;
        audioSrcBewetterung.Play();

        audioSrcBand.clip = sfx.coalmineConveyerBelt;
        audioSrcBand.loop = true;
        audioSrcBand.Play();

        audioSrcLwc.clip = sfx.coalmineLWC;

        audioSrcZecheAtmo.clip = sfx.coalmineZecheWind;
        audioSrcZecheAtmo.Play();

        switchScene.LoadLongwallCutterStatic();
        switchScene.LoadLongwallCutterAnim();

        btnLwcViewpoint.interactable = false;
        btnLwcExit.interactable = false;
        btnReplayTalkingList.gameObject.SetActive(false);

        if(!runtimeDataCh1.isLongwallCutterDone) Invoke("StartViewpointBahnsteig", 1.0f);

        lwcManager.RotateCharacters(-114.0f, -53.0f, -80.0f);
        myPlayer.SetPlayerRotation(0f,false);

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorTexture3DCave);

        if (runtimeDataCh1.isLongwallCutterDone)
        {
            btnReplayTalkingList.gameObject.SetActive(true);
            btnLwcViewpoint.interactable = true;
            btnLwcExit.interactable = true;
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }


    public void ReplayTalkingList()
    {
        if (lwcManager.StandingOnBahnsteig())
        {
            speechManager.playLongwallCutterBahnsteig = true;
        }
        else if(lwcManager.GetCurrentLongWallCutterWP() == MineWayPoints.viewpointLWLWCutter)
        {
            speechManager.playLongwallCutterLongwallCutter = true;
        }
    }

    public void StartAnimKohlenhobel()
    {
        if (!runtimeDataCh1.GetKohlenhobelAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            runtimeDataCh1.GetKohlenhobelAnimator().SetTrigger("backToIdle");
        }

        runtimeDataCh1.GetKohlenhobelAnimator().SetTrigger("play");
        audioSrcLwc.Play();
    }


    public void StartTalking()
    {
        if (lwcManager.StandingOnBahnsteig()) return;

        speechManager.playLongwallCutterLongwallCutter = true;
    }

    public void StartLongwalllCutterSpeech()
    {
        speechManager.playLongwallCutterLongwallCutter = true;
    }

    public void StartViewpointBahnsteig()
    {
        speechManager.playLongwallCutterBahnsteig = true;
    }

    public void GoToTrainRideOut()
    {
        speechManager.StopRunningTL();
        switchScene.GoToTrainRideOut();
    }


    private void Update()
    {
        if(!btnLwcExit.interactable && runtimeDataCh1.isLongwallCutterDone)
        {
            btnLwcExit.interactable = true;
        }

        if (!btnLwcViewpoint.interactable && speechManager.IsLWCBahnsteigFinished())
        {
            btnLwcViewpoint.interactable = true;
            runtimeDataCh1.replayLwcBahnsteig = true;
            btnReplayTalkingList.gameObject.SetActive(true);
        }
        else if (!btnLwcExit.interactable && speechManager.IsLWCLWCFinished())
        {
            btnLwcExit.interactable = true;
            runtimeDataCh1.isLongwallCutterDone = true;
        }
    }
}
