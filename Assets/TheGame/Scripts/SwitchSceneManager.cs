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
        SwitchScene(GameData.sceneMainChapterOne);
    }

    IEnumerator LoadSceneWithTransition(string name)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SwitchScene(name);

    }
}
