using SWS;
using System;
using UnityEngine;
using UnityEngine.UI;

public class LongwallCutterWaypointManager : MonoBehaviour
{
    public splineMove playerSplineMove;
    public splineMove characterSplineMove;
    public PathManager pathViewpointToKohlehobel;
    public Button btnBahnsteig, btnKohlehobel;

    public MineWayPoints GetCurrentLongWallCutterWP()
    {
        if (playerSplineMove.currentPoint == (int)PathWaypoints.startPath)
        {
            return MineWayPoints.viewpointLWBahnsteig;
        }

        return MineWayPoints.viewpointLWLWCutter;
    }

    public void HandleCurrentWP()
    {
        btnBahnsteig.gameObject.SetActive(false);
        btnKohlehobel.gameObject.SetActive(false);

        if (StandingOnBahnsteig())
        {
            btnKohlehobel.gameObject.SetActive(true);
            //btnKohlehobel.transform.parent.transform.localRotation = Quaternion.Euler(0, 60, 0);
        }
        else
        {
            btnBahnsteig.gameObject.SetActive(true);
            //btnBahnsteig.transform.parent.transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        Debug.Log("Handle Current wp: " + GetCurrentLongWallCutterWP());
    }

    public bool StandingOnBahnsteig()
    {
        return GetCurrentLongWallCutterWP() == MineWayPoints.viewpointLWBahnsteig;
    }

    public void MoveToLongwallCutter()
    {
        playerSplineMove.StartMove();
        characterSplineMove.StartMove();
    }

    public void MoveToLWCBahnsteig()
    {
        playerSplineMove.reverse = true;
        playerSplineMove.StartMove();
    }

    void Start()
    {
        HandleCurrentWP();
        playerSplineMove.gameObject.transform.position =  pathViewpointToKohlehobel.waypoints[0].transform.position;
    }
}
