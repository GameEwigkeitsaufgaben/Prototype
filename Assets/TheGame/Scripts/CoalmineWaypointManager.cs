using SWS;
using UnityEngine;
using UnityEngine.UI;

public enum MineWayPoints
{
    insideCave = 0,
    viewpoint = 1,
}

public class CoalmineWaypointManager : MonoBehaviour
{
    public splineMove playerSplineMove;
    public PathManager pathS1CaveToViewpoint, pathS2CaveToViewpoint, pathS3CaveToViewpoint;
    public GameObject wpS1ViewpointSign, wpS2ViewpointSign, wpS3ViewpointSign;
    public float wpAdjustHightViewpoint;

    public GameObject triggerPlayerInCave;
    
    [SerializeField] 
    private Button runtimeViewpointBtn, caveBtn;
    
    private Button wps1ViewpointBtn, wps2ViewpointBtn, wps3ViewpointBtn;
    private bool helperSetPath = false;
    private Player myPlayer;
   
    private void Start()
    {
        myPlayer = playerSplineMove.gameObject.GetComponent<Player>();
        
        if (myPlayer.playerInCave)
        {
            caveBtn.gameObject.SetActive(false);
        }

        AlignSignsWithViewpointWaypoints();

        wps1ViewpointBtn = wpS1ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps2ViewpointBtn = wpS2ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
        wps3ViewpointBtn = wpS3ViewpointSign.GetComponentsInChildren<Transform>()[1].GetComponent<Button>();
    }

    public void AlignSignsWithViewpointWaypoints()
    {
        wpS1ViewpointSign.transform.position = pathS1CaveToViewpoint.GetWaypoint(1).transform.position + new Vector3(0f, wpAdjustHightViewpoint, 0f);
        wpS2ViewpointSign.transform.position = pathS2CaveToViewpoint.GetWaypoint(1).transform.position + new Vector3(0f, wpAdjustHightViewpoint, 0f);
        wpS3ViewpointSign.transform.position = pathS3CaveToViewpoint.GetWaypoint(1).transform.position + new Vector3(0f, wpAdjustHightViewpoint, 0f);
    }

    public void DeactivateButton()
    {
        if (playerSplineMove.currentPoint == (int)MineWayPoints.viewpoint)
        {
            runtimeViewpointBtn.gameObject.SetActive(false);
            caveBtn.gameObject.SetActive(true);
        }
        else if(playerSplineMove.currentPoint == (int)MineWayPoints.insideCave)
        {
            runtimeViewpointBtn.gameObject.SetActive(true);
            caveBtn.gameObject.SetActive(false);
            GameData.liftBtnsEnabled = true;
        }
    }

    public void MoveOut(int sole)
    {
        GameData.liftBtnsEnabled = false;
        
        //Adjust player position in y 
        if(GameData.currentStopSohle == (int)CoalmineStop.Sole1)
        {
            pathS1CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole2)
        {
            pathS2CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole3)
        {
            pathS3CaveToViewpoint.transform.position = new Vector3(0f, myPlayer.transform.position.y, 0f);
        }

        playerSplineMove.reverse = false;
        playerSplineMove.StartMove();
    }
    
    public void MoveIn(int sole)
    {
        playerSplineMove.reverse = true;
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
            runtimeViewpointBtn = wps3ViewpointBtn;
            helperSetPath = true;
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole2 && !helperSetPath)
        {
            playerSplineMove.pathContainer = pathS2CaveToViewpoint;
            runtimeViewpointBtn = wps2ViewpointBtn;
            helperSetPath = true;
        }
        else if (GameData.currentStopSohle == (int)CoalmineStop.Sole1 && !helperSetPath)
        {
            playerSplineMove.pathContainer = pathS1CaveToViewpoint;
            runtimeViewpointBtn = wps1ViewpointBtn;
            helperSetPath = true;
        }
    }
}
