using UnityEngine;
using UnityEngine.UI;

public class ManagerNews : MonoBehaviour
{
    [SerializeField] private Button btnSwitchScene;
    [SerializeField] private AudioSource audioSrcAtmo, audioSrcRaschelnZeitung;

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeDataCh3;
    private SoSfx sfx;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        runtimeDataCh3 = runtimeDataChapters.LoadChap3RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();

        audioSrcAtmo.clip = sfx.monitoringAtmo;
        audioSrcAtmo.Play();

        audioSrcRaschelnZeitung.clip = sfx.raschelZeitung;
        if (!audioSrcRaschelnZeitung.isPlaying) audioSrcRaschelnZeitung.Play();
    }

    public void SwitchTheScene()
    {
        runtimeDataCh3.newsDone = true;
        if (!audioSrcRaschelnZeitung.isPlaying) audioSrcRaschelnZeitung.Play();
        btnSwitchScene.interactable = false;

        Invoke("SwitchTheSceneOnly", 1f);
    }

    private void SwitchTheSceneOnly()
    {
        btnSwitchScene.GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch03Monitoring);
    }
}
