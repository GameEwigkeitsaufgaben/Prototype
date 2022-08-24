using SWS;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum MuseumWaypoints
{
    WP0 = 0,
    WPInfo = 1,
    WPInkohlung = 2,
    WPBergmann = 3,
    WPMythos = 4,
    WPWelt = 5,
    WPExitMuseum0 = 6,
    WPExitMuseum1 = 7,
    WPTV = 8,
    WPFliesspfad = 9,
    WPExitZeche = 10,
    WPBeluft = 11,
    WPAbsetzBecken = 12,
    WPKalkmilch = 13,
    WPSulfat = 14,
    WPOsmose = 15,
    WPCleanWater = 16,
    None
}

public class MuseumPlayer : MonoBehaviour
{
    splineMove mySplineMove;
    public PathManager p0P1, p1P2, p1P3, p1P4, p1P5, p2P3, p2P4, p2P5, p3P4, p3P5, p4P5, p1P6, p2P6, p3P6, p4P6, p5P6, p6P7;

    public MuseumWaypoints currentWP, targetWP;

    public Button btnWPInfo, btnWPInkohlung, btnWPBergmann, btnWPSchwein, btnWPWelt;
    public MuseumOverlay overlay;
    private SoChapOneRuntimeData runtimeData;
    private SoMuseumConfig configMuseum;
    public SwitchSceneManager switchScene;

    private GameObject characterDad, characterGuide, waitingGuide;
    //public bool switchToGuide;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        configMuseum = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
    }

    void Start()
    {
        mySplineMove = gameObject.GetComponent<splineMove>();
        mySplineMove.ChangeSpeed(50.0f);

        if (runtimeData.currentMuseumWaypoint == MuseumWaypoints.WP0) 
            ShowOnlyInfo();
        else if (runtimeData.currentMuseumWaypoint != MuseumWaypoints.WP0)
        {
            gameObject.transform.localPosition = runtimeData.currentGroupPos;
            currentWP = targetWP = runtimeData.currentMuseumWaypoint;
            ShowOtherStations(currentWP);
        }
    }

    public void SetCharcters(GameObject characterDad, GameObject characterGuide, GameObject waitingGuide)
    {
        this.characterDad = characterDad;
        this.characterGuide = characterGuide;
        this.waitingGuide = waitingGuide;
}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GO "+ gameObject.name + mySplineMove.IsMoving());
        //if (mySplineMove.IsMoving()) return;

        Debug.Log("collider trigger " +other.name);
        if (other.name.Contains("WP1"))
        {
            currentWP = MuseumWaypoints.WPInfo;
        }
        else if (other.name.Contains("WP2"))
        {
            currentWP = MuseumWaypoints.WPInkohlung;
        }
        else if (other.name.Contains("WP3"))
        {
            currentWP = MuseumWaypoints.WPBergmann;
        }
        else if (other.name.Contains("WP4"))
        {
            currentWP = MuseumWaypoints.WPMythos;
        }
        else if (other.name.Contains("WP5"))
        {
            currentWP = MuseumWaypoints.WPWelt;
        }
    }

    public void ShowNoStation()
    {
        btnWPInfo.gameObject.SetActive(false);
        btnWPInkohlung.gameObject.SetActive(false);
        btnWPBergmann.gameObject.SetActive(false);
        btnWPSchwein.gameObject.SetActive(false);
        btnWPWelt.gameObject.SetActive(false);
    }

    public void ShowOnlyInfo()
    {
        btnWPInfo.gameObject.SetActive(true);
        btnWPInkohlung.gameObject.GetComponent<Button>().interactable = false;
        btnWPBergmann.gameObject.GetComponent<Button>().interactable = false;
        btnWPSchwein.gameObject.GetComponent<Button>().interactable = false;
        btnWPWelt.gameObject.GetComponent<Button>().interactable = false;
    }

    public void ShowStations()
    {
        btnWPInfo.gameObject.SetActive(false);
        btnWPInkohlung.gameObject.SetActive(true);
        btnWPBergmann.gameObject.SetActive(true);
        btnWPSchwein.gameObject.SetActive(true);
        btnWPWelt.gameObject.SetActive(true);
    }

    public void ShowOtherStations(MuseumWaypoints currntWp)
    {
        btnWPInkohlung.gameObject.GetComponent<Button>().interactable = true;
        btnWPBergmann.gameObject.GetComponent<Button>().interactable = true;
        btnWPSchwein.gameObject.GetComponent<Button>().interactable = true;
        btnWPWelt.gameObject.GetComponent<Button>().interactable = true;

        btnWPInfo.gameObject.SetActive(true);
        btnWPInkohlung.gameObject.SetActive(true);
        btnWPBergmann.gameObject.SetActive(true);
        btnWPSchwein.gameObject.SetActive(true);
        btnWPWelt.gameObject.SetActive(true);

        switch (currentWP)
        {
            case MuseumWaypoints.WPBergmann: 
                btnWPBergmann.gameObject.SetActive(false);
                break;
            case MuseumWaypoints.WPInfo:
                btnWPInfo.gameObject.SetActive(false);
                break;
            case MuseumWaypoints.WPWelt:
                btnWPWelt.gameObject.SetActive(false);
                break;
            case MuseumWaypoints.WPMythos:
                btnWPSchwein.gameObject.SetActive(false);
                break;
            case MuseumWaypoints.WPInkohlung:
                btnWPInkohlung.gameObject.SetActive(false);
                break;
        }
    }

    public void ReachedWP()//Called from UnityEvent Gruppe in Inspector
    {
        currentWP = targetWP;
        characterGuide.GetComponent<Image>().sprite = configMuseum.guideStanding;
        characterGuide.transform.rotation = Quaternion.Euler(Vector3.zero);

        Debug.Log("wp reached -------------------  " + currentWP);

        if (currentWP == MuseumWaypoints.WPInfo)
        {
            Debug.Log("wp reached ------------------- INFOOOOOOOOOOOOOOOO");
            btnWPInfo.gameObject.SetActive(false);
            overlay.ActivateOverlay(MuseumWaypoints.WPInfo);
            
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung)
        {
            btnWPInkohlung.gameObject.SetActive(false);
            overlay.ActivateOverlay(MuseumWaypoints.WPInkohlung);
        }
        else if (currentWP == MuseumWaypoints.WPBergmann)
        {
            btnWPBergmann.gameObject.SetActive(false);
            overlay.ActivateOverlay(MuseumWaypoints.WPBergmann);
        }
        else if (currentWP == MuseumWaypoints.WPMythos)
        {
            btnWPSchwein.gameObject.SetActive(false);
            overlay.ActivateOverlay(MuseumWaypoints.WPMythos);
        }
        else if (currentWP == MuseumWaypoints.WPWelt)
        {
            btnWPWelt.gameObject.SetActive(false);
            overlay.ActivateOverlay(MuseumWaypoints.WPWelt);
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.ChangeSpeed(1.0f);
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum1)
        {
            currentWP = MuseumWaypoints.None;
        }

        runtimeData.currentGroupPos = gameObject.transform.localPosition;
        runtimeData.currentMuseumWaypoint = currentWP;
        
        Debug.Log("current WP " + currentWP);
        
        if (currentWP == MuseumWaypoints.None) return;
        if (currentWP == MuseumWaypoints.WPExitMuseum0) return;
        if (currentWP == MuseumWaypoints.WPExitMuseum1) return;

        ShowOtherStations(currentWP);
        
    }

    public void MoveToWaypoint (int id)
    {
        Debug.Log("-----------Move to waypoint: " +id);
        if (mySplineMove.IsMoving()) return;

        targetWP = (MuseumWaypoints)id;
        Debug.Log("SET target Waypoint to :  " + targetWP + "id is: " + id);
        

        if (currentWP == 0)
        {
            currentWP = MuseumWaypoints.WP0;
        }
        
        mySplineMove.pathContainer = GetPath(currentWP, targetWP);
        mySplineMove.StartMove();
        characterGuide.GetComponent<Image>().sprite = configMuseum.guideWalking;
        characterGuide.transform.rotation = Quaternion.Euler(0f,-180f,0f);
    }

    private PathManager GetPath(MuseumWaypoints cwp, MuseumWaypoints twp)
    {
        if (currentWP == targetWP) return null;
        else if(currentWP == MuseumWaypoints.WP0 && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = false;
            return p0P1;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WP0)
        {
            mySplineMove.reverse = true;
            return p0P1;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WPInkohlung)
        {
            mySplineMove.reverse = false;
            return p1P2;
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = true;
            return p1P2;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WPBergmann)
        {
            mySplineMove.reverse = false;
            return p1P3;
        }
        else if (currentWP == MuseumWaypoints.WPBergmann && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = true;
            return p1P3;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WPMythos)
        {
            mySplineMove.reverse = false;
            return p1P4;
        }
        else if (currentWP == MuseumWaypoints.WPMythos && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = true;
            return p1P4;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WPWelt)
        {
            mySplineMove.reverse = false;
            return p1P5;
        }
        else if (currentWP == MuseumWaypoints.WPWelt && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = true;
            return p1P5;
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung && targetWP == MuseumWaypoints.WPBergmann)
        {
            mySplineMove.reverse = false;
            return p2P3;
        }
        else if (currentWP == MuseumWaypoints.WPBergmann && targetWP == MuseumWaypoints.WPInkohlung)
        {
            mySplineMove.reverse = true;
            return p2P3;
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung && targetWP == MuseumWaypoints.WPMythos)
        {
            mySplineMove.reverse = false;
            return p2P4;
        }
        else if (currentWP == MuseumWaypoints.WPMythos && targetWP == MuseumWaypoints.WPInkohlung)
        {
            mySplineMove.reverse = true;
            return p2P4;
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung && targetWP == MuseumWaypoints.WPWelt)
        {
            mySplineMove.reverse = false;
            return p2P5;
        }
        else if (currentWP == MuseumWaypoints.WPWelt && targetWP == MuseumWaypoints.WPInkohlung)
        {
            mySplineMove.reverse = true;
            return p2P5;
        }
        else if (currentWP == MuseumWaypoints.WPBergmann && targetWP == MuseumWaypoints.WPMythos)
        {
            mySplineMove.reverse = false;
            return p3P4;
        }
        else if (currentWP == MuseumWaypoints.WPMythos && targetWP == MuseumWaypoints.WPBergmann)
        {
            mySplineMove.reverse = true;
            return p3P4;
        }
        else if (currentWP == MuseumWaypoints.WPBergmann && targetWP == MuseumWaypoints.WPWelt)
        {
            mySplineMove.reverse = false;
            return p3P5;
        }
        else if (currentWP == MuseumWaypoints.WPWelt && targetWP == MuseumWaypoints.WPBergmann)
        {
            mySplineMove.reverse = true;
            return p3P5;
        }
        else if (currentWP == MuseumWaypoints.WPMythos && targetWP == MuseumWaypoints.WPWelt)
        {
            mySplineMove.reverse = false;
            return p4P5;
        }
        else if (currentWP == MuseumWaypoints.WPWelt && targetWP == MuseumWaypoints.WPMythos)
        {
            mySplineMove.reverse = true;
            return p4P5;
        }
        else if (currentWP == MuseumWaypoints.WPInfo && targetWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.reverse = false;
            return p1P6;
        }
        else if (currentWP == MuseumWaypoints.WPInkohlung && targetWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.reverse = false;
            return p2P6;
        }
        else if (currentWP == MuseumWaypoints.WPBergmann && targetWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.reverse = false;
            return p3P6;
        }
        else if (currentWP == MuseumWaypoints.WPMythos && targetWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.reverse = false;
            return p4P6;
        }
        else if (currentWP == MuseumWaypoints.WPWelt && targetWP == MuseumWaypoints.WPExitMuseum0)
        {
            mySplineMove.reverse = false;
            return p5P6;
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPInfo)
        {
            mySplineMove.reverse = true;
            return p1P6;
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPInkohlung)
        {
            mySplineMove.reverse = true;
            return p2P6;
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPBergmann)
        {
            mySplineMove.reverse = true;
            return p3P6;
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPMythos)
        {
            mySplineMove.reverse = true;
            return p4P6;
        }
        else if (currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPWelt)
        {
            mySplineMove.reverse = true;
            return p5P6;
        }
        //else if(currentWP == MuseumWaypoints.WPExitMuseum0 && targetWP == MuseumWaypoints.WPExitMuseum1)
        //{
        //    mySplineMove.reverse = false;
        //    return p6P7;
        //}
        else {
            return null;
        }

    }
}
