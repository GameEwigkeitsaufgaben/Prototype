//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;
    public bool isNextSceneLoaded = false;
    public AudioClip trainride;

    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        gameObject.GetComponent<AudioSource>().clip = trainride;
        gameObject.GetComponent<AudioSource>().Play();

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

    private void Start()
    {
       
    }

    private void StartTalkingIn()
    {
        speechManger.playTrainRideIn = true;
    }

    private void StartTalkingOut()
    {
        speechManger.playTrainRideOut = true;
    }

    private void GoToLongwallcutter()
    {
        switchScene.GoToLongwallCutter();
    }

    private void GoToOverlay()
    {
        switchScene.SwitchToChapter1withOverlay("Overlay116");
    }

    private void Update()
    {
        
        if (speechManger.IsTrainRideInTalkingFinished())
        {
            if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideIn)
            {
                if (!isNextSceneLoaded)
                {
                    speechManger.ToggleTrainRideInTalkingFinished();
                    runtimeData.trainRideInDone = true;
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
                    runtimeData.trainRideOutDone = true;
                    Invoke("GoToOverlay", 3f);
                    isNextSceneLoaded = true;
                }
            }
        }
    }
}
