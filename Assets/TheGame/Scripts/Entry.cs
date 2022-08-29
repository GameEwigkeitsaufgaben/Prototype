//An entry consists of 2 parts, the post and the corresponding overlay.
//The data for both is located in the corresponding Scriptable object under Resources PostDataXXX
//Unlocking a post is here in the update methode and based on bool vals in the static class GameData.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Entry : MonoBehaviour
{
    public GameObject post;
    public GameObject overlay;
    public SoPostData postData;
    public Image fbDone;

    //public bool testVideoPlayed = false;

    private WebGlVideoPlayer webglVideoPlayer;

    private OverlayType overlayType; // Enum
    private SoSfx sfx;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private Color fbDoneColor = GameColors.instaPostDone;
    private Color fbInProgress = GameColors.defaultTextColor;

    // Start is called before the first frame update
    void Start()
    {
        if (fbDone != null) fbDone.color = fbInProgress;
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh2 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);

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
            runtimeDataCh1.post111Done = true;
        }
        else if (overlay.name == GameData.NameOverlay112)
        {
            runtimeDataCh1.post112Done = true;
        }
        else if (overlay.name == GameData.NameOverlay113)
        {
            runtimeDataCh1.post113Done = true;
        }
        else if (overlay.name == GameData.NameOverlay114)
        {
            runtimeDataCh1.post114Done = true;
        }
        else if (overlay.name == GameData.NameOverlay118)
        {
            runtimeDataCh1.post118Done = true;
        }
        else if (overlay.name == GameData.NameOverlay1110)
        {
            runtimeDataCh1.post1110Done = true;
        }
        else if (overlay.name == GameData.NameOverlay213)
        {
            runtimeDataCh2.progressPost213Done = true;
        }
        else if (overlay.name == GameData.NameOverlay214)
        {
            runtimeDataCh2.progressPost214Done = true;
        }
        else if (overlay.name == GameData.NameOverlay216)
        {
            runtimeDataCh2.progressPost216Done = true;
        }
        else if (overlay.name == GameData.NameOverlay217)
        {
            runtimeDataCh2.progressPost217Done = true;
        }
        else if (overlay.name == GameData.NameOverlay33)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post33);
        }
        else if (overlay.name == GameData.NameOverlay34)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post34);
        }
        else if (overlay.name == GameData.NameOverlay35)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post35);
        }
        else if (overlay.name == GameData.NameOverlay36)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post36);
        }
        else if (overlay.name == GameData.NameOverlay37)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post37);
        }
        else if (overlay.name == GameData.NameOverlay38)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post38);
        }
        else if (overlay.name == GameData.NameOverlay39)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post39);
        }
        else if (overlay.name == GameData.NameOverlay313)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post313);
        }
        else if (overlay.name == GameData.NameOverlay315)
        {
            runtimeDataCh3.SetPostDone(ProgressChap3enum.Post315);
        }

        if (runtimeDataCh1.musicOn)
        {
            runtimeDataCh1.overlaySoundState = OverlaySoundState.Opened;
            //sfx.ReduceVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
        }
    }

    public void CloseOverlay()
    {
        overlay.GetComponent<Overlay>().CloseOverlay();
        if (runtimeDataCh1.musicOn)
        {
            //sfx.IncreaseVolume(sfx.instaMenuBGmusicLoop, changeVolumeAmount);
            //if (!sfx.IsInstaBGMusicPlaying())
            //{
            //    sfx.PlayClip(sfx.instaMenuBGmusicLoop);
            //}
        }
    }

    private void SetColorDoneFeedbackImage()
    {
        if (fbDone == null) return;

        if (fbDone.color == fbDoneColor) return;

        bool setColorDone = false;
        Debug.Log("-------------------------------" + fbDone.color + " " + fbDoneColor);
        if (gameObject.name == GameData.NameEntry111 && runtimeDataCh1.post111Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry112 && runtimeDataCh1.post112Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry113 && runtimeDataCh1.post113Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry114 && runtimeDataCh1.post114Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry115 && runtimeDataCh1.video115Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry116 && runtimeDataCh1.interaction116Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry117 && runtimeDataCh1.interaction117Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry118 && runtimeDataCh1.post118Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry119 && runtimeDataCh1.quiz119Done) setColorDone = true;
        else if (gameObject.name == GameData.NameEntry1110 && runtimeDataCh1.post1110Done) setColorDone = true;

        if (!setColorDone) return;
        Debug.Log("------------------------------------------------" + gameObject.name);
        if (fbDone != null) fbDone.color = fbDoneColor;
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            SetColorDoneFeedbackImage();

            if (post.GetComponent<Post>().isPostLocked())
            {
                if(gameObject.name == GameData.NameEntry113 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry114 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry115 && runtimeDataCh1.post113Done && runtimeDataCh1.post114Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry116 && runtimeDataCh1.video115Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry117 && runtimeDataCh1.interaction116Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry118 && runtimeDataCh1.interaction117Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry119 && runtimeDataCh1.interaction117Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry1110 && runtimeDataCh1.quiz119Done)
                {
                    post.GetComponent<Post>().UnlockPost();
                    overlay.GetComponent<Overlay>().UpdateOverlayText();
                    GameData.chapterOneUnlocked = 1;
                }
            }
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            if (gameObject.name == GameData.NameEntry2112 && runtimeDataCh2.progressPost2111QuizDone)
            {
                overlay.GetComponent<Overlay>().UpdateOverlayText();
            }

            if (!post.GetComponent<Post>().isPostLocked()) return;

            if(gameObject.name == GameData.NameEntry213 && runtimeDataCh2.progress212MuseumDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry214 && runtimeDataCh2.progress212MuseumDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry215 && runtimeDataCh2.progressPost213Done && runtimeDataCh2.progressPost214Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry216 && runtimeDataCh2.progressPost215Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry217 && runtimeDataCh2.progressPost215Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry218 && runtimeDataCh2.progressPost216Done && runtimeDataCh2.progressPost217Done)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry219 && runtimeDataCh2.progressPost218PyritDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2110 && runtimeDataCh2.progressPost219VideoDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2111 && runtimeDataCh2.progressPost2110GWReinigungDone)
            {
                post.GetComponent<Post>().UnlockPost();
            }
            else if (gameObject.name == GameData.NameEntry2112 && runtimeDataCh2.progressPost2111QuizDone)
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

            if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post32))
            {
                if(gameObject.name == GameData.NameEntry33) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry34) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry35) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry36) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry37) post.GetComponent<Post>().UnlockPost();
                else if (gameObject.name == GameData.NameEntry38) post.GetComponent<Post>().UnlockPost();
            }

            if ((gameObject.name == GameData.NameEntry39 || gameObject.name == GameData.NameEntry310) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post33) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post34) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post35) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post36) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post37) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post38))
            {
                post.GetComponent<Post>().UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry311 && runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310)) post.GetComponent<Post>().UnlockPost();
            else if (gameObject.name == GameData.NameEntry312 && runtimeDataCh3.IsPostDone(ProgressChap3enum.Post311)) post.GetComponent<Post>().UnlockPost();

            else if (gameObject.name == GameData.NameEntry313 || gameObject.name == GameData.NameEntry314)
            {
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post312)) post.GetComponent<Post>().UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry316)
            {
                Debug.Log("in entry: " + GameData.NamePost316 + " " + gameObject.name + " " + ProgressChap3enum.Post315.ToString());
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post315))
                {
                    post.GetComponent<Post>().UnlockPost();
                }
            }

            else if (gameObject.name == GameData.NameEntry317)
            {
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post316))
                {
                    Debug.Log("Quiz solved: ");
                    post.GetComponent<Post>().UnlockPost();
                    overlay.GetComponent<Overlay>().UpdateOverlayText();
                    GameData.chapterThreeUnlocked = 1;

                }
            }
        }
    }
}
