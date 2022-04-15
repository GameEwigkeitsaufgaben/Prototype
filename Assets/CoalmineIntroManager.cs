using UnityEngine;
using UnityEngine.UI;

public class CoalmineIntroManager : MonoBehaviour
{
    public CoalmineSpeechManger mySpeechManger;
    SwitchSceneManager switchScene;
    public Button nextSceneBtn;
    // Start is called before the first frame update
    void Start()
    {
        switchScene = gameObject.GetComponent<SwitchSceneManager>();
        mySpeechManger.playCaveIntro = true;
        ColorBlock colorBlock = ColorBlock.defaultColorBlock;
        colorBlock.normalColor = GameColors.defaultInteractionColorNormal;
        colorBlock.highlightedColor = GameColors.defaultInteractionColorHighlighted;
        colorBlock.pressedColor = GameColors.defaultInteractionColorPresses;

        nextSceneBtn.GetComponent<Button>().colors = colorBlock;
    }
}
