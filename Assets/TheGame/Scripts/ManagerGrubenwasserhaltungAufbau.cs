using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGrubenwasserhaltungAufbau : MonoBehaviour
{
    public List<DragTurmItem> dragItems;
    public bool allItemsSnaped = false;
    public bool audioFinised = false;
    public GameObject btnBackTo3101;
    public Button btnReplayTalkingList;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;

    void Start()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
       
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh3.replayTL3103);

        gameObject.GetComponent<SpeechManagerChapThree>().playPumpAufbau = !runtimeDataCh3.replayTL3103;

        audioFinised = runtimeDataCh3.replayTL3103;
        allItemsSnaped = runtimeDataCh3.IsPostDone(ProgressChap3enum.Post3103);
    }

    public void ReplayTalkingList()
    {
        gameObject.GetComponent<SpeechManagerChapThree>().playPumpAufbau = true;
    }

    public void SwitchTheScene()
    {
        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310))
        {
            GetComponent<SwitchSceneManager>().SwitchToChapter3withOverlay(GameData.NameOverlay310);
        }
        else
        {
            GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch03EmscherWeg);
        }
    }

    void Update()
    {
        if (!btnBackTo3101.GetComponent<Button>().interactable)
        {
            if (!audioFinised)
            {
                audioFinised = gameObject.GetComponent<SpeechManagerChapThree>().IsTalkingListFinished(GameData.NameCH3TLPumpenAufbau);
            }

            if (audioFinised && !runtimeDataCh3.replayTL3103)
            {
                runtimeDataCh3.replayTL3103 = true;
                btnReplayTalkingList.gameObject.SetActive(true);
            }

            if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310)) return;

            if (!allItemsSnaped)
            {
                int index = dragItems.FindIndex(item => item.snaped == false);
                if (index == -1)
                {
                    allItemsSnaped = true;
                }
            }

            if (allItemsSnaped)
            {
                btnBackTo3101.GetComponent<Button>().interactable = true;
                runtimeDataCh3.SetPostDone(ProgressChap3enum.Post3103);

                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post3102)) runtimeDataCh3.SetPostDone(ProgressChap3enum.Post310);
            }
        }
    }
}
