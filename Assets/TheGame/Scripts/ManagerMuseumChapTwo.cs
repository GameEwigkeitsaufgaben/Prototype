using UnityEngine;
using UnityEngine.UI;
using SWS;


public class ManagerMuseumChapTwo : MonoBehaviour
{
    public Button btnNextGrubenwasser;
    public Camera cam;
    public GameObject group;

    public splineMove mySplineMove;
    public PathManager pGroupToTV, pathGroupToFliesspfad, pathGroupToExitZeche;
    public GameObject characterGuide;
    public MuseumOverlay overlay;
    public SpeechManagerMuseum speechManager;
    public WebGlVideoPlayer webglVideoPlayer;
    public RawImage rawImg;

    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChaptersRuntimeData runtimeDataChOverlap;
    private float offsetGroupCam;
    private MuseumWaypoints targetMuseumStation;

    [SerializeField] private MuseumWaypoints currentMuseumStation;
    private bool locomotionInMuseum = false;

    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChOverlap = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChOverlap.SetSceneCursor(runtimeDataChOverlap.cursorDefault);
    }

    // Start is called before the first frame update
    void Start()
    {
        btnNextGrubenwasser.colors = GameColors.GetInteractionColorBlock();
        btnNextGrubenwasser.interactable = false;
        offsetGroupCam = cam.transform.position.x - group.transform.position.x;

        switch (runtimeDataCh02.lastWP)
        {
            case MuseumWaypoints.None:
                speechManager.playMuseumGWIntro = true;
                group.transform.position = new Vector3(12.03f, 2.61f, -4.28f);
                break;
            case MuseumWaypoints.WPTV:
                Debug.Log("Entry at TV");
                group.transform.position =  runtimeDataCh02.groupPosition;
                break;
            case MuseumWaypoints.WPFliesspfad:
                Debug.Log("Entry at FF");
                group.transform.position = runtimeDataCh02.groupPosition;
                break;
        }
       
    }

    public void MoveToMuseumStation(int id)
    {
        locomotionInMuseum = true;
        if (mySplineMove.IsMoving()) return;
        
        switch (id)
        {
            case (int)MuseumWaypoints.WPTV:
                mySplineMove.pathContainer = pGroupToTV;
                targetMuseumStation = MuseumWaypoints.WPTV;
                break;
            case (int)MuseumWaypoints.WPFliesspfad:
                mySplineMove.pathContainer = pathGroupToFliesspfad;
                targetMuseumStation = MuseumWaypoints.WPFliesspfad;
                break;
            case (int)MuseumWaypoints.WPExitZeche:
                mySplineMove.pathContainer = pathGroupToExitZeche;
                targetMuseumStation = MuseumWaypoints.WPExitZeche;
                speechManager.playMuseumExitZeche = true;
                break;
        }

        mySplineMove.StartMove();
        characterGuide.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
        
    }

    public void ReachedWP()//Called from UnityEvent Gruppe in Inspector
    {
        currentMuseumStation =  targetMuseumStation;
        locomotionInMuseum = false;

        switch (currentMuseumStation)
        {
            case MuseumWaypoints.WPTV:
                overlay.ActivateOverlay(MuseumWaypoints.WPTV);
                runtimeDataCh02.lastWP = MuseumWaypoints.WPTV;
                runtimeDataCh02.groupPosition = group.transform.position;
                break;
            case MuseumWaypoints.WPFliesspfad:
                overlay.ActivateOverlay(MuseumWaypoints.WPFliesspfad);
                runtimeDataCh02.lastWP = MuseumWaypoints.WPFliesspfad;
                runtimeDataCh02.groupPosition = group.transform.position;
                break;
            case MuseumWaypoints.WPExitZeche:
                runtimeDataCh02.lastWP = MuseumWaypoints.WPExitZeche;
                runtimeDataCh02.groupPosition = group.transform.position;
                break;
        }
        characterGuide.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    public void RotateCharacter(GameObject character)
    {
        if (locomotionInMuseum)
        {
            character.transform.rotation = Quaternion.Euler(0.0f, -180.0f, 0.0f);
        }
    }

    //Start Dialog
    //Nach End Dialog Freischalten Button Next Grundwasser/Move autoaticlly
    //Stop bei TV, Show overlay mit Dialog, Zeige Video in TV, sobald Fertig, outro Enya mit Denkblase wärend dialog und foto von Wasseraustritt. dann Guide mit dialog.
    // Button Next Fliesspfade, Group und Cam wandert
    //Stop bei Fiesspfade: overlay mit Dialog, Wechle in Interaktion. bei zurück, überleitung zu Zeche und Vater.

    private void Update()
    {
        cam.transform.position = new Vector3(group.transform.position.x + offsetGroupCam, cam.transform.position.y, cam.transform.position.z);

        if (!btnNextGrubenwasser.interactable && speechManager.IsTalkingListFinished(GameData.NameTLMuseumGrundwasserIntro))
        {
            btnNextGrubenwasser.interactable = true;
        }
        if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumOutroExitZeche))
        {
            runtimeDataCh02.grundwasser212Done = true;
            gameObject.GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay("Overlay212");
        }
        //else if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumIntroTV))
        //{
        //    Debug.Log("Finished TV audio intro");
        //    gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
        //}
    }

}
