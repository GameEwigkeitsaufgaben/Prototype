using UnityEngine;

public class Runtime : ScriptableObject
{
    [Header("Cursor")]
    public Texture2D cursorTexture3DCave;
    public Texture2D cursorInteract;
    public Texture2D cursorNoInteract;
    public Texture2D cursorDefault;
    public Texture2D cursorDragTouch;
    public Texture2D cursorDragDrag;
    public Texture2D cursorNoDrag;

    public string postOverlayToLoad = "";
    public OverlaySoundState overlaySoundState;

    [Header("Quiz")]
    public MinerFeedback quizMinerFeedback;
    public GameObject singleSelectAwIdOld = null;

    [Header("General Settings")]
    public bool musicOn = true;
}
