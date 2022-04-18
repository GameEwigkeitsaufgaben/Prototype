using UnityEngine;
using TMPro;

public enum Century
{
    century13,
    century16,
    century19,
    century21
}

public class ManagerHistoryMining : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;
    public TextMeshProUGUI centuryText;
    public SoMuseumConfig museumConfig;

    public Century sliderCentury;
    

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        museumConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        sliderCentury = Century.century13;
    }

    public void GoBackToMuseum()
    {
        runtimeData.isMythDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void Update()
    {
        if(Century.century13 == sliderCentury)
        {
            centuryText.text = museumConfig.textCentury13;
        }
        else if (Century.century16 == sliderCentury)
        {
            centuryText.text = museumConfig.textCentury16;
        }
        else if (Century.century19 == sliderCentury)
        {
            centuryText.text = museumConfig.textCentury19;
        }
        else if (Century.century21 == sliderCentury)
        {
            centuryText.text = museumConfig.textCentury21;
        }
    }

}
