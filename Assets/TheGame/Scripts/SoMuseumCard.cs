using UnityEngine;

[CreateAssetMenu(menuName = "SoMusumCard")]
public class SoMuseumCard : ScriptableObject
{
    public string myText = "";
    public bool statementTrue = false;
    public Sprite mySprite;
    public bool cardFaceDown = true;

    private void OnEnable()
    {
        cardFaceDown = true;
    }
}
