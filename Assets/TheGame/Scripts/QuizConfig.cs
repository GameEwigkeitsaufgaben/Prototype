using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "QuizConfig")]
public class QuizConfig : ScriptableObject
{
    public Sprite btnSprite;
    public Color normal, selected, correct, incorrect;
    public Text overlayKeyUiText;
    public PostData overlayKeySO;
}
