using UnityEngine;
using UnityEngine.UI;

public class MouseChange : MonoBehaviour
{
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private AudioSource audioSrcBtn;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh2 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();

        audioSrcBtn = gameObject.AddComponent<AudioSource>();
        audioSrcBtn.playOnAwake = false;
        audioSrcBtn.clip = sfx.btnClick;
        audioSrcBtn.volume = 0.3f;
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

            else
            {
                gameObject.GetComponent<Button>().onClick.AddListener(PlaySfx);
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
            gameObject.GetComponent<Toggle>().onValueChanged.AddListener(delegate { PlayMechanicBtnSfx(); });
        }
    }

    public void PlayMechanicBtnSfx()
    {
        audioSrcBtn.clip = sfx.mechanicBtnPress;
        audioSrcBtn.Play();
    }

    public void PlaySfx()
    {
        audioSrcBtn.clip = sfx.btnClick;
        if (gameObject.GetComponent<Post>() != null) return;
        else if (gameObject.GetComponent<RawImage>() != null) return;
        else if (gameObject.GetComponent<QuizAnswerUiBehaviour>() != null) return;
        else if (gameObject.tag == "Buzzer") return;

        if (audioSrcBtn.isActiveAndEnabled)
        {
            audioSrcBtn.Play();
        }
    }

    public void MouseEnter()
    {
        if (gameObject.GetComponent<Post>())
        {
            runtimeDataCh1.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "": gameObject.GetComponent<Post>().GetUnlockHint();
            runtimeDataCh2.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "" : gameObject.GetComponent<Post>().GetUnlockHint();
            runtimeDataCh3.hintPostUnlock = (gameObject.GetComponent<Button>().interactable) ? "" : gameObject.GetComponent<Post>().GetUnlockHint();
        }
        
        if(gameObject.GetComponent<Button>() != null && gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(runtimeDataChapters.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Toggle>() != null)
        {
            if (gameObject.GetComponent<Toggle>().isOn)
            {
                Cursor.SetCursor(runtimeDataChapters.cursorNoInteract, hotSpot, cursorMode);
            }
            else
            {
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
                if (runtimeDataCh1.currDragItemExists) return;

                MuseumMinerEquipmentItem item = gameObject.GetComponent<MuseumMinerEquipmentItem>();

                if (item == null) return;
                if (item.isCurrentlyDragging) return;
 
                if (MaxReachedSnaptToTable(item))
                {
                    Cursor.SetCursor(runtimeDataChapters.cursorNoDrag, hotSpot, cursorMode);
                    return;
                }

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
                if (runtimeDataCh1.currDragItemExists) return;
            }
        }
        Cursor.SetCursor(runtimeDataChapters.sceneCursor, Vector2.zero, cursorMode);
        runtimeDataCh1.hintPostUnlock = "";
        runtimeDataCh2.hintPostUnlock = "";
        runtimeDataCh3.hintPostUnlock = "";
    }

    private bool MaxReachedSnaptToTable(MuseumMinerEquipmentItem item)
    {
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

                Cursor.SetCursor(runtimeDataChapters.cursorDragDrag, hotSpot, cursorMode);
                runtimeDataCh1.currDragItemExists = true;

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
                if (!gameObject.GetComponent<DragTurmItem>().snaped)
                {
                    gameObject.transform.position = gameObject.GetComponent<DragTurmItem>().origPos;
                }
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
                runtimeDataCh1.currDragItemExists = false;
            }
        }
    }
}
