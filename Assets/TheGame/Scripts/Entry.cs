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
    private chapter currentCh;

    private Post postComp;
    private Overlay overlayComp;


    private void Awake()
    {
        runtimeDataCh1 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataCh2 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataCh3 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);

        Scene currScene = SceneManager.GetActiveScene();
        if (currScene.name == GameScenes.ch01InstaMain) currentCh = chapter.ch1;
        else if (currScene.name == GameScenes.ch02InstaMain) currentCh = chapter.ch2;
        //else if (currScene.name == GameScenes.ch3InstaMain) currentCh = chapter.ch3;


    }
    // Start is called before the first frame update
    void Start()
    {
        
        if (fbDone != null) fbDone.color = fbInProgress;
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);

        postComp = post.GetComponent<Post>();
        overlayComp = overlay.GetComponent<Overlay>();

        postComp.SetPostData(postData);
        overlayComp.SetOverlayData(postData);

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

        postComp.UpdateIcon();
        overlayComp.SetReplayIcon();       
    }

    public void OpenOverlay()
    {
        overlay.SetActive(true);

        switch (currentCh)
        {
            case chapter.ch1:
                if (overlay.name == GameData.NameOverlay111)
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
                break;
            case chapter.ch2:
                //212, 215, 218, 2110 is interaction, 219 video, 2111 quiz
                Debug.Log("In Open Overlay ------------------------------");

                if (overlay.name == GameData.NameOverlay211)
                {
                    runtimeDataCh2.progressPost211Done = true;
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
                else if (overlay.name == GameData.NameOverlay2112)
                {
                    runtimeDataCh2.progressPost2112Done = true;
                }

                break;
            case chapter.ch3:
                break;
        }

        
        if (overlay.name == GameData.NameOverlay33)
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
        overlayComp.CloseOverlay();
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

        switch (currentCh)
        {
            case chapter.ch1:
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
                break;
            case chapter.ch2:
                if (gameObject.name == GameData.NameEntry21 && runtimeDataCh2.progressPost211Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry22 &&runtimeDataCh2.progress212MuseumDone) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry23 &&runtimeDataCh2.progressPost213Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry24 &&runtimeDataCh2.progressPost214Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry25 &&runtimeDataCh2.progressPost215Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry26 &&runtimeDataCh2.progressPost216Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry27 &&runtimeDataCh2.progressPost217Done) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry28 &&runtimeDataCh2.progressPost218PyritDone) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry29 &&runtimeDataCh2.progressPost219VideoDone) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry210 &&runtimeDataCh2.progressPost2110GWReinigungDone) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry211 &&runtimeDataCh2.progressPost2111QuizDone) setColorDone = true;
                else if (gameObject.name == GameData.NameEntry212 &&runtimeDataCh2.progressPost2112Done) setColorDone = true;
                break;
            case chapter.ch3:
                break;


        }
        

        if (!setColorDone) return;
        if (fbDone != null) fbDone.color = fbDoneColor;
    }

    private void Update()
    {
        switch (currentCh)
        {
            case chapter.ch1:
                SetColorDoneFeedbackImage();

                if (postComp.isPostLocked())
                {
                    if (gameObject.name == GameData.NameEntry113 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry114 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry115 && runtimeDataCh1.post113Done && runtimeDataCh1.post114Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry116 && runtimeDataCh1.video115Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry117 && runtimeDataCh1.interaction116Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry118 && runtimeDataCh1.interaction117Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry119 && runtimeDataCh1.interaction117Done)
                    {
                        postComp.UnlockPost();
                    }
                    else if (gameObject.name == GameData.NameEntry1110 && runtimeDataCh1.quiz119Done)
                    {
                        postComp.UnlockPost();
                        overlayComp.UpdateOverlayText();
                        GameData.chapterOneUnlocked = 1;
                    }
                }
                break;
            case chapter.ch2:
                SetColorDoneFeedbackImage();

                if (gameObject.name == GameData.NameEntry212 && runtimeDataCh2.progressPost2111QuizDone)
                {
                    overlayComp.UpdateOverlayText();
                }

                if (!postComp.isPostLocked()) return;

                if (gameObject.name == GameData.NameEntry23 && runtimeDataCh2.progress212MuseumDone)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry24 && runtimeDataCh2.progress212MuseumDone)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry25 && runtimeDataCh2.progressPost213Done && runtimeDataCh2.progressPost214Done)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry26 && runtimeDataCh2.progressPost215Done)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry27 && runtimeDataCh2.progressPost215Done)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry28 && runtimeDataCh2.progressPost216Done && runtimeDataCh2.progressPost217Done)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry29 && runtimeDataCh2.progressPost218PyritDone)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry210 && runtimeDataCh2.progressPost219VideoDone)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry211 && runtimeDataCh2.progressPost2110GWReinigungDone)
                {
                    postComp.UnlockPost();
                }
                else if (gameObject.name == GameData.NameEntry212 && runtimeDataCh2.progressPost2111QuizDone)
                {
                    postComp.UnlockPost();
                    overlayComp.UpdateOverlayText();
                    GameData.chapterTwoUnlocked = 1;
                }
                break;
        }

        //if(SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        //{
        //    SetColorDoneFeedbackImage();

        //    if (postComp.isPostLocked())
        //    {
        //        if(gameObject.name == GameData.NameEntry113 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry114 && runtimeDataCh1.post111Done && runtimeDataCh1.post112Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry115 && runtimeDataCh1.post113Done && runtimeDataCh1.post114Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry116 && runtimeDataCh1.video115Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry117 && runtimeDataCh1.interaction116Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry118 && runtimeDataCh1.interaction117Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry119 && runtimeDataCh1.interaction117Done)
        //        {
        //            postComp.UnlockPost();
        //        }
        //        else if (gameObject.name == GameData.NameEntry1110 && runtimeDataCh1.quiz119Done)
        //        {
        //            postComp.UnlockPost();
        //            overlayComp.UpdateOverlayText();
        //            GameData.chapterOneUnlocked = 1;
        //        }
        //    }
        //}
        //else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        //{
        //    if (gameObject.name == GameData.NameEntry212 && runtimeDataCh2.progressPost2111QuizDone)
        //    {
        //        overlayComp.UpdateOverlayText();
        //    }

        //    if (!postComp.isPostLocked()) return;

        //    if(gameObject.name == GameData.NameEntry23 && runtimeDataCh2.progress212MuseumDone)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry24 && runtimeDataCh2.progress212MuseumDone)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry25 && runtimeDataCh2.progressPost213Done && runtimeDataCh2.progressPost214Done)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry26 && runtimeDataCh2.progressPost215Done)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry27 && runtimeDataCh2.progressPost215Done)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry28 && runtimeDataCh2.progressPost216Done && runtimeDataCh2.progressPost217Done)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry29 && runtimeDataCh2.progressPost218PyritDone)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry210 && runtimeDataCh2.progressPost219VideoDone)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry211 && runtimeDataCh2.progressPost2110GWReinigungDone)
        //    {
        //        postComp.UnlockPost();
        //    }
        //    else if (gameObject.name == GameData.NameEntry212 && runtimeDataCh2.progressPost2111QuizDone)
        //    {
        //        Debug.Log("Quiz solved: ");
        //        postComp.UnlockPost();
        //        overlayComp.UpdateOverlayText();
        //        GameData.chapterTwoUnlocked = 1;
        //    }

        //}
        if (SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            if (!postComp.isPostLocked()) return;

            if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post32))
            {
                if(gameObject.name == GameData.NameEntry33) postComp.UnlockPost();
                else if (gameObject.name == GameData.NameEntry34) postComp.UnlockPost();
                else if (gameObject.name == GameData.NameEntry35) postComp.UnlockPost();
                else if (gameObject.name == GameData.NameEntry36) postComp.UnlockPost();
                else if (gameObject.name == GameData.NameEntry37) postComp.UnlockPost();
                else if (gameObject.name == GameData.NameEntry38) postComp.UnlockPost();
            }

            if ((gameObject.name == GameData.NameEntry39 || gameObject.name == GameData.NameEntry310) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post33) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post34) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post35) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post36) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post37) &&
                runtimeDataCh3.IsPostDone(ProgressChap3enum.Post38))
            {
                postComp.UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry311 && runtimeDataCh3.IsPostDone(ProgressChap3enum.Post310)) postComp.UnlockPost();
            else if (gameObject.name == GameData.NameEntry312 && runtimeDataCh3.IsPostDone(ProgressChap3enum.Post311)) postComp.UnlockPost();

            else if (gameObject.name == GameData.NameEntry313 || gameObject.name == GameData.NameEntry314)
            {
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post312)) postComp.UnlockPost();
            }

            else if (gameObject.name == GameData.NameEntry316)
            {
                Debug.Log("in entry: " + GameData.NamePost316 + " " + gameObject.name + " " + ProgressChap3enum.Post315.ToString());
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post315))
                {
                    postComp.UnlockPost();
                }
            }

            else if (gameObject.name == GameData.NameEntry317)
            {
                if (runtimeDataCh3.IsPostDone(ProgressChap3enum.Post316))
                {
                    Debug.Log("Quiz solved: ");
                    postComp.UnlockPost();
                    overlayComp.UpdateOverlayText();
                    GameData.chapterThreeUnlocked = 1;

                }
            }
        }
    }
}
