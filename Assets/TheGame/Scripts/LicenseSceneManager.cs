using UnityEngine;
using UnityEngine.UI;

public class LicenseSceneManager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Button confirm, decline;

    private bool scrolledToEnd;

    private void Start()
    {
        confirm.interactable = false;
        decline.interactable = false;
        scrolledToEnd = false;
    }

    //called from ScrollRect OnValueChanged
    public void SetScrolledToEnd()
    {
        if (!scrolledToEnd && scrollRect.verticalScrollbar.value <= 0.28f)
        {
            confirm.interactable = true;
            decline.interactable = true;
            scrolledToEnd = true;
        }
    }

    public void SwitchToDecline()
    {
        gameObject.GetComponent<CoalMineSceneManager>().SwitchScene(GameData.sceneLicenseDeclined);
    }

    public void SwitchToGame()
    {
        gameObject.GetComponent<CoalMineSceneManager>().SwitchScene(GameData.sceneMainMenu);
    }

}
