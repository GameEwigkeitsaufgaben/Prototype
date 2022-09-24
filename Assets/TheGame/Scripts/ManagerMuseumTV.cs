using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//noch unsauber, videoComplete einf�gen statt done (done ist f�r progress). 
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
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    public AudioSource audioSrcAtmo;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        icons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        runtimeDataCh02 = runtimeDataChapters.LoadChap2RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();
    }

    // Start is called before the first frame update
    void Start()
    {
        btnPlay.GetComponent<Image>().sprite = icons.tvPlayIcon;
        btnPause.GetComponent<Image>().sprite = icons.tvPauseIcon;
        btnPause.gameObject.SetActive(false);
        tvState = TVState.VideoStop;
        audioSrcAtmo.clip = sfx.atmoMuseum;
        audioSrcAtmo.Play();
    }

    public void StartTheVideo()
    {
        Debug.Log("Start THE VIDEO----------------------------");
        Debug.Log("before" + tvState);
        audioSrcAtmo.Pause();

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

        webglVideoPlayer.StartVid("none", "ch2-seqGrundwasser.mp4", rawImage);
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSrcAtmo.isPlaying && tvState == TVState.VideoStop)
        {
            audioSrcAtmo.UnPause();
        }
        if (runtimeDataCh02.interactTVDone && tvState == TVState.VideoPlays)
        {
            tvState = TVState.VideoStop;
            btnPause.gameObject.SetActive(false);
            btnPlay.gameObject.SetActive(true);
        }
    }
}
