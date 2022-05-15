using System.Collections;
using System.Collections.Generic;
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

public class MouseInteractionElement : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public MouseInteraction uiType = MouseInteraction.BtnDefaultInteraction;
    private TMP_Text answer;

    private void Start()
    {
        answer = gameObject.GetComponentInChildren<TMP_Text>();
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log("selllllllllllllllllllllllll");
        answer.color = Color.white;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log("selllllllllllllllllllllllll");
        answer.color = Color.black;
        answer.fontStyle = FontStyles.Bold;
    }
}
