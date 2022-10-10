using UnityEngine;

public class MuseumHandschuhe : MonoBehaviour
{
    public MuseumMinerEquipmentItem leftHand, rightHand;

    public void SetBothSnapedToMiner()
    {
        leftHand.snapedTo = SnapetTo.Miner;
        rightHand.snapedTo = SnapetTo.Miner;
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

    public void ResetBothToMiner()
    {
        leftHand.ResetToMiner();
        rightHand.ResetToMiner();
    }

    public void ResetBothToTable()
    {
        leftHand.ResetToTable();
        rightHand.ResetToTable();
    }
}
