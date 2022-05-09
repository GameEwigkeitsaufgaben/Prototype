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
    //https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.9/manual/requirements.html urp for 2020.3.18f1
    
    public Slider slider;
    public Button btnBackToMuseum, btnUrlToSage;
    public TextMeshProUGUI centuryText;
    
    [Header("Assigned at Runtime")]
    public SoMuseumConfig museumConfig;
    private SoChapOneRuntimeData runtimeData;

    public Century sliderCentury;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        museumConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        sliderCentury = Century.none;
        btnBackToMuseum.interactable = false;
    }

    public void OpenUrlSage()
    {
        Application.OpenURL("https://www.hamsterkiste-lesen-und-schreiben.de/schweinehirt");
    }

    public void GoBackToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void Update()
    {
        //Get Century based on slider postiion
        if (slider.normalizedValue > 0.10 && slider.normalizedValue < 0.11) sliderCentury = Century.myth;
        else if (slider.normalizedValue > 0.24 && slider.normalizedValue < 0.28) sliderCentury = Century.century13;
        else if (slider.normalizedValue > 0.34 && slider.normalizedValue < 0.41) sliderCentury = Century.century16;
        else if (slider.normalizedValue > 0.54 && slider.normalizedValue < 0.55) sliderCentury = Century.century19;
        else if (slider.normalizedValue == 1) sliderCentury = Century.century21; 
        else sliderCentury = Century.none;

        if(btnUrlToSage.isActiveAndEnabled) btnUrlToSage.gameObject.SetActive(false);

        //Do someting based on century 
        switch (sliderCentury)
        {
            case Century.myth:
                centuryText.text = museumConfig.textmyth;
                btnUrlToSage.gameObject.SetActive(true);
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
                runtimeData.isMythDone = true;
                btnBackToMuseum.GetComponent<Image>().color = GameColors.defaultInteractionColorNormal;
                btnBackToMuseum.interactable = true;
                break;
            default:
                centuryText.text = GameData.EmptyString;
                break;
        }
    }
}
