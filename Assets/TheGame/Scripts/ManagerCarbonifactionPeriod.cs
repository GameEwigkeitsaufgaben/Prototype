using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCarbonifactionPeriod : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;

    public void OpenTheUrl()
    {

        Application.OpenURL("https://dinosaurpictures.org/ancient-earth/#240");
    }

    // Start is called before the first frame update
    void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        runtimeData.sceneDefaultcursor = null;
    }

    public void GoBackToMuseum()
    {
        runtimeData.isCarbonificationPeriodDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }
}
