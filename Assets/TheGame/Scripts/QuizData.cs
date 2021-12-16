using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum QuizRightAnser
{
    A, B, C, D, NONE
}

[CreateAssetMenu(menuName = "QuizData")]
public class QuizData : ScriptableObject
{
    [TextArea(10, 100)]
    public string question;
    public QuizRightAnser rightAnswer;
    [TextArea(10, 100)]
    public string answerA;
    [TextArea(10, 100)]
    public string answerB;
    [TextArea(10, 100)]
    public string answerC;
    [TextArea(10, 100)]
    public string answerD;
}
