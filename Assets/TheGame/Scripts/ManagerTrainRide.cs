//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;

    private void Awake()
    {
        Invoke("StartTalking", 3f);
    }

    private void StartTalking()
    {
        speechManger.playTrainRide = true;
    }

    public void SwitchTheScene()
    {
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

    private void Update()
    {
        Debug.Log(speechManger.IsTrainRideTalkingFinished());
        
        if (speechManger.IsTrainRideTalkingFinished())
        {
            switchScene.LoadLongwallCutter();
        }
       
    }
}
