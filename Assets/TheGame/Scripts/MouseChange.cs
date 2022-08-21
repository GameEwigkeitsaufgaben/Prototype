using UnityEngine;
using UnityEngine.UI;

public class MouseChange : MonoBehaviour
{
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeDataCh01;
    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeDataCh01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
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
        else if (gameObject.GetComponent<Toggle>() !=null)
        {
            gameObject.GetComponent<Toggle>().colors = GameColors.GetToogleColorBlock();
        }
    }

    public void MouseEnter()
    {
        Debug.Log("Mouse Enter");
        if (gameObject.GetComponent<Post>())
        {
            runtimeDataCh01.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "": gameObject.GetComponent<Post>().GetUnlockHint();
            runtimeDataCh02.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "" : gameObject.GetComponent<Post>().GetUnlockHint();
        }
        
        if(gameObject.GetComponent<Button>() != null && gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Toggle>() != null)
        {
            Debug.Log("in Toggle");
            if (gameObject.GetComponent<Toggle>().isOn)
            {
                Debug.Log("in Toggle is on, set cursur" + runtimeDataChapters.cursorNoInteract.name);
                Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode);
            }
            else
            {
                Debug.Log("in Toggle is off ");
                if(gameObject.GetComponent<Toggle>().interactable) Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
                else { Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode); }

            }
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
            if(gameObject.GetComponent<DragTurmItem>() != null)
            {
                if (gameObject.GetComponent<DragTurmItem>().snaped)
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }
                else
                {
                    if(gameObject.GetComponent<DragTurmItem>().dragging)
                    {
                        Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
                    }
                    else
                    {
                        Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
                    }
                    return;
                }
            }

            else if(gameObject.GetComponent<DragItemThoughts>() != null)
            {
                if(gameObject.GetComponent<DragItemThoughts>().dragable) Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
                else Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                return;
            }

            else if(gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
            {
                if (gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;

                if (gameObject.GetComponent<MuseumMinerEquipmentItem>().myManager.IsMaxItemsOnMinerReached() && gameObject.GetComponent<MuseumMinerEquipmentItem>().snapedTo == SnapetTo.Table)
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }

                if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            }

            else
            {
                Debug.Log("EEEEEEEEEEEEEEEEELSEEEEEEEEEEEE drag drag");
                Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
            }
        }
        else
        {
            if (gameObject.GetComponent<MuseumMinerEquipmentItem>() != null && gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
            
            Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode);
        }
    }

    public void MouseExit()
    {
        if(gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
        {
            if (gameObject.tag == "DragItem" && gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
        }
        
        Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, cursorMode);
        runtimeDataCh01.hintPostUnlock = "";
        runtimeDataCh02.hintPostUnlock = "";
    }

    public void MouseDown()
    {
        if (gameObject.tag == "DragItem")
        {
            if(gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
            {
                if (gameObject.GetComponent<MuseumMinerEquipmentItem>().myManager.IsMaxItemsOnMinerReached())
                {

                    if (SnapetTo.Miner == gameObject.GetComponent<MuseumMinerEquipmentItem>().snapedTo)
                    {
                        Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
                        return;
                    }

                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }
            }

            else if(gameObject.GetComponent<DragItemThoughts>() != null)
            {
                if (!gameObject.GetComponent<DragItemThoughts>().dragable)
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }
            }
            
            else Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
        }
    }

    public void MouseUp()
    {
        Debug.Log("Back to orig");
        if (gameObject.tag == "DragItem")
        {
            if (gameObject.GetComponent<DragTurmItem>() != null)
            {
                Debug.Log("Back to orig");
                if (!gameObject.GetComponent<DragTurmItem>().snaped)
                {
                    gameObject.transform.position = gameObject.GetComponent<DragTurmItem>().origPos;
                    Debug.Log("Back to orig");
                }
                //Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            }

            if (gameObject.GetComponent<DragItemThoughts>() != null)
            {
                Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            }
        }
    }
}
