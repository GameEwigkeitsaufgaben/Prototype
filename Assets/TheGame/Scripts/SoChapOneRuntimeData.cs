using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SoChapOneRuntimeData")]
public class SoChapOneRuntimeData : Runtime
{
    public bool videoPlaying = false;
    //public OverlaySoundState overlaySoundState;

    public bool ch1GeneralUnlocked, ch2GrubenwasserUnlocked;

    public string hintPostUnlock;

    [Header("Coalmine 116")]
    public bool liftBtnsAllEnabled;
    public float playerRotation; //needed for lookaroundMouse
    public bool revisitEntryArea;
    public CoalmineStop currentCoalmineStop;
    public bool playerInsideCave;
    public bool trainArrived;
    public bool viewPointS3passed;
    public bool entryAreaDone;
    public bool sole1Done, sole2Done, sole3BewetterungDone, sole3GebaeudeDone, trainRideInDone, trainRideOutDone, isLongwallCutterDone;
    public Animator kohlenhobelAnimator;

    [Header("Museum 117")]
    public Vector3 currentGroupPos = Vector3.zero; //-13.44922, 2.4, -2.670441
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;
    public bool isMinerDone, isMythDone, isCoalifiationDone, isCarbonificationPeriodDone;
    public SoundMuseum soundSettingMuseum;

    [Header("GameProgress")]
    public bool post111Done, post112Done, post113Done, post114Done, video115Done, interaction116Done, interaction117Done, quiz119Done;
    //progressCh1WithAdmin;

    public Animator GetKohlenhobelAnimator()
    {
        if(kohlenhobelAnimator == null)
        {
            Debug.Log("Kohlenhobel Animator is null");
            return null;
        }
        else
        {
            Debug.Log("Kohlenhobel Animator: " + kohlenhobelAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"));
            return kohlenhobelAnimator;
        }
    }
    private void OnEnable()
    {
        hintPostUnlock = "";
        post111Done = post112Done = post113Done = post114Done = false;
        liftBtnsAllEnabled = false;
        entryAreaDone = false;
        revisitEntryArea = false;
        currentCoalmineStop = CoalmineStop.Unset;
        ch1GeneralUnlocked = false;
        ch2GrubenwasserUnlocked = false;
        Debug.Log("RELOAD RUNTIME DATA");
        trainArrived = viewPointS3passed = false;
        postOverlayToLoad = "";
        playerInsideCave = false;
        video115Done = false;
        currentGroupPos = Vector3.zero;
        currentMuseumWaypoint = MuseumWaypoints.WP0;
        playerRotation = 0f;
        isMinerDone = isMythDone = isCarbonificationPeriodDone = isCoalifiationDone = false;
        sole1Done = sole2Done = sole3BewetterungDone = sole3GebaeudeDone = trainRideInDone = trainRideOutDone = isLongwallCutterDone = false;
        interaction116Done = interaction117Done = quiz119Done = false;
        musicOn = true;
    }

    public void CheckInteraction117Done()
    {
        if (!interaction117Done)
        {
            if (isMinerDone && isMythDone && isCoalifiationDone && isCarbonificationPeriodDone)
            {
                interaction117Done = true;
            }
        }
    }

    public void CheckInteraction116Done()
    {
        if (!interaction116Done)
        {
            if (sole1Done && sole2Done && sole3BewetterungDone && sole3GebaeudeDone && trainRideInDone && trainRideOutDone && isLongwallCutterDone)
            {
                interaction116Done = true;
            }
        }
    }
}
