//Class is resposible for post activation
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum chapter
{
    ch1,
    ch2,
    ch3
}

public enum volume
{
    increase = 1,
    decrease = -1
}

public class ManagerInstaPost : MonoBehaviour
{
    public GameObject overlayParent;
    public Button musicOnOff, chaptersHome, btnVolMinus, btnVolPlus;
    public Scrollbar scrollbar;
    public TMP_Text hints;
    public float volReduceSteps = 0.1f;
    
    private List <Overlay> overlayList = new List<Overlay>();
    private Overlay[] overlayArray;
    private Dictionary<string, Overlay> dictOverlay= new Dictionary<string, Overlay>();
    
    private SoSfx sfx;
    //private Runtime runtimeDataCh1;
    private SoChapOneRuntimeData runtimeDataCh1;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoGameIcons gameIcons;

    public chapter currentCH;
    private AudioSource audioSrcBGInsta;

    public int logAufrufe = 0;

    public void Start()
    {
        //pos saved in inspector in scrollbar call function SaveSliderPos() from this class
        switch (currentCH)
        {
            case chapter.ch1:
                scrollbar.value = runtimeDataCh1.instaSliderPos;
                break;
            case chapter.ch2:
                scrollbar.value = runtimeDataCh2.instaSliderPos;
                break;
            case chapter.ch3:
                //scrollbar.value = runtimeDataCh3.instaSliderPos;
                break;
        }
        
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            currentCH = chapter.ch1;
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            currentCH = chapter.ch2;
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            currentCH = chapter.ch3;
        }

        audioSrcBGInsta = GetComponent<AudioSource>();
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        gameIcons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);

        overlayArray = overlayParent.GetComponentsInChildren<Overlay>(true);

        foreach (Overlay a in overlayArray)
        {
            dictOverlay.Add(a.name, a);
        }

        musicOnOff.colors = GameColors.GetInteractionColorBlock();
        scrollbar.colors = GameColors.GetInteractionColorBlock();
        audioSrcBGInsta.clip = sfx.instaMenuBGmusicLoop;
        audioSrcBGInsta.volume = GameData.maxBGVolumeInsta;
        audioSrcBGInsta.Play();


        switch (currentCH)
        {
            case chapter.ch1:
                runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh1) runtimeDataCh1.SetAllDone();
                EnableDisableMusic(runtimeDataCh1.musicOn);
                break;
            case chapter.ch2:
                runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh2) runtimeDataCh2.SetAllDone();
                EnableDisableMusic(runtimeDataCh2.musicOn);
                break;
            case chapter.ch3:
                runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh3) runtimeDataCh3.SetAllDone();
                EnableDisableMusic(runtimeDataCh3.musicOn);
                break;
        }

        switch (currentCH)
        {
            case chapter.ch1:

                if (runtimeDataCh1.postOverlayToLoad != "" && dictOverlay != null)
                {
                    dictOverlay[runtimeDataCh1.postOverlayToLoad].gameObject.SetActive(true);

                    if (runtimeDataCh1.musicOn)
                    {
                        ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
                    }

                    runtimeDataCh1.postOverlayToLoad = "";
                }

                break;
            case chapter.ch2:
                if (runtimeDataCh2.postOverlayToLoad != "" && dictOverlay != null)
                {
                    Debug.Log("Overlay to load" + runtimeDataCh2.postOverlayToLoad);
                    dictOverlay[runtimeDataCh2.postOverlayToLoad].gameObject.SetActive(true);

                    if (runtimeDataCh2.musicOn)
                    {
                        ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
                    }

                    runtimeDataCh2.postOverlayToLoad = "";
                }

                break;
        }
    }

    public void SaveSliderPos()
    {
        switch (currentCH)
        {
            case chapter.ch1:
                runtimeDataCh1.instaSliderPos = scrollbar.value;
                break;
            case chapter.ch2:
                runtimeDataCh2.instaSliderPos = scrollbar.value;
                break;
        }
    }

    void ReduceVolumeBGMusic(float value)
    {
        audioSrcBGInsta.volume -= value;
    }

    void IncreaseVolumeMusic(float value)
    {
        audioSrcBGInsta.volume += value;
    }

    private void EnableDisableMusic(bool enable)
    {
        if (enable)
        {
            if (audioSrcBGInsta.volume == 0f) IncreaseVolumeMusic(volReduceSteps);
            btnVolMinus.interactable = true;
            if (audioSrcBGInsta.volume != 1.0f) btnVolPlus.interactable = true;
            if (!audioSrcBGInsta.isPlaying)
            {
                audioSrcBGInsta.Play();
            }
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOn;
        }
        else
        {
            if (audioSrcBGInsta.isPlaying)
            {
                audioSrcBGInsta.Stop();
            }
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOff;
            btnVolPlus.interactable = btnVolMinus.interactable = false;
        }
    }

    public void ChangeVolume(int vol)
    {
        float oldVol = audioSrcBGInsta.volume;

        switch((volume)vol)
        {
            case volume.increase:
                if(audioSrcBGInsta.volume <= 1.0f) IncreaseVolumeMusic(volReduceSteps);
            break;
            case volume.decrease:
                if(audioSrcBGInsta.volume >= 0.0f) ReduceVolumeBGMusic(volReduceSteps);
            break;
        }

        if(oldVol != audioSrcBGInsta.volume)
        {
            if (audioSrcBGInsta.volume == 0f) ToggleMusicOnOff();
            else
            {
                if (oldVol == 0) ToggleMusicOnOff();
            }
        }

        btnVolPlus.interactable = (audioSrcBGInsta.volume != 1f) ? true : false;
        btnVolMinus.interactable = (audioSrcBGInsta.volume != 0f) ? true : false;
    }

    //Called from Inspector BtnMusicOnOff OnClick()
    public void ToggleMusicOnOff()
    {
        switch (currentCH)
        {
            case chapter.ch1: runtimeDataCh1.musicOn = !runtimeDataCh1.musicOn;
                break;
            case chapter.ch2: runtimeDataCh2.musicOn = !runtimeDataCh2.musicOn;
                break;
            case chapter.ch3: runtimeDataCh3.musicOn = !runtimeDataCh3.musicOn;
                break;
        }
        
        switch (currentCH)
        {
            case chapter.ch1:
                EnableDisableMusic(runtimeDataCh1.musicOn);
                break;
            case chapter.ch2:
                EnableDisableMusic(runtimeDataCh2.musicOn);
                break;
            case chapter.ch3:
                EnableDisableMusic(runtimeDataCh3.musicOn);
                break;
        }
       
    }

    private void PrintAllDictKeys(string calledFrom)
    {
        foreach (string key in dictOverlay.Keys)
        {
            Debug.Log(calledFrom + ": " + key);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            hints.text = runtimeDataCh1.hintPostUnlock;
            runtimeDataCh1.CheckInteraction116Done();
            runtimeDataCh1.CheckInteraction117Done();
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            hints.text = runtimeDataCh2.hintPostUnlock;
        }
        else if(SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            hints.text = runtimeDataCh3.hintPostUnlock;
        }

        switch(currentCH)
            {
            case chapter.ch1:
                if (runtimeDataCh1.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
                {
                    audioSrcBGInsta.volume = 0f;
                }
                else if (runtimeDataCh1.overlaySoundState == OverlaySoundState.Opened)
                {
                    //ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
                    //runtimeDataCh1.overlaySoundState = OverlaySoundState.SoudAjusted;
                }
                break;
            case chapter.ch2:
                if (runtimeDataCh2.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
                {
                    audioSrcBGInsta.volume = 0f;
                }
                else if (runtimeDataCh2.overlaySoundState == OverlaySoundState.Opened)
                {
                    ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
                    runtimeDataCh2.overlaySoundState = OverlaySoundState.SoudAjusted;
                }
                break;
            case chapter.ch3:
                if (runtimeDataCh3.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
                {
                    audioSrcBGInsta.volume = 0f;
                }
                else if (runtimeDataCh3.overlaySoundState == OverlaySoundState.Opened)
                {
                    ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
                    runtimeDataCh3.overlaySoundState = OverlaySoundState.SoudAjusted;
                }
                break;

        }

    }
}
