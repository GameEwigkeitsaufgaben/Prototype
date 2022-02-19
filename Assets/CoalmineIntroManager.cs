using UnityEngine;

public class CoalmineIntroManager : MonoBehaviour
{
    public CoalmineSpeechManger mySpeechManger;
    SwitchSceneManager switchScene;

    // Start is called before the first frame update
    void Start()
    {
        switchScene = gameObject.GetComponent<SwitchSceneManager>();
        mySpeechManger.playCaveIntro = true;    
    }
}
