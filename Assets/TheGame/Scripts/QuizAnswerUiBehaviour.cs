using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public enum MouseInteraction
{
    BtnDefaultInteraction,
    BtnPost,
    BtnOverlay,
    Scrollbar,
    BtnQuizAnswer
}

//navigation mode select von none auf automatic.

public class QuizAnswerUiBehaviour : MonoBehaviour,ISelectHandler, IDeselectHandler
{
    public string questID;
    public int awId;
    public QuizQuestionType questType;
    public MouseInteraction uiType = MouseInteraction.BtnDefaultInteraction;
    private TMP_Text uiAnswer;
    
    private SoChapOneRuntimeData runtimeData;
    private SoQuizConfig myQuizConfig;

    public bool isSelected = false;
    public bool isCorrect = false;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        myQuizConfig = Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
        uiAnswer = gameObject.GetComponentInChildren<TMP_Text>();
    }

    public bool IsCorrectlySelected()
    {
        return (isCorrect && isSelected) ^ (!isCorrect && !isSelected);
    }

    public bool IsSelected()
    {
        return isSelected;
    }

    public void OnSelect(BaseEventData eventData)
    {
        uiAnswer.color = Color.white;
        uiAnswer.fontStyle = FontStyles.Bold | FontStyles.SmallCaps;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        uiAnswer.color = GameColors.defaultTextColor;
        uiAnswer.fontStyle = FontStyles.Normal;
    }

    public void ShowResult()
    {
        GetComponent<Image>().color = (isCorrect) ? myQuizConfig.correct : myQuizConfig.incorrect;
        
        if (isCorrect || isSelected)
        {
            uiAnswer.color = Color.white;
            
            if (isCorrect) uiAnswer.fontStyle = FontStyles.SmallCaps | FontStyles.Bold;
            if (isSelected) uiAnswer.fontStyle |= FontStyles.Bold;
        } 
    }

    private void Update()
    {
        //delesect button when mouse clicked in background
        if (EventSystem.current.GetComponent<EventSystem>().currentSelectedGameObject == null)
        {
            isSelected = false;
        }
    }
}
