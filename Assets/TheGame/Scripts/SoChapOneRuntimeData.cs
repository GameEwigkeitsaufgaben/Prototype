using UnityEngine;

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
    public bool replayEntryArea;
    public bool sole1Done, sole2Done, sole3BewetterungDone, sole3GebaeudeDone, trainRideInDone, trainRideOutDone, isLongwallCutterDone;
    public Animator kohlenhobelAnimator;

    [Header("Museum 117")]
    public Vector3 currentGroupPos = Vector3.zero; //-13.44922, 2.4, -2.670441
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;
    public bool isMinerDone, isMythDone, isCoalifiationDone, isCarbonificationPeriodDone;
    public bool currDragItemExists = false;
    public SoundMuseum soundSettingMuseum;
    public string quizPointsCh01 = "";
    public float quizPointsOverall = 0;
    public bool replayCoalmineIntro;
    public bool replayInfoPointMuseum, replayMinerEquipment, replayWorld, replayHistoryMining, replayCoalification;
    public bool replayS1Cave, replayS2Cave, replayS3Cave;

    public string generalKeyOverlay = GameData.NameOverlay1110;


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
        quizPointsCh01 = "";
        quizPointsOverall = 0;
        hintPostUnlock = "";
        post111Done = post112Done = post113Done = post114Done = false;
        liftBtnsAllEnabled = false;
        replayEntryArea = false;
        revisitEntryArea = false;
        currentCoalmineStop = CoalmineStop.Unset;
        ch1GeneralUnlocked = false;
        ch2GrubenwasserUnlocked = false;
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
        currDragItemExists = false;
        replayInfoPointMuseum = replayMinerEquipment = replayWorld = replayHistoryMining = replayCoalification =  false;
        replayCoalmineIntro = false;
        replayS1Cave = false;
        replayS2Cave = false;
        replayS3Cave = false;
    }

    public void SetAllDone()
    {
        //Posts Done incl. Video finished;
        post111Done = post112Done = post113Done = post114Done = video115Done = interaction116Done = interaction117Done = quiz119Done = true;

        //Coalmine all stations done;
        replayCoalmineIntro = true; 
        replayEntryArea = true;
        replayS1Cave = true;
        replayS2Cave = true;
        replayS3Cave = true;
        sole1Done = sole2Done = sole3BewetterungDone = sole3GebaeudeDone = trainRideInDone = trainRideOutDone = isLongwallCutterDone = true;
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
