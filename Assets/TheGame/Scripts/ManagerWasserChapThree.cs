using UnityEngine;
using UnityEngine.UI;

public class ManagerWasserChapThree : MonoBehaviour
{
    public Button btnSchautafel3102, btnSchautafel3103;
    public Button btnReplayTalkingList;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SpeechManagerChapThree speechManager;

    public bool audioFinished = false;

    private void Awake()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        speechManager = GetComponent<SpeechManagerChapThree>();
    }
    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        audioFinished = runtimeDataCh3.replayTL3101;
        btnReplayTalkingList.gameObject.SetActive(audioFinished);

        btnSchautafel3102.interactable = audioFinished;
        btnSchautafel3103.interactable = audioFinished;
        

        speechManager.playGrubenwasser = !audioFinished;
    }

    public void ReplayTalkingList()
    {
        speechManager.playGrubenwasser = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310)) return;

        if (!audioFinished)
        {
            audioFinished = speechManager.IsTalkingListFinished(GameData.NameCH3TLGrubenwasser);
            runtimeDataCh3.replayTL3101 = audioFinished;
        }
        else
        {
            if(!btnSchautafel3102.interactable || !btnSchautafel3103.interactable)
            {
                runtimeDataCh3.replayTL3101 = audioFinished;
                btnReplayTalkingList.gameObject.SetActive(true);
                btnSchautafel3102.interactable = btnSchautafel3103.interactable = true;
            }
        }
    }
}
