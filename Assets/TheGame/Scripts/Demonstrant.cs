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
        gehoert = false;
        SetGehoertFeedback(false);
        SpeechBubbleOn(false);
    }

    public void SetGehoertFeedback(bool finished)
    {
        characterImage.color = finished ? feedbackGehoert: fbUngehoert;
    }

    public void SetGehoert()
    {
        if (gehoert) return;

        gehoert = true;
    }

    public void SpeechBubbleOn(bool turnOn)
    {
        speechBubble.gameObject.SetActive(turnOn);
    }
}
