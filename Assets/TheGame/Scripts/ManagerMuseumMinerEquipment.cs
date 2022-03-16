using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    public EquipmentRound round;
    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner  = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();

    // Start is called before the first frame update
    void Start()
    {
        itemsOnMiner = 0;
        round = EquipmentRound.Essential;
    }

    // Update is called once per frame
    void Update()
    {
        if(round == EquipmentRound.Essential && itemsOnMiner == 3)
        {
            SetTableItemsInactive(true);
        }
        else if (round == EquipmentRound.Essential && itemsOnMiner < 3)
        {
            SetTableItemsInactive(false);
        }
    }

    public void CheckRound()
    {

        if(round == EquipmentRound.Essential && itemsOnMiner == 3)
        {
            foreach(MuseumMinerEquipmentItem i in items)
            {
                if(i.snapedTo == SnapetTo.Miner)
                {
                    listItemsOnMiner.Add(i.equipmentItem, i);
                }
               
            }
        }

        if (!listItemsOnMiner.ContainsKey(MinerEquipmentItem.Helm))
        {
            Debug.Log("No Helm");
        }
        if (!listItemsOnMiner.ContainsKey(MinerEquipmentItem.Atemmaske))
        {
            Debug.Log("No Atemmaske");
        }
        if (!listItemsOnMiner.ContainsKey(MinerEquipmentItem.Lampe))
        {
            Debug.Log("No lampe");
        }
  
    }

    private void SetTableItemsInactive(bool inactive)
    {
        if (inactive)
        {
            foreach (MuseumMinerEquipmentItem i in items)
            {
                if (i.gameObject.GetComponent<MuseumMinerEquipmentItem>().snapedTo == SnapetTo.Table && i.gameObject.GetComponent<MuseumMinerEquipmentItem>().previous == SnapetTo.Table)
                {
                    Debug.Log(inactive + " ");
                    i.gameObject.GetComponent<Image>().color = Color.red;
                    i.gameObject.GetComponent<Image>().raycastTarget = false;
                }
            }
        }
        else
        {
            foreach (MuseumMinerEquipmentItem i in items)
            {
                if (i.gameObject.GetComponent<MuseumMinerEquipmentItem>().snapedTo == SnapetTo.Table)
                {
                    i.gameObject.GetComponent<Image>().color = Color.white;
                    i.gameObject.GetComponent<Image>().raycastTarget = true;
                }
            }
        }
        
    }
}
