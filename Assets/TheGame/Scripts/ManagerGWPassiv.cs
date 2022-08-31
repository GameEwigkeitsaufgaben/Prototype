using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGWPassiv : MonoBehaviour
{
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDatatCh2;
    private SwitchSceneManager switchScene;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        runtimeDatatCh2 = runtimeDataChapters.LoadChap2RuntimeData();
        switchScene = GetComponent<SwitchSceneManager>();
    }


    public void BtnToOV()
    {
        runtimeDatatCh2.reinPassivDone = true;

        if(runtimeDatatCh2.reinPassivDone && runtimeDatatCh2.reinAktivDone)
        {
            //GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay(GameData.NameOverlay2110);
            runtimeDatatCh2.progressPost2110GWReinigungDone = true;
        }
        
        switchScene.SwitchScene(GameScenes.ch02gwReinigung);
        

    }
   
}
