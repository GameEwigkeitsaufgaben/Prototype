using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayInPlace : MonoBehaviour
{
    bool started = false;
    public MuseumOverlay overlay;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ActivateOverlay(bool active)
    {
        gameObject.SetActive(active);

        if (started) return;
        
        started = true;
        gameObject.SetActive(false);
    }

    public void ActivateInkolung()
    {
        overlay.ActivateOverlay(MuseumWaypoints.WPInkohlung);
    }

    public void ActivateSchwein()
    {
        overlay.ActivateOverlay(MuseumWaypoints.WPMythos);
    }

    public void ActivateMiner()
    {
        overlay.ActivateOverlay(MuseumWaypoints.WPBergmann);
    }
    public void ActivateCarbonPeriod()
    {
        overlay.ActivateOverlay(MuseumWaypoints.WPWelt);
    }
}
