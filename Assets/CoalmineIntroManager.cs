using UnityEngine;
using Shapes;
using UnityEngine.UI;

public class CoalmineIntroManager : MonoBehaviour
{
    public CoalmineSpeechManger mySpeechManger;
    public Button nextSceneBtn;

    public Disc eaDone, s1Done, s2Done, s3wetterDone, s3GebaudeDone, trainInDone, lwcDone, trainOutDone;

    public Character georg, dad, enya;

    private bool audioStarted = false;

    private SoChapOneRuntimeData runtimeDataCh01;
    private SoChaptersRuntimeData runtimeDataChapers;

    private void Awake()
    {
        runtimeDataCh01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapers = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    void Start()
    {
        runtimeDataChapers.SetSceneCursor(runtimeDataChapers.cursorDefault);

        nextSceneBtn.interactable = runtimeDataCh01.revisitEntryArea;
        nextSceneBtn.GetComponent<Button>().colors = GameColors.GetInteractionColorBlock();

        Color fbColor = Color.green;
      
        if (runtimeDataCh01.entryAreaDone) eaDone.Color = fbColor;
        if (runtimeDataCh01.sole1Done) s1Done.Color = fbColor;
        if (runtimeDataCh01.sole2Done) s2Done.Color = fbColor;
        if (runtimeDataCh01.sole3BewetterungDone) s3GebaudeDone.Color = fbColor;
        if (runtimeDataCh01.sole3GebaeudeDone) s3wetterDone.Color = fbColor;
        if (runtimeDataCh01.trainRideInDone) trainInDone.Color = fbColor;
        if (runtimeDataCh01.isLongwallCutterDone) lwcDone.Color = fbColor;
        if (runtimeDataCh01.trainRideInDone) trainOutDone.Color = fbColor;
    }

    private void Update()
    {   
        if (!nextSceneBtn.interactable)
        {
            if (!audioStarted)
            {
                mySpeechManger.playCaveIntro = true;
                dad.GetComponent<Character>().characterImage.sprite = dad.GetComponent<Character>().characterConfigSO.outsideMineStandingTalking;
                audioStarted = true;
            }
            else
            {
                if (mySpeechManger.IsTalkingFinished(GameData.NameTLMineIntro))
                {
                    nextSceneBtn.interactable = true;
                    dad.GetComponent<Character>().characterImage.sprite = dad.GetComponent<Character>().characterConfigSO.outsideMineStandingSilient;
                }
            }
        }
        else
        {
            if (!audioStarted && runtimeDataCh01.revisitEntryArea)
            {
                CoalmineStop stop = CoalmineStop.Unset;
                georg.SetupElements();
                dad.SetupElements();
                enya.SetupElements();

                if (runtimeDataCh01.interaction116Done)
                {
                    stop = CoalmineStop.Sole3;
                    mySpeechManger.playCaveIntroAllDone = true;
                }
                else
                {
                    if (runtimeDataCh01.isLongwallCutterDone) stop = CoalmineStop.Sole3;
                    else if (runtimeDataCh01.sole3BewetterungDone || runtimeDataCh01.sole3GebaeudeDone) stop = CoalmineStop.Sole3;
                    else if (runtimeDataCh01.sole2Done) stop = CoalmineStop.Sole2;
                    else if (runtimeDataCh01.sole1Done) stop = CoalmineStop.Sole1;
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
