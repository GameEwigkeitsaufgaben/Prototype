using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "SoChapOneRuntimeData")]
public class SoChapOneRuntimeData : ScriptableObject
{
    public string postOverlayToLoad = "";
    public bool videoPlaying = false;
    public OverlaySoundState overlaySoundState;
    public Texture2D cursorTexture3DCave;
    public Texture2D cursorInteract;
    public Texture2D cursorNoInteract;
    public Texture2D cursorDefault;

    public bool ch1GeneralUnlocked, ch2GrubenwasserUnlocked;

    [Header("Coalmine 116")]
    public float playerRotation; //needed for lookaroundMouse
    public bool playerInsideCave;
    public bool trainArrived;
    public bool viewPointS3passed;
    public bool entryAreaDone;
    public bool sole1Done, sole2Done, sole3BewetterungDone, sole3GebaeudeDone, trainRideInDone, trainRideOutDone, isLongwallCutterDone;

    [Header("Museum 117")]
    public Vector3 currentGroupPos = Vector3.zero; //-13.44922, 2.4, -2.670441
    public MuseumWaypoints currentMuseumWaypoint = MuseumWaypoints.WP0;
    public bool isMinerDone, isMythDone, isCoalifiationDone, isCarbonificationPeriodDone;
    public SoundMuseum soundSettingMuseum;

    [Header("Quiz 119")]
    public MinerFeedback quizMinerFeedback;

    [Header("GameProgress")]
    public bool video115Done, interaction116Done, interaction117Done, quiz119Done, progressCh1WithAdmin;



    [Header("General Settings")]
    public bool musicOn = true;

    private void OnEnable()
    {
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
        //Cursor.SetCursor(cursorInteract, Vector2.zero, CursorMode.Auto);
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
