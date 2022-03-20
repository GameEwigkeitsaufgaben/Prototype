using UnityEngine;

public enum MinerEquipmentItem
{
    Helm = 1,
    Lampe = 3,
    Atemmaske = 5,
    Sicherheitsschuhe = 11,
    Stechkarte = 13,
    Schutzbrille = 15,
    Schienbeinschuetzer = 101,
    Anzug = 103,
    Halstuch = 105,
    Handschuhe = 107
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
}
