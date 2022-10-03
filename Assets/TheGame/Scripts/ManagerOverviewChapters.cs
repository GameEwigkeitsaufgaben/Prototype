using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Chapters
{
    Ch01General = 1,
    Ch02Grubenwasser = 2,
    Ch03Ewigkeitsaufgabe = 3
}

public class ManagerOverviewChapters : MonoBehaviour
{
    [SerializeField] private Button btnCredits, btnFotoplatz, btnFwb;
    [SerializeField] private TMP_Text lawNotice;
    [SerializeField] private bool allChapDone;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private AudioSource audioSrc;
    public AudioSource audioSrcAtmo;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        btnFotoplatz.interactable = false;
        audioSrc = GetComponent<AudioSource>();
        sfx = runtimeDataChapters.LoadSfx();
    }

    private void Start()
    {
        btnCredits.colors = GameColors.GetInteractionColorBlock();
        lawNotice.text = GameData.lawNotiz;
        audioSrc.clip = sfx.instaMenuMusicLoop;

        audioSrc.mute = !runtimeDataChapters.musicOn;
        audioSrc.Play();
        audioSrc.volume = runtimeDataChapters.musicVolume;

        
        audioSrcAtmo.clip = sfx.coalmineVerschubFerne;
        audioSrcAtmo.loop = true;
        audioSrcAtmo.Play();

        allChapDone = runtimeDataChapters.ch1GeneralUnlocked && runtimeDataChapters.ch2GrubenwasserUnlocked && runtimeDataChapters.ch3GrubenwasserUnlocked;

        if (allChapDone)
        {
            btnFotoplatz.interactable = true;
        }
    }

    public void GoToFWB()
    {
        Application.OpenURL("https://forum-bergbau-wasser.de/");
    }
}
