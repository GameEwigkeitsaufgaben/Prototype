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
    public TMP_Text fes2, h2o, o2, so4, h, h2so4, fe;

    private SoChaptersRuntimeData runtimeDataChapters;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
                infoZoneHeading.text = "H2o";
                h2o.color = Color.green;
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                infoZoneHeading.text = "O2";
                o2.color = Color.green;
                break;
            case SauresWasserTrigger.Pyrit:
                fes2.color = Color.green;
                h.color = Color.green;
                fe.color = Color.green;
                so4.color = Color.green;
                infoZoneText.text = triggerPyrit;
                infoZoneHeading.text = "FeS2";
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                h.color = Color.green;
                fe.color = Color.green;
                h2so4.color = Color.green;
                break;
        }
    }

    public void SetTextToTrigger(SauresWasserTrigger trigger)
    {
        switch (trigger)
        {
            case SauresWasserTrigger.Wasser:
                infoZoneText.text = triggerWasser;
                break;
            case SauresWasserTrigger.Schacht:
                infoZoneText.text = triggerSchacht;
                break;
            case SauresWasserTrigger.Pyrit:
                infoZoneText.text = triggerPyrit;
                break;
            case SauresWasserTrigger.Austritt:
                infoZoneText.text = triggerAustritt;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
