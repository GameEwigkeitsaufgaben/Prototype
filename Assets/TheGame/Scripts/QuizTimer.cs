using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimer : MonoBehaviour
{
    public Image uiTimer;
    public Text uiTimerText;
    
    public float timeToCompleteQuestion = 30f;
    public float timeToShowCorrectAnswer = 10f;

    public bool loadNextQuestion;
    public float fillFraction;
    float timerValue;
    public bool isAnsweringQuestion = false;
    float maxTime;
    bool timeRunout;

    private void Start()
    {
        timerValue = timeToCompleteQuestion;
        maxTime = timeToCompleteQuestion;
        timeRunout = false;
    }

    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        
        if (isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
                uiTimer.fillAmount = fillFraction;
            }
            else
            {
                isAnsweringQuestion = false;
                timeRunout = true;
            }
        }
    }

    public int GetCompletionTime()
    {
        return Mathf.FloorToInt(fillFraction*100);
    }
    void Update()
    {
        UpdateTimer();
    }

    public void StopTimer()
    {
        isAnsweringQuestion = false;
    }

    public void StartTimer()
    {
        isAnsweringQuestion = true;
        timeToCompleteQuestion = maxTime;
        timerValue = timeToCompleteQuestion;
        timeRunout = false;
    } 

    public bool IsTimeRunOut()
    {
        //Gamer did not answered the question in time
        return timeRunout;
    }

    public void ResetTimeRunOut()
    {
        timeRunout = false;
    }
}
