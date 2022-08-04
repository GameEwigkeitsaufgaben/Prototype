using UnityEngine;
using UnityEngine.UI;

public class Demonstrant : MonoBehaviour
{
    public Image speechBubble;
    public AudioClip audioClip;
    public bool gehoert;

    // Start is called before the first frame update
    void Start()
    {
        gehoert = false;
    }

    public void SpeechBubbleOn(bool turnOn)
    {
        speechBubble.gameObject.SetActive(turnOn);
    }
}
