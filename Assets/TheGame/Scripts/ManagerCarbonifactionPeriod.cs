using UnityEngine;
using UnityEngine.UI;

public class ManagerCarbonifactionPeriod : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;

    public Button externalLink;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        externalLink.colors = GameColors.GetInteractionColorBlock();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }
    private void Start()
    {
    }
    
    //Called from Btn in Scene/Inspector
    public void OpenTheUrl()
    {
        Application.OpenURL("https://dinosaurpictures.org/ancient-earth/#240");
    }

    public void GoBackToMuseum()
    {
        runtimeData.isCarbonificationPeriodDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }
}
