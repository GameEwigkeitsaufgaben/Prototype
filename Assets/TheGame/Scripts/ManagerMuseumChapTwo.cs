using UnityEngine;
using UnityEngine.UI;

public class ManagerMuseumChapTwo : MonoBehaviour
{
    public Button btnNextGrubenwasser;

    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChaptersRuntimeData runtimeDataChOverlap;


    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChOverlap = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        Cursor.SetCursor(runtimeDataChOverlap.cursorDefault, Vector2.zero, CursorMode.Auto);
    }

    // Start is called before the first frame update
    void Start()
    {
        btnNextGrubenwasser.colors = GameColors.GetInteractionColorBlock();
        btnNextGrubenwasser.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
