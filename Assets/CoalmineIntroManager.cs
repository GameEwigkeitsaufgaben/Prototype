using UnityEngine;
using Shapes;
using UnityEngine.UI;

public class CoalmineIntroManager : MonoBehaviour
{
    public CoalmineSpeechManger mySpeechManger;
    public Button nextSceneBtn, btnReplayTalkingList;

    public Image imgEAStation, imgS1Station, imgS2Station, imgS3Station, imgTrainInStation, imgTrainOutStation, imgLwcStation;

    public Disc eaDone, s1Done, s2Done, s3wetterDone, s3GebaudeDone, trainInDone, lwcDone, trainOutDone;

    public Character georg, dad, enya;

    private bool audioStarted = false;

    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChaptersRuntimeData runtimeDataChapers;

    private void Awake()
    {
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapers = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    private bool IsSole3Done()
    {
        return runtimeDataCh1.sole3BewetterungDone && runtimeDataCh1.sole3GebaeudeDone;
    }

    void Start()
    {
        runtimeDataChapers.SetSceneCursor(runtimeDataChapers.cursorDefault);

        nextSceneBtn.interactable = runtimeDataCh1.replayCoalmineIntro;
        btnReplayTalkingList.gameObject.SetActive(runtimeDataCh1.replayCoalmineIntro);

        Color fbColor = Color.green;
        
        if (runtimeDataCh1.replayEntryArea) imgEAStation.color = fbColor;
        if (runtimeDataCh1.sole1Done) imgS1Station.color = fbColor;
        if (runtimeDataCh1.sole2Done) imgS2Station.color = fbColor;
        if (IsSole3Done()) imgS3Station.color = fbColor;
        if (runtimeDataCh1.trainRideInDone) imgTrainInStation.color =  fbColor;
        if (runtimeDataCh1.trainRideOutDone) imgTrainOutStation.color = fbColor;
        if (runtimeDataCh1.isLongwallCutterDone) imgLwcStation.color = fbColor;
    }

    public void ReplayTalkingList()
    {
        mySpeechManger.playCaveIntro = true;
    }

    private void Update()
    {   
        if (!nextSceneBtn.interactable)
        {
            if (!audioStarted)
            {
                mySpeechManger.playCaveIntro = true;
                //dad.GetComponent<Character>().characterImage.sprite = dad.GetComponent<Character>().characterConfigSO.outsideMineStandingTalking;
                audioStarted = true;
            }
            else
            {
                if (mySpeechManger.IsTalkingFinished(GameData.NameTLMineIntro))
                {
                    nextSceneBtn.interactable = true;
                    runtimeDataCh1.replayCoalmineIntro = true;
                    btnReplayTalkingList.gameObject.SetActive(true);
                    //dad.GetComponent<Character>().characterImage.sprite = dad.GetComponent<Character>().characterConfigSO.outsideMineStandingSilient;
                }
            }
        }
        else
        {
            //change sprite based on last visited sole 
            if (!audioStarted && runtimeDataCh1.revisitEntryArea)
            {
                CoalmineStop stop = CoalmineStop.Unset;
                if (georg != null) georg.SetupElements();
                if (dad != null) dad.SetupElements();
                if (enya != null) enya.SetupElements();

                if (runtimeDataCh1.interaction116Done)
                {
                    stop = CoalmineStop.Sole3;
                    mySpeechManger.playCaveIntroAllDone = true;
                }
                else
                {
                    if (runtimeDataCh1.isLongwallCutterDone) stop = CoalmineStop.Sole3;
                    else if (runtimeDataCh1.sole3BewetterungDone || runtimeDataCh1.sole3GebaeudeDone) stop = CoalmineStop.Sole3;
                    else if (runtimeDataCh1.sole2Done) stop = CoalmineStop.Sole2;
                    else if (runtimeDataCh1.sole1Done) stop = CoalmineStop.Sole1;
                    else stop = CoalmineStop.EntryArea;
                    mySpeechManger.playCaveIntroNotAllDone = true;
                }

                georg.ChangeCharacterImage(stop);
                dad.ChangeCharacterImage(stop);
                enya.ChangeCharacterImage(stop);

                audioStarted = true;
            }

            //Reset silent charater after finished talking!
        }
    }
}
