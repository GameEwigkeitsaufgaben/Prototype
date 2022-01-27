using UnityEngine;


public enum QuizAnswer
{
    AnswersElement0, AnswersElement1, AnswersElement2, AnswersElement3, NONE
}

public enum QuizQuestionType
{
    TrueFalse,
    OneTrue,
    AtLeastOneTrue
}

[CreateAssetMenu(menuName = "QuizData")]
public class QuizData : ScriptableObject
{
    public QuizQuestionType questionType;
    public Sprite postImage;

    public int timeToAnswerInSec;

    [TextArea(10, 100)]
    public string question;

    [TextArea(10, 100)]
    public string[] answers;

    public QuizAnswer[] correctAnswers;

    //delete ab hier
    
    [TextArea(10, 100)]
    public string answerA;
    
    [TextArea(10, 100)]
    public string answerB;
    
    [TextArea(10, 100)]
    public string answerC;
    
    [TextArea(10, 100)]
    public string answerD;

}
