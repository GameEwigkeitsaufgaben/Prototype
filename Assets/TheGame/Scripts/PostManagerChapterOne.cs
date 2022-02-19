//Class is resposible for post activation and pin check
using UnityEngine;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    
    private void Awake()
    {
        if (GameData.overlayToLoad != "")
        {
            GameObject go = overlayParent.transform.Find(GameData.overlayToLoad).gameObject;
            go.SetActive(true);
            GameData.overlayToLoad = "";
        }
    }

}
