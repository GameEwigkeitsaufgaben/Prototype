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
    private SoSfx sfx;

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
    private Vector3 tmpVec3;
    private SwitchSceneManager switchSceneManager;
    [SerializeField] private AudioSource audioSrcTreppeWasser, audioSrcVorfluter, audioSrcPumpe, audioSrcAtmo, audioSrcFluss, audioSrcNeutal;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorTexture3DCave);

        runtimeDatatCh2 = runtimeDataChapters.LoadChap2RuntimeData();
        switchSceneManager = GetComponent<SwitchSceneManager>();

        sfx = runtimeDataChapters.LoadSfx();
    }

    void Start()
    {
        offsetGroupCam = 0f;
        btnToNeutralisation.interactable = true;
        btnToBlackbox.interactable = false;
        btnToAbsetzbecken.interactable = false;
        btnToPassiv.interactable = false;

        audioSrcPumpe.clip = sfx.pumpen;
        audioSrcPumpe.loop = true;
        audioSrcPumpe.Play();

        audioSrcAtmo.clip = sfx.atmoNiceWeather;
        audioSrcAtmo.loop = true;
        audioSrcAtmo.Play();

        audioSrcVorfluter.clip = sfx.gwaktivVorfluter;
        audioSrcVorfluter.loop = true;
        audioSrcVorfluter.Play();

        audioSrcTreppeWasser.clip = sfx.gwaktivTreppe;
        audioSrcTreppeWasser.loop = true;
        audioSrcTreppeWasser.Play();

        audioSrcFluss.clip = sfx.gwaktivFluss;
        audioSrcFluss.loop = true;
        audioSrcFluss.Play();

        audioSrcNeutal.clip = sfx.neutralisation;
        audioSrcNeutal.loop = true;
        audioSrcNeutal.Play();

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
    }

    public void SwitchToPassiv()
    {
        runtimeDatatCh2.reinAktivDone = true;

        if(runtimeDatatCh2.reinAktivDone && runtimeDatatCh2.reinPassivDone)
        {
            runtimeDatatCh2.progressPost2110GWReinigungDone = true;
        }
        
        switchSceneManager.SwitchScene(GameScenes.ch02gwReinigung);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveInScene)
        {
            tmpVec3.Set(group.transform.position.x + offsetGroupCam, cam.transform.position.y, cam.transform.position.z);
            cam.transform.position = tmpVec3;
        }
    }
}
