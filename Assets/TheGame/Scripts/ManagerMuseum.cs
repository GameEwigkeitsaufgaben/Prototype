using UnityEngine;
using UnityEngine.UI;

public class ManagerMuseum : MonoBehaviour
{
    public bool isMinerDone;
    public bool isMythDone;
    public bool isCoalifictionDone;
    public bool isEarthHistroyDone;

    public Button btnExitMuseum;
    private Image btnExitImage;
    private SoChapOneRuntimeData runtimeData;
    public SpeechManagerMuseum speechManager;

    private bool startOutro;

    // Start is called before the first frame update
    void Start()
    {
        btnExitImage = btnExitMuseum.GetComponent<Image>();
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
    }

    // Update is called once per frame
    void Update()
    {
        if(btnExitImage.color.a < 0.8f)
        {
            if (runtimeData.isMinerDone && runtimeData.isMythDone && runtimeData.isCoalifiationDone && runtimeData.isCarbonificationPeriodDone)
            {
                btnExitImage.color += new Color32(0, 0, 0, 200);
                speechManager.playMuseumOutro = true;
            }
        }

        if (startOutro)
        {
            startOutro = false;
            //speechManager.playMuseumInfoArrival = true;
        }
        
    }
}