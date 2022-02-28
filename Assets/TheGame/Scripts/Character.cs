using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public SoCharacter characterConfigSO;

    SpeechBubble speechBubble;
    Image characterImage;

    void Start()
    {
        //Aufbau: Name des Characters (Canvas, CanvasScaler Spline Move, this Script)
        //Über dieses Script ist alles andere erreichbar
        
        // Im Canvas die main cam setzen.
        Camera mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        gameObject.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        gameObject.GetComponent<Canvas>().worldCamera = mainCam;

        //Die Kinderholen und GOs speechBubble und Img von Character holen. 
        Component[] character = GetComponentsInChildren<Transform>(true);
        
        for (int i = 0; i < character.Length; i++)
        {
            if (i == 1)
            {   //speechbubble
                speechBubble = character[i].GetComponent<SpeechBubble>();
            }
            if (i == 4)
            {   //Image von Character
                characterImage = character[i].GetComponent<Image>();
            }
        }

        //setze speechbubble sprite
        speechBubble.SetBubbleSprite(characterConfigSO.speechBubble);

        //setzte default sprite für character
        if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01LongwallCutter)
        {
            characterImage.GetComponent<Image>().sprite = characterConfigSO.longwallCutterStandingTalking;
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01Mine)
        {
            characterImage.GetComponent<Image>().sprite = characterConfigSO.entryAreaStandingSilent;
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01MineIntro)
        {
            characterImage.GetComponent<Image>().sprite = characterConfigSO.outsideMineStandingSilient;
        }
    }

    public void RotateCharacter(float yRotation)
    {
        gameObject.transform.localRotation = Quaternion.Euler(0f, yRotation, 0f);
    }

}
