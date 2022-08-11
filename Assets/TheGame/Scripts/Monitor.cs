using UnityEngine;
using TMPro;

public class Monitor : MonoBehaviour
{
    public CanvasGraphs monitorType;

    private ManagerMonitoring manager;
    public bool viewed = false;
    public TMP_Text description;

    private void Awake()
    {
        manager = FindObjectOfType<ManagerMonitoring>();
    }

    public void TurnOn()
    {
        manager.DisableAllBut(monitorType);
        manager.SetDescription(description);
        viewed = true;
    }
}
