//This script manages the coalmine interactive scene, loads necessary static scenes and scenes with animation. 
//It uses the following scripts: 
//The cave.cs (in combination with BottomCollider) manages the movement of the cave up/down, doors open/close. 
//The player.cs Speech manager resides on parent GameObject Characters and manages all dialogue. 
//The SwitchSceneManager which is responsible for loading scenens in the game. 
//It has a link to Cave Collider Button to reset the character sprites accordingly to the coalmine stop.

using UnityEngine;
using UnityEngine.UI;

public class CaveManager : MonoBehaviour
{
    int frameCounter = 0;
    float totalFrameTime, fps;

    public Cave cave;
    public Player player;
    public Character characterEnya, characterDad, characterGeorg;

    private const float defaultYawInCave = 0f;
    private SwitchSceneManager switchScene;
    
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;
    private CoalmineSpeechManger speechManagerMine;
    private CoalmineWaypointManager waypointManagerMine;
    
    public Button exitSceneBtn, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWPBtn, sole3EnterTrainBtn, btnReplayTalkingList;

    //private bool introPlayedOneTime = false;

    public AudioSource[] sfxS1;

    private SoSfx sfx;

    public AudioSource baukipper, kran, water, bewetterung;


    private void ShowFPS()
    {
        frameCounter++;
        //float frameTime = Time.deltaTime;
        totalFrameTime += Time.deltaTime;

        if (totalFrameTime >= 1f)
        {
            int fps = Mathf.RoundToInt(frameCounter/totalFrameTime);
            frameCounter = 0;
            totalFrameTime -= 1f;
        }
    }

    private void Awake()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        waypointManagerMine = GetComponent<CoalmineWaypointManager>();

        btnReplayTalkingList.gameObject.SetActive(runtimeData.replayEntryArea);

    }

    private void Start()
    {
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorTexture3DCave);
        
        //load speechmanager at runtime
        speechManagerMine = characterDad.transform.parent.GetComponent<CoalmineSpeechManger>();

        //load all needed scenes additive
        switchScene = gameObject.GetComponent<SwitchSceneManager>();

        switchScene.LoadEntryArea();
        switchScene.LoadCaveTunnel();
        switchScene.LoadSole1();
        switchScene.LoadSole2();
        switchScene.LoadSole3();
        switchScene.LoadMineAnimation();


        //SetupAudio;
        sfx.SetClipByAddToDict(bewetterung, sfx.coalmineWindInTunnel);
        sfx.SetClipByAddToDict(baukipper, sfx.caolmineLader);
        sfx.SetClipByAddToDict(kran, sfx.coalmineWorkingMachinesMetal);
        sfx.SetClipByAddToDict(water, sfx.caolmineSplashingWater);

        sole3EnterTrainBtn.gameObject.SetActive(false);

        if (runtimeData.sole1Done || runtimeData.replayS1Cave)
        {
            sole1WPViewpointBtn.interactable = true;
        }
        if (runtimeData.sole2Done || runtimeData.replayS2Cave)
        {
            sole2WPViewpointBtn.interactable = true;
        }
        if ((runtimeData.sole3BewetterungDone && runtimeData.sole3GebaeudeDone) || runtimeData.replayS3Cave)
        {
            waypointManagerMine.wps3ViewpointBtn.interactable = true;
        }
    }

    private void OnEnable()
    {
        //At first load
        if (!runtimeData.revisitEntryArea)
        {
            player.SetPlayerRotation(defaultYawInCave, false); //Default 90 degree for cave orientation
            cave.SetAllButtonsInteractable(false);
            cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            runtimeData.currentCoalmineStop = CoalmineStop.EntryArea;
            cave.currentStop = cave.targetStop = runtimeData.currentCoalmineStop;
            player.SetPlayerRotation(defaultYawInCave, false); //Default 90 degree for cave orientation
            cave.SetAllButtonsInteractable(true);
        }
    }

    public void GoToTrainRideIn()
    {
        GameData.sohleToReload = (int)CoalmineStop.Sole3;
        speechManagerMine.StopRunningTL();
        cave.StoreCavePosition();
        player.StorePlayerAtBahnsteigPositon();
        switchScene.GoToTrainRideIn();
    }

    private void OnDestroy()
    {
        runtimeData.liftBtnsAllEnabled = false;
        StopAllCoroutines();
    }

    public void ReplayTalkingList()
    {
        switch (runtimeData.currentCoalmineStop)
        {
            case CoalmineStop.EntryArea:
                speechManagerMine.playEntryArea = true;
                break;
            case CoalmineStop.Sole1:
                switch (waypointManagerMine.currentWP)
                {
                    case MineWayPoints.insideCave:
                        speechManagerMine.playSole1 = true;
                        break;
                    case MineWayPoints.viewpoint:
                        speechManagerMine.playSole1Vp = true;
                        break;
                }
               
                break;
            case CoalmineStop.Sole2:
                switch (waypointManagerMine.currentWP)
                {
                    case MineWayPoints.insideCave:
                        speechManagerMine.playSole2 = true;
                        break;
                    case MineWayPoints.viewpoint:
                        speechManagerMine.playSole2Badewannen= true;
                        break;
                }

                break;
            case CoalmineStop.Sole3:
                switch (waypointManagerMine.currentWP)
                {
                    case MineWayPoints.viewpointBewetterung:
                        btnReplayTalkingList.gameObject.SetActive(true);
                        speechManagerMine.playSole3WPBewetterung = true;
                        break;
                    case MineWayPoints.viewpointOVMine:
                        btnReplayTalkingList.gameObject.SetActive(true);
                        speechManagerMine.playSole3WPOVMine = true;
                        break;
                    case MineWayPoints.insideCave:
                        btnReplayTalkingList.gameObject.SetActive(true);
                        speechManagerMine.playSole3WPCave = true;
                        break;
                    case MineWayPoints.viewpointBahnsteig:
                        btnReplayTalkingList.gameObject.SetActive(false);
                        break;
                    case MineWayPoints.viewpoint:
                        btnReplayTalkingList.gameObject.SetActive(false);
                        break;
                }

                break;
        }
    }

    private void Update()
    {
        //ShowFPS();

        //set in false in update, set in true in tl finished;
        if (!runtimeData.replayEntryArea && speechManagerMine.IsMineEATalkingFinished())
        {
            cave.SetAllButtonsInteractable(true);
            runtimeData.replayEntryArea = true;
            btnReplayTalkingList.gameObject.SetActive(true);
        }

        switch (runtimeData.currentCoalmineStop)
        {
            case CoalmineStop.Sole1:
                if (runtimeData.replayS1Cave && !btnReplayTalkingList.gameObject.activeSelf)
                {
                    btnReplayTalkingList.gameObject.SetActive(true);
                }
                break;
            case CoalmineStop.Sole2:
                if (runtimeData.replayS2Cave && !btnReplayTalkingList.gameObject.activeSelf)
                {
                    btnReplayTalkingList.gameObject.SetActive(true);
                }
                break;
            case CoalmineStop.Sole3:
                if (runtimeData.replayS3Cave && !btnReplayTalkingList.gameObject.activeSelf)
                {
                    btnReplayTalkingList.gameObject.SetActive(true);
                }

                //kann ich da rausfinden ob ich auf current wp viewpoint Bahnsteig bin.
                if(waypointManagerMine.currentWP == MineWayPoints.viewpointBahnsteig && btnReplayTalkingList.gameObject.activeSelf)
                {
                    btnReplayTalkingList.gameObject.SetActive(false);
                }

                if (waypointManagerMine.currentWP == MineWayPoints.viewpoint && btnReplayTalkingList.gameObject.activeSelf)
                {
                    btnReplayTalkingList.gameObject.SetActive(false);
                }

                break;
        }

        //if(runtimeData.replayS1Cave && !btnReplayTalkingList.gameObject.activeSelf)
        //{
        //    btnReplayTalkingList.gameObject.SetActive(true);
        //}
        //if (runtimeData.replayS2Cave && !btnReplayTalkingList.gameObject.activeSelf)
        //{
        //    btnReplayTalkingList.gameObject.SetActive(true);
        //}

        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole1)
        {
            sfx.PlayClipsInSole1Sfx();
            //sfx.StartSfxS1(sfxS1);
        }
        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2)
        {
            sfx.PlaySfxSole2();
        }

        if (waypointManagerMine.IsBahnsteigCurrentWP() && runtimeData.trainArrived)
        {
            if(!sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(true);
        }
        else
        {
            if (sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(false);
        }

        if (btnReplayTalkingList.gameObject.activeSelf && cave.moveDirection != CaveMovement.OnHold)
        {
            btnReplayTalkingList.gameObject.SetActive(false);
        }
    }
}
