using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseChange : MonoBehaviour
{
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeDataCh01;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeDataCh01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
    }

    private void Start()
    {
        if(gameObject.GetComponent<Button>() != null)
        {
            if (gameObject.GetComponent<Post>() != null)
            {
                gameObject.GetComponent<Button>().colors = GameColors.GetPostColorBlock();
                return;
            }
            else if (gameObject.GetComponent<RawImage>() != null)
            {
                gameObject.GetComponent<Button>().colors = GameColors.GetOverlayColorBlock();
                return;
            }
            else if (gameObject.GetComponent<QuizAnswerUiBehaviour>() != null)
            {
                if (gameObject.GetComponent<QuizAnswerUiBehaviour>().uiType == MouseInteraction.BtnQuizAnswer)
                {
                    gameObject.GetComponent<Button>().colors = GameColors.GetQuizAnswerColorBlock();
                    return;
                }
            }
            else if (gameObject.tag == "Buzzer")
            {
                gameObject.GetComponent<Button>().colors = GameColors.GetBuzzerColorBlockProve();
                return;
            }

            gameObject.GetComponent<Button>().colors = GameColors.GetInteractionColorBlock();
            gameObject.GetComponent<Button>().navigation = GameData.GetNoneNavigation();
        }
        else if(gameObject.GetComponent<Scrollbar>() != null)
        {
            gameObject.GetComponent<Scrollbar>().colors = GameColors.GetInteractionColorBlock();
        }
        else if (gameObject.GetComponent<Slider>() != null)
        {
            gameObject.GetComponent<Slider>().colors = GameColors.GetInteractionColorBlock();
        }
    }

    public void MouseEnter()
    {
        if (gameObject.GetComponent<Post>())
        {
            runtimeDataCh01.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "": gameObject.GetComponent<Post>().GetUnlockHint();
        }
        
        if(gameObject.GetComponent<Button>() != null && gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Scrollbar>() != null)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Slider>() != null)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<MuseumCard>() != null)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.tag == "DragItem")
        {
            Debug.Log("DragItem found");
            if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().myManager.IsDragItemOk())
            {
                Debug.Log("set curser no drag");
                Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                return;
            }

            if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            
        }
        else
        {
            if (gameObject.GetComponent<MuseumMinerEquipmentItem>() != null && gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
            
            Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode);
        }

    }

    public void MouseExit()
    {
        Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, cursorMode);
        runtimeDataCh01.hintPostUnlock = "";
    }

    public void MouseDown()
    {
        if (gameObject.tag == "DragItem")
        {
            if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().myManager.IsDragItemOk())
            {
                Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                return;
            }
            Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
        }
    }

    public void MouseUp()
    {
        if (gameObject.tag == "DragItem")
        {
            Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
        }
    }
}
