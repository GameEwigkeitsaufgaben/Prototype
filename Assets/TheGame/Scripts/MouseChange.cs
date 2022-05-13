using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseChange : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D caveCursourTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
    }

    private void Start()
    {
        if(gameObject.GetComponent<Button>() != null)
        {
            if (gameObject.GetComponent<Post>() != null || gameObject.GetComponent<Overlay>())
            {
                gameObject.GetComponent<Button>().colors = GameColors.GetPostColorBlock();
                return;
            }
            if (gameObject.GetComponent<RawImage>() != null)
            {
                //gameObject.GetComponent<Button>().colors = GameColors.GetOverlayColorBlock();
                return;
            }

            gameObject.GetComponent<Button>().colors = GameColors.GetInteractionColorBlock();
        }
        else if(gameObject.GetComponent<Scrollbar>() != null)
        {
            gameObject.GetComponent<Scrollbar>().colors = GameColors.GetInteractionColorBlock();
        }
        
        //if (SceneManager.GetActiveScene().name != GameScenes.ch01Mine) return;
        
        Cursor.SetCursor(runtimeData.cursorDefault, hotSpot, cursorMode);
    }

    public void MouseEnter()
    {
        if(gameObject.GetComponent<Button>() != null && gameObject.GetComponent<Button>().interactable)
        {
            Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(runtimeData.cursorNoInteract, hotSpot, cursorMode);
        }

    }

    public void MouseExit()
    {
        Cursor.SetCursor(runtimeData.cursorDefault, Vector2.zero, cursorMode);
    }
}
