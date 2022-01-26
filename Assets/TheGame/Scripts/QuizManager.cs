using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Canvas endCanvas, quizCanvas;
    public VerticalLayoutGroup answerButtonGroup;
    public Text uiQuestion;
    public Text uiPoints;

    public QuizData[] quizDataItems;
    public List<QuizQuestionItem> questionItemList = new List<QuizQuestionItem>();
    public List<QuizQuestionItem> questionItemListshuffled;

    int currentProgressIndex = 0;
    int points = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(QuizData i in quizDataItems)
        {
            questionItemList.Add(new QuizQuestionItem(i, answerButtonGroup));
        }

        questionItemListshuffled = Shuffle(questionItemList);

        if (questionItemListshuffled != null)
        {
            SetupQuestion(currentProgressIndex);
        }
    }

    public void LoadNextQuestion()
    {
        if (questionItemListshuffled[currentProgressIndex].unProved)
        {
           
            foreach(QuizAnswerItem a in questionItemListshuffled[currentProgressIndex].answers)
            {
                a.ShowResult();
                if(a.isCorrect && a.buttonSelected)
                {
                    //1x mit Zeit multipilzieren sind zeitpunkte;
                    points++;
                    uiPoints.text = points.ToString();

                }
            }
            
            questionItemListshuffled[currentProgressIndex].unProved = false;
            return;   
        }

        foreach (Transform child in answerButtonGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        currentProgressIndex++;
        if (currentProgressIndex < questionItemListshuffled.Count)
        {
            SetupQuestion(currentProgressIndex);
            //Debug.Log(points);
        }
        else
        {
            endCanvas.gameObject.SetActive(true);
            quizCanvas.gameObject.SetActive(false);
        }    
        
    }

    void SetupQuestion(int progressIndex)
    {
            uiQuestion.text = questionItemListshuffled[progressIndex].GetQuestionText();
            ActivateAnswerButtons(progressIndex);
    }

    void ActivateAnswerButtons(int progressIndex)
    {
        foreach(var a in questionItemList[progressIndex].answers)
        {
            a.btn.gameObject.SetActive(true);
        }
    }

    public List<QuizQuestionItem> Shuffle(List<QuizQuestionItem> list)
    {
        List<QuizQuestionItem> tmpList = list;

        for(var i = tmpList.Count-1; i > 0; i--)
        {
            var rndIndex = Random.Range(0, i);
            Debug.Log("rndIndex: " + rndIndex);
            tmpList = Swap(tmpList, i, rndIndex);
        }

        return tmpList;
    }

    //https://stackoverflow.com/questions/273313/randomize-a-listt
    //https://answers.unity.com/questions/486626/how-can-i-shuffle-alist.html
    public List<QuizQuestionItem> Swap(List<QuizQuestionItem> list, int indexA, int indexB)
    {
        foreach (var i in list)
        {
            i.PrintQuestionIdenifier();
        }
        
        QuizQuestionItem temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;

        foreach (var i in list)
        {
            i.PrintQuestionIdenifier();
        }

        return list;
    }

    private void PrintList(List<QuizQuestionItem> tempList)
    {
        foreach(QuizQuestionItem a in tempList)
        {
            a.PrintQuestionIdenifier();
        }
    }
}
