using UnityEngine;
using UnityEngine.UI;

public class ManagerCarbonifactionPeriod : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;
    public Button externalLink;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        externalLink.colors = GameColors.GetInteractionColorBlock();
    }
    private void Start()
    {
        Cursor.SetCursor(runtimeData.cursorDefault, Vector2.zero, CursorMode.Auto);
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
