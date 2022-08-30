using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum Pumpen
{
    pumpe1 = 1,
    pumpe2 = 2,
    pumpe3 = 3,
    pumpeOff = 0
}

public class ManagerPumpen : MonoBehaviour
{
    public TMP_Text[] textInScene;
    public Animator animator;

    public Button btnBackToOverlay;
    public Toggle toggleP1, toggleP2, toggleP3;

    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChaptersRuntimeData runtimeDataChapters;
    public AudioSource audioSrc;
    public AudioClip failPumpe1, failPumpe3;

    SpeechManagerMuseumChapTwo speechManagerCh2;

    void Start()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        speechManagerCh2 = GetComponent<SpeechManagerMuseumChapTwo>();
        speechManagerCh2.playZechePumpeIntro = true;

        for(int i = 0; i < textInScene.Length; i++)
        {
            textInScene[i].color = GameColors.defaultTextColor;
        }

        toggleP1.interactable = false;
        toggleP2.interactable = false;
        toggleP3.interactable = false;
    }

    public void CheckPumpOutSole2(Pumpen pumpe)
    {
        switch (pumpe)
        {
            case Pumpen.pumpe1:
                toggleP2.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
            case Pumpen.pumpe2:
                toggleP1.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
            case Pumpen.pumpe3:
                toggleP2.interactable = false;
                toggleP3.interactable = false;
                //starte Audio:
                break;
        }
    }

    public void TurnOnPumpe(int pumpenid)
    {
        
        switch (pumpenid)
        {
            case 1:
                    animator.SetTrigger(Pumpen.pumpe1.ToString());
                    audioSrc.clip = failPumpe1;
                    audioSrc.Play();
                break;
            case 2:
                animator.SetTrigger(Pumpen.pumpe2.ToString());
                break;
            case 3:
                animator.SetTrigger(Pumpen.pumpe3.ToString());
                audioSrc.clip = failPumpe3;
                audioSrc.Play();
                break;
            case 0:
                animator.SetTrigger(Pumpen.pumpeOff.ToString());
                break;
        }

        runtimeDataCh2.interactPumpenDone = true;
        runtimeDataCh2.progressPost215Done = true;
        btnBackToOverlay.interactable = true;
        toggleP1.interactable = false;
        toggleP2.interactable = false;
        toggleP3.interactable = false;

    }

    private void Update()
    {
        if (speechManagerCh2.IsTalkingListFinished(GameData.NameCH2TLZechePumpeIntro))
        {
            toggleP1.interactable = true;
            toggleP2.interactable = true;
            toggleP3.interactable = true;
        }
    }
}
