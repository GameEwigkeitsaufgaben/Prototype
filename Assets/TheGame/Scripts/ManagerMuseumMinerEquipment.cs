using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MinerEquipmentItem
{
    Anzug = 8,
    Atemmaske = 3,
    Schutzbrille = 6,
    Helm = 1,
    Stechkarte = 5,
    Schienbeinschuetzer = 7,
    Halstuch = 9,
    Lampe = 2,
    Handschuhe = 10,
    Sicherheitsschuhe = 4
}

public enum SnapetTo
{
    Table,
    Miner
}
public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    public MuseumMinerEquipmentItem[] items;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
