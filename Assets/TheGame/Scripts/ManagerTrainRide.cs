using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public Image eyesEnya, eyesDad, eyesGeorg;
   // AudioClip train, talkingDad;

    private void Awake()
    {
       // Invoke("StartTalking", 5f);
    }

    private void StartTalking()
    {
        speechManger.playTrainRide = true;
    }

    private void CloseEyes(Image eyes)
    {
        eyes.GetComponent<Image>().fillAmount = 0;
    }

    private void OpenEyes(Image eyes)
    {
        eyes.GetComponent<Image>().fillAmount = 0.8f;
    }
    
    private void Blinking(Image eyes)
    {
        int blinkFrequenzy = Random.Range(0, 5);
        CloseEyes(eyes);
        //StartCoroutine(Blink(e));
    }

    IEnumerator Blink(Image eyes, int seconds)
    {
        CloseEyes(eyes);
        yield return new WaitForSeconds(1);
        OpenEyes(eyes);
    }




    public void SwitchTheScene()
    {
        Debug.Log("kohlehobel in switch the scene: " + GameData.rideIn);
        if (GameData.rideIn)
        {
            //switchScene.SwitchSceneWithTransition(ScenesChapterOne.LongwallCutter);
        }
        else
        {
            //switchScene.SwitchSceneWithTransition(ScenesChapterOne.MineSoleThreeStatic);  
        }

        GameData.rideIn = !GameData.rideIn;
    }
}
