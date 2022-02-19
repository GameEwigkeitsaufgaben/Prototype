using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "QuizConfig")]
public class SoQuizConfig : ScriptableObject
{
    public Sprite btnSprite;
    public Color normal, selected, correct, incorrect;
    public PostData overlayKeySO;
}
