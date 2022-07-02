//An entry consists of 2 parts, the post and the corresponding overlay.
//The data for both is located in the corresponding Scriptable object under Resources PostDataXXX
//Unlocking a post is here in the update methode and based on bool vals in the static class GameData.

using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Entry : MonoBehaviour
{
   // private const string Entry115 = "Entry115";
    private const string Entry116 = "Entry116";
    private const string Entry1110 = "Entry1110";
   // private const string Entry117 = "Entry117";
    private const string Entry118 = "Entry118";
    private const string Entry119 = "Entry119";
   // private const float changeVolumeAmount = 0.15f;
    public GameObject post;
    public GameObject overlay;
    public SoPostData postData;
   

    //public bool testVideoPlayed = false;

    private WebGlVideoPlayer webglVideoPlayer;

    private OverlayType overlayType; // Enum
    private SoSfx sfx;
    private SoChapOneRuntimeData runtimeData;
    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChapThreeRuntimeData runtimeDataCh03;

    // Start is called before the first frame update
    void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataCh03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);

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

        if(overlay.name == GameData.NameOverlay111)
        {
            runtimeData.post111Done = true;
        }
        else if (overlay.name == GameData.NameOverlay112)
        {
            runtimeData.post112Done = true;
        }
        else if (overlay.name == GameData.NameOverlay113)
        {
            runtimeData.post113Done = true;
        }
        else if (overlay.name == GameData.NameOverlay114)
        {
            runtimeData.post114Done = true;
        }
        else if (overlay.name == GameData.NameOverlay213)
        {
            runtimeDataCh02.progressPost213Done = true;
        }
        else if (overlay.name == GameData.NameOverlay214)
        {
            runtimeDataCh02.progressPost214Done = true;
        }
        else if (overlay.name == GameData.NameOverlay216)
        {
            runtimeDataCh02.progressPost216Done = true;
        }
        else if (overlay.name == GameData.NameOverlay217)
        {
            runtimeDataCh02.progressPost217Done = true;
        }

        if (runtimeData.musicOn)
        {
            runtimeData.overlaySoundState = OverlaySoundState.Opened;
            //sfx.ReduceVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
        }
    }

    public void CloseOverlay()
    {
        overlay.GetComponent<Overlay>().CloseOverlay();
        if (runtimeData.musicOn)
        {
            //sfx.IncreaseVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
            //if (!sfx.IsInstaBGMusicPlaying())
            //{
            //    sfx.PlayClip(sfx.instaMenuBGmusicLoop);
            //}
        }
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            if (post.GetComponent<Post>().isPostLocked())
            {
                if(gameObject.name == GameData.NameEntry113 && runtimeData.post111Done && runtimeData.post112Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry114 && runtimeData.post111Done && runtimeData.post112Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry115 && runtimeData.post113Done && runtimeData.post114Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry116 && runtimeData.video115Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry117 && runtimeData.interaction116Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == Entry118 && runtimeData.interaction117Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == Entry119 && runtimeData.interaction117Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == Entry1110 && runtimeData.quiz119Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                    overlay.GetComponent<Overlay>().UpdateOverlayText();
                    GameData.chapterOneUnlocked = 1;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            if (gameObject.name == GameData.NameEntry2112 && runtimeDataCh02.progressPost2111QuizDone)
            {
                overlay.GetComponent<Overlay>().UpdateOverlayText();
            }

            if (!post.GetComponent<Post>().isPostLocked()) return;

            if(gameObject.name == GameData.NameEntry213 && runtimeDataCh02.progress212MuseumDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry214 && runtimeDataCh02.progress212MuseumDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry215 && runtimeDataCh02.progressPost213Done && runtimeDataCh02.progressPost214Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry216 && runtimeDataCh02.progressPost215Pumpen)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry217 && runtimeDataCh02.progressPost215Pumpen)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry218 && runtimeDataCh02.progressPost216Done && runtimeDataCh02.progressPost217Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry219 && runtimeDataCh02.progressPost218PyritDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2110 && runtimeDataCh02.progressPost219VideoDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2111 && runtimeDataCh02.progressPost2110GWReinigungDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2112 && runtimeDataCh02.progressPost2111QuizDone)
            {
                Debug.Log("Quiz solved: ");
                post.GetComponent<Post>().UnlockPost();
                overlay.GetComponent<Overlay>().UpdateOverlayText();
                GameData.chapterTwoUnlocked = 1;
            }

        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            if (!post.GetComponent<Post>().isPostLocked()) return;

            if (runtimeDataCh03.IsPostDone(ProgressChap3enum.Post32))
            {
                if(gameObject.name == GameData.NameEntry33) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry34) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry35) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry36) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry37) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry38) post.GetComponent<Post>().UnlockPost();
            }

            if((gameObject.name == GameData.NameEntry39 || gameObject.name == GameData.NameEntry310) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post33) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post34) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post35) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post36) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post37) &&
                runtimeDataCh03.IsPostDone(ProgressChap3enum.Post38))
                {
                    post.GetComponent<Post>().UnlockPost();
                }

            else if (gameObject.name == GameData.NameEntry311 && runtimeDataCh03.IsPostDone(ProgressChap3enum.Post310)) post.GetComponent<Post>().UnlockPost();
            else if (gameObject.name == GameData.NameEntry312 && runtimeDataCh03.IsPostDone(ProgressChap3enum.Post311)) post.GetComponent<Post>().UnlockPost();

            else if (gameObject.name == GameData.NameEntry313 || gameObject.name == GameData.NameEntry314)
            {
                if(runtimeDataCh03.IsPostDone(ProgressChap3enum.Post312)) post.GetComponent<Post>().UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry316)
            {
                if (runtimeDataCh03.IsPostDone(ProgressChap3enum.Post315)) post.GetComponent<Post>().UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry317)
            {
                if (runtimeDataCh03.IsPostDone(ProgressChap3enum.Post316)) post.GetComponent<Post>().UnlockPost();
            }
        }
    }
}
