using UnityEngine;

public class ManagerOnboarding : MonoBehaviour
{
    public void SwitchOnboarding1To2 ()
    {
        Invoke("SwitchScene1To2", 0.3f);
    }

    public void SwitchOnboarding2To3()
    {
        Invoke("SwitchScene2To3", 0.3f);
    }

    public void SwitchOnboarding3ToOv()
    {
        Invoke("SwitchSceneOBToOV", 0.3f);
    }

    public void SwitchScene1To2()
    {
        GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch01GameOnboarding2);
    }
    public void SwitchScene2To3()
    {
        GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch01GameOnboarding3);
    }

    public void SwitchSceneOBToOV()
    {
        GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch00ChapterOverview);
    }
}
