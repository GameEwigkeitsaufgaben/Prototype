//Class is resposible for post activation
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    public Button musicOnOff, chaptersHome;
    public Scrollbar scrollbar;
    public TMP_Text hints;
    
    private List <Overlay> overlayList = new List<Overlay>();
    private Overlay[] overlayArray;
    private Dictionary<string, Overlay> dictOverlay= new Dictionary<string, Overlay>();
    
    private SoSfx sfx;
    private Runtime runtimeData;
    private SoChapOneRuntimeData runtimeDataChap01;
    private SoChapTwoRuntimeData runtimeDataChap02;
    private SoChapThreeRuntimeData runtimeDataChap03;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoGameIcons gameIcons;

    private void Awake()
    {
        runtimeDataChap01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChap02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChap03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        if (SceneManager.GetActiveScene().name == GameScenes.ch01InstaMain)
        {
            runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            runtimeData = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
            runtimeDataChap02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);

            if (runtimeDataChapters.progressWithAdminCh2)
            {
                runtimeDataChap02.SetAllDone();
            }
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            runtimeData = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
            if (runtimeDataChapters.progressWithAdminCh3)
            {
                runtimeDataChap03.SetAllDone();
            }
        }

        gameIcons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);

        musicOnOff.colors = GameColors.GetInteractionColorBlock();
        scrollbar.colors = GameColors.GetInteractionColorBlock();

        overlayArray = overlayParent.GetComponentsInChildren<Overlay>(true);

        foreach (Overlay a in overlayArray)
        {
            dictOverlay.Add(a.name, a);
        }

        gameObject.GetComponent<AudioSource>().clip = sfx.instaMenuBGmusicLoop;
        gameObject.GetComponent<AudioSource>().volume = GameData.maxBGVolumeInsta;
        gameObject.GetComponent<AudioSource>().Play();

        if(runtimeData != null)
        {
            EnableDisableMusic(runtimeData.musicOn);
        }
        else
        {
            Debug.Log("no runtime data");
        }
        
        
        if (runtimeData.postOverlayToLoad != "" && dictOverlay != null)
        {
            dictOverlay[runtimeData.postOverlayToLoad].gameObject.SetActive(true);
            
            if (runtimeData.musicOn)
            {
                Debug.Log("svx is null: " + (sfx == null));
                ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
            }
            
            runtimeData.postOverlayToLoad = "";
        }
    }

    void ReduceVolumeBGMusic(float value)
    {
        gameObject.GetComponent<AudioSource>().volume -= value;
    }

    private void EnableDisableMusic(bool enable)
    {
        if (enable)
        {
            if (!gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Play();
            }
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOn;
        }
        else
        {
            if (gameObject.GetComponent<AudioSource>().isPlaying)
            {
                gameObject.GetComponent<AudioSource>().Stop();
            }
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOff;
        }
    }

    //Called from Inspector BtnMusicOnOff OnClick()
    public void ToggleMusicOnOff()
    {
        runtimeData.musicOn = !runtimeData.musicOn;

        EnableDisableMusic(runtimeData.musicOn);
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
            hints.text = runtimeDataChap01.hintPostUnlock;
            runtimeDataChap01.CheckInteraction116Done();
            runtimeDataChap01.CheckInteraction117Done();
        }
        else if (SceneManager.GetActiveScene().name == GameScenes.ch02InstaMain)
        {
            hints.text = runtimeDataChap02.hintPostUnlock;
        }
        else if(SceneManager.GetActiveScene().name == GameScenes.ch03InstaMain)
        {
            hints.text = runtimeDataChap02.hintPostUnlock;
        }

        //potential f?r verbesserung,nur anschauen wenn n?tig
        if (runtimeData.overlaySoundState == OverlaySoundState.NoSound)
        {
            gameObject.GetComponent<AudioSource>().volume = 0f;
        } 
        else if (runtimeData.overlaySoundState == OverlaySoundState.Opened)
        {
            ReduceVolumeBGMusic(GameData.overlayVolumeInsta);
            runtimeData.overlaySoundState = OverlaySoundState.SoudAjusted;
        }
        else if (runtimeData.overlaySoundState == OverlaySoundState.Closed)
        {
            gameObject.GetComponent<AudioSource>().volume = GameData.maxBGVolumeInsta;
        }
        else if (runtimeData.overlaySoundState == OverlaySoundState.SoudAjusted)
        {
            gameObject.GetComponent<AudioSource>().volume = GameData.overlayVolumeInsta;
        }
    }
}
