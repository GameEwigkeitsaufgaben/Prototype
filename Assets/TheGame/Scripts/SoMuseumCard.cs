using UnityEngine;

[CreateAssetMenu(menuName = "SoMusumCard")]
public class SoMuseumCard : ScriptableObject
{
    [TextArea(10, 100)]
    public string statement = "";
    public bool statementTrue = false;
    public Sprite mySprite;
    public bool cardFaceDown = false;

    private void OnEnable()
    {
        cardFaceDown = false;
    }
}
