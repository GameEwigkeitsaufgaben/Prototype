using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerNews : MonoBehaviour
{
    [SerializeField] private Button btnSwitchScene;

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataCh3;
    

    private void Awake()
    {
        
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
    }

    public void SwitchTheScene()
    {
        runtimeDataCh3.newsDone = true;
        btnSwitchScene.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch03Monitoring);
    }




}
