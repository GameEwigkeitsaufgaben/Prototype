//This script manages the coalmine interactive scene, loads necessary static scenes and scenes with animation. 
//The cave.cs 
//The player.cs Speech manager is onto paranet GameObject Characters

using UnityEngine;
using UnityEngine.UI;

public class CaveManager : MonoBehaviour
{
    public Cave cave;
    public Player player;
    public SwitchSceneManager switchScene;

    public Button exitScene, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWP;
   // public SprechblaseController sprechblaseController;

    public GameObject triggerEinstieg;

    private bool introPlayedOneTime = false;

    private void Start()
    {
        //triggerEinstieg.SetActive(false);
        GameData.scene1162LoadedOnce = true;

        switchScene.LoadEntryArea();
        switchScene.LoadCaveTunnel();
        switchScene.LoadSohle1();
        switchScene.LoadSohle2();
        switchScene.LoadSohle3();
    }

    public void SetTargetForPlayer(GameObject obj)
    {
        player.SetTarget(obj);
    }

    private void OnEnable()
    {
        Debug.Log("In On Enable");
        Debug.Log("sohle to reload " + GameData.sohleToReload);

        if ((CoalmineStop)GameData.sohleToReload == CoalmineStop.Sole3)
        {
            cave.ReloadSohle3AsCurrent();
            player.SetPlayerToAnkerPosition();
        }
    }

    public void GoToSohle3Kohlehobel()
    {
        switchScene.SwitchSceneWithTransition(GameScenes.ch01MineSoleThreeTrainRide);
        GameData.sohleToReload = (int)CoalmineStop.Sole3;
        cave.StoreCavePosition();
    }

    private void OnDestroy()
    {
        GameData.liftBtnsEnabled = false;
    }

    private void Update()
    {
        if (cave.caveDoorsClosed && exitScene.interactable)
        {
            exitScene.interactable = false;
            //sole1WPViewpointBtn.interactable = false;
        }

        if (!cave.caveDoorsClosed && !exitScene.interactable)
        {
            exitScene.interactable = true;
            //sole1WPViewpointBtn.interactable = true;
        }
    }
}
