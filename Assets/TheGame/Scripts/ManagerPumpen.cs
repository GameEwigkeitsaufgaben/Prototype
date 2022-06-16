using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Pumpen
{
    pumpe1 = 1,
    pumpe2 = 2,
    pumpe3 = 3,
    pumpeOff = 0
}

public class ManagerPumpen : MonoBehaviour
{
    public TMP_Text[] textInScene;
    public Animator animator;

    public Button btnBackToOverlay;
    public Toggle toggleP1, toggleP2, toggleP3;

    private SoChapTwoRuntimeData runtimeDataChap02;
    private SoChaptersRuntimeData runtimeDataChapters;

    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChap02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        for(int i = 0; i < textInScene.Length; i++)
        {
            textInScene[i].color = GameColors.defaultTextColor;
        }
    }

    public void CheckPumpOutSole2(Pumpen pumpe)
    {
        switch (pumpe)
        {
            case Pumpen.pumpe1:
                toggleP2.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
            case Pumpen.pumpe2:
                toggleP1.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
            case Pumpen.pumpe3:
                toggleP2.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
        }
    }

    public void TurnOnPumpe(int pumpenid)
    {
        //if()
        switch (pumpenid)
        {
            case 1:
                if (!toggleP1.isOn)
                {
                    animator.SetTrigger(Pumpen.pumpe1.ToString());
                }
                else
                {

                }
                break;
            case 2:
                animator.SetTrigger(Pumpen.pumpe2.ToString());
                break;
            case 3:
                animator.SetTrigger(Pumpen.pumpe2.ToString());
                break;
            case 0:
                animator.SetTrigger(Pumpen.pumpeOff.ToString());
                break;
        }
       
    }
}
