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
                Debug.Log("DRAG DRAG" + gameObject.name);

                if (runtimeDataCh01.currDragItemExists) return;

                MuseumMinerEquipmentItem item = gameObject.GetComponent<MuseumMinerEquipmentItem>();

                if (item == null) return;
                if (item.isCurrentlyDragging) return;

                Debug.Log("OverrideDrag " + gameObject.name);
 
                if (MaxReachedSnaptToTable(item))
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }

                //if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging)
                Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            }

            else
            {
                Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
            }
        }
        else
        {
            Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
            if (gameObject.GetComponent<MuseumMinerEquipmentItem>() != null && gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
            
            Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode);
        }
    }

    public void MouseExit()
    {
        if (gameObject.tag == "DragItem")
        {
            if (gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
            {
                if (gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
                if (runtimeDataCh01.currDragItemExists) return;
            }
        }
        Debug.Log("Mouse EXIT---------------");
        Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, cursorMode);
        runtimeDataCh01.hintPostUnlock = "";
        runtimeDataCh02.hintPostUnlock = "";
    }

    private bool MaxReachedSnaptToTable(MuseumMinerEquipmentItem item)
    {
        //Debug.Log("Max reached");

        //if (item.myManager.IsMaxItemsOnMinerReached())
        //{
        //    if (item.snapedTo == SnapetTo.Table)
        //    {
        //        Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
        //        Debug.Log("Max reached, set no drag");
        //        return;
        //    }
        //}

        return item.myManager.IsMaxItemsOnMinerReached() && (item.snapedTo == SnapetTo.Table);
    }

    public void MouseDown()
    {
        if (gameObject.tag == "DragItem")
        {
            //ToDo: Working but, dirty Mechanic Mouse up/down, enter/exit!
            if(gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
            {
                MuseumMinerEquipmentItem item = gameObject.GetComponent<MuseumMinerEquipmentItem>();
                if (item == null) return;

                if (MaxReachedSnaptToTable(item))
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }
                Debug.Log("geht weiter ------------------------- ");

                Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
                runtimeDataCh01.currDragItemExists = true;

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

            else if (gameObject.GetComponent<DragItemThoughts>() != null)
            {
                Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
            }

            else if (gameObject.GetComponent<MuseumMinerEquipmentItem>() != null)
            {

                MuseumMinerEquipmentItem item = gameObject.GetComponent<MuseumMinerEquipmentItem>();
                if (item == null) return;

                if (MaxReachedSnaptToTable(item))
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }

                Cursor.SetCursor(runtimeDataChapters.cursorDragTouch, hotSpot, cursorMode);
                runtimeDataCh01.currDragItemExists = false;
            }
        }
    }
}
