//Add a new Waypoint to s3:
//1) make a gameobject with a collider, 2) go to Waypoint Manager in your Project. 3) Set Name of path, add to collider Objects the paths (press p).
//4) Copy postion from S3CaveToViewpoint to the newly created path. 5) Adjust your Waypoints position, copy/paste values bewetterung, etc. to your wps.

using SWS;
using UnityEngine;
using UnityEngine.UI;

public enum MineWayPoints
{
    insideCave = 0,
    viewpoint = 1,
    viewpointBewetterung = 2,
    viewpointBahnsteig = 3,
    viewpointOVMine = 4,
    viewpointLWBahnsteig = 5,
    viewpointLWLWCutter = 6,
    error = 7,
    reloadBahnsteig = 8
}

public enum PathWaypoints
{
    startPath = 0,
    entPath=1
}

public class CoalmineWaypointManager : MonoBehaviour
{  
    public splineMove playerSplineMove;
    public splineMove characterSplineMove;

    public PathManager pathS1CaveToViewpoint, pathS2CaveToViewpoint, pathS3CaveToViewpoint;
    public PathManager pathS3ViewpointToBewetterung, pathS3ViewpointToBahnsteig, pathS3ViewpointToOVMine, 
                       pathS3BahnsteigToOVMine, pathS3BewetterungToBahnsteig, pathS3BewetterungToOVMine;
    public PathManager pathLWBahnsteigToLWC;

    public PathManager charS3CaveToVP;

    //wpSx represent 
    public GameObject wpS1ViewpointSign, wpS2ViewpointSign, wpS3ViewpointSign, wpInsideCaveSign;
    public float wpAdjustHightViewpoint;

    public GameObject triggerPlayerInCave;
    public Canvas bahnsteig, bewetterung, ovmine, viewpoint;
    public CoalmineSpeechManger speechManager;

    private bool helperSetPath = false;
    private Player myPlayer;

    private SoChapOneRuntimeData runtimeData;
    private SoSfx sfx;

    [Header("Assigned during runtime")]
    public Button wps1ViewpointBtn, wps2ViewpointBtn, wps3ViewpointBtn;
    
    [SerializeField] 
    private Button runtimeViewpointBtn, caveBtn, bahnsteigBtn, bewetterungBtn, ovmineBtn;
    
    public MineWayPoints currentWP;

    //public bool trainArrived = false;

    //https://forum.unity.com/threads/onenable-before-awake.361429/
    private void Awake()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);

        //The order has to be Canvas (parent), first child ist btn, second child is text; 
        wps1ViewpointBtn = wpS1ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps2ViewpointBtn = wpS2ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps3ViewpointBtn = wpS3ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        bahnsteigBtn = bahnsteig.GetComponentsInChildren<Transform>()[1].GetComponentInChildren<Button>();
        bewetterungBtn = bewetterung.GetComponentsInChildren<Transform>()[1].GetComponentInChildren<Button>();
        ovmineBtn = ovmine.GetComponentsInChildren<Transform>()[1].GetComponentInChildren<Button>();
        caveBtn = wpInsideCaveSign.GetComponentsInChildren<Transform>()[1].GetComponentInChildren<Button>();
    }

    private void Start()
    {
        myPlayer = playerSplineMove.gameObject.GetComponent<Player>();

        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        
        if (myPlayer.playerInCave)
        {
            caveBtn.gameObject.SetActive(false);
        }
    }

    public bool IsBahnsteigCurrentWP()
    {
        //Debug.Log("for train: is bahnsteig current wP: " + (currentWP == MineWayPoints.viewpointBahnsteig || currentWP == MineWayPoints.reloadBahnsteig));
        //Debug.Log("for train: " + currentWP);
        return (currentWP == MineWayPoints.viewpointBahnsteig || currentWP == MineWayPoints.reloadBahnsteig);
    }

    //von splinemove im inspector aufgerufen
    public void DeactivateButton()
    {
        //if (playerSplineMove.currentPoint == (int)MineWayPoints.viewpoint)
        //{
        //    runtimeViewpointBtn.gameObject.SetActive(false);
        //    caveBtn.gameObject.SetActive(true);
        //}
        //else if(playerSplineMove.currentPoint == (int)MineWayPoints.insideCave)
        //{
        //    runtimeViewpointBtn.gameObject.SetActive(true);
        //    //caveBtn.gameObject.SetActive(false);
        //    GameData.liftBtnsEnabled = true;
        //}
    }

    public MineWayPoints GetCurrentWP()
    {
        if (currentWP == MineWayPoints.reloadBahnsteig) return MineWayPoints.viewpointBahnsteig;
        Debug.Log("player current point start (0): " + playerSplineMove.currentPoint);

        if(playerSplineMove.currentPoint == (int)PathWaypoints.startPath)
        {
            Debug.Log("player current point start (0) name : " + playerSplineMove.pathContainer.name);
            if (playerSplineMove.pathContainer.name == pathS3CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS2CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS1CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBahnsteig.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBewetterung.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToOVMine.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3BewetterungToOVMine.name) return MineWayPoints.viewpointBewetterung;
            else if (playerSplineMove.pathContainer.name == pathS3BewetterungToBahnsteig.name) return MineWayPoints.viewpointBewetterung;
            else if (playerSplineMove.pathContainer.name == pathS3BahnsteigToOVMine.name) return MineWayPoints.viewpointBahnsteig;
        }

        Debug.Log("player current point end (1) name : " + playerSplineMove.pathContainer.name);
        if (playerSplineMove.pathContainer.name == pathS3CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS2CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS1CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBahnsteig.name) return MineWayPoints.viewpointBahnsteig;
        else if (playerSplineMove.pathContainer.name == pathS3BewetterungToBahnsteig.name) return MineWayPoints.viewpointBahnsteig;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBewetterung.name) return MineWayPoints.viewpointBewetterung;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToOVMine.name) return MineWayPoints.viewpointOVMine;
        else if (playerSplineMove.pathContainer.name == pathS3BewetterungToOVMine.name) return MineWayPoints.viewpointOVMine;
        else if (playerSplineMove.pathContainer.name == pathS3BahnsteigToOVMine.name) return MineWayPoints.viewpointOVMine;

        return MineWayPoints.error;
    }

    public void SetCurrentWaypoint()
    {
        currentWP = GetCurrentWP();

        SetWaypointMarkers();
    }


    public void DetectAndSetPath(MineWayPoints targetWP)
    {
        SetCurrentWaypoint();

        if(currentWP == MineWayPoints.error)
        {
            Debug.Log("++++++++++++++++++++Etwas schiefgelaufen!! " + GetCurrentWP());
            return;
        }

        
        if (targetWP == MineWayPoints.viewpoint)
        {
            if (currentWP == MineWayPoints.viewpointBewetterung)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToBewetterung;
                playerSplineMove.reverse = true;
            }
            else if (IsBahnsteigCurrentWP())
            {
                playerSplineMove.pathContainer = pathS3ViewpointToBahnsteig;
                playerSplineMove.reverse = true;
            }
            else if (currentWP == MineWayPoints.viewpointOVMine)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToOVMine;
                playerSplineMove.reverse = true;
            }
            else if (currentWP == MineWayPoints.insideCave)
            {
                playerSplineMove.pathContainer = pathS3CaveToViewpoint;
                playerSplineMove.reverse = false;
             
                //characterSplineMove.pathContainer = charS3CaveToVP;
                //characterSplineMove.reverse = false;
            }
        }
        else if (targetWP == MineWayPoints.viewpointBewetterung)
        {
            if (currentWP == MineWayPoints.viewpoint)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToBewetterung;
                playerSplineMove.reverse = false;
            }
            else if (IsBahnsteigCurrentWP())
            {
                playerSplineMove.pathContainer = pathS3BewetterungToBahnsteig;
                playerSplineMove.reverse = true;
            }
            else if (currentWP == MineWayPoints.viewpointOVMine)
            {
                playerSplineMove.pathContainer = pathS3BewetterungToOVMine;
                playerSplineMove.reverse = true;
            }
        }
        else if (targetWP == MineWayPoints.viewpointBahnsteig)
        {
            if (currentWP == MineWayPoints.viewpoint)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToBahnsteig;
                playerSplineMove.reverse = false;
            }
            else if (currentWP == MineWayPoints.viewpointBewetterung)
            {
                playerSplineMove.pathContainer = pathS3BewetterungToBahnsteig;
                playerSplineMove.reverse = false;
            }
            else if (currentWP == MineWayPoints.viewpointOVMine)
            {
                playerSplineMove.pathContainer = pathS3BahnsteigToOVMine;
                playerSplineMove.reverse = true;
            }
        }
        else if (targetWP == MineWayPoints.viewpointOVMine)
        {
            if (currentWP == MineWayPoints.viewpoint)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToOVMine;
                playerSplineMove.reverse = false;
            }
            else if (IsBahnsteigCurrentWP())
            {
                playerSplineMove.pathContainer = pathS3BahnsteigToOVMine;
                playerSplineMove.reverse = false;
            }
            else if (currentWP == MineWayPoints.viewpointBewetterung)
            {
                playerSplineMove.pathContainer = pathS3BewetterungToOVMine;
                playerSplineMove.reverse = false;
            }
        }
        Debug.Log("current WP " + currentWP + " target WP " + targetWP + " -pathname " + playerSplineMove.pathContainer.name + " -reverse " + playerSplineMove.reverse);
    }

    public void SetS3WaypointsInteractable(bool interactable)
    {
        ovmineBtn.interactable = bahnsteigBtn.interactable = wps3ViewpointBtn.interactable =
            bewetterungBtn.interactable = caveBtn.interactable = interactable;
    }

    public void ReloadWPBahnsteig()
    {
        currentWP = MineWayPoints.reloadBahnsteig;
        SetWaypointMarkers();
    }

    public void PlayViewpointtTalking()
    {
        switch (currentWP)
        {
            case MineWayPoints.viewpoint:
                if (runtimeData.currentCoalmineStop == CoalmineStop.Sole1) speechManager.playSole1Vp = true;
                if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2) speechManager.playSole2Badewannen = true;
                break;
            case MineWayPoints.viewpointBewetterung:
                speechManager.playSole3WPBewetterung = true;
                break;
            case MineWayPoints.viewpointLWBahnsteig:
                speechManager.playSole3WPBahnsteig = true;
                break;
            case MineWayPoints.viewpointOVMine:
                speechManager.playSole3WPOVMine = true;
                break;
        }
    }
    

    public void SetWaypointMarkers()
    {
        Debug.Log("MineWaypoint is :"  + currentWP);

        if (currentWP == MineWayPoints.insideCave)
        {
            caveBtn.gameObject.SetActive(false);
            wps3ViewpointBtn.gameObject.SetActive(true);
            wps2ViewpointBtn.gameObject.SetActive(true);
            wps1ViewpointBtn.gameObject.SetActive(true);
            myPlayer.SetPlayerRotation(0f,false);
        }
        else if (currentWP == MineWayPoints.viewpoint)
        {
            caveBtn.gameObject.SetActive(true);
            wps3ViewpointBtn.gameObject.SetActive(false);
            wps2ViewpointBtn.gameObject.SetActive(false);
            wps1ViewpointBtn.gameObject.SetActive(false);
        }

        if (runtimeData.currentCoalmineStop != CoalmineStop.Sole3) return;

        SetAllWPBtnActive(true);

        if (currentWP == MineWayPoints.viewpointBewetterung)
        {
            bewetterungBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            ChangeS3WPRotations(-90.0f, -90.0f, -90.0f, 0.0f);

            runtimeData.sole3BewetterungDone = true;

            //Debug.Log(currentWP + "sould be set " + transform.localPosition);
        }
        else if (currentWP == MineWayPoints.viewpoint)
        {
            wps3ViewpointBtn.gameObject.SetActive(false);
            ChangeS3WPRotations(0.0f, 90.0f, 90.0f, 150.0f);
            runtimeData.viewPointS3passed = true;
        }
        else if (IsBahnsteigCurrentWP())
        {
            bahnsteigBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            ChangeS3WPRotations(-55.0f, 0.0f, 175.0f, 158.0f);
        }
        else if (currentWP == MineWayPoints.viewpointOVMine)
        {
            ovmineBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            ChangeS3WPRotations(180.0f, -33.0f, 0.0f, -250.0f);

            runtimeData.sole3GebaeudeDone = true;
        }
        else if (currentWP == MineWayPoints.insideCave)
        {
            SetAllWPBtnActive(false);
            wps3ViewpointBtn.gameObject.SetActive(true);

            ChangeS3WPRotations(102.0f, 0.0f, 0.0f, 0.0f);
            //myPlayer.SetPlayerToAnkerPosition();
            //myPlayer.ReloadPlayerAtS3Cave();
            //Debug.Log("------------------SetInsideCave");
        }
    }

    //Ändern der Rotation der Viewpoits passiert über das Canvas(WP), WPs werden
    //so in die Richtung gedreht, dass die Buttons clickable sind. 
    //Wichtig auch die Position nur über das Canvas ändern, sonst funkt die Rotation nicht mehr. 
    private void ChangeS3WPRotations(float viewpointRotationY, float bahnsteigRotationY, float ovMineRotationY, float bewetterungRotationY)
    {
        viewpoint.transform.localRotation = Quaternion.Euler(0, viewpointRotationY, 0);
        bahnsteig.transform.localRotation = Quaternion.Euler(0, bahnsteigRotationY, 0);
        ovmine.transform.localRotation = Quaternion.Euler(0, ovMineRotationY, 0);
        bewetterung.transform.localRotation = Quaternion.Euler(0, bewetterungRotationY, 0);
    }

    //called from inspector: UnityEvents in payerspline move
    public void SetAllWPBtnActive(bool active)
    {
        caveBtn.gameObject.SetActive(active);

        wps1ViewpointBtn.gameObject.SetActive(active);
        wps2ViewpointBtn.gameObject.SetActive(active);
        wps3ViewpointBtn.gameObject.SetActive(active);
        
        ovmineBtn.gameObject.SetActive(active);
        bahnsteigBtn.gameObject.SetActive(active);
        bewetterungBtn.gameObject.SetActive(active);

        Debug.Log("setAllbtn to "+ active + " ---------------------------------");
    }

    //Methods Called from Inspector
    public void MoveOut()
    {
        runtimeData.liftBtnsAllEnabled = false;

        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole3) return;

        playerSplineMove.reverse = false;
        playerSplineMove.StartMove();
    }

    public void MoveIn()
    {
        //needed because more than one path uses the viewpoint waypoint
        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole3)
        {
            playerSplineMove.pathContainer = pathS3CaveToViewpoint;
        }

        playerSplineMove.reverse = true;
        playerSplineMove.StartMove();
        SetCharacterPosition(new Vector3(0.0f, 1.6f, 0.0f));
    }

    public void MoveToBewetterung()
    {
        SetCharacterPosition(new Vector3(35.3f, 1.6f, -11.6f));
        DetectAndSetPath(MineWayPoints.viewpointBewetterung);
        playerSplineMove.StartMove();
    }

    public void MoveToBahnsteig()
    {
        SetCharacterPosition(new Vector3(21.77f, 1.6f, -4f));
        DetectAndSetPath(MineWayPoints.viewpointBahnsteig);
        playerSplineMove.StartMove();
    }

    public void MoveToOVMine()
    {
        SetCharacterPosition(new Vector3(23.0f, 1.6f, -8f));
        DetectAndSetPath(MineWayPoints.viewpointOVMine);
        playerSplineMove.StartMove();
    }

    public void MoveToViewpoint()
    {
        if (runtimeData.currentCoalmineStop != CoalmineStop.Sole3) return;
        
        SetCharacterPosition(new Vector3(7.11f, 1.6f, -2.16f));
        DetectAndSetPath(MineWayPoints.viewpoint);
        playerSplineMove.StartMove();
    }

    private void SetCharacterPosition(Vector3 charpos)
    {
        characterSplineMove.gameObject.transform.localPosition = charpos;
    }

    private void Update()
    {
        if (GameData.moveCave)
        {
            myPlayer.followAnker = true;
            helperSetPath = false;
            return;
        }

        myPlayer.followAnker = false;

        if (runtimeData.currentCoalmineStop == CoalmineStop.EntryArea) return;

        switch (runtimeData.currentCoalmineStop)
        {
            case CoalmineStop.Sole1:

                //setPath
                if (!helperSetPath)
                {
                    playerSplineMove.pathContainer = pathS1CaveToViewpoint;
                    pathS1CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);

                    SetAllWPBtnActive(true);
                    caveBtn.gameObject.SetActive(false);

                    helperSetPath = true;
                }

                if (runtimeData.sole1Done) return;


                if (speechManager.IsMineS1CaveTalkingFinished() && !runtimeData.replayS1Cave)
                {
                    runtimeData.replayS1Cave = true;
                    wps1ViewpointBtn.GetComponent<Button>().interactable = true;
                    return;
                }

                if (speechManager.IsMineS1VpTalkingFinished())
                {
                    runtimeData.sole1Done = true;
                }

                break;
            case CoalmineStop.Sole2:

                //setPath
                if (!helperSetPath)
                {
                    playerSplineMove.pathContainer = pathS2CaveToViewpoint;
                    pathS2CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);

                    SetAllWPBtnActive(true);
                    caveBtn.gameObject.SetActive(false);

                    helperSetPath = true;
                }

                if (runtimeData.sole2Done) return;

                if (speechManager.IsMineS2CaveTalkingFinished() && !runtimeData.replayS2Cave)
                {
                    runtimeData.replayS2Cave = true;
                    wps2ViewpointBtn.GetComponent<Button>().interactable = true;
                    return;
                }

                if (speechManager.IsMineS2VpTalkingFinished())
                {
                    runtimeData.sole2Done = true;
                }

                break;
            case CoalmineStop.Sole3:

                //SetPath
                if (!helperSetPath)
                {
                    playerSplineMove.pathContainer = pathS3CaveToViewpoint;

                    //AdjustWpHightToPlayerHight
                    pathS3CaveToViewpoint.transform.position = pathS3ViewpointToBewetterung.transform.position =
                        pathS3ViewpointToBahnsteig.transform.position = pathS3ViewpointToOVMine.transform.position =
                        pathS3BewetterungToBahnsteig.transform.position = pathS3BewetterungToOVMine.transform.position =
                        pathS3BahnsteigToOVMine.transform.position =
                        new Vector3(0f, myPlayer.transform.position.y, 0f);

                    helperSetPath = true;

                    if (GameData.sohleToReload == (int)CoalmineStop.Sole3)
                    {
                        ReloadWPBahnsteig();
                        myPlayer.RealoadPlayerAtS3Bahnsteig();
                        return;
                    }

                    SetAllWPBtnActive(false);
                    wps3ViewpointBtn.gameObject.SetActive(true);
                }

                //Move in sole
                if (runtimeData.sole3BewetterungDone && runtimeData.sole3GebaeudeDone) return;

                if (runtimeData.sole3BewetterungDone || runtimeData.sole3GebaeudeDone)
                {
                    wps3ViewpointBtn.GetComponent<Button>().interactable = true;
                    return;
                }
                else
                {
                    if (speechManager.IsTalkingFinished(GameData.NameTLMineS3Cave) && !runtimeData.replayS3Cave)
                    {
                        runtimeData.replayS3Cave = true;
                        wps3ViewpointBtn.GetComponent<Button>().interactable = true;
                        return;
                    }
                }

                break;
        }

        //audios in Cave Collider started!
        //if(runtimeData.currentCoalmineStop == CoalmineStop.Sole1 && !runtimeData.sole1Done)
        //{
        //    if (speechManager.IsMineS1CaveTalkingFinished() && !runtimeData.replayS1Cave)
        //    {
        //        runtimeData.replayS1Cave = true;
        //        wps1ViewpointBtn.GetComponent<Button>().interactable = true;
        //        return;
        //    }

        //    if (speechManager.IsMineS1VpTalkingFinished())
        //    {
        //        runtimeData.sole1Done = true;
        //    }
        //}

        //if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2 && !runtimeData.sole2Done)
        //{
        //    if (speechManager.IsMineS2CaveTalkingFinished() && !runtimeData.replayS2Cave)
        //    {
        //        runtimeData.replayS2Cave = true;
        //        wps2ViewpointBtn.GetComponent<Button>().interactable = true;
        //        return;
        //    }

        //    if (speechManager.IsMineS2VpTalkingFinished())
        //    {
        //        runtimeData.sole2Done = true;
        //    }
        //}

        //if (runtimeData.currentCoalmineStop == CoalmineStop.Sole3)
        //{
        //    if (runtimeData.sole3BewetterungDone || runtimeData.sole3GebaeudeDone)
        //    {
        //        wps3ViewpointBtn.GetComponent<Button>().interactable = true;
        //        return;
        //    }
        //    else
        //    {
        //        if (speechManager.IsTalkingFinished(GameData.NameTLMineS3Cave) && !wps3ViewpointBtn.GetComponent<Button>().interactable)
        //        {
        //            wps3ViewpointBtn.GetComponent<Button>().interactable = true;
        //            return;
        //        }
        //    }
        //}

       

        //if (runtimeData.currentCoalmineStop == CoalmineStop.Sole3 && !helperSetPath)
        //{
        //    //playerSplineMove.pathContainer = pathS3CaveToViewpoint;

        //    ////AdjustWpHightToPlayerHight
        //    //pathS3CaveToViewpoint.transform.position = pathS3ViewpointToBewetterung.transform.position = 
        //    //    pathS3ViewpointToBahnsteig.transform.position = pathS3ViewpointToOVMine.transform.position = 
        //    //    pathS3BewetterungToBahnsteig.transform.position = pathS3BewetterungToOVMine.transform.position = 
        //    //    pathS3BahnsteigToOVMine.transform.position = 
        //    //    new Vector3(0f, myPlayer.transform.position.y, 0f);

        //    //helperSetPath = true;

            
        //    //if (GameData.sohleToReload == (int)CoalmineStop.Sole3)
        //    //{
        //    //    ReloadWPBahnsteig();
        //    //    myPlayer.RealoadPlayerAtS3Bahnsteig();
        //    //    return;
        //    //}

        //    //SetAllWPBtnActive(false);
        //    //wps3ViewpointBtn.gameObject.SetActive(true);
        //}
        //else if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2 && !helperSetPath)
        //{
        //    //playerSplineMove.pathContainer = pathS2CaveToViewpoint;
        //    //pathS2CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);
            
        //    //SetAllWPBtnActive(true);
        //    //caveBtn.gameObject.SetActive(false);

        //    //helperSetPath = true;
        //}
        //else if (runtimeData.currentCoalmineStop == CoalmineStop.Sole1 && !helperSetPath)
        //{
        //    playerSplineMove.pathContainer = pathS1CaveToViewpoint;
        //    pathS1CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);

        //    SetAllWPBtnActive(true);
        //    caveBtn.gameObject.SetActive(false);

        //    helperSetPath = true;
        //}
    }
}
