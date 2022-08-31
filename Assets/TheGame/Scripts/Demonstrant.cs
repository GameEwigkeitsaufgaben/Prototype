using UnityEngine;
using UnityEngine.UI;

public class Demonstrant : MonoBehaviour
{
    public Image speechBubble;
    public Image characterImage;
    public AudioClip audioClip;
    public bool gehoert;
    private Color feedbackGehoert = Color.white;
    private Color fbUngehoert = new Color32(99, 99, 99, 255);

    private void Awake()
    {
        characterImage = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGehoert(false);
        SpeechBubbleOn(false);
    }

    public void SetGehoert(bool gehoertJa)
    {
        if (gehoert) return;
        
        if (gehoertJa)
        {
            gehoert = true;
            characterImage.color = feedbackGehoert;
            return;
        }

        gehoert = false;
        characterImage.color = fbUngehoert;
    }

    public void SpeechBubbleOn(bool turnOn)
    {
        speechBubble.gameObject.SetActive(turnOn);
    }
}
