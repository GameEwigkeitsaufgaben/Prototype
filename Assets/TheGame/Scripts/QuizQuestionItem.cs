using System.Collections.Generic;
using UnityEngine;

public class QuizQuestionItem
{
    private QuizData questionItemData;
    private QuizQuestionType questionType;
    public List<QuizAnswerItem> answers = new List<QuizAnswerItem>();

    public QuizQuestionItem(QuizData cdata)
    {
        questionItemData = cdata;

        for (int i = 0; i < questionItemData.answers.Length; i++)
        {
            QuizAnswerItem aw = new QuizAnswerItem();
            aw.questionIdentifier = questionItemData.name.Trim().ToLower();
            aw.answer = questionItemData.answers[i];
            aw.answerIdentifier = i;
            aw.isCorrect = false;
            answers.Add(aw);
        }

        //set correct answers to true, marked in the the array correct answers in data
        for (int i = 0; i < questionItemData.correctAnswers.Length; i++)
        {
            answers[(int)questionItemData.correctAnswers[i]].isCorrect = true;
        }
    }

    public string GetQuestionText()
    {
        if(questionItemData == null)
        {
            Debug.Log("data is null");
        }
        else
        {
            Debug.Log("question Data " + questionItemData.name);
            Debug.Log("question Data " + questionItemData.question);
        }
        return questionItemData.question;
    }

    public void PrintCorrectAnswers()
    {
        for(int i = 0; i < answers.Count; i++)
        {
            if (answers[i].isCorrect)
            {
                answers[i].PrintItemData();
            }
        }
    }

    public void PrintQuestionIdenifier()
    {
        Debug.Log(questionItemData.name.Trim().ToLower());
    }


}
