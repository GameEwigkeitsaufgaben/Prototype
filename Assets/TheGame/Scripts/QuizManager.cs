using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MinerFeedback
{
    Idle,
    Correct,
    Incorrect
}

public class QuizManager : MonoBehaviour
{
    private const string generalKeyOverlay = "Overlay1110";
    private const string btnTextCheckAnswers = "Prüfen";
    private const string btnTextNextAnswer = "Weiter";

    public Canvas endCanvas, quizCanvas;
    public VerticalLayoutGroup answerButtonGroup;
    //public Text uiQuestion;
    public TextMeshProUGUI uiQuestion;
    public Text uiPoints;
    public Text uiQuestType;
    public Slider uiProgressBar;
    public Text uiSimpleProgressView;
    public Image uiPostImage;
    public Button uiButtonNext;
    public Image imgMinerFeedback;
    public Text textMinerFeedback;
    public AudioSource audioAnswerCorrect, audioWrongAudio;
    public bool randomizeQuestions = false;
    public SwitchSceneManager switchScene;

    public QuizData[] quizDataItems;
    public List<QuizQuestionItem> questionItemList = new List<QuizQuestionItem>();
    public List<QuizQuestionItem> questionItemListshuffled;

    private int currentProgressIndex = 0;
    private int points = 0;
    
    private QuizTimer quizTimer;

    [Header("Assigned at runtime")]
    [SerializeField] private SoSfx sfx;
    [SerializeField] private SoChapOneRuntimeData runtimeData;
    [SerializeField] private SoQuizConfig quizConfig;


    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        quizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
    }

    // Start is called before the first frame update
    void Start()
    {
        quizTimer = FindObjectOfType<QuizTimer>();
        audioAnswerCorrect.clip = sfx.correctAnswer;
        audioWrongAudio.clip = sfx.incorrectAnswer;
        uiButtonNext.GetComponent<AudioSource>().clip = sfx.btnClick;

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

        uiSimpleProgressView.text = "FRAGE 1 von " + questionItemListshuffled.Count;
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
                runtimeData.quizMinerFeedback = MinerFeedback.Correct;
            }
            else
            {
                audioWrongAudio.Play();
                runtimeData.quizMinerFeedback = MinerFeedback.Incorrect;
            }

            foreach (Transform child in answerButtonGroup.transform)
            {
                child.gameObject.GetComponent<Button>().interactable = false;
            }

            questionItemListshuffled[currentProgressIndex].unProved = false;
            uiButtonNext.GetComponentInChildren<Text>().text = btnTextNextAnswer;
            return;   
        }

        foreach (Transform child in answerButtonGroup.transform)
        {
            child.gameObject.SetActive(false);
        }

        currentProgressIndex++;
        uiProgressBar.value++;

        uiSimpleProgressView.text = "FRAGE " + (currentProgressIndex+1) + " von " + questionItemListshuffled.Count;

        if (currentProgressIndex < questionItemListshuffled.Count)
        {
            SetupQuestion(currentProgressIndex);
        }
        else
        {
            GameData.quizChapterOnePoints = points;
            runtimeData.quiz119Done = true;
            switchScene.SwitchToChapter1withOverlay(generalKeyOverlay);
        }    
    }

    void SetupQuestion(int progressIndex)
    {
        runtimeData.quizMinerFeedback = MinerFeedback.Idle;
        uiQuestion.text = questionItemListshuffled[progressIndex].GetQuestionText();
        uiPostImage.sprite = questionItemListshuffled[progressIndex].GetPostImage();
        uiButtonNext.GetComponentInChildren<Text>().text = btnTextCheckAnswers;
        uiQuestType.text = questionItemListshuffled[progressIndex].GetQuestionTypeString();

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

        switch (runtimeData.quizMinerFeedback)
        {
            case MinerFeedback.Idle:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackIdle;
                textMinerFeedback.transform.parent.gameObject.SetActive(false);
                break;
            case MinerFeedback.Correct:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackCorrect;
                textMinerFeedback.text = "Richtig, \n echt super!";
                textMinerFeedback.transform.parent.gameObject.SetActive(true);
                break;
            case MinerFeedback.Incorrect:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackIncorrect;
                textMinerFeedback.text = "Oh nein, \n richtig wäre ... ";
                textMinerFeedback.transform.parent.gameObject.SetActive(true);
                break;
        }


    }
}
