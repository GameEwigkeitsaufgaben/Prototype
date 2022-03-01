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
        SceneManager.LoadScene(GameScenes.ch01Mine, LoadSceneMode.Single);
        Debug.Log("LoadScene sohle1");
    }

    public void LoadSole1()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleOneStatic, LoadSceneMode.Additive);
        Debug.Log("LoadScene sohle1");
    }
    public void LoadSole2()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleTwoStatic, LoadSceneMode.Additive);
    }

    internal void LoadSole3()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleThreeStatic, LoadSceneMode.Additive);
    }

    internal void LoadCaveTunnel()
    {
        SceneManager.LoadScene(GameScenes.ch01MineCaveTunnelStatic, LoadSceneMode.Additive);
    }

    public void LoadEntryArea()
    {
        SceneManager.LoadScene(GameScenes.ch01MineEntryAreaStatic, LoadSceneMode.Additive);
        
    }

    public void GoToLongwallCutter()
    {
        SceneManager.LoadScene(GameScenes.ch01LongwallCutter, LoadSceneMode.Single);
    }

    public void LoadLongwallCutterStatic()
    {
        SceneManager.LoadScene(GameScenes.ch01LongwallCutterStatic, LoadSceneMode.Additive);
    }

    public void LoadLongwallCutterAnim()
    {
        SceneManager.LoadScene(GameScenes.ch01LongwallCutterAnimation, LoadSceneMode.Additive);
    }

    public void GoToTrainRideOut()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleThreeTrainRideOut, LoadSceneMode.Single);
    }

    public void GoToTrainRideIn()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleThreeTrainRideIn, LoadSceneMode.Single);
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    //is called from more than one uielements in inspector
    public void GoToChapterOverview()
    {
        //SwitchScene(GameData.sceneChapterMainMenu);
        SwitchScene(GameScenes.ch00ChapterOverview);
    }

    //rideIn true if you go to the longwall cutter
    //rideIn false if you go way from the longwal cutter, i.e. go back to surface
    //public void GoToTrainRide(bool rideIn)
    //{
    //    GameData.rideIn = rideIn;
    //    SwitchScene(GameScenes.ch01MineSoleThreeTrainRide);
    //}

    public void SwitchSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public static string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void SwitchToChapter1withOverlay(string overlayName)
    {
        GameData.overlayToLoad = overlayName;
        SwitchScene(GameScenes.ch01InstaMain); 
    }

    IEnumerator LoadSceneWithTransition(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SwitchScene(name);
    }
}
