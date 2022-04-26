using UnityEngine;
using UnityEngine.UI;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;
    public Image graying;

    public SpeechManagerMuseum speechManager;
    public Button closeBtn;

    private GameObject parentMaskPanel;

    bool playOverlay;
    private SoMuseumConfig configMuseum;
    private SoChapOneRuntimeData runtimeData;

    private void Start()
    {
        parentMaskPanel = container.transform.parent.gameObject;
        configMuseum = Resources.Load<SoMuseumConfig>("ConfigMuseum");
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        gameObject.transform.localPosition = runtimeData.currentGroupPos;
        //Debug.Log("in start musum overlay " + runtimeData.currentGroupPos);
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
        graying.gameObject.SetActive(true);
    }

    public void StopOverlay()
    {
        playOverlay = false;
    }

    private void Update()
    {
        //playOverlay with will be set to true in ActivateOverlay(), speechmanager starts the audio, here is proved if audio is finished.
        if (speechManager.IsMusuemInfoIntroFinished() && playOverlay)
        {
            playOverlay = false;
            //https://forum.unity.com/threads/solved-scenemanager-loadscene-make-the-scene-darker-a-bug.542440/
        }

        if (playOverlay)
        {
            if (speechManager.IsMusuemMinerEquipmentFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
                playOverlay = false;
            }
            else if (speechManager.IsMusuemHistroyCarbonFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToWorld();
                playOverlay = false;
            }
            else if (speechManager.IsMusuemHistoryMiningFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToMythos();
                playOverlay = false;
            }
            else if (speechManager.IsMusuemCoalifictionFinished())
            {
                gameObject.GetComponent<SwitchSceneManager>().GoToCoalification();
                playOverlay = false;
            }
        }

        
        if (!playOverlay && parentMaskPanel.activeSelf)
        {
            parentMaskPanel.SetActive(false);
            graying.gameObject.SetActive(false);
            closeBtn.gameObject.SetActive(false);
            Debug.Log("Play overlay" + playOverlay + " active mask panel " + parentMaskPanel.activeSelf);
            speechManager.StopSpeaking();
        }
    }
}
