using UnityEngine;

public class ManagerCoalification : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
    }

    public void GoBackToMuseum()
    {
        runtimeData.isCoalifiationDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }
}
