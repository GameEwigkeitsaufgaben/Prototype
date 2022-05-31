using UnityEngine;
using UnityEngine.UI;

public class CaveButton : MonoBehaviour
{
    public GameObject feedbackObject;
    public CoalmineStop relatedStop;
    private CaveMoveUpDownController caveMoveController;
    public bool isSelected = false;
    public bool hasChanged = false;

    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
    }

    private void Start()
    {
        caveMoveController = transform.parent.transform.parent.transform.parent.GetComponent<CaveMoveUpDownController>(); //go to cave, set controller

        if(relatedStop == runtimeData.currentCoalmineStop)
        {
            isSelected = true;
        }
    }
    

    public void GoToMyStop()
    {
        Debug.Log("pressed got to stop");
        if (caveMoveController.CheckNextStopInvalid()) return;
        Debug.Log("pressed got to stop after return");
        caveMoveController.GoToStop(relatedStop);
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
