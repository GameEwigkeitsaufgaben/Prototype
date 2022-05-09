using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
    }

    public void LoadMine()
    {
        SceneManager.LoadScene(GameScenes.ch01Mine, LoadSceneMode.Single);
        Debug.Log("LoadScene Mine");
    }

    public void LoadSole1()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleOneStatic, LoadSceneMode.Additive);
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

    public void LoadMineAnimation()
    {
        SceneManager.LoadScene(GameScenes.ch01MineAnimation, LoadSceneMode.Additive);
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

    public void GotToCredits()
    {
        SceneManager.LoadScene(GameScenes.ch00GameCredits, LoadSceneMode.Single);
    }

    public void GoToTrainRideOut()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleThreeTrainRideOut, LoadSceneMode.Single);
    }

    public void GoToTrainRideIn()
    {
        SceneManager.LoadScene(GameScenes.ch01MineSoleThreeTrainRideIn, LoadSceneMode.Single);
    }

    public void GoToInstaChapOne()
    {
        SceneManager.LoadScene(GameScenes.ch01InstaMain, LoadSceneMode.Single);
    }

    public void GoToMuseum()
    {
        Debug.Log("Scene schould be loaded");
        SceneManager.LoadScene(GameScenes.ch01Museum, LoadSceneMode.Single);
    }
    public void GoToMinerEquipment()
    {
        Debug.Log("Scene schould be loaded");
        SceneManager.LoadScene(GameScenes.ch01MuseumMinerEquipment, LoadSceneMode.Single);
    }

    public void GoToWorld()
    {
        Debug.Log("Scene schould be loaded");
        SceneManager.LoadScene(GameScenes.ch01MuseumCarbonPeriod, LoadSceneMode.Single);
    }

    public void GoToCoalification()
    {
        Debug.Log("Scene schould be loaded --------------------------------------------------------------------");
        SceneManager.LoadScene(GameScenes.ch01MuseumCoalification, LoadSceneMode.Single);
    }

    public void GoToMythos()
    {
        Debug.Log("Scene schould be loaded");
        SceneManager.LoadScene(GameScenes.ch01MuseumHistoryMining, LoadSceneMode.Single);
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
        runtimeData.postOverlayToLoad = overlayName;
        SwitchScene(GameScenes.ch01InstaMain); 
    }

    IEnumerator LoadSceneWithTransition(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SwitchScene(name);
    }
}
