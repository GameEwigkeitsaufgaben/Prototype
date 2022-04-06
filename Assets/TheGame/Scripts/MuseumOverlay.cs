using UnityEngine;
using UnityEngine.UI;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;

    public SpeechManagerMuseum speechManager;
    public Button closeBtn;

    private GameObject parentMaskPanel;

    bool playOverlay;
    private SoMuseumConfig configMuseum;

    private void Start()
    {
        parentMaskPanel = container.transform.parent.gameObject;
        configMuseum = Resources.Load<SoMuseumConfig>("ConfigMuseum");
    }

    public void ActivateOverlay(MuseumWaypoints wp)
    {
        parentMaskPanel.SetActive(true);
        playOverlay = true;

        if(wp == MuseumWaypoints.WPInfo)
        {
            container.sprite = configMuseum.info;
            speechManager.playMuseumInfoArrival = true;
        }
        else if (wp == MuseumWaypoints.WPBergmann)
        {
            container.sprite = configMuseum.miner;
            speechManager.playMinerEquipment = true;
        }
        else if (wp == MuseumWaypoints.WPWelt)
        {
            container.sprite = configMuseum.world;
            speechManager.playMuseumWorld = true;
        }
        else if (wp == MuseumWaypoints.WPMythos)
        {
            container.sprite = configMuseum.myth;
            speechManager.playMuseumCoalHistory = true;
        }
        else if (wp == MuseumWaypoints.WPInkohlung)
        {
            container.sprite = configMuseum.carbonification;
            speechManager.playMuseumCarbonification = true;
        }
        closeBtn.gameObject.SetActive(true);
    }

    public void StopOverlay()
    {
        playOverlay = false;
    }

    private void Update()
    {
        //playOverlay will be set to true in ActivateOverlay()
        if (speechManager.IsMusuemInfoIntroFinished() && playOverlay)
        {
            playOverlay = false;
            //https://forum.unity.com/threads/solved-scenemanager-loadscene-make-the-scene-darker-a-bug.542440/
        }

        if (speechManager.IsMusuemMinerEquipmentFinished() && playOverlay)
        {
            playOverlay = false;
            gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
        }

        if (speechManager.IsMusuemWorldFinished() && playOverlay)
        {
            playOverlay = false;
            gameObject.GetComponent<SwitchSceneManager>().GoToWorld();
        }
        if (speechManager.IsMusuemCoalHistoryFinished() && playOverlay)
        {
            playOverlay = false;
            //gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
        }
        if (speechManager.IsMusuemCarbonificationFinished() && playOverlay)
        {
            playOverlay = false;
            //gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
        }

        if (!playOverlay)
        {
            parentMaskPanel.SetActive(false);
            closeBtn.gameObject.SetActive(false);
            speechManager.StopSpeaking();
        }
    }
}
