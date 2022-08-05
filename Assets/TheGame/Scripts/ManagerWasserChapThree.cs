using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerWasserChapThree : MonoBehaviour
{
    public Button btnSchautafel3102, btnSchautafel3103;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SpeechManagerChapThree speechManager;

    public bool audioFinished = false;

    private void Awake()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        speechManager = GetComponent<SpeechManagerChapThree>();
    }
    // Start is called before the first frame update
    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        btnSchautafel3102.interactable = false;
        btnSchautafel3103.interactable = false;

        speechManager.playGrubenwasser = true;

        if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post3101))
        {
            btnSchautafel3102.interactable = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioFinished)
        {
            audioFinished = speechManager.IsTalkingListFinished(GameData.NameCH3TLGrubenwasser);
        }

        if(audioFinished && !btnSchautafel3102.interactable)
        {
            btnSchautafel3102.interactable = true;
            btnSchautafel3103.interactable = true;
        }
    }
}
