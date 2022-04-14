using UnityEngine;

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
    }

    public void GoToMyStop()
    {
        if (GameData.moveCave) return;

        caveMoveController.GoToStop(myStop);
        isSelected = true;
    }

    public void DisableIsSelected()
    {
        isSelected = false;
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
