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

    QuizRightAnser rightAnswer;

    // Start is called before the first frame update
    void Start()
    {
        questionText.text = quizData.question;
        answerA.text = quizData.answerA;
        answerB.text = quizData.answerB;
        answerC.text = quizData.answerC;
        answerD.text = quizData.answerD;
        rightAnswer = quizData.rightAnswer;
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

        Button btnOK = ReturnAnswerButton(quizData.rightAnswer);
        btnOK.gameObject.SetActive(true);
        btnOK.interactable = false;
        cb = btnOK.colors;
        cb.disabledColor = Color.green;
        btnOK.colors = cb;
        btnNext.gameObject.SetActive(true);
    }

    void SetUITimeOut()
    {
        answerA.transform.parent.gameObject.SetActive(false);
        answerB.transform.parent.gameObject.SetActive(false);
        answerC.transform.parent.gameObject.SetActive(false);
        answerD.transform.parent.gameObject.SetActive(false);
        SetUIChoiceOK(ReturnAnswerButton(quizData.rightAnswer));
        btnNext.gameObject.SetActive(true);
    }
       

    public void CheckAnswer(Button btn)
    {
        QuizRightAnser answerToTest = ReturnTypeQuizRightAnswer(btn);

        answerA.transform.parent.gameObject.SetActive(false);
        answerB.transform.parent.gameObject.SetActive(false);
        answerC.transform.parent.gameObject.SetActive(false);
        answerD.transform.parent.gameObject.SetActive(false);

        if (answerToTest == quizData.rightAnswer)
        {
            SetUIChoiceOK(btn);
        }
        else
        {
            SetUIChoiceWrong(btn);
        }
    }

    private QuizRightAnser ReturnTypeQuizRightAnswer(Button btn)
    {
        if (btn.name.EndsWith("A"))
        {
            return QuizRightAnser.A;
        }
        else if (btn.name.EndsWith("B"))
        {
            return QuizRightAnser.B;
        }
        else if (btn.name.EndsWith("C"))
        {
            return QuizRightAnser.C;
        }
        else if (btn.name.EndsWith("D"))
        {
            return QuizRightAnser.C;
        }
        else
        {
            return QuizRightAnser.NONE;
        }
        
    }

    private Button ReturnAnswerButton(QuizRightAnser aw)
    {
        switch (aw)
        {
            case QuizRightAnser.A: 
                return answerA.transform.parent.gameObject.GetComponent<Button>();
            case QuizRightAnser.B:
                return answerB.transform.parent.gameObject.GetComponent<Button>();
            case QuizRightAnser.C:
                return answerC.transform.parent.gameObject.GetComponent<Button>();
            case QuizRightAnser.D:
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
