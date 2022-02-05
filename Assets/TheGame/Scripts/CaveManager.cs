//Management Scene
//
using UnityEngine;
using UnityEngine.UI;

public class CaveManager : MonoBehaviour
{
    public Cave cave;
    public Player player;
    public SwitchSceneManager switchScene;

    public Button exitScene;
    public SprechblaseController sprechblaseController;

    public GameObject triggerEinstieg;

    bool introPlayedOneTime = false;
    

    private void Start()
    {
        //triggerEinstieg.SetActive(false);
        GameData.scene1162LoadedOnce = true;
        switchScene.LoadEntryArea();
        switchScene.LoadSohle1();
        switchScene.LoadSohle2();

    }

    public void SetTargetForPlayer(GameObject obj)
    {
        player.SetTarget(obj);
    }

    private void OnEnable()
    {
        Debug.Log("In On Enable");
        Debug.Log("sohle to reload " + GameData.sohleToReload);

        if((CurrentStop)GameData.sohleToReload == CurrentStop.Sohle3)
        {
            cave.ReloadSohle3AsCurrent();
            player.SetPlayerToAnkerPosition();
        }
    }

    public void GoToSohle3Kohlehobel()
    {
        switchScene.SwitchSceneWithTransition(ScenesChapterOne.MineSoleThreeTrainRide);
        GameData.sohleToReload = (int)CurrentStop.Sohle3;
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
        }

        if (!cave.caveDoorsClosed && !exitScene.interactable)
        {
            exitScene.interactable = true;
        }
    }
}
