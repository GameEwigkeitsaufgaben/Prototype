using UnityEngine;
using UnityEngine.UI;

public class MuseumOverlay : MonoBehaviour
{
    //public Canvas panel;
    public Image container;

    public SpeechManagerMuseum speechManager;

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
            Debug.Log(configMuseum.info.name);
            container.sprite = configMuseum.info;
            speechManager.playMuseumInfoArrival = true;
        }
        else if (wp == MuseumWaypoints.WPBergmann)
        {
            Debug.Log(configMuseum.miner.name);
            container.sprite = configMuseum.miner;
            speechManager.playMinerEquipment = true;
        }

    }

    private void Update()
    {
        //playOverlay will be set to true in ActivateOverlay()
        if (speechManager.IsMusuemInfoIntroFinished() && playOverlay)
        {
            playOverlay = false;
        }

        if (speechManager.IsMusuemMinerEquipmentFinished() && playOverlay)
        {
            playOverlay = false;
            gameObject.GetComponent<SwitchSceneManager>().GoToMinerEquipment();
        }

        if (!playOverlay)
        {
            parentMaskPanel.SetActive(false);
        }
    }
}
