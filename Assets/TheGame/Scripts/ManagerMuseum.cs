using UnityEngine;
using UnityEngine.UI;

public class ManagerMuseum : MonoBehaviour
{
    //public bool isMinerDone;
    //public bool isMythDone;
    //public bool isCoalifictionDone;
    //public bool isEarthHistroyDone;

    public Button btnExitMuseum;
    public SpeechManagerMuseum speechManager;
    public MuseumPlayer walkingGroup;
    public SwitchSceneManager switchScene;

    private bool startOutro;
    private Image btnExitImage;
    private SoChapOneRuntimeData runtimeData;
    private bool museumDoneSet;
    public GameObject characterDad, characterGuide, waitingGuide;

    // Start is called before the first frame update
    void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        walkingGroup.SetCharcters(characterDad, characterGuide, waitingGuide);

        btnExitImage = btnExitMuseum.GetComponent<Image>();
        btnExitImage.gameObject.GetComponent<Button>().interactable = false;
        museumDoneSet = false;
    }

    // Update is called once per frame
    void Update()
    {
        runtimeData.CheckInteraction117Done();
        
        if (!museumDoneSet && runtimeData.interaction117Done)
        {
            speechManager.playMuseumOutro = true;
            museumDoneSet = true;
            walkingGroup.MoveToWaypoint((int)MuseumWaypoints.WPExitMuseum0);
        }

        if (speechManager.IsMusuemInfoIntroFinished())
        {
           characterDad.gameObject.SetActive(false);
           characterGuide.gameObject.SetActive(true);
            waitingGuide.gameObject.SetActive(false);
        }
        
        if (speechManager.IsMuseumOutroFinished())
        {
            walkingGroup.MoveToWaypoint((int)MuseumWaypoints.WPExitMuseum1);
        }

        if(runtimeData.currentMuseumWaypoint == MuseumWaypoints.None)
        {
            switchScene.SwitchToChapter1withOverlay("Overlay117");
        }



        if (startOutro)
        {
            startOutro = false;
        }
        
    }
}
