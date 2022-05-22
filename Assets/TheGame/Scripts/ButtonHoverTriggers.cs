using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverTriggers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MouseChange mouseChange;

    void Start()
    {
        mouseChange = gameObject.GetComponent<MouseChange>();    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseChange.MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseChange.MouseExit();
    }
}
