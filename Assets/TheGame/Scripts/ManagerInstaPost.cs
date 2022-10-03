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
                scrollbar.value = runtimeDataCh3.instaSliderPos;
                break;
        }
        

        if (runtimeDataChapters.musicVolume != GameData.defaultVolumeInsta) audioSrcBGInsta.volume = runtimeDataChapters.musicVolume;
    }

    private void Awake()
    {
        //Get active Scene
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

        //audio setting

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

        audioSrcBGInsta = GetComponent<AudioSource>();
        audioSrcBGInsta.clip = sfx.instaMenuMusicLoop;
        audioSrcBGInsta.volume = runtimeDataChapters.musicVolume;
        audioSrcBGInsta.Play();
        EnableDisableMusic(runtimeDataChapters.musicOn);
        audioSrcBGInsta.mute = !runtimeDataChapters.musicOn;


        switch (currentCH)
        {
            case chapter.ch1:
                runtimeDataCh1 = runtimeDataChapters.LoadChap1RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh1) runtimeDataCh1.SetAllDone();
                //EnableDisableMusic(runtimeDataCh1.musicOn);
                break;
            case chapter.ch2:
                runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh2) runtimeDataCh2.SetAllDone();
                //EnableDisableMusic(runtimeDataCh2.musicOn);
                break;
            case chapter.ch3:
                runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
                if (runtimeDataChapters.progressWithAdminCh3) runtimeDataCh3.SetAllDone();
                //EnableDisableMusic(runtimeDataCh3.musicOn);
                break;
        }



        switch (currentCH)
        {
            case chapter.ch1:

                if (runtimeDataCh1.postOverlayToLoad != "" && dictOverlay != null)
                {
                    dictOverlay[runtimeDataCh1.postOverlayToLoad].gameObject.SetActive(true);

                    runtimeDataCh1.postOverlayToLoad = "";
                }

                break;
            case chapter.ch2:
                if (runtimeDataCh2.postOverlayToLoad != "" && dictOverlay != null)
                {
                    dictOverlay[runtimeDataCh2.postOverlayToLoad].gameObject.SetActive(true);

                    runtimeDataCh2.postOverlayToLoad = "";
                }
                break;
            case chapter.ch3:
                if (runtimeDataCh3.postOverlayToLoad != "" && dictOverlay != null)
                {
                    dictOverlay[runtimeDataCh3.postOverlayToLoad].gameObject.SetActive(true);

                    runtimeDataCh3.postOverlayToLoad = "";
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
            case chapter.ch3:
                runtimeDataCh3.instaSliderPos = scrollbar.value;
                break;
        }
    }


    private void SetMusicON(bool musicOn)
    {
        runtimeDataChapters.musicOn = musicOn;
        //switch (currentCH)
        //{
        //    case chapter.ch1:
        //        runtimeDataCh1.musicOn = musicOn;
        //        break;
        //    case chapter.ch2:
        //        runtimeDataCh2.musicOn = musicOn;
        //        break;
        //    case chapter.ch3:
        //        runtimeDataCh3.musicOn = musicOn;
        //        break;
        //}
    }

    void ReduceVolumeBGMusic(float value)
    {
        audioSrcBGInsta.volume -= value;
        runtimeDataChapters.musicVolume = audioSrcBGInsta.volume;

        if (audioSrcBGInsta.volume == 1.0f)
        {
            btnVolPlus.interactable = false;
        }
        
        else if (audioSrcBGInsta.volume == 0.0f)
        {
            btnVolMinus.interactable = false;
            btnVolPlus.interactable = false;
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOff;
            SetMusicON(false);
        }
        
        else if (audioSrcBGInsta.volume != 0.0f)
        {
            btnVolMinus.interactable = true;
            btnVolPlus.interactable = true;
        }
    }

    void IncreaseVolumeMusic(float value)
    {
        audioSrcBGInsta.volume += value;
        runtimeDataChapters.musicVolume = audioSrcBGInsta.volume;

        if (audioSrcBGInsta.volume == 1.0f)
        {
            btnVolPlus.interactable = false;
        }

        else if(audioSrcBGInsta.volume != 0.0f)
        {
            btnVolMinus.interactable = true;
            btnVolPlus.interactable = true;
        }
    }

    private void EnableDisableMusic(bool enable)
    {
        if (enable)
        {
            if (!audioSrcBGInsta.isPlaying)
            {
                audioSrcBGInsta.Play();
            }
            
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOn;
            SetMusicON(true);

            if (audioSrcBGInsta.volume > 0.0f && audioSrcBGInsta.volume < 1.0f)
            {
                btnVolMinus.interactable = true;
                btnVolPlus.interactable = true;
            }

            else if (audioSrcBGInsta.volume == 1.0f)
            {
                btnVolMinus.interactable = true;
                btnVolPlus.interactable = false;
            }

            else if (audioSrcBGInsta.volume == 0.0f)
            {
                IncreaseVolumeMusic(0.1f);
                //btnVolMinus.interactable = false;
                //btnVolPlus.interactable = true;

            }
        }
        else
        {
            if (audioSrcBGInsta.isPlaying)
            {
                audioSrcBGInsta.Stop();
            }

            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOff;
            SetMusicON(false);
            btnVolPlus.interactable = btnVolMinus.interactable = false;
        }
    }

    public void ChangeVolume(int vol)
    {
        switch((volume)vol)
        {
            case volume.increase:
               IncreaseVolumeMusic(volReduceSteps);
            break;
            case volume.decrease:
                ReduceVolumeBGMusic(volReduceSteps);
            break;
        }
    }

    //Called from Inspector BtnMusicOnOff OnClick()
    public void ToggleMusicOnOff()
    {
        runtimeDataChapters.musicOn = !runtimeDataChapters.musicOn;

        //switch (currentCH)
        //{
        //    case chapter.ch1:
        //        runtimeDataCh1.musicOn = !runtimeDataCh1.musicOn;
        //        break;
        //    case chapter.ch2:
        //        runtimeDataCh2.musicOn = !runtimeDataCh2.musicOn;
        //        break;
        //    case chapter.ch3: 
        //        runtimeDataCh3.musicOn = !runtimeDataCh3.musicOn;
        //        break;
        //}
        
        switch (currentCH)
        {
            case chapter.ch1:
                EnableDisableMusic(runtimeDataChapters.musicOn);
                break;
            case chapter.ch2:
                EnableDisableMusic(runtimeDataChapters.musicOn);
                break;
            case chapter.ch3:
                EnableDisableMusic(runtimeDataChapters.musicOn);
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

        //switch(currentCH)
        //    {
        //    case chapter.ch1:
        //        if (runtimeDataCh1.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
        //        {
        //            audioSrcBGInsta.volume = 0f;
        //        }
        //        else if (runtimeDataCh1.overlaySoundState == OverlaySoundState.Opened)
        //        {
        //            //ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
        //            //runtimeDataCh1.overlaySoundState = OverlaySoundState.SoudAjusted;
        //        }
        //        break;
        //    case chapter.ch2:
        //        if (runtimeDataCh2.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
        //        {
        //            audioSrcBGInsta.volume = 0f;
        //        }
        //        else if (runtimeDataCh2.overlaySoundState == OverlaySoundState.Opened)
        //        {
        //            ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
        //            runtimeDataCh2.overlaySoundState = OverlaySoundState.SoudAjusted;
        //        }
        //        break;
        //    case chapter.ch3:
        //        if (runtimeDataCh3.overlaySoundState == OverlaySoundState.NoSound && (audioSrcBGInsta.volume != 0f))
        //        {
        //            audioSrcBGInsta.volume = 0f;
        //        }
        //        else if (runtimeDataCh3.overlaySoundState == OverlaySoundState.Opened)
        //        {
        //            ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
        //            runtimeDataCh3.overlaySoundState = OverlaySoundState.SoudAjusted;
        //        }
        //        break;

        //}

    }
}
