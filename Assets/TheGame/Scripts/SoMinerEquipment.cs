using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum MinerEquipmentItem
{
    Helm = 4,
    Lampe = 8,
    Atemmaske = 1,
    Sicherheitsschuhe = 2,
    Stechkarte = 5,
    Schutzbrille = 3,
    Schienbeinschuetzer = 6,
    Anzug = 0,
    Halstuch = 7,
    Handschuhe = 9
}

//essential-lebensnotwendig, protection-schutz schwere verletzung, specialTask - sonstig wichtig
public enum EquipmentRound
{
    Essential,
    Protection,
    SpecialTask
}

public enum SnapetTo
{
    Table,
    Miner
}

[CreateAssetMenu(menuName = "MinerEquipmentConfig")]
public class SoMinerEquipment : ScriptableObject
{
    public Sprite lampTable, lampMiner;
    public Sprite handschuhLeft, handschuhRight;
    public Sprite cardTable, cardMiner;
    public Sprite anzug, brille, schuhe, schienbeinschuetzer, halstuch, mask;
    public AudioClip beginDrag,endDrag, badJobClip, goodJobClip;
}
