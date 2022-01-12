using SWS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MyWaypoints
{
    WP0 = 0,
    WP1 = 1,
    WP2 = 2,
    WP3 = 3
}

public class MuseumPlayer : MonoBehaviour
{
    splineMove mySplineMove;
    public PathManager p0,p1,p2;

    public MyWaypoints currentWP, targetWP;
    
    // Start is called before the first frame update
    void Start()
    {
        mySplineMove = gameObject.GetComponent<splineMove>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider trigger " +other.name);
        if (other.name.Contains("WP1"))
        {
            currentWP = MyWaypoints.WP1;
        }
        else if (other.name.Contains("WP2"))
        {
            currentWP = MyWaypoints.WP2;
        }
        else if (other.name.Contains("WP3"))
        {
            currentWP = MyWaypoints.WP3;
        }
    }

    public void ReachedWP()
    {
        currentWP = targetWP;
        Debug.Log("current WP " + currentWP);
    }

    public void moveToWP1()
    {
        targetWP = MyWaypoints.WP1;
        //currentWP = MyWaypoints.WP0;
        if (currentWP == 0)
        {
            currentWP = MyWaypoints.WP0;
        }
        
        mySplineMove.pathContainer = GetPath(currentWP,targetWP);
        mySplineMove.StartMove();
    }

    public void moveToWP2()
    {
        targetWP = MyWaypoints.WP2;
        Debug.Log("after press move: " +currentWP + " " + targetWP);

        mySplineMove.pathContainer = GetPath(currentWP, targetWP);
        mySplineMove.StartMove();
    }

    public void moveToWP3()
    {
        targetWP = MyWaypoints.WP3;
        Debug.Log("after press move: " + currentWP + " " + targetWP);

        mySplineMove.pathContainer = p2;
        mySplineMove.StartMove();
    }

    private PathManager GetPath(MyWaypoints cwp, MyWaypoints twp)
    {

        if (currentWP == targetWP) return null;
        else if(currentWP == MyWaypoints.WP0 && targetWP == MyWaypoints.WP1)
        {
            mySplineMove.reverse = false;
            return p0;
        }
        else if (currentWP == MyWaypoints.WP1 && targetWP == MyWaypoints.WP0)
        {
            mySplineMove.reverse = true;
            return p0;
        }
        else if (currentWP == MyWaypoints.WP1 && targetWP == MyWaypoints.WP2)
        {
            mySplineMove.reverse = false;
            return p1;
        }
        else if (currentWP == MyWaypoints.WP1 && targetWP == MyWaypoints.WP3)
        {
            mySplineMove.reverse = false;
            return p2;
        }
        else if (currentWP == MyWaypoints.WP2 && targetWP == MyWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p1;
        }
        else if (currentWP == MyWaypoints.WP3 && targetWP == MyWaypoints.WP1)
        {
            mySplineMove.reverse = true;
            return p2;
        }
        else {
            return null;
        }

    }
}
