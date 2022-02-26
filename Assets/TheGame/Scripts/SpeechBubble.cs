using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CharactersInGame
{
    Enya = 0,
    Dad = 1,
    Georg = 2,
    Museumguide = 3
}

public class SpeechBubble : MonoBehaviour
{
    public CharactersInGame myCharacter;
    public Image bubbleImage;
    public Button bubbleButton;

    bool isOn = false;

    private void Awake()
    {
        SetupSpeechBubble();
    }

    void Start()
    {
        gameObject.SetActive(false);
        if (CharactersInGame.Dad == myCharacter && SwitchSceneManager.GetCurrentSceneName() == GameScenes.ch01LongwallCutter)
        {
            RotateSpeechBubble(180f);
        }
    }

    private void SetupSpeechBubble()
    {
        Transform[] myElements = GetComponentsInChildren<Transform>(true);
        for (int i = 0; i < myElements.Length; i++)
        {
            bubbleImage = myElements[1].GetComponent<Image>();
            bubbleButton = myElements[2].GetComponent<Button>();

            Debug.Log("in speechbubble " + i + myElements[i].name);
        }
    }

    public void SetBubbleSprite(Sprite bSprite)
    {
        if (bubbleImage == null)
        {
            SetupSpeechBubble();
        }

        bubbleImage.sprite = bSprite;
    }

    public void RotateSpeechBubble(float yRotation)
    {
        gameObject.transform.localRotation = Quaternion.Euler(gameObject.transform.localRotation.x, yRotation, gameObject.transform.localRotation.z);
        Debug.Log("in rotate Speechbubble");
    }
}
