using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum QuestionType
{
    eineRichtig,
    mehrereRichtig
}

public class QuizQuestionWIP : MonoBehaviour
{
    public QuizData data;
    public List<Answer> answers = new List<Answer>();

    public ManagerQuizAllChap manager;

    public void GenerateAnswers()
    {
        int index = 0;
        foreach (string s in data.answers)
        {
            var newButton = TMP_DefaultControls.CreateButton(new TMP_DefaultControls.Resources());
            newButton.AddComponent<Answer>();

            Answer a = newButton.GetComponent<Answer>();
            a.name = "answer" + index;
            index++;
            a.SetOrigParent(this.transform);
            a.answer = s;
            a.gameObject.SetActive(false);

            answers.Add(a);
        }

        foreach (QuizAnswer q in data.correctAnswers)
        {
            switch (q)
            {
                case QuizAnswer.AnswersElement0:
                    answers[0].isCorrect = true;
                    break;
                case QuizAnswer.AnswersElement1:
                    answers[1].isCorrect = true;
                    break;
                case QuizAnswer.AnswersElement2:
                    answers[2].isCorrect = true;
                    break;
                case QuizAnswer.AnswersElement3:
                    answers[3].isCorrect = true;
                    break;
                case QuizAnswer.NONE:
                    Debug.Log("None answer is true");
                    break;

            }
        }
    }

    public void SetupQueststion(QuizData quizData, ManagerQuizAllChap manager)
    {
        data = quizData;
        this.manager = manager;

        GenerateAnswers();
    }

    public void ShowData()
    {
        manager.uiQuestion.text = data.question;
        manager.uiQuestType.text = "#" + data.qType.ToString();
        manager.uiPostImage.sprite = data.postImage;

        ShowAnswers();
    }

    public void ShowAnswers()
    {
        foreach(Answer a in answers)
        {
            a.Show();
        }
    }
}
