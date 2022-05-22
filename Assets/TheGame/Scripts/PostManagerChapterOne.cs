//Class is resposible for post activation
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    public Button musicOnOff, chaptersHome;
    public Scrollbar scrollbar;
    
    private List <Overlay> overlayList = new List<Overlay>();
    private Overlay[] overlayArray;
    private Dictionary<string, Overlay> dictOverlay= new Dictionary<string, Overlay>();
    
    private SoSfx sfx;
    private SoChapOneRuntimeData runtimeData;
    private SoGameIcons gameIcons;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
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

        EnableDisableMusic(runtimeData.musicOn);

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
        
        runtimeData.CheckInteraction116Done();
        runtimeData.CheckInteraction117Done();

        //potential für verbesserung,nur anschauen wenn ötig
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
