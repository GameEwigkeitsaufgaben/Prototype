using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CaveWayPoints
{
    insideCave = 0,
    viewpoint = 1,
    Sole2Cave = 2,
    Sole2Tunnel = 3,
    Sole3Cave = 4,
    Sole3Bahnsteig = 5,
    Sole3Tafel= 6
}

public class CaveWaypointManager : MonoBehaviour
{
    public splineMove playerSplineMove;
    public PathManager ps1CaveToTunnel, ps2CaveToViewpoint;
    public GameObject triggerPlayerInCave;
    

    public CaveWayPoints currentWP, targetWP;
    public Button sole1StollenBtn, sole2WPViewpointBtn, caveBtn;

    bool helperSetPath = false;
    Player myPlayer;

    private void Start()
    {
        myPlayer = playerSplineMove.gameObject.GetComponent<Player>();
        
        if (myPlayer.playerInCave)
        {
            caveBtn.gameObject.SetActive(false);
        }

        
    }


    public void DeactivateButton()
    {
        Debug.Log("Soooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo current " + playerSplineMove.currentPoint);
        Debug.Log("path name" + playerSplineMove.pathContainer.name);
        if (playerSplineMove.currentPoint == (int)CaveWayPoints.viewpoint)
        {
            Debug.Log("path name" + playerSplineMove.pathContainer.name);
            sole1StollenBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(true);
        }

        if(playerSplineMove.currentPoint == (int)CaveWayPoints.insideCave)
        {
            sole1StollenBtn.gameObject.SetActive(true);
            caveBtn.gameObject.SetActive(false);
            GameData.liftBtnsEnabled = true;
        }

        if (playerSplineMove.currentPoint == (int)CaveWayPoints.Sole2Cave)
        {
            sole1StollenBtn.gameObject.SetActive(true);
            caveBtn.gameObject.SetActive(false);
            GameData.liftBtnsEnabled = true;
        }
    }

    public void MoveOut(int sole)
    {
        Debug.Log("Soooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo move out " + playerSplineMove.currentPoint);
        GameData.liftBtnsEnabled = false;

        ps1CaveToTunnel.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);


        playerSplineMove.reverse = false;
        playerSplineMove.StartMove();
    }

    public void MoveIn(int sole)
    {
        playerSplineMove.reverse = true;
        Debug.Log("Soooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo move in " + playerSplineMove.currentPoint);
        playerSplineMove.StartMove();
    }

    public void MoveToWaypoint(splineMove spMove, int id)
    {
        if (spMove.IsMoving()) return;

        targetWP = (CaveWayPoints)id;
        Debug.Log("SET target Waypoint to :  " + targetWP + "id is: " + id);

        if (currentWP == 0)
        {
            currentWP = CaveWayPoints.insideCave;
        }

        //spMove.pathContainer = ps1CaveToTunnel;
        spMove.StartMove();
    }


    public void ReachedWP()
    {
        currentWP = targetWP;
        Debug.Log("current WP " + currentWP);
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

        //Based on sole stopped update the pathinformation for the waypoints. 
        if(GameData.currentStopSohle == (int)CurrentStop.Sohle1 && !helperSetPath)
        {
            playerSplineMove.pathContainer = ps1CaveToTunnel;
            helperSetPath = true;
        }

        if (GameData.currentStopSohle == (int)CurrentStop.Sohle2 && !helperSetPath)
        {
            playerSplineMove.pathContainer = ps2CaveToViewpoint;
            helperSetPath = true;
        }
    }
}
