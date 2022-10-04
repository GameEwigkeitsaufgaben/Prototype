using UnityEngine;
using UnityEngine.UI;

public class AudioProgress : MonoBehaviour
{
    public float overallTime, timerTime;
    public float fillFraction;
    public bool timerStarted;
    public Image timeImage;

    public void StartTimer(float overallTlTimeInSec)
    {
        timeImage.gameObject.SetActive(true);
        overallTime = overallTlTimeInSec;
        timerTime = overallTlTimeInSec;
        timerStarted = true;
    }

    void Update()
    {
        if (!timerStarted) return;
        
        timerTime -= Time.deltaTime;

        if (timerTime > 0)
        {
            fillFraction = timerTime / overallTime;
            timeImage.fillAmount = fillFraction;
        }
        else
        {
            timerStarted = false;
        }
    }
}
