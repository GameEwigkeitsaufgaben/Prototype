using UnityEngine;
using UnityEngine.Video;

public class Entry : MonoBehaviour
{
    private const string Entry115 = "Entry115";
    public GameObject post;
    public GameObject overlay;
    public PostData postData;
    private WebGlVideoPlayer webglVideoPlayer;


    private OverlayType overlayType; // Enum

    // Start is called before the first frame update
    void Start()
    {
        post.GetComponent<Post>().SetOverlayData(postData);
        overlay.GetComponent<Overlay>().SetOverlayData(postData);
        Debug.Log("Entry created, post overlay set" + gameObject.name);
        webglVideoPlayer = GameObject.FindObjectOfType<WebGlVideoPlayer>();
        webglVideoPlayer.videoPlayer.loopPointReached += SetReplayIcon;
        Debug.Log("webglplaer ist null in entry "+ webglVideoPlayer);
    }

    public void SetReplayIcon(VideoPlayer vp)
    {

        //is only important for video, so if type is not a video return
        if (postData.overlayType != OverlayType.VIDEO) return;

        //if the postdata video is the same as in the videoplayer go on and reset icon.
        if (!vp.url.EndsWith(postData.videoName)) return;

        post.GetComponent<Post>().UpdateIcon();
        overlay.GetComponent<Overlay>().SetReplayIcon();       

        if (gameObject.name == Entry115)
        {
            GameData.introPlayedOnce = true;
            
        }
    }

    public void OpenOverlay()
    {
        overlay.SetActive(true);
    }

    private void Update()
    {
        if (GameData.introPlayedOnce && gameObject.name == "Entry116")
        {
            GameData.PrintState();
            post.GetComponent<Post>().UnlockPost();
            GameData.introPlayedOnce = false;
        }
    }

}
