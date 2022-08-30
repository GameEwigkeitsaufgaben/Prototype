using System.Collections;
using System.Collections.Generic;
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
    public const string triggerSchacht = "Durch die vom Menschen angelegten Schächte und Stollen kommt Sauerstoff in tiefen Bereiche des Bergwerks und somit in die " +
        "Versickerungsräume der tiefen Gesteinsschichten!";
    public const string triggerPyrit = "Trifft Wasser auf Pyrit, welches oft vorkommt in der Kohle und im Nebengestein, wird die Gitterstruktur von Pyrit aufgelöst - zerstört! " +
        "Es kommt zur Oxidation. Die Schwefelmoleküle verbinden sich mit dem Sauserstoff. Eisen bleibt übrig.";
    public const string triggerAustritt = "Mit dem Raufpumpen des Grubenwassers kommen die gelösten Elemente an die Oberfläche. " +
        "Da kommt es zur 2ten Oxidationsstufe. Das Eisen fällt aus. Es beginnt sofort zu rosten. Und es bildet sich Schwefelsäure. Darum wird das Wasser rot. ";

    public TMP_Text infoZoneText,infoZoneHeading;
    public Molecule molfes2, molh2o, molo2, molso4, molh, molfe2, molfe3;

    private Color foundColor = Color.green;

    public GameObject headingH2o, headingO2, headingSo4, headingFeS2;

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
                //infoZoneHeading.text = "H2o";
                headingH2o.SetActive(true);
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                //infoZoneHeading.text = "O2";
                headingO2.SetActive(true);
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                //infoZoneHeading.text = "FeS2";
                headingFeS2.SetActive(true);
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                //infoZoneHeading.text = "H2oS4";
                headingSo4.SetActive(true);
                break;
        }
    }
}
