using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCredits : MonoBehaviour
{
    private SoChaptersRuntimeData runtimeChapers;
    private SoSfx sfx;

    [SerializeField] private AudioSource audioSrcMusic;
    

    private void Awake()
    {
        runtimeChapers = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        sfx = runtimeChapers.LoadSfx();
    }

    void Start()
    {
        runtimeChapers.SetAndStartMusic(audioSrcMusic, sfx.instaMenuMusicLoop);
    }
}
