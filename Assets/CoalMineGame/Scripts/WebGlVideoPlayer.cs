using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class WebGlVideoPlayer : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    
    public string videoFileName;
    public GameObject rawImage;


    private void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, videoFileName);
        videoPlayer.isLooping = false;
        //rawImage.SetActive(false);
        Debug.Log("Set Video Url and configurations: " + videoPlayer.url);

    }

    public void StartTheVideo()
    {
        if (!videoPlayer.isPlaying)
        {
            // rawImage.SetActive(true);
            rawImage.GetComponent<RawImage>().color = new Color(255,255,255,255);
            videoPlayer.Play();
            Debug.Log("video is playing: " + videoPlayer.isPlaying);
            //gameData.introVideoIsPlaying = true;
            videoPlayer.loopPointReached += RestoreIntroVideo;

        }
    }

    public void StopTheVideo()
    {
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Stop();
            GameData.restorIntroVideo = true;
        }
    }

    public void RestoreIntroVideo(VideoPlayer vp)
    {
        if (!GameData.introPlayedOnce)
        {
            GameData.introPlayedOnce = true;
        }
        
        GameData.restorIntroVideo = true;
        rawImage.GetComponent<RawImage>().color = new Color(255, 255, 255, 0);
    }

}