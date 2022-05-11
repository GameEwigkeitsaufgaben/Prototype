using UnityEngine;

public class MouseChange : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Texture2D caveCursourTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private SoChapOneRuntimeData runtimeData;

    private void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        cursorTexture = runtimeData.cursorInteract;
    }

    public void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(runtimeData.sceneDefaultcursor, Vector2.zero, cursorMode);
    }
}
