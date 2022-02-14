using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadMine()
    {
        SceneManager.LoadScene(ScenesChapterOne.Mine, LoadSceneMode.Additive);
        Debug.Log("LoadScene sohle1");
    }

    public void LoadSohle1()
    {
        SceneManager.LoadScene(ScenesChapterOne.MineSoleOneStatic, LoadSceneMode.Additive);
        Debug.Log("LoadScene sohle1");
    }
    public void LoadSohle2()
    {
        SceneManager.LoadScene(ScenesChapterOne.MineSoleTwoStatic, LoadSceneMode.Additive);
    }

    internal void LoadSohle3()
    {
        SceneManager.LoadScene(ScenesChapterOne.MineSoleThreeStatic, LoadSceneMode.Additive);
    }

    public void LoadEntryArea()
    {
        SceneManager.LoadScene(ScenesChapterOne.MineEntryAreaStatic, LoadSceneMode.Additive);
    }

    public void LoadLongwallCutter()
    {
        SceneManager.LoadScene(ScenesChapterOne.LongwallCutter, LoadSceneMode.Single);
    }

    public void LoadLongwallCutterStatic()
    {
        SceneManager.LoadScene(ScenesChapterOne.LongwallCutterStatic, LoadSceneMode.Additive);
    }

    public void LoadLongwallCutterAnim()
    {
        SceneManager.LoadScene(ScenesChapterOne.LongwallCutterAnimation, LoadSceneMode.Additive);
    }

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
