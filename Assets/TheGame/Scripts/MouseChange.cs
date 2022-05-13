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
            gameObject.GetComponent<Button>().colors = (gameObject.GetComponent<Post>() != null) ? GameColors.GetPostColorBlock() : GameColors.GetInteractionColorBlock();
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
        Cursor.SetCursor(runtimeData.cursorInteract, hotSpot, cursorMode);
    }

    public void MouseExit()
    {
        Cursor.SetCursor(runtimeData.cursorDefault, Vector2.zero, cursorMode);
    }
}
