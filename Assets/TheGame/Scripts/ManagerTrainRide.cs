//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;
    public bool isNextSceneLoaded = false;

    private void Awake()
    {
        isNextSceneLoaded = false;

        if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideIn)
        {
            Invoke("StartTalkingIn", 3f);
            
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideOut)
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

    private void GoToMine()
    {
        switchScene.LoadMine();
    }

    private void Update()
    {
       // Debug.Log(speechManger.IsTrainRideInTalkingFinished());
        
        if (speechManger.IsTrainRideInTalkingFinished())
        {
            if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideIn)
            {
               
                if (!isNextSceneLoaded)
                {
                    speechManger.ToggleTrainRideInTalkingFinished();
                    Invoke("GoToLongwallcutter", 3f);
                    isNextSceneLoaded = true;
                }
                
                
            }
               
        }
       else if (speechManger.IsTrainRideOutTalkingFinished())
        {
            if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideOut)
            {
                
                if (!isNextSceneLoaded)
                {
                    speechManger.ToggleTrainRideOutTalkingFinished();
                    Invoke("GoToMine", 3f);
                    isNextSceneLoaded = true;
                }
                
               
            }
        }
    }
}
