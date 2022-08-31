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

    public GameObject headingImgChance, headingImgNoChance, headingImgNN;

    // Start is called before the first frame update
    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
       
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        animator.enabled = true;
        MirrorBergbauvertreter(false);

        float x = -300f, y = 150f;

        Vector3 tmpVec3 = Vector3.zero; 

        foreach (DragItemThoughts d in dragitems)
        {
            tmpVec3.Set(x, y, 0f);
            d.gameObject.transform.localPosition = tmpVec3;
            d.origPos = tmpVec3;
            y -= 10f;
            x += 10f;
        }

        speechManager = GetComponent<SpeechManagerChapThree>();
        btnNext.interactable = runtimeDataCh3.IsPostDone(ProgressChap3enum.Post312);
    }


    public void MirrorBergbauvertreter(bool yes)
    {
        bergvertreter1.SetActive(yes);
        bergvertreter2.SetActive(yes);
    }

    public void PauseDragAllDragItems(bool pause)
    {
        Vector2 sizeVec2 = new Vector2(150f, 110f);
        headingImgChance.GetComponent<RectTransform>().localScale = Vector3.one;
        headingImgNN.GetComponent<RectTransform>().localScale = Vector3.one;
        headingImgNoChance.GetComponent<RectTransform>().localScale = Vector3.one;

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
