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

    bool audioFinished;
    bool allRiversSnaped;



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
        gameObject.GetComponent<SpeechManagerChapThree>().playPolder = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (btnBackToOverlay.interactable) return;

        if (!audioFinished)
        {
            audioFinished = gameObject.GetComponent<SpeechManagerChapThree>().IsTalkingListFinished(GameData.NameCH3TLPolder);
            runtimeDataCh3.replayTL3141 = true;
        }

        if (audioFinished && runtimeDataCh3.DropTargetsAllItemsSnaped(dragItems))
        {
            btnBackToOverlay.interactable = true;
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post314);
        }
    }
}
