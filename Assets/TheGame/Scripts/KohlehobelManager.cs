using UnityEngine;

public class KohlehobelManager : MonoBehaviour
{
    public Player player;
    public SwitchSceneManager switchScene;
    public CoalmineSpeechManger speechManager;

    // Start is called before the first frame update
    void Start()
    {
        switchScene.LoadLongwallCutterStatic();
        switchScene.LoadLongwallCutterAnim();
    }

    //Positionieren und Ausrichten Player (entry Area)    //integriert in player todo umschreiben, übergabe pos von kh in methode.
    
    //Buttons mit Pfaden verlinken (3sohle)


    //Audios auf Buttons abspielen (3sohle)
    //SFX 

    private void Update()
    {
        
    }
}
