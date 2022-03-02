using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public SoCharacter characterConfigSO;

    SpeechBubble speechBubble;
    Image characterImage;

    int previousStop = -1;
    public bool sole1ImgUpdated = false, s2ImgUpdated = false, s3ImgUpdated = false;
    public bool entryAreaUpdated = false;

    void Start()
    {
        //Aufbau: Name des Characters (Canvas, CanvasScaler Spline Move, this Script)
        //�ber dieses Script ist alles andere erreichbar
        
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

        //setzte default sprite f�r character
        if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01LongwallCutter)
        {
            characterImage.GetComponent<Image>().sprite = characterConfigSO.longwallCutterStandingTalking;
        }
        else if (SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01Mine)
        {
            ChangeCharacterImage(CoalmineStop.EntryArea);
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

    public void ChangeCharacterImage(CoalmineStop stop)
    {
        switch (stop)
        {
            case CoalmineStop.EntryArea:
                Debug.Log("in change img ea");
                entryAreaUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.entryAreaStandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != "noCharacterSprite") break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.entryAreaStandingTalking;
                break;

            case CoalmineStop.Sole1:
                Debug.Log("in change img s1");
                sole1ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole1StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != "noCharacterSprite") break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole1StandingTalking;
                break;

            case CoalmineStop.Sole2:
                Debug.Log("in change img s2");
                s2ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole2StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != "noCharacterSprite") break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole2StandingTalking;
                break;

            case CoalmineStop.Sole3:
                Debug.Log("in change img s3");
                s3ImgUpdated = true;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole3StandingSilent;
                if (characterImage.GetComponent<Image>().sprite.name != "noCharacterSprite") break;
                characterImage.GetComponent<Image>().sprite = characterConfigSO.sole3StandingTalking;
                break;
        }
    }

    private void Update()
    {
        //within cave
        //Update characterimage accordingly to cavestop; the character in s3 is the most dirty one, if the character reaches this sole no more updates happen and
        //updates only happen if the cave is moving down.
        if (s3ImgUpdated) return;

        bool stopChanged = previousStop != GameData.currentStopSohle;
        bool caveDirectionIsDown = (int)CaveMovement.MoveDown == GameData.moveDirection;

        if (!entryAreaUpdated)
        {
            if (GameData.currentStopSohle == (int)CoalmineStop.EntryArea)
            {
                ChangeCharacterImage(CoalmineStop.EntryArea);
                previousStop = (int)CoalmineStop.EntryArea;
            }
        }
        else if (!sole1ImgUpdated && (GameData.currentStopSohle == (int)CoalmineStop.Sole1))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole1);
                previousStop = (int)CoalmineStop.Sole1;
            }
        }
        else if (!s2ImgUpdated && (GameData.currentStopSohle == (int)CoalmineStop.Sole2))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole2);
                previousStop = (int)CoalmineStop.Sole2;
            }
        }
        else if (!s3ImgUpdated && (GameData.currentStopSohle == (int)CoalmineStop.Sole3))
        {
            if (stopChanged && caveDirectionIsDown)
            {
                ChangeCharacterImage(CoalmineStop.Sole3);
                previousStop = (int)CoalmineStop.Sole3;
            }
        }
    }
}
