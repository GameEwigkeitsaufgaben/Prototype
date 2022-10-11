using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerPolder : MonoBehaviour
{
    public Button btnBackToOverlay;
    public Button btnReplayTalkingList;
    public List <DragItemRiver> dragItems;
    public AudioSource audioSrcAtmo;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private SpeechManagerChapThree speechMgrCh03;

    bool audioFinished;
    bool allRiversSnaped;

    private void Awake()
    {
        speechMgrCh03 = gameObject.GetComponent<SpeechManagerChapThree>();
    }

    // Start is called before the first frame update
    void Start()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);

        sfx = runtimeDataChapters.LoadSfx();

        audioSrcAtmo.clip = sfx.atmoNiceWeather;
        audioSrcAtmo.loop = true;
        audioSrcAtmo.Play();

        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post314))
        {
            btnBackToOverlay.interactable = true;
            btnReplayTalkingList.gameObject.SetActive(true);
            audioFinished = true;
            allRiversSnaped = true;
            return;
        }

        if (runtimeDataCh3.replayTL3141)
        {
            audioFinished = true;
        }

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        btnBackToOverlay.interactable = false;
        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh3.replayTL3141);
        
        if (!audioFinished) ReplayTalkingList();
    }
    public void ReplayTalkingList()
    {
        speechMgrCh03.playPolder = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (btnBackToOverlay.interactable) return;

        if (!runtimeDataCh3.replayTL3141 && speechMgrCh03.IsTalkingListFinished(GameData.NameCH3TLPolder))
        {
            btnReplayTalkingList.gameObject.SetActive(true);
            runtimeDataCh3.replayTL3141 = true;
        }

        if (runtimeDataCh3.replayTL3141 && runtimeDataCh3.DropTargetsAllItemsSnaped(dragItems))
        {
            btnBackToOverlay.interactable = true;
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post314);
        }
    }
}
