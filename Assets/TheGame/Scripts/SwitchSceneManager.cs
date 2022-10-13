using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    SoChapOneRuntimeData runtimeDataCh1;
    SoChapTwoRuntimeData runtimeDataCh2;
    SoChapThreeRuntimeData runtimeDataCh3;

    private void Awake()
    {
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh2 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
    }

    public void LoadMine()
    {
        SceneManager.LoadScene(GameScenes.ch01Mine, LoadSceneMode.Single);
    }

    public void LoadFotoplatz()
    {
        SceneManager.LoadScene(GameScenes.ch00Fotoplatz, LoadSceneMode.Single);
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

    public void GoToFliesspfade()
    {
        SceneManager.LoadScene(GameScenes.ch02Fliesspfade, LoadSceneMode.Single);
    }

    public void GoToCH2Museum()
    {
        SceneManager.LoadScene(GameScenes.ch02Museum, LoadSceneMode.Single);
    }

    public void GoToGWCleanActive()
    {
        SceneManager.LoadScene(GameScenes.ch02gwReinigungAktiv, LoadSceneMode.Single);
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
        SceneManager.LoadScene(GameScenes.ch01Museum, LoadSceneMode.Single);
    }
    public void GoToMinerEquipment()
    {
        SceneManager.LoadScene(GameScenes.ch01MuseumMinerEquipment, LoadSceneMode.Single);
    }

    public void GoToWorld()
    {
        SceneManager.LoadScene(GameScenes.ch01MuseumCarbonPeriod, LoadSceneMode.Single);
    }

    public void GoToCoalification()
    {
        SceneManager.LoadScene(GameScenes.ch01MuseumCoalification, LoadSceneMode.Single);
    }

    public void GoToMythos()
    {
        SceneManager.LoadScene(GameScenes.ch01MuseumHistoryMining, LoadSceneMode.Single);
    }

    public void GoToCh2MuseumTV()
    {
        SwitchScene(GameScenes.ch02MuseumTV, LoadSceneMode.Single);
    }

    public void SwitchScene(string sceneName, LoadSceneMode sceneMode)
    {
        SceneManager.LoadScene(sceneName, sceneMode);
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
        if (overlayName == GameData.NameOverlay1110)
        {
            runtimeDataCh1.post1110Done = true;
        }

        runtimeDataCh1.postOverlayToLoad = overlayName;
        SwitchScene(GameScenes.ch01InstaMain); 
    }
    public void SwitchToChapter2withOverlay(string overlayName)
    {

        if (overlayName == GameData.NameOverlay2112)
        {
            runtimeDataCh2.progressPost2112Done = true;
        }

        runtimeDataCh2.postOverlayToLoad = overlayName;
        SwitchScene(GameScenes.ch02InstaMain);
    }

    public void SwitchToChapter3withOverlay(string overlayName)
    {
        if (overlayName == GameData.NameOverlay317)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post317);
        }

        runtimeDataCh3.postOverlayToLoad = overlayName;
        SwitchScene(GameScenes.ch03InstaMain);
    }

    public int GetActiveQuizScene()
    {
        if (SceneManager.GetActiveScene().name == GameScenes.ch01Quiz)
            return 1;
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02Quiz)
            return 2;
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03Quiz)
            return 3;

        return -1;
    }

    IEnumerator LoadSceneWithTransition(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SwitchScene(name);
    }
}
