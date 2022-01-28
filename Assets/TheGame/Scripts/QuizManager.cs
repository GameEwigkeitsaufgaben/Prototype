using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public Canvas endCanvas, quizCanvas;
    public VerticalLayoutGroup answerButtonGroup;
    public Text uiQuestion;
    public Text uiPoints;
    public Slider uiProgressBar;
    public Image uiPostImage;
    public Button uiButtonNext;
    public AudioSource audioAnswerCorrect, audioWrongAudio;
    public bool randomizeQuestions = false;


    public QuizData[] quizDataItems;
    public List<QuizQuestionItem> questionItemList = new List<QuizQuestionItem>();
    public List<QuizQuestionItem> questionItemListshuffled;

    int currentProgressIndex = 0;
    int points = 0;
    
    QuizTimer quizTimer;

    // Start is called before the first frame update
    void Start()
    {
        quizTimer = FindObjectOfType<QuizTimer>();

        foreach(QuizData i in quizDataItems)
        {
            questionItemList.Add(new QuizQuestionItem(i, answerButtonGroup));
        }

        if (randomizeQuestions)
        {
            questionItemListshuffled = Shuffle(questionItemList);
        }
        else
        {
            questionItemListshuffled = questionItemList;
        }
        
        if (questionItemListshuffled != null)
        {
            SetupQuestion(currentProgressIndex);
        }

        uiProgressBar.maxValue = questionItemListshuffled.Count;
        uiProgressBar.value = 1;

        uiPoints.text = points.ToString();
        quizTimer.StartTimer();
    }

    public void LoadNextQuestion()
    {
        quizTimer.StopTimer();

        
        if (questionItemListshuffled[currentProgressIndex].unProved)
        {
            int tmpPoints = 1;

            foreach (QuizAnswerItem a in questionItemListshuffled[currentProgressIndex].answers)
            {
                a.ShowResult();
                tmpPoints *= a.GetPointForAnswer();
            }

            points += quizTimer.GetCompletionTime() * tmpPoints;
            
            if (tmpPoints != 0)
            {
                uiPoints.text = points.ToString();
                audioAnswerCorrect.Play();
            }
            else
            {
                audioWrongAudio.Play();
            }

            foreach (Transform child in answerButtonGroup.transform)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }

            questionItemListshuffled[currentProgressIndex].unProved = false;
            uiButtonNext.GetComponentInChildren<Text>().text = "Weiter";
            return;   
        }

        foreach (Transform child in answerButtonGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        currentProgressIndex++;
        uiProgressBar.value++;

        if (currentProgressIndex < questionItemListshuffled.Count)
        {
            SetupQuestion(currentProgressIndex);
        }
        else
        {
            endCanvas.gameObject.SetActive(true);
            quizCanvas.gameObject.SetActive(false);
            GameData.quizChapterOnePoints = points;
        }    
        
    }

    void SetupQuestion(int progressIndex)
    {
        uiQuestion.text = questionItemListshuffled[progressIndex].GetQuestionText();
        uiPostImage.sprite = questionItemListshuffled[progressIndex].GetPostImage();
        uiButtonNext.GetComponentInChildren<Text>().text = "Prüfen";


        foreach (Transform child in answerButtonGroup.transform)
        {
            child.gameObject.GetComponent<Button>().interactable = true;
        }

        ActivateAnswerButtons(progressIndex);
        quizTimer.StartTimer(questionItemListshuffled[progressIndex].GetTimeToAnswerQuestion());
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

    private void Update()
    {
        if (quizTimer.IsTimeRunOut())
        {
            quizTimer.ResetTimeRunOut();
            LoadNextQuestion();
        }
    }
}
