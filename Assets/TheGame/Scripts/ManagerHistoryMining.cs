using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum Century
{
    myth,
    century13,
    century16,
    century19,
    century21,
    none
}

public class ManagerHistoryMining : MonoBehaviour
{
    //https://forum.unity.com/threads/many-text-mesh-pro-elements-in-a-scene-what-is-a-possible-solution.665614/ TMP Performance
    //https://docs.unity3d.com/Packages/com.unity.render-pipelines.universal@10.9/manual/requirements.html urp for 2020.3.18f1
    
    public Slider slider;
    public Button btnBackToMuseum; 
    public GameObject btnUrlToSage;
    public TextMeshProUGUI centuryText;
    public GameObject imgSanduhr;
    [SerializeField] private AudioSource audioSrcTime, audioSrcMusic, audioSrcAtmoMuseum;
    
    [Header("Assigned at Runtime")]
    public SoMuseumConfig museumConfig;
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private AudioClip timeSound;

    public Century sliderCentury;

    [Header("Range Slider")]
    public Vector2 sageRange;
    public Vector2 thirteenthRange;
    public Vector2 sixteenthRange;
    public Vector2 nineteenthRange;
    public Vector2 twentyfirstRange;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        museumConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        sfx = runtimeDataChapters.LoadSfx();
    }

    private void Start()
    {
        sliderCentury = Century.none;
        btnBackToMuseum.interactable = runtimeData.isMythDone;

        runtimeData.cursorDefault = null;

        audioSrcTime.loop = true;
        audioSrcTime.playOnAwake = false;

        audioSrcAtmoMuseum.clip = sfx.atmoMuseum;
        audioSrcAtmoMuseum.loop = true;
        audioSrcAtmoMuseum.Play();

        runtimeDataChapters.SetAndStartMusic(audioSrcMusic, sfx.instaMenuMusicLoop);
    }

    //public void OpenUrlSage()
    //{
    //    Application.OpenURL("https://www.hamsterkiste-lesen-und-schreiben.de/schweinehirt");
    //}

    public void GoBackToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void Update()
    {
        //Get Century based on slider postiion
        if (slider.normalizedValue > sageRange.x && slider.normalizedValue < sageRange.y) sliderCentury = Century.myth;
        else if (slider.normalizedValue > thirteenthRange.x && slider.normalizedValue < thirteenthRange.y) sliderCentury = Century.century13;
        else if (slider.normalizedValue > sixteenthRange.x && slider.normalizedValue < sixteenthRange.y) sliderCentury = Century.century16;
        else if (slider.normalizedValue > nineteenthRange.x && slider.normalizedValue < nineteenthRange.y) sliderCentury = Century.century19;
        else if (slider.normalizedValue > twentyfirstRange.x) sliderCentury = Century.century21; 
        else sliderCentury = Century.none;

        if(btnUrlToSage.activeSelf) btnUrlToSage.gameObject.SetActive(false);

        //Do someting based on century 
        switch (sliderCentury)
        {
            case Century.myth:
                imgSanduhr.gameObject.SetActive(false);
                centuryText.text = museumConfig.textmyth;
                btnUrlToSage.gameObject.SetActive(true);
                audioSrcTime.mute = false;
                audioSrcTime.clip = sfx.sageFeuer;
                if (!audioSrcTime.isPlaying)
                {
                    audioSrcTime.Play();
                }
                break;
            case Century.century13:
                imgSanduhr.gameObject.SetActive(false);
                centuryText.text = museumConfig.textCentury13;
                audioSrcTime.mute = false;
                audioSrcTime.clip = sfx.jh13;
                if (!audioSrcTime.isPlaying)
                {
                    audioSrcTime.Play();
                }
                break;
            case Century.century16:
                imgSanduhr.gameObject.SetActive(false);
                centuryText.text = museumConfig.textCentury16;
                audioSrcTime.mute = false;
                audioSrcTime.clip = sfx.jh16;
                if (!audioSrcTime.isPlaying)
                {
                    audioSrcTime.Play();
                }
                break;
            case Century.century19:
                imgSanduhr.gameObject.SetActive(false);
                centuryText.text = museumConfig.textCentury19;
                audioSrcTime.mute = false;
                audioSrcTime.clip = sfx.jh19;
                if (!audioSrcTime.isPlaying)
                {
                    audioSrcTime.Play();
                }
                break;
            case Century.century21:
                imgSanduhr.gameObject.SetActive(false);
                centuryText.text = museumConfig.textCentury21;
                runtimeData.isMythDone = true;
                btnBackToMuseum.interactable = true;
                audioSrcTime.mute = false;
                audioSrcTime.clip = sfx.jh21;
                if (!audioSrcTime.isPlaying)
                {
                    audioSrcTime.Play();
                }
                break;
            default:
                centuryText.text = GameData.EmptyString;
                imgSanduhr.gameObject.SetActive(true);
                audioSrcTime.mute = true;
                break;
        }
    }
}
