using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGWChancen : MonoBehaviour
{
    public List<DragItemThoughts> dragitems = new List<DragItemThoughts>();
    public SpeechManagerChapThree speechManager;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    public string currentTL = "";
    public Animator animator;
    public GameObject bergvertreter1, bergvertreter2;
    public Button btnNext;
    public bool allItemsSnaped = false;

    // Start is called before the first frame update
    void Start()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        animator.enabled = true;
        MirrorBergbauvertreter(false);
        btnNext.interactable = false;

        float x = -300f, y = 150f;
        foreach (DragItemThoughts d in dragitems)
        {
            d.gameObject.transform.localPosition = new Vector3(x, y, 0f);
            y -= 10f;
            x += 10f;
        }

        speechManager = GetComponent<SpeechManagerChapThree>();
    }


    public void MirrorBergbauvertreter(bool yes)
    {
        bergvertreter1.SetActive(yes);
        bergvertreter2.SetActive(yes);
    }

    public void PauseDragAllDragItems(bool pause)
    {
        foreach (DragItemThoughts d in dragitems)
        {
            d.dragable = !pause;
            if (d.snaped)
            {
                d.btnRahmen.enabled = !pause;
            }
            else
            {
                d.buzzword.gameObject.SetActive(!pause);
            }
        }
    }

    private void Update()
    {
        if (!btnNext.interactable)
        {
            if (!allItemsSnaped && runtimeDataCh3.DropTargetsAllItemsSnaped(dragitems))
            {
                allItemsSnaped = true;
            }
        }

        if(allItemsSnaped && animator.enabled && !GetComponent<AudioSource>().isPlaying)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post312);
            btnNext.interactable = true;
            animator.enabled = false;
        }

        if (currentTL != "" && speechManager.IsTalkingListFinished(currentTL))
        {
            currentTL = "";
            animator.enabled = true;
            MirrorBergbauvertreter(false);
            PauseDragAllDragItems(false);
        }
    }
}
