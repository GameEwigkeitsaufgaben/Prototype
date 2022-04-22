//Class is resposible for post activation
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostManagerChapterOne : MonoBehaviour
{
    public GameObject overlayParent;
    public Button musicOnOff;
    
    private List <Overlay> overlayList = new List<Overlay>();
    private Overlay[] overlayArray;
    private Dictionary<string, Overlay> dictOverlay= new Dictionary<string, Overlay>();
    private SoSfx sfx;
    private SoChapOneRuntimeData runtimeData;
    private SoGameIcons gameIcons;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        gameIcons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);

        EnableDisableMusic(runtimeData.musicOn);
        //musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOn;
        //sfx.PlayClip(gameObject, sfx.instaMenuBGmusicLoop);

        overlayArray = overlayParent.GetComponentsInChildren<Overlay>(true);

        foreach (Overlay a in overlayArray)
        {
            dictOverlay.Add(a.name, a);
        }

        if (GameData.overlayToLoad != "" && dictOverlay != null)
        {
            dictOverlay[GameData.overlayToLoad].gameObject.SetActive(true);
            Debug.Log("Time to stop clip instabgmusic");
            sfx.StopClip(sfx.instaMenuBGmusicLoop);
            GameData.overlayToLoad = "";
        }
    }

    private void EnableDisableMusic(bool enable)
    {
        if (enable)
        {
            sfx.PlayClip(gameObject, sfx.instaMenuBGmusicLoop);
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOn;
        }
        else
        {
            sfx.StopClip(sfx.instaMenuBGmusicLoop);
            musicOnOff.GetComponent<Image>().sprite = gameIcons.musicOff;
        }
    }


    public void ToggleMusicOnOff()
    {
        runtimeData.musicOn = !runtimeData.musicOn;

        EnableDisableMusic(runtimeData.musicOn);
    }

    private void PrintAllDictKeys(string calledFrom)
    {
        //Debug.Log("Dict length in "+ calledFrom +" "+ dictOverlay.Count);
        foreach (string key in dictOverlay.Keys)
        {
            Debug.Log(calledFrom + ": " + key);
        }
    }

    private void Update()
    {
        if (!runtimeData.interaction116Done)
        {
            if (runtimeData.sole1done && runtimeData.sole2done && runtimeData.sole3BewetterungDone && runtimeData.sole3GebaeudeDone && runtimeData.trainRideInDone && runtimeData.trainRideOutDone && runtimeData.isLongwallCutterDone)
            {
                runtimeData.interaction116Done = true;
            }
        }
        
    }
}
