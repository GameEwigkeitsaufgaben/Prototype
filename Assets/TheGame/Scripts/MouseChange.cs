using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseChange : MonoBehaviour
{
    //public Texture2D cursorTexture;
    //public Texture2D caveCursourTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
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

        //if (SceneManager.GetActiveScene().name != GameScenes.ch01Mine) return;

        Cursor.SetCursor(runtimeData.cursorDefault, hotSpot, cursorMode);
    }

    public void MouseEnter()
    {
        Debug.Log(gameObject.name);
            //+ " btn ok " + (gameObject.GetComponent<Button>() != null) + " " + gameObject.GetComponent<Button>().interactable);
        if(gameObject.GetComponent<Button>() != null && gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Scrollbar>() != null)
        {
            Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<Slider>() != null)
        {
            Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.GetComponent<MuseumCard>() != null)
        {
            Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
        }
        else if (gameObject.tag == "DragItem")
        {
            if (!gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) Cursor.SetCursor(runtimeData.cursorDragTouch, hotSpot, cursorMode);
            Debug.Log("DragItem found + " + runtimeData.cursorNoDrag.name);
        }
        else
        {
            if (gameObject.GetComponent<MuseumMinerEquipmentItem>().isCurrentlyDragging) return;
            
            Cursor.SetCursor(runtimeData.cursorNoInteract, hotSpot, cursorMode);
        }

    }

    public void MouseExit()
    {
        if(gameObject.GetComponent<MinerEquipmentItem>() != null) Cursor.SetCursor(runtimeData.cursorDefault, Vector2.zero, cursorMode);
    }

    public void MouseDown()
    {
        if (gameObject.tag == "DragItem")
        {
            Cursor.SetCursor(runtimeData.cursorDragDrag, hotSpot, cursorMode);
            Debug.Log("DragItem down + ");

        }
    }

    public void MouseUp()
    {
        if (gameObject.tag == "DragItem")
        {
            Cursor.SetCursor(runtimeData.cursorDragTouch, hotSpot, cursorMode);
            Debug.Log("DragItem up + ");

        }
    }
}
