using UnityEngine;

public enum Century
{
    century13,
    century16,
    century19
}

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
