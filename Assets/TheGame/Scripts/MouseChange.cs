using System.Collections;
using System.Collections.Generic;
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
        
    }

    public void OnMouseEnter()
    {
        //Debug.Log("set cursur enter");
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    public void OnMouseExit()
    {
        //Debug.Log("set cursur exit");
        Cursor.SetCursor(runtimeData.cursorTexture3DCave, Vector2.zero, cursorMode);
    }
}
