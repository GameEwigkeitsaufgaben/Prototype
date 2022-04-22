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

    public Button exitSceneBtn, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWPBtn, sole3EnterTrainBtn;
   // public SprechblaseController sprechblaseController;

    //public GameObject triggerEinstieg;

    private bool introPlayedOneTime = false;
    private SoSfx sfx;

    public AudioSource baukipper, kran, water;

    private void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
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
       
    }

    private void OnEnable()
    {
        Debug.Log("In On Enable");
        Debug.Log("sohle to reload " + GameData.sohleToReload);
        
        //At first load
        if(GameData.currentStopSohle == (int)CoalmineStop.Unset)
        {
            GameData.currentStopSohle = (int)CoalmineStop.EntryArea;
            player.SetPlayerRotation(defaultYawInCave, false); //Default 90 degree for cave orientation
        }
        
        //at Reload
        if ((CoalmineStop)GameData.sohleToReload == CoalmineStop.Sole3)
        {
            GameData.currentStopSohle = (int)CoalmineStop.Sole3;
            cave.ReloadCaveAtSohle3();
            //player=?
            //player.SetPlayerBodyRotation(defaultYawInCave, false); //Default 90 degree for cave orientation
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

        if (GetComponent<CoalmineWaypointManager>().IsBahnsteigCurrentWP())
        {
            if(!sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(true);
        }
        else
        {
            if (sole3EnterTrainBtn.IsActive())
                sole3EnterTrainBtn.gameObject.SetActive(false);
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
