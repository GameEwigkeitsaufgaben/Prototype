using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CanvasGraphs
{   Intro,
    PH,
    Wasserstand,
    Leitfaehigkeit,
    PCB,
    Wassertemperatur
}

public class ManagerMonitoring : MonoBehaviour
{
    public Button btnReplayAudio;
    public Canvas canvasIntro, canvasPh, canvasWasserstand, canvasLeitfaehigkeit, canvasPcb, canvasWassertemperatur;

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataChap3;
    private SpeechManagerChapThree speechManager;

   

    Dictionary<string, Canvas> canvasGraphs = new Dictionary<string, Canvas>();

    [SerializeReference] private CanvasGraphs selectedMonitor;

    public Material[] mats;

    private void Awake()
    {
        canvasGraphs.Add(CanvasGraphs.Intro.ToString(), canvasIntro);
        canvasGraphs.Add(CanvasGraphs.PH.ToString(), canvasPh);
        canvasGraphs.Add(CanvasGraphs.Wasserstand.ToString(), canvasWasserstand);
        canvasGraphs.Add(CanvasGraphs.Leitfaehigkeit.ToString(), canvasLeitfaehigkeit);
        canvasGraphs.Add(CanvasGraphs.PCB.ToString(), canvasPcb);
        canvasGraphs.Add(CanvasGraphs.Wassertemperatur.ToString(), canvasWassertemperatur);

        DisableAllBut(CanvasGraphs.Intro);

        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataChap3 = runtimeDataChapters.LoadChap3RuntimeData();
    }

    private void Start()
    {
        speechManager = GetComponent<SpeechManagerChapThree>();


        //Audio Talkinglist
        btnReplayAudio.gameObject.SetActive(runtimeDataChap3.replayTL3111);

        if (runtimeDataChap3.replayTL3111) return;

        PlayMonitoringTL();
    }

    public void DisableAllBut(CanvasGraphs graph)
    {
        foreach (KeyValuePair<string, Canvas> c in canvasGraphs)
        {
            c.Value.gameObject.SetActive(false);
        }

        foreach (var item in mats)
        {
            item.SetFloat("_ClipThreshold", 1f);
        }

        canvasGraphs[graph.ToString()].gameObject.SetActive(true);
    }

    public void PlayMonitoringTL()
    {
        speechManager.playMonitoring = true;
    }

    private void Update()
    {
        if (!runtimeDataChap3.replayTL3111 && speechManager.IsTalkingListFinished(GameData.NameCH3TLMonitoring))
        {
            runtimeDataChap3.replayTL3111 = true;
            btnReplayAudio.gameObject.SetActive(true);
        }   
    }
}
