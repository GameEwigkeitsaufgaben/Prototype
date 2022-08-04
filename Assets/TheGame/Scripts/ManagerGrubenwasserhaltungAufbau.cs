using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerGrubenwasserhaltungAufbau : MonoBehaviour
{
    public List<TurmDragItem> dragItems = new List<TurmDragItem>();

    public bool allItemsSnaped = false;
    public bool audioFinised = false;
    public GameObject btnBackTo3101;

    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;

    // Start is called before the first frame update
    void Start()
    {
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    // Update is called once per frame
    void Update()
    {
        if (!btnBackTo3101.GetComponent<Button>().interactable)
        {
            if (audioFinised && allItemsSnaped)
            {
                runtimeDataCh3.SetPostDone(ProgressChap3enum.Post3103);
                btnBackTo3101.GetComponent<Button>().interactable = true;
            }

            if (!allItemsSnaped)
            {
                int index = dragItems.FindIndex(item => item.snaped == true);
                if (index > -1)
                {
                    allItemsSnaped = true;
                }
            }
        }
    }
}
