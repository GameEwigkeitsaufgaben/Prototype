using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SWS;

public enum ReinigungStation
{
    Belueftung,
    Neutralisation,
    Absetzbecken,
    Blackbox,
    Vorfluter
}

public class ManagerReinigungAktiv : MonoBehaviour
{
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDatatCh2;

    public Button btnToNeutralisation, btnToAbsetzbecken, btnToBlackbox, btnToVorfluter;
    public Button btnRToBeluft, btnRToNeutral, btnRAbsetz, btnRBlackbox;
    public Button btnToPassiv;
    
    public splineMove mySplineMove;
    public GameObject group;
    public Camera cam;
    public GameObject shoes;
    public PathManager pbelueftungToNeutral, pNeutralToAbsetz, pAbsetzToBlackbox, pBlackboxToVorfluter;

    bool moveInScene = false;
    public ReinigungStation currentStation, targetStation;
    private float offsetGroupCam;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        runtimeDatatCh2 = runtimeDataChapters.LoadChap2RuntimeData();
    }

    void Start()
    {
        //offsetGroupCam = cam.transform.position.x - group.transform.position.x;
        offsetGroupCam = 0f;
        btnToNeutralisation.interactable = true;
        btnToBlackbox.interactable = false;
        btnToAbsetzbecken.interactable = false;
        btnToPassiv.interactable = false;

    }

    public void MoveReverseToReinigungStation(int id)
    {
        moveInScene = true;
        if (mySplineMove.IsMoving()) return;
        
        switch (id)
        {
            case (int)ReinigungStation.Belueftung:
                mySplineMove.pathContainer = pbelueftungToNeutral;
                targetStation = ReinigungStation.Belueftung;
                break;
            case (int)ReinigungStation.Neutralisation:
                mySplineMove.pathContainer = pNeutralToAbsetz;
                targetStation = ReinigungStation.Neutralisation;
                btnToNeutralisation.interactable = false;
                break;
            case (int)ReinigungStation.Absetzbecken:
                mySplineMove.pathContainer = pAbsetzToBlackbox;
                targetStation = ReinigungStation.Absetzbecken;
                break;
            case (int)ReinigungStation.Blackbox:
                mySplineMove.pathContainer = pBlackboxToVorfluter;
                targetStation = ReinigungStation.Blackbox;
                break;
            case (int)ReinigungStation.Vorfluter:
                break;
        }

        btnToNeutralisation.interactable = false;
        btnToAbsetzbecken.interactable = false;
        btnToBlackbox.interactable = false;
        btnToVorfluter.interactable = false;

        btnRBlackbox.interactable = false;
        btnRAbsetz.interactable = false;
        btnRToNeutral.interactable = false;
        btnRToBeluft.interactable = false;


        mySplineMove.reverse = true;
        mySplineMove.StartMove();
        //characterGuide.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
    }

    public void MoveToReinigungStation(int id)
    {
        moveInScene = true;
        if (mySplineMove.IsMoving()) return;

        switch (id)
        {
            case (int)ReinigungStation.Belueftung:
                break;
            case (int)ReinigungStation.Neutralisation:
                mySplineMove.pathContainer = pbelueftungToNeutral;
                targetStation = ReinigungStation.Neutralisation;
                break;
            case (int)ReinigungStation.Absetzbecken:
                mySplineMove.pathContainer = pNeutralToAbsetz;
                targetStation = ReinigungStation.Absetzbecken;
                break;
            case (int)ReinigungStation.Blackbox:
                mySplineMove.pathContainer = pAbsetzToBlackbox;
                targetStation = ReinigungStation.Blackbox;
                break;
            case (int)ReinigungStation.Vorfluter:
                mySplineMove.pathContainer = pBlackboxToVorfluter;
                targetStation = ReinigungStation.Vorfluter;
                break;
        }

        btnToNeutralisation.interactable = false;
        btnToAbsetzbecken.interactable = false;
        btnToBlackbox.interactable = false;
        btnToVorfluter.interactable = false;

        btnRBlackbox.interactable = false;
        btnRAbsetz.interactable = false;
        btnRToNeutral.interactable = false;
        btnRToBeluft.interactable = false;

        mySplineMove.reverse = false;
        mySplineMove.StartMove();
        //characterGuide.transform.rotation = Quaternion.Euler(0f, -180f, 0f);
    }

    public void ReachedWP()//Called from UnityEvent Gruppe in Inspector
    {
        currentStation = targetStation;
        moveInScene = false;
        shoes.transform.position = cam.transform.position;

        switch (currentStation)
        {
            case ReinigungStation.Belueftung:
                btnToNeutralisation.interactable = true;
                break;
            case ReinigungStation.Neutralisation:
                btnToAbsetzbecken.interactable = true;
                btnRToBeluft.interactable = true;
                break;
            case ReinigungStation.Absetzbecken:
                btnToBlackbox.interactable = true;
                btnRToNeutral.interactable = true;
                break;
            case ReinigungStation.Blackbox:
                btnToVorfluter.interactable = true;
                btnRAbsetz.interactable = true;
                break;
            case ReinigungStation.Vorfluter:
                btnRBlackbox.interactable = true;
                runtimeDatatCh2.reinAktivDone = true;
                btnToPassiv.interactable = true;
                break;
        }

        //characterGuide.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }

    public void SwitchToPassiv()
    {
        if(runtimeDatatCh2.reinAktivDone && runtimeDatatCh2.reinPassivDone)
        {
            GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay(GameData.NameOverlay2110);
            return;
        }
        
        GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02gwReinigung);
    }

    // Update is called once per frame
    void Update()
    {
        if(moveInScene)
        cam.transform.position = new Vector3(group.transform.position.x + offsetGroupCam, cam.transform.position.y, cam.transform.position.z);
    }
}
