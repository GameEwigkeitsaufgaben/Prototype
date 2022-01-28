using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizQuestionItem
{
    private QuizData questionItemData;
    private QuizQuestionType questionType;
    //private VerticalLayoutGroup buttonGroup;

    public List<QuizAnswerItem> answers = new List<QuizAnswerItem>();
    public bool unProved = true;

    public QuizQuestionItem(QuizData cdata, VerticalLayoutGroup parent)
    {
        questionItemData = cdata;
        //buttonGroup = parent;

        for (int i = 0; i < questionItemData.answers.Length; i++)
        {
            QuizAnswerItem aw = new QuizAnswerItem();
            aw.questionIdentifier = questionItemData.name.Trim().ToLower();
            aw.answer = questionItemData.answers[i];
            aw.answerIdentifier = i;
            aw.isCorrect = false;
            aw.CreateButton(parent);
            aw.timeToAnswerInSec = questionItemData.timeToAnswerInSec;
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
        return questionItemData.question;
    }

    public int GetTimeToAnswerQuestion()
    {
        return questionItemData.timeToAnswerInSec;
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

    public string GetQuestionIdenifier()
    {
        return questionItemData.name.Trim().ToLower();
    }

    public Sprite GetPostImage()
    {
        return questionItemData.postImage;
    }
}
