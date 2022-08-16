using System.Collections.Generic;
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
        ch1GeneralUnlocked = ch2GrubenwasserUnlocked = ch3GrubenwasserUnlocked = false;
        progressWithAdminCh1 = progressWithAdminCh2 = progressWithAdminCh3 = false;
    }

    public SoChapOneRuntimeData LoadChap1RuntimeData()
    {
        return Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
    }

    public SoChapTwoRuntimeData LoadChap2RuntimeData()
    {
        return Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
    }    
    
    public SoChapThreeRuntimeData LoadChap3RuntimeData()
    {
        return Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
    }

    public SoQuizConfig LoadConfigQuiz()
    {
        return Resources.Load<SoQuizConfig>(GameData.NameConfigQuiz);
    }

    public void SetSceneCursor(Texture2D defaultSceneCursor)
    {
        sceneCursor = defaultSceneCursor;
        Cursor.SetCursor(defaultSceneCursor, Vector2.zero, CursorMode.Auto);
    }

    public void DisableAllSpeechlists(Dictionary<string, SpeechList> speechListDict)
    {
        foreach (var slist in speechListDict)
        {
            slist.Value.finishedToogle = false;
        }
    }

    public void XX(SpeechList currentList, AudioSource audioSrc, Dictionary<string,SpeechList> list)
    {
        if (currentList != null)
        {
            if (audioSrc.isPlaying) audioSrc.Stop();

            DisableAllSpeechlists(list);
            currentList.enabled = true;
            currentList.PlayAll();
            currentList = null;
        }
    }
}