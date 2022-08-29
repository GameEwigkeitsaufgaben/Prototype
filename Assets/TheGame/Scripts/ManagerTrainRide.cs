//Blinking Eyes in extra Script added to Georg, Enya, Dad, ...

using UnityEngine;

public class ManagerTrainRide : MonoBehaviour
{
    public CoalmineSpeechManger speechManger;
    public SwitchSceneManager switchScene;
    public bool isNextSceneLoaded = false;
    //public AudioClip trainride;

    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);

        sfx = runtimeDataChapters.LoadSfx();
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        
        gameObject.GetComponent<AudioSource>().clip = sfx.coalmineMovingTrain;
        gameObject.GetComponent<AudioSource>().Play();

        isNextSceneLoaded = false;

        StopAllCoroutines();

    }

    private void Start()
    {

        if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideIn)
        {
            Invoke("StartTalkingIn", 2f);
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineSoleThreeTrainRideOut)
        {
            Invoke("StartTalkingOut", 2f);
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

    private void GoToLongwallcutter()
    {
        switchScene.GoToLongwallCutter();
    }

    private void GoToOverlay()
    {
        switchScene.SwitchToChapter1withOverlay(GameData.NameOverlay116);
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
                    runtimeDataCh1.trainRideInDone = true;
                    Invoke("GoToLongwallcutter", 2f);
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
                    runtimeDataCh1.trainRideOutDone = true;
                    Invoke("GoToOverlay", 2f);
                    isNextSceneLoaded = true;
                }
            }
        }
    }
}
