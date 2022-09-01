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
    public Canvas mirrorDad;
    public GameObject replayButton;

    public Button btnBackToOverlay;
    public Toggle toggleP1, toggleP2, toggleP3;

    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoChaptersRuntimeData runtimeDataChapters;
    public AudioSource audioSrc;
    public AudioClip failPumpe1, failPumpe3;
    public AnimationClip p1, p2, p3, off;

    SpeechManagerMuseumChapTwo speechManagerCh2;

    float time = 0f;
    bool interact = true;

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

        btnBackToOverlay.interactable = runtimeDataCh2.interactPumpenDone;
        replayButton.SetActive(runtimeDataCh2.replayPumpen);
        TurnOnPumpe(0);

        Debug.Log(p1.length + " " +p2.length + " " + p3.length);
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

    public void ReplayTalkingList()
    {
        speechManagerCh2.playZechePumpeIntro = true;
        mirrorDad.gameObject.SetActive(true);
    }


    public void TurnOnPumpe(int pumpenid)
    {
        switch (pumpenid)
        {
            case 1:
                if (!toggleP1.GetComponent<Toggle>().isOn) return;
                animator.SetTrigger(Pumpen.pumpe1.ToString());
                audioSrc.clip = failPumpe1;
                audioSrc.Play();
                break;
            case 2:
                if (!toggleP2.GetComponent<Toggle>().isOn) return;
                animator.SetTrigger(Pumpen.pumpe3.ToString());
                break;
            case 3:
                if (!toggleP3.GetComponent<Toggle>().isOn) return;
                animator.SetTrigger(Pumpen.pumpe2.ToString());
                audioSrc.clip = failPumpe3;
                audioSrc.Play();
                break;
            case 0:
                animator.SetTrigger(Pumpen.pumpeOff.ToString());
                break;
        }

        runtimeDataCh2.interactPumpenDone = true;
        runtimeDataCh2.progressPost215Done = true;
        toggleP1.interactable = false;
        toggleP2.interactable = false;
        toggleP3.interactable = false;

    }

    public void PumpeIsFinished(int pumpenid)
    {
        switch (pumpenid)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
        }

        Debug.Log("");
    }

    private void Update()
    {
       
        if (speechManagerCh2.IsTalkingListFinished(GameData.NameCH2TLZechePumpeIntro))
        {
            toggleP1.interactable = true;
            toggleP2.interactable = true;
            toggleP3.interactable = true;

            mirrorDad.gameObject.SetActive(false);
            runtimeDataCh2.replayPumpen = true;
            replayButton.SetActive(true);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(Pumpen.pumpe1.ToString()) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Debug.Log("+++++++++++++++++++++++++++ off triggern! p1");
            toggleP1.GetComponent<Toggle>().isOn = false;
            interact = false;

            TurnOnPumpe(0);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(Pumpen.pumpe2.ToString()) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Debug.Log("+++++++++++++++++++++++++++ off triggern! p2");
            toggleP3.GetComponent<Toggle>().isOn = false;
            interact = false;

            TurnOnPumpe(0);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(Pumpen.pumpe3.ToString()) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            Debug.Log("+++++++++++++++++++++++++++ off triggern p3!");
            toggleP2.GetComponent<Toggle>().isOn = false;
            interact = false;

            runtimeDataCh2.interactPumpenDone = true;
            btnBackToOverlay.interactable = true;

            TurnOnPumpe(0);
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(Pumpen.pumpeOff.ToString()) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            if (interact) return;
            toggleP1.interactable = true;
            toggleP2.interactable = true;
            toggleP3.interactable = true;
            interact = true;
        }



    }
}
