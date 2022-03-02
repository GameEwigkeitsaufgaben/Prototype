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
    private SwitchSceneManager switchScene;
    public Character characterEnya, characterDad, characterGeorg;

    public Button exitScene, sole1WPViewpointBtn, sole2WPViewpointBtn, sole1caveWP;
   // public SprechblaseController sprechblaseController;

    //public GameObject triggerEinstieg;

    private bool introPlayedOneTime = false;

    private void Start()
    {
        //triggerEinstieg.SetActive(false);

        switchScene = gameObject.GetComponent<SwitchSceneManager>();

        switchScene.LoadEntryArea();
        switchScene.LoadCaveTunnel();
        switchScene.LoadSole1();
        switchScene.LoadSole2();
        switchScene.LoadSole3();

        //cave.caveTriggerBottom.SetCharacters(characterEnya, characterDad, characterGeorg);
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
