using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public VerticalLayoutGroup answerButtionGroup;
    public Text uiQuestion;

    public QuizData[] quizDataItems;
    public List<QuizQuestionItem> questionItemList = new List<QuizQuestionItem>();
    public List<QuizQuestionItem> questionItemListshuffled;

    int currentProgressIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(QuizData i in quizDataItems)
        {
            questionItemList.Add(new QuizQuestionItem(i));
        }

        questionItemListshuffled = Shuffle(questionItemList);
        //Debug.Log("---------shuffled");
        //PrintList(questionItemListshuffled);
        if (questionItemListshuffled != null)
        {
            SetupQuestion(currentProgressIndex);
        }
    }

    public void LoadNextQuestion()
    {
        foreach (Transform child in answerButtionGroup.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        currentProgressIndex++;
        SetupQuestion(currentProgressIndex);
    }

    void SetupQuestion(int progressIndex)
    {
        uiQuestion.text = questionItemListshuffled[progressIndex].GetQuestionText();
        CreateAnswerButton(progressIndex);
    }


    void CreateAnswerButton(int progressIndex)
    {
        foreach(var a in questionItemList[progressIndex].answers)
        {
            var newButton = DefaultControls.CreateButton(new DefaultControls.Resources());
            newButton.transform.SetParent(answerButtionGroup.transform);
            newButton.transform.localScale = Vector3.one;
            newButton.transform.localPosition = new Vector3(newButton.transform.localPosition.x,
                                                                newButton.transform.localPosition.y,
                                                                0);
            newButton.GetComponent<Image>().sprite = Resources.Load<Sprite>("neon_square_orange");
            newButton.GetComponentInChildren<Text>().text = a.answer;
            newButton.GetComponentInChildren<Text>().fontSize = 20;
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
