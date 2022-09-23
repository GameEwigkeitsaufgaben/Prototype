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
    [SerializeField] private Button btnCredits, btnFotoplatz;
    [SerializeField] private TMP_Text lawNotice;
    [SerializeField] private bool allChapDone;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private AudioSource audioSrc;

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
        audioSrc.Play();
        audioSrc.volume = GameData.defaultVolumeInsta;

        allChapDone = runtimeDataChapters.ch1GeneralUnlocked && runtimeDataChapters.ch2GrubenwasserUnlocked && runtimeDataChapters.ch3GrubenwasserUnlocked;

        if (allChapDone)
        {
            btnFotoplatz.interactable = true;
        }
    }
}
