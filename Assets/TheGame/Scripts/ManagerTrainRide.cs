//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;

    private void Awake()
    {
        if (GameData.rideIn)
        {
            Invoke("StartTalkingIn", 3f);
            
        }
        else
        {
            Invoke("StartTalkingOut", 3f);
        }
    }



    private void StartTalkingIn()
    {
        speechManger.playTrainRideIn = true;
    }

    private void StartTalkingOut()
    {
        speechManger.playTrainRideOut = true;
    }

    //public void SwitchTheScene()
    //{
    //    if (GameData.rideIn)
    //    {
    //        //switchScene.SwitchSceneWithTransition(ScenesChapterOne.LongwallCutter);
    //    }
    //    else
    //    {
    //        //switchScene.SwitchSceneWithTransition(ScenesChapterOne.MineSoleThreeStatic);  
    //    }

    //    GameData.rideIn = !GameData.rideIn;
    //}

    private void GoToLongwallcutter()
    {
        switchScene.GoToLongwallCutter();
    }

    private void Update()
    {
        Debug.Log(speechManger.IsTrainRideTalkingFinished());
        
        if (speechManger.IsTrainRideTalkingFinished())
        {
            Invoke("GoToLongwallcutter", 3f);
            speechManger.ToggleTrainRideTalkingFinished();
        }
       
    }
}
