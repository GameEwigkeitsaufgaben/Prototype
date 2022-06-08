using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//noch unsauber, videoComplete einfügen statt done (done ist für progress). 
public enum TVState
{ 
    VideoPlays,
    VideoPauses,
    VideoStop
}
public class ManagerMuseumTV : MonoBehaviour
{
    public WebGlVideoPlayer webglVideoPlayer;
    public RawImage rawImage;

    public Button btnPlay, btnPause;

    private SoGameIcons icons;
    public TVState tvState;
    private SoChapTwoRuntimeData runtimeDataCh02;

    private void Awake()
    {
        icons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
    }

    // Start is called before the first frame update
    void Start()
    {
        btnPlay.GetComponent<Image>().sprite = icons.tvPlayIcon;
        btnPause.GetComponent<Image>().sprite = icons.tvPauseIcon;
        btnPause.gameObject.SetActive(false);
        tvState = TVState.VideoStop;
    }

    public void StartTheVideo()
    {
        Debug.Log("before" + tvState);
        switch (tvState)
        {
            case TVState.VideoStop:
                tvState = TVState.VideoPlays;
                break;
            case TVState.VideoPlays:
                tvState = TVState.VideoPauses;
                break;
            case TVState.VideoPauses:
                tvState = TVState.VideoPlays;
                break;
        }

        Debug.Log("after" + tvState);

        switch (tvState)
        {
            case TVState.VideoStop:
                btnPause.gameObject.SetActive(false);
                btnPlay.gameObject.SetActive(true);
                break;
            case TVState.VideoPlays:
                btnPause.gameObject.SetActive(true);
                btnPlay.gameObject.SetActive(false);
                break;
            case TVState.VideoPauses:
                btnPause.gameObject.SetActive(false);
                btnPlay.gameObject.SetActive(true);
                break;
        }

        webglVideoPlayer.StartVid("none", "Watherdrop.mp4", rawImage);
    }

    // Update is called once per frame
    void Update()
    {
        if (runtimeDataCh02.interactTVDone && tvState == TVState.VideoPlays)
        {
            tvState = TVState.VideoStop;
            btnPause.gameObject.SetActive(false);
            btnPlay.gameObject.SetActive(true);
        }
    }
}
