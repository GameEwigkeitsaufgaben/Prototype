using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizQuestionManager : MonoBehaviour
{
    public QuizData quizData;
    public Text questionText;
    public Text answerA;
    public Text answerB;
    public Text answerC;
    public Text answerD;
    public Text timerText;
    public Button btnNext;
    public float maxTimeInSec = 30.0f;


    private float elapsed;
    bool timeOutSet = false;

    QuizAnswer[] rightAnswer;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = quizData.question;
        //answerA.text = quizData.answerA;
        //answerB.text = quizData.answerB;
        //answerC.text = quizData.answerC;
        //answerD.text = quizData.answerD;
        //rightAnswer = quizData.rightAnswer;
        elapsed = maxTimeInSec;
    }

    void SetUIChoiceOK(Button btn)
    {
        btn.gameObject.SetActive(true);
        ColorBlock cb = btn.colors;
        cb.disabledColor = Color.green;
        btn.colors = cb;
        btn.interactable = false;
        btnNext.gameObject.SetActive(true);
    }

    void SetUIChoiceWrong(Button btn)
    {
        btn.gameObject.SetActive(true);
        ColorBlock cb = btn.colors;
        cb.disabledColor = Color.red;
        btn.interactable = false;
        btn.colors = cb;

        //Button btnOK = ReturnAnswerButton(quizData.rightAnswer);
        //btnOK.gameObject.SetActive(true);
        //btnOK.interactable = false;
        //cb = btnOK.colors;
        //cb.disabledColor = Color.green;
        //btnOK.colors = cb;
        //btnNext.gameObject.SetActive(true);
    }

    void SetUITimeOut()
    {
        answerA.transform.parent.gameObject.SetActive(false);
        answerB.transform.parent.gameObject.SetActive(false);
        answerC.transform.parent.gameObject.SetActive(false);
        answerD.transform.parent.gameObject.SetActive(false);
        //SetUIChoiceOK(ReturnAnswerButton(quizData.rightAnswer));
        //btnNext.gameObject.SetActive(true);
    }
       

    public void CheckAnswer(Button btn)
    {
        QuizAnswer answerToTest = ReturnTypeQuizRightAnswer(btn);

        answerA.transform.parent.gameObject.SetActive(false);
        answerB.transform.parent.gameObject.SetActive(false);
        answerC.transform.parent.gameObject.SetActive(false);
        answerD.transform.parent.gameObject.SetActive(false);
    }

    private QuizAnswer ReturnTypeQuizRightAnswer(Button btn)
    {
        if (btn.name.EndsWith("A"))
        {
            return QuizAnswer.AnswersElement0;
        }
        else if (btn.name.EndsWith("B"))
        {
            return QuizAnswer.AnswersElement1;
        }
        else if (btn.name.EndsWith("C"))
        {
            return QuizAnswer.AnswersElement2;
        }
        else if (btn.name.EndsWith("D"))
        {
            return QuizAnswer.AnswersElement2;
        }
        else
        {
            return QuizAnswer.NONE;
        }
        
    }

    private Button[] ReturnRightAnswers(QuizAnswer[] rightAnswers)
    {
        Button[] myRightAnswers = new Button[rightAnswers.Length];
        for (int i = 0; i < rightAnswers.Length; i++)
        {
            myRightAnswers[i] = ReturnAnswerButton(rightAnswers[i]);
        }
        return myRightAnswers;
    }

    private Button ReturnAnswerButton(QuizAnswer aw)
    {
        switch (aw)
        {
            case QuizAnswer.AnswersElement0:
                return answerA.transform.parent.gameObject.GetComponent<Button>();
            case QuizAnswer.AnswersElement1:
                return answerB.transform.parent.gameObject.GetComponent<Button>();
            case QuizAnswer.AnswersElement2:
                return answerC.transform.parent.gameObject.GetComponent<Button>();
            case QuizAnswer.AnswersElement3:
                return answerD.transform.parent.gameObject.GetComponent<Button>();
            default:
                return null;
        }
        
    }

    string FormatTime(float theTime)
    {
        string formatedTime =
            string.Format("{0:00}:{1:00}",
                            Mathf.Floor(theTime / 60),
                            Mathf.Floor(theTime % 60));

        return formatedTime;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed -= Time.deltaTime;
        
        if (elapsed >= 0)
        {
            timerText.text = FormatTime(elapsed);
            return;
        }
        
        if (timeOutSet) return;
     
        SetUITimeOut();
        
        timeOutSet = true;
    }
}
