using UnityEngine;
using UnityEngine.UI;

public class CaveButton : MonoBehaviour
{
    public GameObject feedbackObject;
    public CoalmineStop myStop;
    private CaveMoveUpDownController caveMoveController;
    public bool isSelected = false;
    public bool hasChanged = false;

    private void Start()
    {
        caveMoveController = transform.parent.transform.parent.transform.parent.GetComponent<CaveMoveUpDownController>(); //go to cave, set controller

        if(myStop == (CoalmineStop)GameData.currentStopSohle)
        {
            isSelected = true;
        }

        //GetComponent<Button>().interactable = false;
    }
    

    public void GoToMyStop()
    {
        if (caveMoveController.CheckNextStopInvalid()) return;
        caveMoveController.GoToStop(myStop);
        isSelected = true;
    }

    public void DisableButtonSelected()
    {
        isSelected = false;
    }

    public void SetInteractable(bool interactable)
    {
        GetComponent<Button>().interactable = interactable;
    }

    private void Update()
    {
        if(isSelected != hasChanged)
        {
            feedbackObject.SetActive(isSelected);
            hasChanged = isSelected;
        }
    }
}
