using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBlackscreen : MonoBehaviour
{
   // AudioClip train, talkingDad;

    public AudioSource envSoundTrain;
    public AudioSource dad;
    public SwitchSceneManager switchScene;
    public bool kh = true;

    private void Awake()
    {
        envSoundTrain.Play();

        if (GameData.gotToKohlehobel)
        {
            dad.Play();
        }

        Invoke("SwitchTheScene", 4f);
    }

    // Start is called before the first frame update
    void Start()
    {


    }

    public void SwitchTheScene()
    {
        Debug.Log("kohlehobel in switch the scene: " + GameData.gotToKohlehobel);
        if (GameData.gotToKohlehobel)
        {
            switchScene.SwitchSceneWithTransition(GameData.scene11651Kohlehobel);
        }
        else
        {
            switchScene.SwitchSceneWithTransition(GameData.scene1162);  
        }

        GameData.gotToKohlehobel = !GameData.gotToKohlehobel;
    }
}
