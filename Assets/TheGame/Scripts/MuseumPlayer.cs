using SWS;
using UnityEngine;

public enum MuseumWaypoints
{
    WP0 = 0,
    WP1 = 1,
    WP2 = 2,
    WP3 = 3,
    WP4 = 4,
    WP5 = 5,
}

public class MuseumPlayer : MonoBehaviour
{
    splineMove mySplineMove;
    public PathManager p0P1, p1P2, p1P3, p1P4, p1P5, p2P3, p2P4, p2P5, p3P4, p3P5, p4P5;

    public MuseumWaypoints currentWP, targetWP;
    
    // Start is called before the first frame update
    void Start()
    {
        mySplineMove = gameObject.GetComponent<splineMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GO "+ gameObject.name + mySplineMove.IsMoving());
        //if (mySplineMove.IsMoving()) return;

        Debug.Log("collider trigger " +other.name);
        if (other.name.Contains("WP1"))
        {
            currentWP = MuseumWaypoints.WP1;
        }
        else if (other.name.Contains("WP2"))
        {
            currentWP = MuseumWaypoints.WP2;
        }
        else if (other.name.Contains("WP3"))
        {
            currentWP = MuseumWaypoints.WP3;
        }
        else if (other.name.Contains("WP4"))
        {
            currentWP = MuseumWaypoints.WP4;
        }
        else if (other.name.Contains("WP5"))
        {
            currentWP = MuseumWaypoints.WP5;
        }
    }

    public void ReachedWP()
    {
        currentWP = targetWP;
        Debug.Log("current WP " + currentWP);
    }

    public void MoveToWaypoint (int id)
    {
        if (mySplineMove.IsMoving()) return;

        targetWP = (MuseumWaypoints)id;
        Debug.Log("SET target Waypoint to :  " + targetWP + "id is: " + id);
        
        if (currentWP == 0)
        {
            currentWP = MuseumWaypoints.WP0;
        }

        mySplineMove.pathContainer = GetPath(currentWP, targetWP);
        mySplineMove.StartMove();
    }

    private PathManager GetPath(MuseumWaypoints cwp, MuseumWaypoints twp)
    {

        if (currentWP == targetWP) return null;
        else if(currentWP == MuseumWaypoints.WP0 && targetWP == MuseumWaypoints.WP1)
        {
            mySplineMove.reverse = false;
            return p0P1;
        }
        else if (currentWP == MuseumWaypoints.WP1 && targetWP == MuseumWaypoints.WP0)
        {
            mySplineMove.reverse = true;
            return p0P1;
        }
        else if (currentWP == MuseumWaypoints.WP1 && targetWP == MuseumWaypoints.WP2)
        {
            mySplineMove.reverse = false;
            return p1P2;
        }
        else if (currentWP == MuseumWaypoints.WP2 && targetWP == MuseumWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p1P2;
        }
        else if (currentWP == MuseumWaypoints.WP1 && targetWP == MuseumWaypoints.WP3)
        {
            mySplineMove.reverse = false;
            return p1P3;
        }
        else if (currentWP == MuseumWaypoints.WP3 && targetWP == MuseumWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p1P3;
        }
        else if (currentWP == MuseumWaypoints.WP1 && targetWP == MuseumWaypoints.WP4)
        {
            mySplineMove.reverse = false;
            return p1P4;
        }
        else if (currentWP == MuseumWaypoints.WP4 && targetWP == MuseumWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p1P4;
        }
        else if (currentWP == MuseumWaypoints.WP1 && targetWP == MuseumWaypoints.WP5)
        {
            mySplineMove.reverse = false;
            return p1P5;
        }
        else if (currentWP == MuseumWaypoints.WP5 && targetWP == MuseumWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p1P5;
        }
        else if (currentWP == MuseumWaypoints.WP2 && targetWP == MuseumWaypoints.WP3)
        {
            mySplineMove.reverse = false;
            return p2P3;
        }
        else if (currentWP == MuseumWaypoints.WP3 && targetWP == MuseumWaypoints.WP2)
        {
            mySplineMove.reverse = true;
            return p2P3;
        }
        else if (currentWP == MuseumWaypoints.WP2 && targetWP == MuseumWaypoints.WP4)
        {
            mySplineMove.reverse = false;
            return p2P4;
        }
        else if (currentWP == MuseumWaypoints.WP4 && targetWP == MuseumWaypoints.WP2)
        {
            mySplineMove.reverse = true;
            return p2P4;
        }
        else if (currentWP == MuseumWaypoints.WP2 && targetWP == MuseumWaypoints.WP5)
        {
            mySplineMove.reverse = false;
            return p2P5;
        }
        else if (currentWP == MuseumWaypoints.WP5 && targetWP == MuseumWaypoints.WP2)
        {
            mySplineMove.reverse = true;
            return p2P5;
        }
        else if (currentWP == MuseumWaypoints.WP3 && targetWP == MuseumWaypoints.WP4)
        {
            mySplineMove.reverse = false;
            return p3P4;
        }
        else if (currentWP == MuseumWaypoints.WP4 && targetWP == MuseumWaypoints.WP3)
        {
            mySplineMove.reverse = true;
            return p3P4;
        }
        else if (currentWP == MuseumWaypoints.WP3 && targetWP == MuseumWaypoints.WP5)
        {
            mySplineMove.reverse = false;
            return p3P5;
        }
        else if (currentWP == MuseumWaypoints.WP5 && targetWP == MuseumWaypoints.WP3)
        {
            mySplineMove.reverse = true;
            return p3P5;
        }
        else if (currentWP == MuseumWaypoints.WP4 && targetWP == MuseumWaypoints.WP5)
        {
            mySplineMove.reverse = false;
            return p4P5;
        }
        else if (currentWP == MuseumWaypoints.WP5 && targetWP == MuseumWaypoints.WP4)
        {
            mySplineMove.reverse = true;
            return p4P5;
        }
        else {
            return null;
        }

    }
}
