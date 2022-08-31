using UnityEngine;
using UnityEngine.UI;

public class ManagerReinigungGw : MonoBehaviour
{
    public Button btnProceed;

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SpeechManagerMuseumChapTwo speechManagerCh2;
    private SwitchSceneManager switchScene;

    public Button btnReplayTalkingList, btnActive, btnPassive;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
        speechManagerCh2 = GetComponent<SpeechManagerMuseumChapTwo>();
        switchScene = GetComponent<SwitchSceneManager>();
    }

    void Start()
    {
        btnProceed.interactable = runtimeDataCh2.progressPost2110GWReinigungDone;
        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh2.replayTL21101Reinigung);
        
        if (runtimeDataCh2.replayTL21101Reinigung)
        {
            EnableBtns(true);
            return; 
        }

        speechManagerCh2.playZecheIntroReinigung = true;
        EnableBtns(false);
    }

    private void EnableBtns(bool interact)
    {
        btnActive.interactable = interact;
        btnPassive.interactable = interact;
    }

    public void GoToActiveCleaning()
    {
        switchScene.SwitchScene(GameScenes.ch02gwReinigungAktiv);
    }

    public void GoToPassivCleaning()
    {
        switchScene.SwitchScene(GameScenes.ch02gwReinigungPassiv);
    }

    public void GoTOOverlay()
    {
        switchScene.SwitchToChapter2withOverlay(GameScenes.ch02gwReinigung);
    }

    void Update()
    {
        if (speechManagerCh2.IsTalkingListFinished(GameData.NameCH2TLZecheIntroReiniung))
        {
            runtimeDataCh2.replayTL21101Reinigung = true;
            btnReplayTalkingList.gameObject.SetActive(true);
            EnableBtns(true);
        }

        if(!btnProceed.interactable && runtimeDataCh2.progressPost2110GWReinigungDone)
        {
            btnProceed.interactable = true;
        }

    }
}
