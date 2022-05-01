using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Century
{
    myth,
    century13,
    century16,
    century19,
    century21,
    none
}

public class ManagerHistoryMining : MonoBehaviour
{
    //https://forum.unity.com/threads/many-text-mesh-pro-elements-in-a-scene-what-is-a-possible-solution.665614/ TMP Performance
    public Slider slider;
    public Button backToMuseum;

    private SoChapOneRuntimeData runtimeData;
    public TextMeshProUGUI centuryText;
    public SoMuseumConfig museumConfig;
    
    public Century sliderCentury;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        museumConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        sliderCentury = Century.none;
        backToMuseum.interactable = false;
    }

    public void GoBackToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void Update()
    {

        if (slider.normalizedValue > 0.10 && slider.normalizedValue < 0.11) sliderCentury = Century.myth;
        else if (slider.normalizedValue > 0.24 && slider.normalizedValue < 0.28) sliderCentury = Century.century13;
        else if (slider.normalizedValue > 0.34 && slider.normalizedValue < 0.41) sliderCentury = Century.century16;
        else if (slider.normalizedValue > 0.54 && slider.normalizedValue < 0.55) sliderCentury = Century.century19;
        else if (slider.normalizedValue == 1)
        {
            sliderCentury = Century.century21;
            runtimeData.isMythDone = true;
            backToMuseum.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            backToMuseum.interactable = true;
        }
        else
        {
            sliderCentury = Century.none;
        }

        switch (sliderCentury)
        {
            case Century.myth:
                centuryText.text = museumConfig.textmyth;
                break;
            case Century.century13:
                centuryText.text = museumConfig.textCentury13;
                break;
            case Century.century16:
                centuryText.text = museumConfig.textCentury16;
                break;
            case Century.century19:
                centuryText.text = museumConfig.textCentury19;
                break;
            case Century.century21:
                centuryText.text = museumConfig.textCentury21;
                break;
            default:
                centuryText.text = "";
                break;
        }

        //if (Century.myth == sliderCentury) centuryText.text = museumConfig.textmyth;
        //else if (Century.century13 == sliderCentury)
        //{
        //    centuryText.text = museumConfig.textCentury13;
        //}
        //else if (Century.century16 == sliderCentury)
        //{
        //    centuryText.text = museumConfig.textCentury16;
        //}
        //else if (Century.century19 == sliderCentury)
        //{
        //    centuryText.text = museumConfig.textCentury19;
        //}
        //else if (Century.century21 == sliderCentury)
        //{
        //    centuryText.text = museumConfig.textCentury21;
        //}
        //else if (Century.none == sliderCentury)
        //{
        //    centuryText.text = "";
        //}
    }

}
