//Class is resposible for post activation and pin check
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    private void Awake()
    {
        Debug.Log("Awake in " + gameObject.name);

        if (GameData.overlayToLoad != "")
        {
            GameObject go = overlayParent.transform.Find(GameData.overlayToLoad).gameObject;
            go.SetActive(true);
            GameData.overlayToLoad = "";
        }
    }

    private void Start()
    {
        Debug.Log("Start in "+ gameObject.name);
    }


}
