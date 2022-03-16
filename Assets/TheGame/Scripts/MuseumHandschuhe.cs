using UnityEngine;

public class MuseumHandschuhe : MonoBehaviour
{
    public MuseumMinerEquipmentItem leftHand, rightHand;

    public void SetBothSnapedToMiner()
    {
        leftHand.snapedTo = SnapetTo.Miner;
        rightHand.snapedTo = SnapetTo.Miner;
        Debug.Log("Both set to miner");
    }

    public void SetToogleSameForBoth()
    {
        leftHand.previous = leftHand.snapedTo;
        rightHand.previous = rightHand.snapedTo;
    }

    public void SetBothSnapedToTable()
    {
        leftHand.snapedTo = SnapetTo.Table;
        rightHand.snapedTo = SnapetTo.Table;
    }

    public void SetMinerPos()
    {
        leftHand.transform.position = leftHand.correspondingItemOnMiner.transform.position;
        rightHand.transform.position = rightHand.correspondingItemOnMiner.transform.position;
    }

    public void SetTablePos()
    {
        leftHand.transform.position = leftHand.origPosTable;
        rightHand.transform.position = rightHand.origPosTable;
    }
}
