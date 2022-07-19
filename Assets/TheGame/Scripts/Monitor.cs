using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monitor : MonoBehaviour
{
    [SerializeReference] private CanvasGraphs monitorType;

    private ManagerMonitoring manager;

    private void Awake()
    {
        manager = FindObjectOfType<ManagerMonitoring>();
    }

    public void TurnOn()
    {
        manager.DisableAllBut(monitorType);
    }
}
