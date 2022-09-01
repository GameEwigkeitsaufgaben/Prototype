using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum SauresWasserTrigger
{
    Wasser,
    Schacht,
    Pyrit,
    Austritt
}

public class ManagerSauresWasser : MonoBehaviour
{

    public const string triggerWasser = "Mit dem Versickern von Regen dringt Wasser über unterschiedliche Fließpfade in die Hohlräume der unterschiedlichen Gesteinsschichten ein!";
    public const string triggerSchacht = "Durch die vom Menschen angelegten Schächte und Stollen kommt Sauerstoff in tiefe Bereiche des Bergwerks und somit in die " +
        "Versickerungsräume der tiefen Gesteins- schichten!";
    public const string triggerPyrit = "Trifft Wasser auf Pyrit, welches oft vorkommt in der Kohle und im Nebengestein, wird die Gitterstruktur von Pyrit aufgelöst - zerstört! " +
        "Es kommt zur Oxidation. Die Schwefelmoleküle verbinden sich mit dem Sauserstoff. Eisen bleibt übrig.";
    public const string triggerAustritt = "Mit dem Raufpumpen des Grubenwassers kommen die gelösten Elemente an die Oberfläche. " +
        "Da kommt es zur zweiten Oxidationsstufe. Das Eisen fällt aus. Es beginnt sofort zu rosten. Und es bildet sich Schwefelsäure. Darum wird das Wasser rot. ";

    public TMP_Text infoZoneText,infoZoneHeading;
    public Molecule molfes2, molh2o, molo2, molso4, molh, molfe2, molfe3;

    private Color foundColor = Color.green;

    public GameObject headingH2o, headingO2, headingSo4, headingFeS2;
    public GameObject phRegen, phOxi1, phOxi2;
    public SpeechManagerMuseumChapTwo speechManager;
    public GameObject Lupe, btnReplayTalkingList, mirror; 
    public Button btnProceed;
    public AudioSource aRegen, aBewetterung, aKnistern, aAusleiten; 

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDataCh2;
    private SoSfx sfx;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDragTouch);
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();
        sfx = runtimeDataChapters.LoadSfx();

        aRegen.clip = sfx.regen;
        aBewetterung.clip = sfx.coalmineWindInTunnel;
        aAusleiten.clip = sfx.atmoWasserRinnt;

        aRegen.Play();
        aBewetterung.Play();
        aKnistern.Play();
        aAusleiten.Play();

        headingFeS2.SetActive(false);
        headingH2o.SetActive(false);
        headingO2.SetActive(false);
        headingSo4.SetActive(false);
        phOxi1.SetActive(false);
        phOxi2.SetActive(false);
        phRegen.SetActive(false);

        btnProceed.interactable = false;

        if (runtimeDataCh2.replayPyrit)
        {
            btnReplayTalkingList.SetActive(true);
        }
        else
        {
            Debug.Log("starte talking");
            speechManager.playZechePyrit = true;
            mirror.SetActive(true);
            Lupe.SetActive(false);
            btnReplayTalkingList.SetActive(false);
        }

        if (runtimeDataCh2.progressPost218PyritDone)
        {
            btnProceed.interactable = true;
        }
    }

    public void ResetIonHeading()
    {
        headingFeS2.SetActive(false);
        headingH2o.SetActive(false);
        headingO2.SetActive(false);
        headingSo4.SetActive(false);
        phOxi1.SetActive(false);
        phOxi2.SetActive(false);
        phRegen.SetActive(false);
        infoZoneHeading.gameObject.SetActive(true);
        infoZoneText.text = "";
    }

    //int regen-0, bewetterung-1, knistern-2, abwasser-3
    public void SetAudio(int station)
    {
        switch (station)
        {
            case 0:
                aRegen.volume = 1f;
                aBewetterung.volume = 0.3f;
                aKnistern.volume = 0.3f;
                aAusleiten.volume = 0.3f;
                break;
            case 1:
                aRegen.volume = 0.3f;
                aBewetterung.volume = 1f;
                aKnistern.volume = 0.3f;
                aAusleiten.volume = 0.3f;
                break;
            case 2:
                aRegen.volume = 0.3f;
                aBewetterung.volume = 0.3f;
                aKnistern.volume = 1f;
                aAusleiten.volume = 0.3f;
                break;
            case 3:
                aRegen.volume = 0.3f;
                aBewetterung.volume = 0.3f;
                aKnistern.volume = 0.3f;
                aAusleiten.volume = 1f;
                break;
            case 4:
                aRegen.volume = 0.3f;
                aBewetterung.volume = 0.3f;
                aKnistern.volume = 0.3f;
                aAusleiten.volume = 0.3f;
                break;
        }
    }

    public void ReplayTalkingList()
    {
        speechManager.playZechePyrit = true;
        mirror.SetActive(true);
        Lupe.SetActive(false);
    }

    public void SetMolekuelFound(SauresWasserTrigger trigger)
    {
        switch (trigger)
        {
            case SauresWasserTrigger.Wasser:
                infoZoneText.text = triggerWasser;
                molh2o.SetColor(foundColor);
                molh2o.SetDone();
                SetAudio(0);
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                molo2.SetColor(foundColor);
                molo2.SetDone();
                SetAudio(1);
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                molfes2.SetColor(foundColor);
                molh.SetColor(foundColor);
                molfe2.SetColor(foundColor);
                molso4.SetColor(foundColor);
                SetAudio(2);

                molfes2.SetDone();
                molh.SetDone();
                molfe2.SetDone();
                molso4.SetDone();
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                molh.SetColor(foundColor);
                molfe3.SetColor(foundColor);
                molfe3.SetDone();
                SetAudio(3);
                break;
        }

        if (molh2o.IsDone() && molo2.IsDone() && molfes2.IsDone() && molh.IsDone() && molfe2.IsDone() & molso4.IsDone() && molfe3.IsDone())
        {
            runtimeDataCh2.progressPost218PyritDone = true;
            btnProceed.interactable = true;
            return;
        }

        Debug.Log("missing ion");
        
    }

    public void SwitchScene()
    {
        GetComponent<SwitchSceneManager>().SwitchToChapter2withOverlay(GameData.NameOverlay218);
        runtimeDataCh2.progressPost218PyritDone = true;
    }

    //ph=0 rain, ph=1 oxi1, ph=2 oxi2
    public void SetPH(int ph)
    {
        switch (ph)
        {
            case 0:
                phRegen.SetActive(true);
                phOxi1.SetActive(false);
                phOxi2.SetActive(false);
                break;
            case 1:
                phRegen.SetActive(false);
                phOxi1.SetActive(true);
                phOxi2.SetActive(false);
                break;
            case 2:
                phRegen.SetActive(false);
                phOxi1.SetActive(false);
                phOxi2.SetActive(true);
                break;
        }
    }

    public void SetTextToTrigger(SauresWasserTrigger trigger)
    {
        headingFeS2.SetActive(false);
        headingH2o.SetActive(false);
        headingO2.SetActive(false);
        headingSo4.SetActive(false);
        infoZoneHeading.gameObject.SetActive(false);

        switch (trigger)
        {
            case SauresWasserTrigger.Wasser:
                infoZoneText.text = triggerWasser;
                headingH2o.SetActive(true);
                SetPH(0);
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                headingO2.SetActive(true);
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                headingFeS2.SetActive(true);
                SetPH(1);
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                headingSo4.SetActive(true);
                SetPH(2);
                break;
        }
    }

    private void Update()
    {
        if (speechManager.IsTalkingListFinished(GameData.NameCH2TLZechePyrit))
        {
            Lupe.SetActive(true);
            mirror.SetActive(false);
            runtimeDataCh2.replayPyrit = true;
            btnReplayTalkingList.SetActive(true);
        }
    }
}
