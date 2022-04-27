using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "QuizConfig")]
public class SoQuizConfig : ScriptableObject
{
    public Sprite btnSprite;
    public Sprite minerFeedbackIdle, minerFeedbackCorrect, minerFeedbackIncorrect;
    public Color normal, selected, correct, incorrect;
    public SoPostData overlayKeySO;
}
