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
    public Canvas canvasIntro, canvasPh, canvasWasserstand, canvasLeitfaehigkeit, canvasPcb, canvasWassertemperatur;

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
}
