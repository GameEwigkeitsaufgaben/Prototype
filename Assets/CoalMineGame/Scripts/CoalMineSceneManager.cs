using UnityEngine;
using UnityEngine.SceneManagement;

public class CoalMineSceneManager : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
