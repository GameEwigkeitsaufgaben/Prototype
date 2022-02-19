using UnityEngine;

[CreateAssetMenu(menuName = "QuizConfig")]
public class QuizConfig : ScriptableObject
{
    public Sprite btnSprite;
    public Color normal, selected, correct, incorrect;
}
