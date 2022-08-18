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

    public const string triggerWasser = "Mit dem Versickern von Regen dringt �ber unterschiedliche Flie�pfade Wasser in die Versickerungsr�ume der unterschiedlichen Gesteinsschichten!";
    public const string triggerSchacht = "Durch die vom Menschen angelegen Sch�chte und Stollen kommt Sauerstoff in tiefe Bereiche des Bergwerks und somit in die Versickerungsr�ume der tiefen Gesteinsschichte! ";
    public const string triggerPyrit = "Trifft Wasser auf Pyrit, welches oft vorkommt in der Kohle und im Nebengestein, wird die Blockstruktur von Pyrit aufgel�st - zerst�rt! Es kommt zur ersten Oxidationsstufe. Die Schwefelmolek�le verbinden sich mit dem Sauserstoff. Eisen bleibt �ber.";
    public const string triggerAustritt = "Mit dem raufpumpen des Grubenwassers kommen die gl�sten Elemente an die Oberfl�che. Da kommt es zur 2ten Oxidationsstufe. Das Eisen f�llt aus. Es beginnt sofort zu rosten. Und es bildet sich Schwefels�ure. Darum wird das Wasser rot. ";

    public TMP_Text infoZoneText,infoZoneHeading;
    public Molecule molfes2, molh2o, molo2, molso4, molh, molfe2, molfe3;

    private Color foundColor = Color.green;

    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDragTouch);
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

    public void SetTextToTrigger(SauresWasserTrigger trigger)
    {
        switch (trigger)
        {
            case SauresWasserTrigger.Wasser:
                infoZoneText.text = triggerWasser;
                infoZoneHeading.text = "H2o";
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                infoZoneHeading.text = "O2";
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                infoZoneHeading.text = "FeS2";
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                infoZoneHeading.text = "H2oS4";
                break;
        }
    }

}
