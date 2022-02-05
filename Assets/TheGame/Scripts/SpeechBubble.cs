using UnityEngine;

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
    bool isOn = false;
    
    void Start()
    {
        gameObject.SetActive(false);
    }

}
