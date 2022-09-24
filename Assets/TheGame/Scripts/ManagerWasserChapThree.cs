using UnityEngine;
using UnityEngine.UI;

public class ManagerWasserChapThree : MonoBehaviour
{
    public Button btnSchautafel3102, btnSchautafel3103;
    public Button btnReplayTalkingList;
    public Button btnProceed;
    public AudioSource audioSrcAtmo;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SpeechManagerChapThree speechManager;

    private SoSfx sfx;

    public bool audioFinished = false;


    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();
        speechManager = GetComponent<SpeechManagerChapThree>();

    }
    void Start()
    {
        audioSrcAtmo.clip = sfx.wasserhaltungAussen;
        audioSrcAtmo.loop = true;
        audioSrcAtmo.Play();

        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310))
        {
            btnProceed.interactable = btnSchautafel3102.interactable = btnSchautafel3103.interactable = true;
            btnReplayTalkingList.gameObject.SetActive(true);
            return;
        }

        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh3.replayTL3101);

        btnProceed.interactable = false;
        btnSchautafel3102.interactable = runtimeDataCh3.replayTL3101;
        btnSchautafel3103.interactable = runtimeDataCh3.replayTL3101;

        speechManager.playGrubenwasser = !runtimeDataCh3.replayTL3101;
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
