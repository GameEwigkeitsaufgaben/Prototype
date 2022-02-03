using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
   // AudioClip train, talkingDad;

    public AudioSource envSoundTrain;
    public AudioSource dad;
    public SwitchSceneManager switchScene;
    public bool kh = true;

    private void Awake()
    {
        envSoundTrain.Play();

        if (GameData.rideIn)
        {
            dad.Play();
        }

        Invoke("SwitchTheScene", 4f);
    }

    public void SwitchTheScene()
    {
        Debug.Log("kohlehobel in switch the scene: " + GameData.rideIn);
        if (GameData.rideIn)
        {
            switchScene.SwitchSceneWithTransition(ScenesChapterOne.LongwallCutter);
        }
        else
        {
            switchScene.SwitchSceneWithTransition(ScenesChapterOne.MineSoleThreeStatic);  
        }

        GameData.rideIn = !GameData.rideIn;
    }
}
