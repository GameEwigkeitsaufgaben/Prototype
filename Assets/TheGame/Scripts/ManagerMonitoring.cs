using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    public Button btnReplayAudio, btnBackToOverlay;
    public Canvas canvasIntro, canvasPh, canvasWasserstand, canvasLeitfaehigkeit, canvasPcb, canvasWassertemperatur;
    public List<Monitor> stations;
    public bool newsDone, stationsDone;
    public TMP_Text introText;


    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataChap3;
    private SpeechManagerChapThree speechManager;
    private TMP_Text activeDesc;

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

        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataChap3 = runtimeDataChapters.LoadChap3RuntimeData();
    }

    private void Start()
    {
        speechManager = GetComponent<SpeechManagerChapThree>();

        //Audio Talkinglist
        btnReplayAudio.gameObject.SetActive(runtimeDataChap3.replayTL3111);

        DisableAllBut(CanvasGraphs.Intro);
        activeDesc = introText;

        if (runtimeDataChap3.replayTL3111) return;

        PlayMonitoringTL();

        if (runtimeDataChap3.IsPostDone(ProgressChap3enum.Post311))
        {
            btnBackToOverlay.interactable = true;
            return;
        }

        btnBackToOverlay.interactable = false;
    }

    public void SetDescription(TMP_Text desc)
    {
        activeDesc.gameObject.SetActive(false);
        desc.gameObject.SetActive(true);
        activeDesc = desc;
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

        if (runtimeDataChap3.IsPostDone(ProgressChap3enum.Post311)) return;

        if(!stationsDone) stationsDone = runtimeDataChap3.monitorsDone = runtimeDataChap3.AllDone(stations);

        if(runtimeDataChap3.newsDone && runtimeDataChap3.monitorsDone)
        {
            runtimeDataChap3.SetPostDone(ProgressChap3enum.Post311);
            btnBackToOverlay.interactable = true;
        }
    }
}
