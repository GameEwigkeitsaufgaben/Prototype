using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WebGlVideoPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    
    private string videoPostName;
    
    private GameObject rawImage;
    private bool videoIsPlaying = false;
    private bool videoSetUpDone = false;
    private SoSfx sfx; //will be assigned from Entry;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChapTwoRuntimeData runtimeDataCh2;


    private void Awake()
    {
        //ToDo: null check
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
    }
    private void Start()
    {
        Debug.Log("Set Video Url and configurations: " + videoPlayer.url);
    }

    public void SetSFX(SoSfx tmpSFX)
    {
        sfx = tmpSFX;
    }

    public bool IsVideoPaused()
    {
        return videoPlayer.isPaused;
    }

    public bool IsVideoPlaying()
    {
        return videoPlayer.isPlaying;
    }

    public void StartVid(string videoPostName, string videoName, RawImage imgRaw)
    {
       
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            return;
        }

        if (!videoSetUpDone)
        {
            SetVideo(videoName, imgRaw);
            videoSetUpDone = true;
        }

        videoPlayer.Play();
        videoPlayer.loopPointReached += SetTVStationDone;
    }

    public void StartTheVideo(string videoPostName, string videoName, RawImage imgRaw)
    {
        if (!videoSetUpDone)
        {
            SetVideo(videoName, imgRaw);
            videoSetUpDone = true;
        }

        this.videoPostName = videoPostName;

        if (videoPlayer.isPlaying) return;
        
        if (!videoIsPlaying)
        {
            if(sfx != null)
            {
                sfx.StopClip(sfx.instaMenuMusicLoop);
            }

            videoPlayer.Play();

            if(videoPostName == GameData.NameOverlay115) 
            {
                videoPlayer.loopPointReached += SetVideopostToRead;
                //runtimeDataCh1.overlaySoundState = OverlaySoundState.NoSound;
            }

            if (videoPostName == GameData.NameOverlay219)
            {
                videoPlayer.loopPointReached += SetVideoDone;
            }

            videoIsPlaying = true;
            //runtimeData.videoPlaying = true;
            //runtimeDataCh1.overlaySoundState = OverlaySoundState.NoSound;
        }
        else
        {
            if (videoPlayer.isPlaying)
            {
                videoPlayer.Stop();
            }

            videoIsPlaying = false;
            runtimeDataCh1.videoPlaying = false;
            runtimeDataCh1.overlaySoundState = OverlaySoundState.SoudAjusted;
        }

        if (SceneManager.GetActiveScene().name == GameScenes.ch02Museum) return;
        if (SceneManager.GetActiveScene().name == GameScenes.ch02MuseumTV) return;
        rawImage.transform.parent.transform.parent.GetComponent<Overlay>().SetIconActive(!videoIsPlaying);
    }

    public void PlayTheVideo(VideoPlayer vp)
    {
        videoPlayer.Play();
        videoPlayer.loopPointReached += SetVideopostToRead;
    }

    public void SetVideo(string videoName, RawImage imgRaw)
    {
        rawImage = imgRaw.gameObject;
        rawImage.GetComponent<RawImage>().texture = videoPlayer.targetTexture;
        
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoName);

        videoPlayer.isLooping = false;
    }

    public void StopTheVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
        }

        videoIsPlaying = false;
        runtimeDataCh1.videoPlaying = false;
    }

    public void SetTVStationDone(VideoPlayer vp)
    {
        runtimeDataCh2.interactTVDone = true;
    }

    public void SetVideoDone(VideoPlayer vp)
    {
        videoIsPlaying = false;
        runtimeDataCh2.progressPost219VideoDone = true;
    }
  
    public void SetVideopostToRead(VideoPlayer vp)
    {
        videoIsPlaying = false;
        runtimeDataCh1.overlaySoundState = OverlaySoundState.SoudAjusted;

        runtimeDataCh1.video115Done = true;
    }

}