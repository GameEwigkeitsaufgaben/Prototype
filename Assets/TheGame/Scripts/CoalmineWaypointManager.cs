//Add a new Waypoint to s3:
//make to gameobjects with colliders, got to Waypoint Manager in your Project. Set Name of path, add to collider Objects the paths (press p).
//Copy postion from S3CaveToViewpoint to the newly created path. Adjust your Waypoints position, copy/paste values bewetterung, etc. to your wps.

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
    error = 5
}

public enum PathWaypoints
{
    startPath = 0,
    entPath=1
}

public class CoalmineWaypointManager : MonoBehaviour
{  
    public splineMove playerSplineMove;
    public PathManager pathS1CaveToViewpoint, pathS2CaveToViewpoint, pathS3CaveToViewpoint;
    public PathManager pathS3ViewpointToBewetterung, pathS3ViewpointToBahnsteig, pathS3ViewpointToOVMine, 
                       pathS3BahnsteigToOVMine, pathS3BewetterungToBahnsteig, pathS3BewetterungToOVMine;
    public GameObject wpS1ViewpointSign, wpS2ViewpointSign, wpS3ViewpointSign, caveSign;
    public float wpAdjustHightViewpoint;

    public GameObject triggerPlayerInCave;

    [SerializeField]
    private Button wps1ViewpointBtn, wps2ViewpointBtn, wps3ViewpointBtn;
    [SerializeField] 
    private Button runtimeViewpointBtn, caveBtn, bahnsteigBtn, bewetterungBtn, ovmineBtn;
    

    public Canvas bahnsteig, bewetterung, ovmine, viewpoint;
    private bool helperSetPath = false;
    private Player myPlayer;
    MineWayPoints currentWP;

    public bool trainArrived = false;


    private void Start()
    {
        myPlayer = playerSplineMove.gameObject.GetComponent<Player>();
        
        if (myPlayer.playerInCave)
        {
            caveBtn.gameObject.SetActive(false);
        }

        wps1ViewpointBtn = wpS1ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps2ViewpointBtn = wpS2ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps3ViewpointBtn = wpS3ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();

        bahnsteigBtn = bahnsteig.GetComponentInChildren<Button>();
        bewetterungBtn = bewetterung.GetComponentInChildren<Button>();
        ovmineBtn = ovmine.GetComponentInChildren<Button>();
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
        if(playerSplineMove.currentPoint == (int)PathWaypoints.startPath)
        {
            if (playerSplineMove.pathContainer.name == pathS3CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS2CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS1CaveToViewpoint.name) return MineWayPoints.insideCave;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBahnsteig.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBewetterung.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3ViewpointToOVMine.name) return MineWayPoints.viewpoint;
            else if (playerSplineMove.pathContainer.name == pathS3BewetterungToOVMine.name) return MineWayPoints.viewpointBewetterung;
            else if (playerSplineMove.pathContainer.name == pathS3BahnsteigToOVMine.name) return MineWayPoints.viewpointBahnsteig;
        }

        if (playerSplineMove.pathContainer.name == pathS3CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS2CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS1CaveToViewpoint.name) return MineWayPoints.viewpoint;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBahnsteig.name) return MineWayPoints.viewpointBahnsteig;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToBewetterung.name) return MineWayPoints.viewpointBewetterung;
        else if (playerSplineMove.pathContainer.name == pathS3ViewpointToOVMine.name) return MineWayPoints.viewpointOVMine;
        else if (playerSplineMove.pathContainer.name == pathS3BewetterungToOVMine.name) return MineWayPoints.viewpointOVMine;
        else if (playerSplineMove.pathContainer.name == pathS3BahnsteigToOVMine.name) return MineWayPoints.viewpointOVMine;

        return MineWayPoints.error;
    }

    public void DetectAndSetPath(MineWayPoints targetWP)
    {
        currentWP = GetCurrentWP();

        SetWaypointMarkers();

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
            else if (currentWP == MineWayPoints.viewpointBahnsteig)
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
            }
        }
        else if (targetWP == MineWayPoints.viewpointBewetterung)
        {
            if (currentWP == MineWayPoints.viewpoint)
            {
                playerSplineMove.pathContainer = pathS3ViewpointToBewetterung;
                playerSplineMove.reverse = false;
            }
            else if (currentWP == MineWayPoints.viewpointBahnsteig)
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
            else if (currentWP == MineWayPoints.viewpointBahnsteig)
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
        Debug.Log("current WP " + currentWP + " target WP " + targetWP + " -pathname " + playerSplineMove.name + " -reverse " + playerSplineMove.reverse);
    }

    public void SetS3WaypointsInteractable(bool interactable)
    {
        ovmineBtn.interactable = bahnsteigBtn.interactable = wps3ViewpointBtn.interactable =
            bewetterungBtn.interactable = caveBtn.interactable = interactable;
    }

    public void SetWaypointMarkers()
    {
        MineWayPoints tmp = GetCurrentWP();

        if (tmp == MineWayPoints.insideCave)
        {
            caveBtn.gameObject.SetActive(false);
            wps3ViewpointBtn.gameObject.SetActive(true);
            wps2ViewpointBtn.gameObject.SetActive(true);
            wps1ViewpointBtn.gameObject.SetActive(true);
        }
        else if (tmp == MineWayPoints.viewpoint)
        {
            caveBtn.gameObject.SetActive(true);
            wps3ViewpointBtn.gameObject.SetActive(false);
            wps2ViewpointBtn.gameObject.SetActive(false);
            wps1ViewpointBtn.gameObject.SetActive(false);
        }

        if (GameData.currentStopSohle != (int)CoalmineStop.Sole3) return;
        
        if (tmp == MineWayPoints.viewpointBewetterung)
        {
            bewetterungBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            viewpoint.transform.localRotation = Quaternion.Euler(0, 40, 0);
            bahnsteig.transform.localRotation = Quaternion.Euler(0, 90, 0);
            ovmine.transform.localRotation = Quaternion.Euler(0, 90, 0);
            

            Debug.Log(tmp + "sould be set " + transform.localPosition);
        }
        else if (tmp == MineWayPoints.viewpoint)
        {
            SetAllWPBtnActive(true);

            bahnsteig.transform.localRotation = Quaternion.Euler(0, 90, 0);
            ovmine.transform.localRotation = Quaternion.Euler(0, 90, 0);
            bewetterung.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (tmp == MineWayPoints.viewpointBahnsteig)
        {
            bahnsteigBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            viewpoint.transform.localRotation = Quaternion.Euler(0, -55, 0);
            ovmine.transform.localRotation = Quaternion.Euler(0, 175, 0);
            bewetterung.transform.localRotation = Quaternion.Euler(0, 103, 0);
        }
        else if (tmp == MineWayPoints.viewpointOVMine)
        {
            ovmineBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(false);

            viewpoint.transform.localRotation = Quaternion.Euler(0, 180, 0);
            bahnsteig.transform.localRotation = Quaternion.Euler(0, -33, 0);
            bewetterung.transform.localRotation = Quaternion.Euler(0, -65, 0);
        }
        else if (tmp == MineWayPoints.insideCave)
        {
            SetAllWPBtnActive(false);
            wps3ViewpointBtn.gameObject.SetActive(true);
        }
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

        Debug.Log("setAllbtn active---------------------------------");
    }

    //Methods Called from Inspector
    public void MoveOut()
    {
        GameData.liftBtnsEnabled = false;

        if (GameData.currentStopSohle == (int)CoalmineStop.Sole3) return;

        playerSplineMove.reverse = false;
        playerSplineMove.StartMove();
    }

    public void MoveIn()
    {
        //needed because more than one path in this sole is shares the viewpoint waypoint
        if (GameData.currentStopSohle == (int)CoalmineStop.Sole3)
        {
            playerSplineMove.pathContainer = pathS3CaveToViewpoint;
        }

        playerSplineMove.reverse = true;
        playerSplineMove.StartMove();
    }

    public void MoveToBewetterung()
    {
        DetectAndSetPath(MineWayPoints.viewpointBewetterung);
        playerSplineMove.StartMove();
    }

    public void MoveToBahnsteig()
    {
        DetectAndSetPath(MineWayPoints.viewpointBahnsteig);
        playerSplineMove.StartMove();
    }

    public void MoveToOVMine()
    {
        DetectAndSetPath(MineWayPoints.viewpointOVMine);
        playerSplineMove.StartMove();
    }

    public void MoveToViewpoint()
    {
        if (GameData.currentStopSohle != (int)CoalmineStop.Sole3) return;
        DetectAndSetPath(MineWayPoints.viewpoint);
        playerSplineMove.StartMove();
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

        if (GameData.currentStopSohle == (int)CoalmineStop.EntryArea) return;

        if (GameData.currentStopSohle == (int)CoalmineStop.Sole3 && !helperSetPath)
        {
            playerSplineMove.pathContainer = pathS3CaveToViewpoint;

            //AdjustWpHightToPlayerHight
            pathS3CaveToViewpoint.transform.position = pathS3ViewpointToBewetterung.transform.position = 
                pathS3ViewpointToBahnsteig.transform.position = pathS3ViewpointToOVMine.transform.position = 
                pathS3BewetterungToBahnsteig.transform.position = pathS3BewetterungToOVMine.transform.position = 
                pathS3BahnsteigToOVMine.transform.position = 
                new Vector3(0f, myPlayer.transform.position.y, 0f);

            SetAllWPBtnActive(false);
            wps3ViewpointBtn.gameObject.SetActive(true);

            helperSetPath = true;
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole2 && !helperSetPath)
        {
            playerSplineMove.pathContainer = pathS2CaveToViewpoint;
            pathS2CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);
            
            SetAllWPBtnActive(true);
            caveBtn.gameObject.SetActive(false);

            helperSetPath = true;
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole1 && !helperSetPath)
        {
            playerSplineMove.pathContainer = pathS1CaveToViewpoint;
            pathS1CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);

            SetAllWPBtnActive(true);
            caveBtn.gameObject.SetActive(false);

            helperSetPath = true;
        }
    }
}
