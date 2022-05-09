using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum StatesInkohlung
{
    PlantDeath,
    PeatIsFormed,
    Repetition,
    TectonicForces,
    None
}

public class ManagerCoalification : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;
    private SoMuseumConfig configMuseum;
    public Slider slider;
    public TMP_Text infoText;
    [SerializeField] private StatesInkohlung statesInkohlung;
    public Button btnGoToMuseum; 


    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        configMuseum = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
    }

    private void Start()
    {
        statesInkohlung = StatesInkohlung.None;
    }

    public void SetStateAndText()
    {
        Debug.Log("val slider: " + slider.value);
        if (slider.value <= 0.048) statesInkohlung = StatesInkohlung.PlantDeath;
        else if (slider.value > 0.048 && slider.value <= 0.116) statesInkohlung = StatesInkohlung.PeatIsFormed;
        else if (slider.value > 0.116 && slider.value <= 0.183) statesInkohlung = StatesInkohlung.Repetition;
        else if (slider.value > 0.658 && slider.value < 1.0) statesInkohlung = StatesInkohlung.TectonicForces;
        else if (slider.value == 1) runtimeData.isCoalifiationDone = true;

        switch (statesInkohlung)
        {
            case StatesInkohlung.PlantDeath:
                infoText.text = configMuseum.pflanzenSterben;
                break;
            case StatesInkohlung.PeatIsFormed:
                infoText.text = configMuseum.TorfEntsteht;
                break;
            case StatesInkohlung.Repetition:
                infoText.text = configMuseum.Wiederholung;
                break;
            case StatesInkohlung.TectonicForces:
                infoText.text = configMuseum.tektonischeKraefte;
                break;
            case StatesInkohlung.None:
                infoText.text = GameData.EmptyString;
                break;
        }

        if (btnGoToMuseum.interactable) return;

        if (runtimeData.isCoalifiationDone) btnGoToMuseum.interactable = true;
        
    }


    public void GoBackToMuseum()
    {
        //runtimeData.isCoalifiationDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }
}
