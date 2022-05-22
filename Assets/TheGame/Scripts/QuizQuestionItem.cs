using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizQuestionItem
{
    private const string eineAntwortRichtig = "#eineRichtig";
    private const string mehrereAntwortenRichtig = "#mehrereRichtig";
    private QuizData questionItemData;
    private QuizQuestionType questionType;


    public List<QuizAnswerItem> answerList = new List<QuizAnswerItem>();
    public bool unProved = true;

    public QuizQuestionItem(QuizData cdata, VerticalLayoutGroup parent)
    {
        questionItemData = cdata;

        for (int i = 0; i < questionItemData.answers.Length; i++)
        {
            QuizAnswerItem aw = new QuizAnswerItem();
            aw.answer = questionItemData.answers[i];
            aw.timeToAnswerInSec = questionItemData.timeToAnswerInSec;

            aw.CreateButton(parent, i);
            aw.btn.GetComponent<QuizAnswerUiBehaviour>().questID = questionItemData.name.Trim().ToLower();
            aw.btn.GetComponent<QuizAnswerUiBehaviour>().awId = i;
            aw.btn.GetComponent<QuizAnswerUiBehaviour>().questType = questionItemData.questionType;
            aw.btn.GetComponent<QuizAnswerUiBehaviour>().uiType = MouseInteraction.BtnQuizAnswer;
            
            answerList.Add(aw);
        }

        //set correct answers to true, marked in the the array correct answers in data
        for (int i = 0; i < questionItemData.correctAnswers.Length; i++)
        {
            answerList[(int)questionItemData.correctAnswers[i]].btn.GetComponent<QuizAnswerUiBehaviour>().isCorrect = true;
        }
    }

    public string GetQuestionTypeString()
    {
        string x = "";

        if (questionItemData.questionType == QuizQuestionType.OneTrue || questionItemData.questionType == QuizQuestionType.TrueFalse)
        {
            x = eineAntwortRichtig;
        }
        else if(questionItemData.questionType == QuizQuestionType.AtLeastOneTrue)
        {
            x = mehrereAntwortenRichtig;
        }
        
        return x;
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
        for(int i = 0; i < answerList.Count; i++)
        {
            if (answerList[i].isCorrect)
            {
                answerList[i].PrintItemData();
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
