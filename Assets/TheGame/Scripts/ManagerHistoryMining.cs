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

    [Header("Range Slider")]
    public Vector2 sageRange;
    public Vector2 thirteenthRange;
    public Vector2 sixteenthRange;
    public Vector2 nineteenthRange;
    public Vector2 twentyfirstRange;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        museumConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        sliderCentury = Century.none;
        btnBackToMuseum.interactable = false;

        runtimeData.cursorDefault = null;
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
        if (slider.normalizedValue > sageRange.x && slider.normalizedValue < sageRange.y) sliderCentury = Century.myth;
        else if (slider.normalizedValue > thirteenthRange.x && slider.normalizedValue < thirteenthRange.y) sliderCentury = Century.century13;
        else if (slider.normalizedValue > sixteenthRange.x && slider.normalizedValue < sixteenthRange.y) sliderCentury = Century.century16;
        else if (slider.normalizedValue > nineteenthRange.x && slider.normalizedValue < nineteenthRange.y) sliderCentury = Century.century19;
        else if (slider.normalizedValue > twentyfirstRange.x) sliderCentury = Century.century21; 
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
