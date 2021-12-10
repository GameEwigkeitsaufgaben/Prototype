using UnityEngine;

public class Entry : MonoBehaviour
{
    public GameObject post;
    public GameObject overlay;
    public PostData postData;

    private OverlayType overlayType; // Enum

    // Start is called before the first frame update
    void Start()
    {
        post.GetComponent<Post>().SetOverlayData(postData);
        overlay.GetComponent<Overlay>().SetOverlayData(postData);
        Debug.Log("Entry created, post overlay set" + gameObject.name);
    }

    public void OpenOverlay()
    {
        //post.GetComponent<Post>().SetButtonFunctionInteractable(false);
        overlay.SetActive(true);
    }

}
