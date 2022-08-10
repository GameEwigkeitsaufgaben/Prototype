using UnityEngine;

public class Monitor : MonoBehaviour
{
    [SerializeReference] private CanvasGraphs monitorType;

    private ManagerMonitoring manager;
    public bool viewed = false;

    private void Awake()
    {
        manager = FindObjectOfType<ManagerMonitoring>();
    }

    public void TurnOn()
    {
        manager.DisableAllBut(monitorType);
        viewed = true;
    }
}
