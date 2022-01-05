using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchSceneManager : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
    public void SwitchToChapter1withOverlay(string overlayName)
    {
        GameData.overlayToLoad = overlayName;
        SwitchScene(GameData.sceneMainChapterOne);
    }

}
