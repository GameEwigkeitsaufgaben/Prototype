using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizTimer : MonoBehaviour
{
    public Image uiTimer;
    public Text uiTimerText;
    
    //public float timeToCompleteQuestion2;

    public float timerTime = 5f;

    //public float timeToShowCorrectAnswer;

    public bool loadNextQuestion;
    public float fillFraction;
    public bool isAnsweringQuestion = false;
    float maxTime;
    private bool timeOut;


    [SerializeField]float timerValue;

    private void Start()
    {
        //timerValue = timeToCompleteQuestion2;
        timerValue = timerTime;
        timeOut = false;
    }

    void UpdateTimer()
    {
        if (!isAnsweringQuestion) return;

        timerValue -= Time.deltaTime;
        
        if (isAnsweringQuestion)
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timerTime;
                uiTimer.fillAmount = fillFraction;
            }
            else
            {
                isAnsweringQuestion = false;
                timeOut = true;
            }
        }
    }

    public int GetCompletionTime()
    {
        return Mathf.FloorToInt(fillFraction*100);
    }

    public void StopTimer()
    {
        isAnsweringQuestion = false;
    }

    public void StartTimer()
    {
        timerValue = timerTime;
        isAnsweringQuestion = true;
        timeOut = false;
    }

    public void StartTimer(int time)
    {
        isAnsweringQuestion = true;
        timeOut = false;
    }

    public bool IsTimeRunOut()
    {
        //Gamer did not answered the question in time
        return timeOut;
    }

    public void ResetTimeRunOut()
    {
        timeOut = false;
    }
    
    private void Update()
    {
        UpdateTimer();
    }
}
