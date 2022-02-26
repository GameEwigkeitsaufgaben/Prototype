using SWS;
using UnityEngine;
using UnityEngine.UI;

public class LongwallCutterWaypointManager : MonoBehaviour
{
    public splineMove playerSplineMove;
    public splineMove georgSplineMove, enyaSplineMove, vaterSplineMove;
    public PathManager pathViewpointToKohlehobel;
    public Button btnBahnsteig, btnKohlehobel;

    private void Start()
    {
        HandleCurrentWP();
        playerSplineMove.gameObject.transform.position = pathViewpointToKohlehobel.waypoints[0].transform.position;
    }

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
        }
        else
        {
            btnBahnsteig.gameObject.SetActive(true);
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
        georgSplineMove.StartMove();
        enyaSplineMove.StartMove();
        vaterSplineMove.StartMove();
    }

    public void MoveToLWCBahnsteig()
    {
        playerSplineMove.reverse = true;
        playerSplineMove.StartMove();
    }

    public void RotateCharacters()
    {
        georgSplineMove.gameObject.GetComponent<Character>().RotateCharacter(0);
        enyaSplineMove.gameObject.GetComponent<Character>().RotateCharacter(0);
        vaterSplineMove.gameObject.GetComponent<Character>().RotateCharacter(0);
    }
}
