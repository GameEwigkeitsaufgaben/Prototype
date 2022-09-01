using UnityEngine;
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

    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapTwoRuntimeData runtimeDataCh2;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDragTouch);
        runtimeDataCh2 = runtimeDataChapters.LoadChap2RuntimeData();

        headingFeS2.SetActive(false);
        headingH2o.SetActive(false);
        headingO2.SetActive(false);
        headingSo4.SetActive(false);
        phOxi1.SetActive(false);
        phOxi2.SetActive(false);
        phRegen.SetActive(false);
    }

    public void SetMolekuelFound(SauresWasserTrigger trigger)
    {
        switch (trigger)
        {
            case SauresWasserTrigger.Wasser:
                infoZoneText.text = triggerWasser;
                molh2o.SetColor(foundColor);
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                molo2.SetColor(foundColor);
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                molfes2.SetColor(foundColor);
                molh.SetColor(foundColor);
                molfe2.SetColor(foundColor);
                molso4.SetColor(foundColor);
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                molh.SetColor(foundColor);
                molfe3.SetColor(foundColor);
                break;
        }
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
}
