using UnityEngine;
using UnityEngine.UI;
using SWS;

public enum TVStation
{
    IntroOverlay,
    OutroTalking
}

public class ManagerMuseumChapTwo : MonoBehaviour
{
    public Button 
        btnNextGrubenwasser, 
        btnFliessPfade,
        btnReplayTalkingList;
    public Camera cam;
    public GameObject group;

    public splineMove mySplineMove;
    public PathManager pGroupToTV, pathGroupToFliesspfad, pathGroupToExitZeche;
    public PathManager pBeluftToB1;
    public GameObject characterGuide;
    public MuseumOverlay overlay;
    public SpeechManagerMuseumChapTwo speechManagerch2;
    public WebGlVideoPlayer webglVideoPlayer;
    public RawImage rawImg;
    public GameObject denkBubble;

    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChaptersRuntimeData runtimeDataChapters;
    private float offsetGroupCam;
    private MuseumWaypoints targetMuseumStation;

    [SerializeField] private MuseumWaypoints currentMuseumStation;
    private bool locomotionInMuseum = false;

    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    // Start is called before the first frame update
    void Start()
    {
        denkBubble.SetActive(false);
        btnNextGrubenwasser.colors = GameColors.GetInteractionColorBlock();
        btnNextGrubenwasser.interactable = false;
        btnFliessPfade.colors = GameColors.GetInteractionColorBlock();
        btnFliessPfade.interactable = false;
        btnReplayTalkingList.gameObject.SetActive(false);

        offsetGroupCam = cam.transform.position.x - group.transform.position.x;

        switch (runtimeDataCh02.lastWP)
        {
            case MuseumWaypoints.None:
                speechManagerch2.playMuseumGWIntro = true;
                runtimeDataCh02.state = TVStation.IntroOverlay;
                group.transform.position = new Vector3(12.03f, 2.61f, -4.28f);
                break;
            case MuseumWaypoints.WPTV:
                Debug.Log("Entry at TV");
                group.transform.position = runtimeDataCh02.groupPosition;

                currentMuseumStation = runtimeDataCh02.lastWP;
                if (runtimeDataCh02.replay2121TVoutro)
                {
                    btnReplayTalkingList.gameObject.SetActive(true);
                }
                else
                {
                    runtimeDataCh02.state = TVStation.OutroTalking;
                    speechManagerch2.playSecSilent = true;
                }
                
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
            case (int)MuseumWaypoints.None:
                mySplineMove.pathContainer = pGroupToTV;
                mySplineMove.reverse = true;
                targetMuseumStation = MuseumWaypoints.None;
                
                break;
            case (int)MuseumWaypoints.WPTV:
                mySplineMove.pathContainer = pGroupToTV;
                mySplineMove.reverse = false;
                targetMuseumStation = MuseumWaypoints.WPTV;
                btnReplayTalkingList.gameObject.SetActive(false);
                break;
            case (int)MuseumWaypoints.WPFliesspfad:
                mySplineMove.pathContainer = pathGroupToFliesspfad;
                targetMuseumStation = MuseumWaypoints.WPFliesspfad;
                btnReplayTalkingList.gameObject.SetActive(false);
                break;
            case (int)MuseumWaypoints.WPExitZeche:
                mySplineMove.pathContainer = pathGroupToExitZeche;
                targetMuseumStation = MuseumWaypoints.WPExitZeche;
                speechManagerch2.playMuseumExitZeche = true;
                break;
            case (int)MuseumWaypoints.WPBeluft:
                mySplineMove.pathContainer = pBeluftToB1;
                targetMuseumStation = MuseumWaypoints.WPAbsetzBecken;
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
            case MuseumWaypoints.None:
                btnReplayTalkingList.gameObject.SetActive(runtimeDataCh02.replayTL2120intro);
                runtimeDataCh02.lastWP = MuseumWaypoints.None;
                runtimeDataCh02.groupPosition = group.transform.position;
                break;
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
            case MuseumWaypoints.WPAbsetzBecken:
                runtimeDataCh02.lastWP = MuseumWaypoints.WPAbsetzBecken;
                //runtimeDataCh02.groupPosition = group.transform.position;
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

    public void ReplayTalkingList()
    {
        if(MuseumWaypoints.None == currentMuseumStation)
        {
            speechManagerch2.playMuseumGWIntro = true;
        }
        else if (MuseumWaypoints.WPTV == currentMuseumStation)
        {
            switch (runtimeDataCh02.state)
            {
                case TVStation.IntroOverlay:
                    overlay.ActivateOverlay(MuseumWaypoints.WPTV);
                    break;
                case TVStation.OutroTalking:
                    speechManagerch2.playSecSilent = true;
                    break;
            }
            //if(currentMuseumStation == runtimeDataCh02.lastWP)
            //{
            //    speechManagerch2.playSecSilent = true;
            //}
            //else
            //{
            //    overlay.ActivateOverlay(MuseumWaypoints.WPTV);
            //}
        }
    }

    public void GoToTVScene()
    {
        gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
    }


    private void Update()
    {
        cam.transform.position = new Vector3(group.transform.position.x + offsetGroupCam, cam.transform.position.y, cam.transform.position.z);

        if (!runtimeDataCh02.replayTL2120intro && speechManagerch2.IsTalkingListFinished(GameData.NameCH2TLMuseumGrundwasserIntro))
        {
            runtimeDataCh02.replayTL2120intro = true;
            btnReplayTalkingList.gameObject.SetActive(true);
        }

        if (!btnNextGrubenwasser.interactable && runtimeDataCh02.replayTL2120intro)
        {
            btnNextGrubenwasser.interactable = true;
        }

        if (speechManagerch2.IsTalkingListFinished(GameData.NameTLSecSilent))
        {
            runtimeDataCh02.state = TVStation.OutroTalking;
            speechManagerch2.playMuseumGWTVOutro = true;
            denkBubble.SetActive(true);
        }

        if (speechManagerch2.IsTalkingListFinished(GameData.NameCH2TLMuseumOutroTV))
        {
            denkBubble.SetActive(false);
            runtimeDataCh02.replay2121TVoutro = true;
            btnReplayTalkingList.gameObject.SetActive(true);
        }

        if (!runtimeDataCh02.replayOverlay2121)
        {

        }
        if (!btnFliessPfade.interactable) 
        {
            if(runtimeDataCh02.replayOverlay2121 && runtimeDataCh02.replay2121TVoutro && runtimeDataCh02.interactTVDone)
            {
                btnFliessPfade.interactable = true;
            }
           
        }
        if (speechManagerch2.IsTalkingListFinished(GameData.NameCH2TLMuseumOutroExitZeche))
        {
            runtimeDataCh02.progressPost2110GWReinigungDone = true;
            gameObject.GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay(GameData.NameOverlay212);
        }
        //else if (speechManager.IsTalkingListFinished(GameData.NameTLMuseumIntroTV))
        //{
        //    Debug.Log("Finished TV audio intro");
        //    gameObject.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02MuseumTV);
        //}
    }

}
