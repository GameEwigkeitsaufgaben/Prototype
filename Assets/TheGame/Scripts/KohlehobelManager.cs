using UnityEngine;
using UnityEngine.UI;

public class KohlehobelManager : MonoBehaviour
{
    public Player myPlayer;
    public SwitchSceneManager switchScene;
    public CoalmineSpeechManger speechManager;
    public LongwallCutterWaypointManager lwcManager;
    public Character enya, georg, dad;
    public Button btnLwcViewpoint, btnLwcExit;

    private SoChapOneRuntimeData runtimeData01;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    public AudioSource audioSrcBewetterung, audioSrcLwc;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeData01 = runtimeDataChapters.LoadChap1RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSrcBewetterung.clip = sfx.coalmineZecheWind;
        audioSrcBewetterung.playOnAwake = true;
        switchScene.LoadLongwallCutterStatic();
        switchScene.LoadLongwallCutterAnim();

        btnLwcViewpoint.interactable = false;
        btnLwcExit.interactable = false;

        Invoke("StartViewpointBahnsteig", 2.0f);

        lwcManager.RotateCharacters(-114.0f, -53.0f, -80.0f);
        myPlayer.SetPlayerRotation(0f,false);

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorTexture3DCave);
        //runtimeDataChapters.sceneCursor = runtimeData01.cursorTexture3DCave;
        //Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, CursorMode.Auto);
    }

    public void StartAnimKohlenhobel()
    {
        if (!runtimeData01.GetKohlenhobelAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            runtimeData01.GetKohlenhobelAnimator().SetTrigger("backToIdle");
        }

        runtimeData01.GetKohlenhobelAnimator().SetTrigger("play");
        audioSrcLwc.Play();
    }

    //Positionieren und Ausrichten in LongwallCutterWaypointManager

    //Audios auf Buttons abspielen (3sohle)
    //SFX 

    public void StartTalking()
    {
        Debug.Log("in start Talking");

        Debug.Log(lwcManager.StandingOnBahnsteig());

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


    private void Update()
    {
        if (!btnLwcViewpoint.interactable && speechManager.IsLWCBahnsteigFinished())
        {
            btnLwcViewpoint.interactable = true;
        }
        else if (!btnLwcExit.interactable && speechManager.IsLWCLWCFinished())
        {
            btnLwcExit.interactable = true;
        }
    }
}
