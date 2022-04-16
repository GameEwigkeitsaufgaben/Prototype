using UnityEngine;

public class ManagerHistoryMining : MonoBehaviour
{
    private SoChapOneRuntimeData runtimeData;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
    }

    public void GoBackToMuseum()
    {
        runtimeData.isMythDone = true;
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

}
