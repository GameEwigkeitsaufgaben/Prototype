using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public enum MouseInteraction
{
    BtnDefaultInteraction,
    BtnPost,
    BtnOverlay,
    Scrollbar,
    BtnQuizAnswer
}

//Bei mehrfachauswahl mit navigation mode select von none auf automatic setzen.

public class MouseInteractionElement : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public MouseInteraction uiType = MouseInteraction.BtnDefaultInteraction;
    private TMP_Text answer;
    [SerializeField] private bool isSelcted = false;

    private void Start()
    {
        answer = gameObject.GetComponentInChildren<TMP_Text>();
    }

    public bool IsSelected()
    {
        return isSelcted;
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(eventData.selectedObject.name == "AnswerBtn")
        {
            Debug.Log("selllllllllllll " + eventData.selectedObject);
            answer.color = Color.white;
            Debug.Log("selected " + GetComponentInChildren<TMP_Text>().text);
            isSelcted = true;

        }
        
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (eventData.selectedObject.name == "AnswerBtn")
        {
            answer.color = GameColors.defaultTextColor;
            Debug.Log("unselected " + GetComponentInChildren<TMP_Text>().text);
            isSelcted = false;
        }        
    }
}
