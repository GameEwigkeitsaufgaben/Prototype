using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum MinerFeedback
{
    Idle,
    Correct,
    Incorrect
}

public class QuizManager : MonoBehaviour
{
    private const string btnTextCheckAnswers = "Prüfen";
    private const string btnTextNextAnswer = "Weiter";

    private const string generalKeyOverlay = "Overlay1110";


    public Canvas quizCanvas;
    public VerticalLayoutGroup answerButtonGroup;
    public TMP_Text uiQuestion;
    public Text uiPoints;
    public Text uiQuestType;
    public Text uiSimpleProgressView;
    public Slider uiProgressBar;

    public Image uiPostImage;
    public Image buzzerTop;
    public Image imgMinerFeedback;
    public Button uiButtonNext;
    public TMP_Text textMinerFeedback;
    public AudioSource audioAnswerCorrect, audioWrongAudio;
    public bool randomizeQuestions = false;
    public SwitchSceneManager switchScene;

    public QuizData[] quizDataItems;
    public List<QuizQuestionItem> questionItemList = new List<QuizQuestionItem>();
    public List<QuizQuestionItem> questionItemListshuffled;

    private int currentProgressIndex = 0;
    private int pointsSum = 0;
    private int pointsPerQuestion = 0;
    
    private QuizTimer quizTimer;

    [Header("Assigned at runtime")]
    [SerializeField] private SoSfx sfx;
    [SerializeField] private Runtime runtimeData;
    SoChapOneRuntimeData runtimeDataCh01;
    [SerializeField] private SoQuizConfig quizConfig;


    private void Awake()
    {
        runtimeDataCh01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);

        if (SceneManager.GetActiveScene().name == GameScenes.ch01Quiz)
        { 
            runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02Quiz)
        {
            runtimeData = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        }
        
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        quizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
    }

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

        questionItemListshuffled = (randomizeQuestions) ? Shuffle(questionItemList) : questionItemList;
        
        if (questionItemListshuffled != null)
        {
            SetupQuestion(currentProgressIndex);
        }

        uiSimpleProgressView.text = "FRAGE 1 von " + questionItemListshuffled.Count;
        uiProgressBar.maxValue = questionItemListshuffled.Count;
        uiProgressBar.value = 1;

        uiPoints.text = pointsSum.ToString();
        quizTimer.StartTimer();
    }

    public void LoadNextQuestion()
    {
        quizTimer.StopTimer();

        if (questionItemListshuffled[currentProgressIndex].unProved)
        {
            int tmpPoints = 1;

            foreach (QuizAnswerItem a in questionItemListshuffled[currentProgressIndex].answerList)
            {
                a.ShowResult();
                tmpPoints *= a.GetPointForAnswer();
            }

            pointsPerQuestion = quizTimer.GetCompletionTime() * tmpPoints;
            pointsSum += pointsPerQuestion;
            
            if (tmpPoints != 0)
            {
                uiPoints.text = pointsSum.ToString();
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
            buzzerTop.tag = "Untagged";
            uiButtonNext.GetComponent<Button>().colors = GameColors.GetInteractionColorBlock();
            uiButtonNext.GetComponentInChildren<TMP_Text>().text = btnTextNextAnswer;

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
            uiSimpleProgressView.text = "FRAGE " + (currentProgressIndex + 1) + " von " + questionItemListshuffled.Count;
        }
        else
        {
            runtimeDataCh01.quizPointsCh01 = pointsSum.ToString();
            runtimeDataCh01.quiz119Done = true;
            if (SceneManager.GetActiveScene().name == GameData.NameRuntimeDataChap01)
            {
                runtimeDataCh01.quiz119Done = true;
            }
            switchScene.SwitchToChapter1withOverlay(generalKeyOverlay);
        }    
    }

    void SetupQuestion(int progressIndex)
    {
        runtimeData.quizMinerFeedback = MinerFeedback.Idle;
        runtimeData.singleSelectAwIdOld = null;
        uiQuestion.text = questionItemListshuffled[progressIndex].GetQuestionText();
        uiPostImage.sprite = questionItemListshuffled[progressIndex].GetPostImage();
        buzzerTop.tag = "Buzzer"; //braucht man evt. nicht
        uiButtonNext.GetComponent<Button>().colors = GameColors.GetBuzzerColorBlockProve();
        uiButtonNext.GetComponentInChildren<TMP_Text>().text = btnTextCheckAnswers;

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
        foreach(var a in questionItemList[progressIndex].answerList)
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
        QuizQuestionItem temp = list[indexA];
        list[indexA] = list[indexB];
        list[indexB] = temp;

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
                textMinerFeedback.text = "+ " +pointsPerQuestion+ " Punkte\nSuper gemacht!";
                textMinerFeedback.transform.parent.gameObject.SetActive(true);
                break;
            case MinerFeedback.Incorrect:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackIncorrect;
                textMinerFeedback.text = "+ " + pointsPerQuestion + " Punkte\nOjee, leider falsch!";
                textMinerFeedback.transform.parent.gameObject.SetActive(true);
                break;
        }
    }
}
