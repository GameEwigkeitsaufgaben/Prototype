using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum FliesspfadState
{
    idle,
    fp1,
    fp2,
    fp3
}

public class ManagerFliesspfade : MonoBehaviour
{
    private const string TriggerNameFP1 = "fp1";
    private const string TriggerNameFP2 = "fp2";
    private const string TriggerNameFP3 = "fp3";
    private const string StateNameIdle = "idleFliesspfad";
    private const string StateNameFP1 = "fliesspfad1";
    private const string StateNameFP2 = "fliesspfad2";
    private const string StateNameFP3 = "fliesspfad3";
    private const string HeadingInfoIdle = "Regenwasser wird zu Grundwasser";
    private const string HeadingInfoFP1 = "Fließpfad 1";
    private const string HeadingInfoFP2 = "Fließpfad 2";
    private const string HeadingInfoFP3 = "Fließpfad 3";

    public GameObject eventsystem;
    public Animator animator;
    public TMP_Text textinfoFP, headingInfoFp;
    public AudioSource audioSrc, audioSrcRain, audioSrcFp2, audioSrcFp3, wetterNice;
    public Button backToMuseum;
    public Toggle btnFp1, btnFp2, btnFp3;
    public Image solidBg; 
    bool allWatched = false, consumedFp1, consumedFp2, consumedFp3;
    bool btn1selected;
    FliesspfadState currentFp, previousFp;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoSfx sfx;

    Color rain = new Color32(152,152,152,255);

    private string textIntro = "Wasser versickert und fließt in Hohlräumen im Untergrund. Diese Hohlräume können kleine Poren zwischen einzelnen Sand- und Kieskörnern sein, " +
        "Klüfte im Fels oder auch große Gänge, wie im Bergwerk. " +
        "Je größer die Hohlräume sind, umso schneller kann Wasser versickern und zu Grundwasser werden.";
    private string textFp1 = "Regenwasser versickert und fließt in den winzigen Poren zwischen den einzelnen Körnern des Gesteins. " +
        "Je kleiner diese Poren sind, umso langsamer geht das. " +
        "Dort fließt das Wasser nur wenige Zentimeter bis Meter pro Tag. " +
        "Manchmal auch gar nicht. ";
    private string textFp2 = "Wenn der Fels in der Tiefe Risse hat, die Geologen nennen das Klüfte, dann kann das Wasser in den Klüften schon sehr viel schneller fließen. " +
        "Je nachdem wie groß die Klüfte sind, können es schon einige Meter pro Tag sein.";
    private string textFp3 = "Die Schächte und Stollen bilden große Hohlräume im Untergrund, in denen das Wasser ganz schnell fließen kann, wie in einem Bach an der Oberfläche. " +
        "Aus den Poren und Klüften im Berg fließt das Wasser in diese Hohlräume des Bergbaus und füllt sie langsam auf, wenn man nichts dagegen unternimmt. Hier spricht man dann von Grubenwasser.";

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);

        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();
        
    }

    void Start()
    {
        backToMuseum.interactable = runtimeDataCh2.fliesspfadeDone;
        headingInfoFp.text = HeadingInfoIdle;
        textinfoFP.text = textIntro;
        currentFp = previousFp = FliesspfadState.idle;
        solidBg.color = Color.white;

        audioSrcRain.clip = sfx.regen;
        audioSrcFp2.clip = sfx.fp2;
        audioSrcFp3.clip = sfx.fp3;
        wetterNice.clip = sfx.athmoNiceWeather;
        wetterNice.Play();
    }

    void Update()
    {
        if (consumedFp1 && consumedFp2 && consumedFp3)
        {
            backToMuseum.interactable = true;
            runtimeDataCh2.fliesspfadeDone = true;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(StateNameIdle))
        {
            currentFp = FliesspfadState.idle;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(StateNameFP1))
        {
            currentFp = FliesspfadState.fp1;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("fliesspfad1 0"))
        {
            currentFp = FliesspfadState.fp2;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(StateNameFP2))
        {
            currentFp = FliesspfadState.fp2;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("fliesspfad1 1"))
        {
            currentFp = FliesspfadState.fp3;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("fliesspfad2 0"))
        {
            currentFp = FliesspfadState.fp3;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName(StateNameFP3))
        {
            currentFp = FliesspfadState.fp3;
        }

        if (currentFp != previousFp)
        {
            previousFp = currentFp;

            switch (currentFp)
            {
                case FliesspfadState.idle:
                    btnFp1.interactable = btnFp2.interactable = btnFp3.interactable = true;
                    headingInfoFp.text = HeadingInfoIdle;
                    textinfoFP.text = textIntro;
                    solidBg.color = Color.white;
                    wetterNice.Play();
                    audioSrc.Stop();
                    audioSrcRain.Stop();
                    audioSrcFp2.Stop();
                    audioSrcFp3.Stop();

                    break;
                case FliesspfadState.fp1:
                    btnFp2.interactable = false;
                    btnFp3.interactable = false;
                    solidBg.color = rain;
                    audioSrc.clip = sfx.wolken;

                    wetterNice.Stop();
                    audioSrc.Play();
                    audioSrcRain.PlayDelayed(2);
                    break;
                case FliesspfadState.fp2:
                    btnFp1.interactable = false;
                    btnFp3.interactable = false;
                    solidBg.color = rain;

                    wetterNice.Stop();
                    audioSrc.Play();
                    audioSrcRain.PlayDelayed(2);
                    audioSrcFp2.PlayDelayed(3);
                    break;
                case FliesspfadState.fp3:
                    btnFp1.interactable = false;
                    btnFp2.interactable = false;
                    solidBg.color = rain;

                    wetterNice.Stop();
                    audioSrc.Play();
                    audioSrcRain.PlayDelayed(2);
                    audioSrcFp3.PlayDelayed(3);
                    break;
            }

            if (currentFp != FliesspfadState.idle) return;

            headingInfoFp.text = HeadingInfoIdle;
            
            btnFp1.isOn = false;
            btnFp2.isOn = false;
            btnFp3.isOn = false;
        }

    }

    public void SwitchToMuseum()
    {
        GetComponent<SwitchSceneManager>().SwitchScene(GameScenes.ch02Museum);
        runtimeDataCh2.lastWP = MuseumWaypoints.WPFliesspfad;
    }

    public void PlayAnimation(string triggerName)
    {

        if (currentFp != FliesspfadState.idle) return;

        bool valOn = btnFp1.isOn || btnFp2.isOn || btnFp3.isOn;

        if (!valOn) return;
       
        if (triggerName == TriggerNameFP1)
        {
            textinfoFP.text = textFp1;
            headingInfoFp.text = HeadingInfoFP1;
            consumedFp1 = true;
            btn1selected = true;
        }
        else if(triggerName == TriggerNameFP2)
        {
            textinfoFP.text = textFp2;
            headingInfoFp.text = HeadingInfoFP2;
            consumedFp2 = true;
        }
        else if (triggerName == TriggerNameFP3)
        {
            textinfoFP.text = textFp3;
            headingInfoFp.text = HeadingInfoFP3;
            consumedFp3 = true;
        }

       animator.SetTrigger(triggerName);
    }
}
