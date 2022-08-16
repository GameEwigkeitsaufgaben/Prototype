using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerQuizAllChap : MonoBehaviour
{
    private const string weiter = "Weiter";
    private const string pruefen = "Pr�fen";

    [Header("Assign GameObjects")]
    [SerializeField] private QuizData[] questionsData;
    [SerializeField] private GameObject parentQuestions;

    [Header("Assign UI Elements")]
    public TMP_Text uiQuestion;
    public Text uiQuestType;
    public Text scoreOverall;
    public TMP_Text scorePerAnswer;
    public Image uiPostImage;
    public VerticalLayoutGroup verticalLayoutGroup;
    public Button btnProcessAnswer;
    public Image imgMinerFeedback;
    public TMP_Text textMinerPointsFeedback;
    public TMP_Text textMinerWordsFeedback;
    public Text progressFeedback;

    [Header("Assigned at runtime")]
    [SerializeField] private List<QuizQuestionWIP> questions;

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoQuizConfig quizConfig;
    private SwitchSceneManager switchScene;
    private QuizTimer quizTimer;
    private SoSfx sfx;

    private int currentQuestionIndex;
    private int activeScene; //1 chap1, 2 chap2, 3 chap3;

    private void Awake()
    {
        switchScene = GetComponent<SwitchSceneManager>();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);

        activeScene = switchScene.GetActiveQuizScene();
        
        switch (activeScene)
        {
            case 1: 
                runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
                break;
            case 2:
                runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
                break;
            case 3:
                runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
                break;
        }

        quizConfig = runtimeDataChapters.LoadConfigQuiz();
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
    }

    void Start()
    {
        //Generate all Questions based on Quizdata
        questions = GenerateQuestionList();

        //Init and Start Timer
        quizTimer = FindObjectOfType<QuizTimer>();
        quizTimer.StartTimer();

        //progress
        currentQuestionIndex = 0;
        questions[currentQuestionIndex].ShowData();
        UpdateProgressUI();
        SetMinerFeedback(MinerFeedback.Idle, 0f);
    }

    public void UpdateProgressUI()
    {
        progressFeedback.text = "FRAGE " + (currentQuestionIndex + 1) + " von " + questions.Count;
    }

    public void SetMinerFeedback(MinerFeedback fb,  float pointsPerQuestion)
    {
        AudioSource audioSrc = imgMinerFeedback.GetComponent<AudioSource>();
        switch (fb)
        {
            case MinerFeedback.Idle:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackIdle;
                textMinerPointsFeedback.transform.parent.gameObject.SetActive(false);
                break;
            case MinerFeedback.Correct:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackCorrect;
                audioSrc.PlayOneShot(sfx.correctAnswer);
                textMinerPointsFeedback.text = "+ " + pointsPerQuestion + " Punkte";
                textMinerWordsFeedback.text = "Super gemacht!";
                textMinerPointsFeedback.transform.parent.gameObject.SetActive(true);
                break;
            case MinerFeedback.Incorrect:
                imgMinerFeedback.sprite = quizConfig.minerFeedbackIncorrect;
                audioSrc.PlayOneShot(sfx.incorrectAnswer);
                textMinerPointsFeedback.text = "+ " + pointsPerQuestion + " Punkte";
                textMinerWordsFeedback.text = "Ojee, leider falsch!";
                textMinerPointsFeedback.transform.parent.gameObject.SetActive(true);
                break;
        }
    }

    public void UpdateSelect(GameObject obj)
    {
        switch (questions[currentQuestionIndex].data.qType)
        {
            case QuestionType.eineRichtig:
                foreach (Answer a in questions[currentQuestionIndex].answers)
                {
                    if (obj.name != a.name)
                    {
                        a.SetUnselected();
                        a.ShowUnprovenUnselect();
                    }
                    else
                    {
                        a.SetSelected();
                        a.ShowUnprovenSelect();
                    }
                }
                break;
            case QuestionType.mehrereRichtig:

                Answer b = obj.GetComponent<Answer>();
                b.mouseDown = !b.mouseDown;

                if (b.mouseDown) return;

                if (b.isSelected)
                {
                    b.SetUnselected();
                    return;
                }

                b.SetSelected();
                break;
        }
    }

    private List<QuizQuestionWIP> GenerateQuestionList()
    {
        List<QuizQuestionWIP> l = new List<QuizQuestionWIP>();

        foreach(QuizData q in questionsData)
        {
            GameObject obj = new GameObject();
            obj.name = "quest" + q.name;
            obj.AddComponent<QuizQuestionWIP>();
            obj.transform.SetParent(parentQuestions.transform);
            obj.GetComponent<QuizQuestionWIP>().SetupQueststion(q, this);
            l.Add(obj.GetComponent<QuizQuestionWIP>());
        }

        return l;
    }

    public void ProcessAnswer()
    {
        TMP_Text btnNextIdentifier = btnProcessAnswer.GetComponentInChildren<TMP_Text>();

        //Case Weiter;
        if (btnNextIdentifier.text == weiter)
        {
            ShowNextQuestion();
            quizTimer.StartTimer();
            SetMinerFeedback(MinerFeedback.Idle, 0f);
            btnNextIdentifier.text = pruefen;
            return;
        }
        
        //Case Pr�fen;
        quizTimer.StopTimer();
        float completionTime = quizTimer.GetCompletionTime();

        float pointsPerAnswer = 0;
        int correctlySelected = 0;
        switch (questions[currentQuestionIndex].data.qType)
        {
            case QuestionType.eineRichtig:
                Answer[] aws = verticalLayoutGroup.GetComponentsInChildren<Answer>();

                foreach (Answer a in aws)
                {
                    if (a.isCorrect && a.isSelected)
                    {
                        correctlySelected++;
                    }

                    a.ShowResult();
                }

                //Calculate and show points
                pointsPerAnswer = correctlySelected * completionTime;

                break;
            case QuestionType.mehrereRichtig:

                Answer[] aws2 = verticalLayoutGroup.GetComponentsInChildren<Answer>();

                foreach (Answer a in aws2)
                {
                    if (a.isCorrect && a.isSelected)
                    {
                        correctlySelected++;
                    }

                    a.ShowResult();
                }

                //calculate and show points - only right selected answers are counted
                float factor = (float)correctlySelected / questions[currentQuestionIndex].data.answers.Length;
                pointsPerAnswer = Mathf.Floor(factor * completionTime);
                break;
        }

        if (pointsPerAnswer == 0)
        {
            SetMinerFeedback(MinerFeedback.Incorrect, pointsPerAnswer);
        }
        else
        {
            SetMinerFeedback(MinerFeedback.Correct, pointsPerAnswer);
        }

        switch (activeScene)
        {
            case 1:
                runtimeDataCh1.quizPointsOverall += pointsPerAnswer;
                scoreOverall.text = runtimeDataCh1.quizPointsOverall.ToString();
                break;
            case 2:
                runtimeDataCh2.quizPointsOverall += pointsPerAnswer;
                scoreOverall.text = runtimeDataCh2.quizPointsOverall.ToString();
                break;
            case 3:
                runtimeDataCh3.quizPointsOverall += pointsPerAnswer;
                scoreOverall.text = runtimeDataCh3.quizPointsOverall.ToString();
                break;
        }

        btnNextIdentifier.text = weiter;
    }

    private void ShowNextQuestion()
    {
        currentQuestionIndex++;
        UpdateProgressUI();

        if (currentQuestionIndex == questions.Count)
        {
            switch (activeScene)
            {
                case 1:
                    switchScene.SwitchToChapter1withOverlay(runtimeDataCh1.generalKeyOverlay);
                    runtimeDataCh1.quiz119Done = true;
                    break;
                case 2:
                    switchScene.SwitchToChapter2withOverlay(runtimeDataCh2.generalKeyOverlay);
                    runtimeDataCh2.progressPost2111QuizDone = true;
                    break;
                case 3:
                    switchScene.SwitchToChapter3withOverlay(runtimeDataCh3.generalKeyOverlay);
                    runtimeDataCh3.SetPostDone(ProgressChap3enum.Post316);
                    break;
            }

            return;
        }

        Answer[] aws = verticalLayoutGroup.GetComponentsInChildren<Answer>();
        foreach (Answer a in aws)
        {
            a.gameObject.transform.SetParent(a.origParent);
            a.gameObject.SetActive(false);
        }

        if (currentQuestionIndex < questions.Count)
        {
            questions[currentQuestionIndex].ShowData();
        }
    }

    private void Update()
    {
        if (quizTimer.IsTimeRunOut())
        {
            ProcessAnswer();
            quizTimer.ResetTimeRunOut();
        }
    }
}