using UnityEngine;

[CreateAssetMenu(menuName = "SoChaptersRuntimeData")]
public class SoChaptersRuntimeData : ScriptableObject
{
    public bool ch1GeneralUnlocked;
    public bool ch2GrubenwasserUnlocked;
    public bool ch3GrubenwasserUnlocked;

    public bool progressWithAdminCh1, progressWithAdminCh2, progressWithAdminCh3;

    [Header("Cursor")]
    public Texture2D cursorTexture3DCave;
    public Texture2D cursorInteract;
    public Texture2D cursorNoInteract;
    public Texture2D cursorDefault;
    public Texture2D cursorDragTouch;
    public Texture2D cursorDragDrag;
    public Texture2D cursorNoDrag;

    public Texture2D sceneCursor;

    private void OnEnable()
    {
        Debug.Log("Enable SoChaptersRuntimeData");
        ch1GeneralUnlocked = ch2GrubenwasserUnlocked = ch3GrubenwasserUnlocked = false;
        progressWithAdminCh1 = progressWithAdminCh2 = progressWithAdminCh3 = false;
    }

    public void SetSceneCursor(Texture2D defaultSceneCursor)
    {
        sceneCursor = defaultSceneCursor;
        Cursor.SetCursor(defaultSceneCursor, Vector2.zero, CursorMode.Auto);
    }
}