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
    public AudioSource audioSrcAtmo, audioSrcWetter;

    private SwitchSceneManager switchSceneMgr;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private Button btnBackToEntryPoint;

    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
        switchSceneMgr = GetComponent<SwitchSceneManager>();
        sfx = runtimeDataChapters.LoadSfx();
        btnBackToEntryPoint = btnBackTo3101.GetComponent<Button>();

        audioSrcAtmo.clip = sfx.wasserhaltungAussen;
        audioSrcAtmo.loop = true;
        audioSrcAtmo.Play();

        audioSrcWetter.clip = sfx.atmoNiceWeather;
        audioSrcWetter.loop = true;
        audioSrcWetter.Play();
       
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310) ||
            runtimeDataCh3.IsPostDone(ProgressChap3enum.Post3103))
        {
            btnReplayTalkingList.gameObject.SetActive(true);
            btnBackToEntryPoint.interactable = true;
            return;
        }

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
        switchSceneMgr.SwitchScene(GameScenes.ch03EmscherWeg);
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
                if (!btnBackToEntryPoint.interactable)
                {
                    if (runtimeDataCh3.replayTL3103)
                    {
                        btnBackToEntryPoint.interactable = true;
                        runtimeDataCh3.SetPostDone(ProgressChap3enum.Post3103);

                        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post3102)) runtimeDataCh3.SetPostDone(ProgressChap3enum.Post310);
                    }
                }
            }
        }
    }
}
