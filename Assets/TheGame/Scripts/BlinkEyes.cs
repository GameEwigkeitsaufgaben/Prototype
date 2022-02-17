using UnityEngine;
using UnityEngine.UI;

public class BlinkEyes : MonoBehaviour
{
    public Image eyes;
    public bool eyesOpened = true;
    public bool blink = true;
    public float blinkdurationInSec = 2f;
    public float waitTimeToNextBlink = 2f;
    float timeRemaining;

    private void Start()
    {
        timeRemaining = waitTimeToNextBlink;
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            blink = true;
        }
        
        if (!blink) return;

        if (eyesOpened)
        {
            eyes.fillAmount -= 1.0f / blinkdurationInSec * Time.deltaTime;
            if (eyes.fillAmount <= 0) eyesOpened = !eyesOpened;
        }
        else
        {
            eyes.fillAmount += 1.0f / blinkdurationInSec * Time.deltaTime;
            if (eyes.fillAmount >= 0.8)
            {
                eyesOpened = !eyesOpened;
                blink = false;
                timeRemaining = Random.Range(0f, 5f);
            }
        }
    }
}
