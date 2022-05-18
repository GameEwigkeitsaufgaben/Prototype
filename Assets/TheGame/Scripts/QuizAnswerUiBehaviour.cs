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

//Bei mehrfachauswahl mit navigation mode select von none auf automatic setzen.

public class QuizAnswerUiBehaviour : MonoBehaviour, ISelectHandler, IDeselectHandler
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
        Debug.Log(gameObject.name + " id" +gameObject.GetInstanceID());
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
        Debug.Log("On select ............... " + eventData.selectedObject.name);
        Debug.Log("On select by click............... " + eventData.selectedObject.GetComponent<QuizAnswerUiBehaviour>().isSelected);
       
        uiAnswer.color = Color.white;
        uiAnswer.fontStyle = FontStyles.Bold;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("On deselect --------------------- " + eventData.selectedObject.name);
        Debug.Log("On deselect by click............... " + eventData.selectedObject.GetComponent<QuizAnswerUiBehaviour>().isSelected);
        uiAnswer.color = GameColors.defaultTextColor;
        uiAnswer.fontStyle = FontStyles.SmallCaps;
    }

    public void ShowResult()
    {
        GetComponent<Image>().color = (isCorrect) ? myQuizConfig.correct : myQuizConfig.incorrect;
        
        if (isCorrect || isSelected)
        {
            uiAnswer.color = Color.white;
            uiAnswer.fontStyle = FontStyles.Bold;
        }
    }
}
