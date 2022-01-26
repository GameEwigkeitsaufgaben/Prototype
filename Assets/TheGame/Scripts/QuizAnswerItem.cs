using UnityEngine;

public class QuizAnswerItem
{
    public string questionIdentifier;
    public string answer;
    public int answerIdentifier;
    public bool isCorrect;

    public QuizAnswerItem() {}

    public void PrintItemData()
    {
        Debug.Log("questionIdentifier: " + questionIdentifier + "\n" +
                  "answer: " + answer + "\n" +
                  "answerIdentifier: " + answerIdentifier + "\n" +
                  "isCorrect: " + isCorrect);
    }
}
