using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Chapters
{
    Ch01General = 1,
    Ch02Grubenwasser = 2,
    Ch03Ewigkeitsaufgabe = 3
}

public class ManagerOverviewChapters : MonoBehaviour
{
    [SerializeField] Button credits;
    [SerializeField] TMP_Text lawNotice;
    SoChapOneRuntimeData runtimeDataCh1;
    SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    private void Start()
    {
        credits.colors = GameColors.GetInteractionColorBlock();
        lawNotice.text = GameData.lawNotiz;
    }
}
