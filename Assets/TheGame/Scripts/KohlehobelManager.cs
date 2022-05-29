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

    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    // Start is called before the first frame update
    void Start()
    {
        switchScene.LoadLongwallCutterStatic();
        switchScene.LoadLongwallCutterAnim();

        btnLwcViewpoint.interactable = false;
        btnLwcExit.interactable = false;

        Invoke("StartViewpointBahnsteig", 2.0f);

        lwcManager.RotateCharacters(-114.0f, -53.0f, -80.0f);
        myPlayer.SetPlayerRotation(0f,false);

        runtimeDataChapters.sceneCursor = runtimeData.cursorTexture3DCave;
        Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, CursorMode.Auto);
    }

    public void StartAnimKohlenhobel()
    {
        if (!runtimeData.GetKohlenhobelAnimator().GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            runtimeData.GetKohlenhobelAnimator().SetTrigger("backToIdle");
        }

        runtimeData.GetKohlenhobelAnimator().SetTrigger("play");
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
