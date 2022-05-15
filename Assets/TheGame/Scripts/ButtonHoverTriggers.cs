using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverTriggers : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    MouseChange mouseChange;

    // Start is called before the first frame update
    void Start()
    {
        mouseChange = gameObject.GetComponent<MouseChange>();    
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("enter mouse");
        mouseChange.MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("exit mouse");
        mouseChange.MouseExit();
    }
}
