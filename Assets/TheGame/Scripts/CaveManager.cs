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
    private const float defaultYawInCave = 0f;
    public Cave cave;
    public Player player;
    private SwitchSceneManager switchScene;
    public Character characterEnya, characterDad, characterGeorg;

    private SoChapOneRuntimeData runtimeData;
    private CoalmineSpeechManger speechManagerMine;
    
    public Button exitSceneBtn, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWPBtn, sole3EnterTrainBtn;
   // public SprechblaseController sprechblaseController;

    //public GameObject triggerEinstieg;

    private bool introPlayedOneTime = false;
    private SoSfx sfx;

    public AudioSource baukipper, kran, water;
    public Texture2D cursorTexture;

    private void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        runtimeData.sceneDefaultcursor = runtimeData.cursorTexture3DCave;
        Cursor.SetCursor(runtimeData.sceneDefaultcursor, Vector2.zero, CursorMode.Auto);
        speechManagerMine = characterDad.transform.parent.GetComponent<CoalmineSpeechManger>();

        //triggerEinstieg.SetActive(false);

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
        Debug.Log("In On Enable");
        Debug.Log("sohle to reload " + GameData.sohleToReload);
        
        //At first load
        if(GameData.currentStopSohle == (int)CoalmineStop.Unset)
        {
            player.SetPlayerRotation(defaultYawInCave, false); //Default 90 degree for cave orientation
            cave.SetAllButtonsInteractable(false);
            cave.liftBtns[0].gameObject.GetComponent<Button>().interactable = true;
        }
        
        //at Reload
        if ((CoalmineStop)GameData.sohleToReload == CoalmineStop.Sole3)
        {
            GameData.currentStopSohle = (int)CoalmineStop.Sole3;
            cave.ReloadCaveAtSohle3();
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
        GameData.liftBtnsEnabled = false;
    }

    private void Update()
    {
        if(GameData.currentStopSohle == (int)CoalmineStop.Sole1)
        {
            sfx.PlayClipsInSole1Sfx();
        }
        if (GameData.currentStopSohle == (int)CoalmineStop.Sole2)
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
            cave.SetAllButtonsInteractable(true);
            introPlayedOneTime = true;
            runtimeData.entryAreaDone = true;
        }
          
        //if (cave.caveDoorsClosed && exitScene.interactable)
        //{
        //    exitScene.interactable = false;
        //    //sole1WPViewpointBtn.interactable = false;
        //}

        //if (!cave.caveDoorsClosed && !exitScene.interactable)
        //{
        //    exitScene.interactable = true;
        //    //sole1WPViewpointBtn.interactable = true;
        //}
    }
}
