//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;
    public bool isNextSceneLoaded = false;
    private SoChapOneRuntimeData runtimeData;
    public AudioClip trainride;

    private void Awake()
    {
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
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeData.cursorDefault = null;
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
