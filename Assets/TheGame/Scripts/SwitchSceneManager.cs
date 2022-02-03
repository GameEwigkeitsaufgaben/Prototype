using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //is called from more than one uielements in inspector
    public void GoToChapterOverview()
    {
        SwitchScene(GameData.sceneChapterMainMenu);
    }

    //rideIn true if you go to the longwall cutter
    //rideIn false if you go way from the longwal cutter, i.e. go back to surface
    public void GoToTrainRide(bool rideIn)
    {
        GameData.rideIn = rideIn;
        SwitchScene(ScenesChapterOne.MineSoleThreeTrainRide);
    }

    public void SwitchSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void SwitchToChapter1withOverlay(string overlayName)
    {
        GameData.overlayToLoad = overlayName;
        SwitchScene(ScenesChapterOne.InstaMainChapterOne); 
    }

    IEnumerator LoadSceneWithTransition(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SwitchScene(name);
    }
}
