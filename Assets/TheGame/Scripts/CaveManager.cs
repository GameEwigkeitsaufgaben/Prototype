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
    public Cave cave;
    public Player player;
    public Character characterEnya, characterDad, characterGeorg;

    private const float defaultYawInCave = 0f;
    private SwitchSceneManager switchScene;
    
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;
    private CoalmineSpeechManger speechManagerMine;
    
    public Button exitSceneBtn, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWPBtn, sole3EnterTrainBtn;

    private bool introPlayedOneTime = false;
    private SoSfx sfx;

    public AudioSource baukipper, kran, water;

    private void Awake()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
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

        sfx.SetClipByAddToDict(baukipper, sfx.caolmineLader);
        sfx.SetClipByAddToDict(kran, sfx.coalmineWorkingMachinesMetal);
        sfx.SetClipByAddToDict(water, sfx.caolmineSplashingWater);

        sole3EnterTrainBtn.gameObject.SetActive(false);
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
        cave.StoreCavePosition();
        player.StorePlayerAtBahnsteigPositon();
        switchScene.GoToTrainRideIn();
    }

    private void OnDestroy()
    {
        runtimeData.liftBtnsAllEnabled = false;
    }

    private void Update()
    {
        if(runtimeData.currentCoalmineStop == CoalmineStop.Sole1)
        {
            sfx.PlayClipsInSole1Sfx();
        }
        if (runtimeData.currentCoalmineStop == CoalmineStop.Sole2)
        {
            sfx.PlaySfxSole2();
        }

        if (GetComponent<CoalmineWaypointManager>().IsBahnsteigCurrentWP() && runtimeData.trainArrived)
        {
            if(!sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(true);
        }
        else
        {
            if (sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(false);
        }

        if(!introPlayedOneTime && speechManagerMine.IsMineEATalkingFinished())
        {
            Debug.Log("set done ea");
            cave.SetAllButtonsInteractable(true);
            introPlayedOneTime = true;
            runtimeData.entryAreaDone = true;
        }
    }
}
