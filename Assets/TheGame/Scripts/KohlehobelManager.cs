using UnityEngine;

public class KohlehobelManager : MonoBehaviour
{
    public Player player;
    public SwitchSceneManager switchScene;
    public Transform firstPlayerPos;
    public CoalmineSpeechManger speechManager;

    // Start is called before the first frame update
    void Start()
    {
        switchScene.LoadLongwallCutterStatic();
    }

    //Positionieren und Ausrichten Player (entry Area)    
    //Buttons mit Pfaden verlinken (3sohle)
    //Audios auf Buttons abspielen (3sohle)
    //SFX 

    private void Update()
    {
        
    }
}
