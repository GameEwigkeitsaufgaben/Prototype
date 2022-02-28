using UnityEngine;

public class KohlehobelManager : MonoBehaviour
{
    public Player player;
    public SwitchSceneManager switchScene;
    public CoalmineSpeechManger speechManager;
    public LongwallCutterWaypointManager lwcManager;
    public Character enya, georg, dad;

    // Start is called before the first frame update
    void Start()
    {
        switchScene.LoadLongwallCutterStatic();
        switchScene.LoadLongwallCutterAnim();

        Invoke("StartViewpointBahnsteig", 3.0f);

        dad.RotateCharacter(-114f);
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
}
