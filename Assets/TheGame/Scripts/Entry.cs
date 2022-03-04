//An entry consists of 2 parts, the post and the corresponding overlay.
//The data for both is located in the corresponding Scriptable object under Resources PostDataXXX
//Unlocking a post is here in the update methode and based on bool vals in the static class GameData.

using UnityEngine;
using UnityEngine.Video;

public class Entry : MonoBehaviour
{
    private const string Entry115 = "Entry115";
    private const string Entry116 = "Entry116";
    private const string Entry1110 = "Entry1110";
    private const float changeVolumeAmount = 0.15f;
    public GameObject post;
    public GameObject overlay;
    public SoPostData postData;
   

    //public bool testVideoPlayed = false;

    private WebGlVideoPlayer webglVideoPlayer;

    private OverlayType overlayType; // Enum
    private SoSfx sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = Resources.Load<SoSfx>("SfxConfig");
        post.GetComponent<Post>().SetPostData(postData);
        overlay.GetComponent<Overlay>().SetOverlayData(postData);

        try
        {
            webglVideoPlayer = GameObject.FindObjectOfType<WebGlVideoPlayer>();
            webglVideoPlayer.videoPlayer.loopPointReached += SetReplayIcon;
            webglVideoPlayer.SetSFX(sfx);
        }
        catch (System.Exception)
        {

            Debug.Log("No Videoplayer found");
        }

        Debug.Log(gameObject.name + " created.");
    }

    public void SetReplayIcon(VideoPlayer vp)
    {
        //is only important for video, so if type is not a video return
        if (postData.overlayType != OverlayType.VIDEO) return;

        //if the postdata video is the same as in the videoplayer go on and reset icon.
        if (!vp.url.EndsWith(postData.videoName)) return;

        post.GetComponent<Post>().UpdateIcon();
        overlay.GetComponent<Overlay>().SetReplayIcon();       
    }

    public void OpenOverlay()
    {
        overlay.SetActive(true);
        sfx.ReduceVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
    }

    public void CloseOverlay()
    {
        overlay.GetComponent<Overlay>().CloseOverlay();
        sfx.IncreaseVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
        if (!sfx.IsInstaBGMusicPlaying())
        {
            sfx.PlayClip(sfx.instaMenuBGmusicLoop);
        }
       
    }

    private void Update()
    {
        if (GameData.introVideoPlayedOnce && gameObject.name == Entry116)
        {
            GameData.PrintState();
            post.GetComponent<Post>().UnlockPost();
            GameData.introVideoPlayedOnce = false;
        }
        if (GameData.quizFinished && gameObject.name == Entry1110)
        {
            post.GetComponent<Post>().UnlockPost();
            overlay.GetComponent<Overlay>().UpdateOverlayText();
            GameData.quizFinished = false;
            GameData.chapterOneUnlocked = 1;
        }
    }
}
