using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum StatesInkohlung
{
    PlantDeath,
    PeatIsFormed,
    Repetition,
    TectonicForces,
    None
}

public class ManagerCoalification : MonoBehaviour
{
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapOneRuntimeData runtimeData;
    private SoMuseumConfig configMuseum;
    private SoSfx sfx;

    public Slider slider;
    public TMP_Text infoText;
    [SerializeField] private StatesInkohlung statesInkohlung;
    [SerializeField] private AudioSource audioSrcAtmo, audioSrcTrees, audioSrcRegen, audioSourceDruck;
    public Button btnGoToMuseum;
    public AudioSource audioSrcMusic;

    public bool sliderMoving = false;
    public bool enableAnim = false;
    public Animator anim;
    public float oldSliderVAl = 0f;

    private bool startTreeFalling, startFlutung, startRegen;


    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeData = runtimeDataChapters.LoadChap1RuntimeData();
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        sfx = runtimeDataChapters.LoadSfx();
        audioSrcAtmo.clip = sfx.atmoCoalificationSfx;
        audioSrcAtmo.playOnAwake = false;
        audioSrcAtmo.loop = true;

        audioSrcRegen.clip = sfx.regen;
        audioSrcRegen.playOnAwake = false;
        audioSrcRegen.loop = true;

        audioSrcTrees.clip = sfx.treesCoalificationSfx;
        audioSrcTrees.playOnAwake = false;

        runtimeDataChapters.SetAndStartMusic(audioSrcMusic, sfx.instaMenuMusicLoop);

        configMuseum = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
    }

    private void Start()
    {
        SetStateAndText();
        anim.enabled = false;
    }

    public void SetStateAndText()
    {
        sliderMoving = true;

        if(slider.value >= 0.06 && slider.value <= 0.08 ||
            slider.value >= 0.13 && slider.value <= 0.12 ||
            slider.value >= 0.28 && slider.value <= 0.43)
        {
            if (!audioSourceDruck.isPlaying) audioSourceDruck.Play();
        }

        if (slider.value <= 0.048)
        {
            statesInkohlung = StatesInkohlung.PlantDeath;

            if (slider.value >= 0.03 && slider.value <= 0.033) startTreeFalling = true;
        }
        else if (slider.value > 0.048 && slider.value <= 0.116) statesInkohlung = StatesInkohlung.PeatIsFormed;
        else if (slider.value > 0.116 && slider.value <= 0.658)
        {
            statesInkohlung = StatesInkohlung.Repetition;

            if (slider.value >= 0.19 && slider.value <= 0.192) startTreeFalling = true;
            if (slider.value >= 0.56 && slider.value <= 0.562) startTreeFalling = true;
            if (audioSrcRegen.isPlaying) audioSrcRegen.Stop();
        }
        else if (slider.value > 0.658 && slider.value < 1.0)
        {
            statesInkohlung = StatesInkohlung.TectonicForces;

            if (slider.value >= 0.7 && slider.value <= 0.87)
            {
                    startRegen = true;
            }

            if (slider.value >= 0.89 && slider.value <= 0.90)
            {
                if (audioSrcAtmo.isPlaying)
                {
                    audioSrcAtmo.Stop();
                }
            }

            if (slider.value >= 0.91 && slider.value <= 0.98)
            {
                startFlutung = true;
                audioSrcRegen.Stop();
            }
        }
        else if (slider.value == 1) runtimeData.isCoalifiationDone = true;

        switch (statesInkohlung)
        {
            case StatesInkohlung.PlantDeath:
                infoText.text = configMuseum.pflanzenSterben;
                if(!audioSrcAtmo.isPlaying)audioSrcAtmo.Play();
                break;
            case StatesInkohlung.PeatIsFormed:
                infoText.text = configMuseum.torfEntsteht;
                break;
            case StatesInkohlung.Repetition:
                infoText.text = configMuseum.Wiederholung;
                if (!audioSrcAtmo.isPlaying) audioSrcAtmo.Play();
                break;
            case StatesInkohlung.TectonicForces:
                audioSrcAtmo.Stop();
                infoText.text = configMuseum.tektonischeKraefte;
                break;
            case StatesInkohlung.None:
                infoText.text = GameData.EmptyString;
                break;
        }

        if (startTreeFalling)
        {
            if (!audioSrcTrees.isPlaying)
            {
                audioSrcTrees.Play();
                startTreeFalling = false;
            }
        }

        if (startFlutung)
        {
            if (!audioSrcAtmo.isPlaying)
            {
                audioSrcAtmo.Play();
                startFlutung = false;
            }
        }

        if (startRegen)
        {
            if (!audioSrcRegen.isPlaying)
            {
                audioSrcRegen.Play();
                startRegen = false;
            }
        }

        if (btnGoToMuseum.interactable) return;

        btnGoToMuseum.interactable = runtimeData.isCoalifiationDone;


    }

    public void GoBackToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void Update()
    {
        if (sliderMoving)
        {
            if (!anim.enabled) anim.enabled = true;
        }
        else
        {
            if (anim.enabled) anim.enabled = false;
        }

        sliderMoving = false;
    }
}
