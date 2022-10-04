using SWS;
using UnityEngine;
using UnityEngine.UI;

public class LongwallCutterWaypointManager : MonoBehaviour
{
    public splineMove playerSplineMove;
    public splineMove georgSplineMove, enyaSplineMove, vaterSplineMove;
    public PathManager pathViewpointToKohlehobel;
    public Button btnBahnsteig, btnKohlehobel;

    private SoChapOneRuntimeData runtimeData;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
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
            RotateCharacters(-114.0f, -53.0f, -80.0f);
        }
        else
        {
            btnBahnsteig.gameObject.SetActive(true);
            RotateCharacters(-180.0f, 0.0f, 0.0f);

            runtimeData.isLongwallCutterDone = true;
        }
    }

    public bool StandingOnBahnsteig()
    {
        return GetCurrentLongWallCutterWP() == MineWayPoints.viewpointLWBahnsteig;
    }

    public void MoveToLongwallCutter()
    {
        playerSplineMove.reverse = false;
        playerSplineMove.StartMove();
        georgSplineMove.reverse = false;
        georgSplineMove.StartMove();
        enyaSplineMove.reverse = false;
        enyaSplineMove.StartMove();
        vaterSplineMove.reverse = false;
        vaterSplineMove.StartMove();
    }

    public void MoveToLWCBahnsteig()
    {
        playerSplineMove.reverse = true;
        playerSplineMove.StartMove();
        georgSplineMove.reverse = true;
        georgSplineMove.StartMove();
        enyaSplineMove.reverse = true;
        enyaSplineMove.StartMove();
        vaterSplineMove.reverse = true;
        vaterSplineMove.StartMove();
    }

    public void RotateCharacters(float yDad, float yEnya, float yGeorg)
    {
        vaterSplineMove.gameObject.GetComponent<Character>().RotateCharacter(yDad);
        enyaSplineMove.gameObject.GetComponent<Character>().RotateCharacter(yEnya);
        georgSplineMove.gameObject.GetComponent<Character>().RotateCharacter(yGeorg);
    }
}
