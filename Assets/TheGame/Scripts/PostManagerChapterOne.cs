//Class is resposible for post activation
using System.Collections.Generic;
using UnityEngine;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    private List <Overlay> overlayList = new List<Overlay>();
    private Overlay[] overlayArray;
    private Dictionary<string, Overlay> dictOverlay= new Dictionary<string, Overlay>();

    private void Awake()
    {
        overlayArray = overlayParent.GetComponentsInChildren<Overlay>(true);

        foreach (Overlay a in overlayArray)
        {
            dictOverlay.Add(a.name, a);
        }

        if (GameData.overlayToLoad != "" && dictOverlay != null)
        {
            dictOverlay[GameData.overlayToLoad].gameObject.SetActive(true);
            GameData.overlayToLoad = "";
        }
    }

    private void PrintAllDictKeys(string calledFrom)
    {
        Debug.Log("Dict length in "+ calledFrom +" "+ dictOverlay.Count);

        foreach (string key in dictOverlay.Keys)
        {
            Debug.Log(calledFrom + ": " + key);
        }
    }
}
