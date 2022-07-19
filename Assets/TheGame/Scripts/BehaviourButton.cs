using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BehaviourButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MouseChange mouse;

    private void Start()
    {
        gameObject.AddComponent<MouseChange>();
        mouse = gameObject.GetComponent<MouseChange>();

        if (gameObject.GetComponent<Image>().sprite.name == "UISprite") return;
        
        gameObject.AddComponent<ReactOnlyOnInTranspartentParts>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse.MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse.MouseExit();
    }
}
