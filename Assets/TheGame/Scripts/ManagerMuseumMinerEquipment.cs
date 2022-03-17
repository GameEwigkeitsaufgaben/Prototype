using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    public EquipmentRound round;
    public Image denkbubbleWorstcase;
    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner  = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();
    private List<MinerEquipmentItem> plainList = new List<MinerEquipmentItem>();
    private IEnumerator worstcasesCoroutine;
    public Sprite sNoHelm, sNoLamp, sNoMask, s4;

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
            //foreach(MuseumMinerEquipmentItem i in items)
            //{
            //    if(i.snapedTo == SnapetTo.Miner)
            //    {
            //        listItemsOnMiner.Add(i.equipmentItem, i);
            //    }
            //}

            foreach(MuseumMinerEquipmentItem i in items)
            {
                if(i.snapedTo == SnapetTo.Miner)
                {
                    if(!plainList.Contains(i.equipmentItem)) plainList.Add(i.equipmentItem);
                }
            }
        }
        worstcasesCoroutine = PlayWorstcases(plainList.Contains(MinerEquipmentItem.Helm), plainList.Contains(MinerEquipmentItem.Atemmaske), plainList.Contains(MinerEquipmentItem.Lampe));
        StartCoroutine(worstcasesCoroutine);

    }

    IEnumerator PlayWorstcases(bool helm, bool mask, bool lamp)
    {
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(!helm || !mask || !lamp);

        denkbubbleWorstcase.GetComponent<Image>().preserveAspect = true;
        
        if (!helm)
        {
            denkbubbleWorstcase.GetComponent<Image>().sprite = sNoHelm;
            Debug.Log("No Helm");
            yield return new WaitForSeconds(2f);
        }
        if (!mask)
        {
            denkbubbleWorstcase.GetComponent<Image>().sprite = sNoMask;
            Debug.Log("No Atemmaske");
            yield return new WaitForSeconds(2f);
        }
        if (!lamp)
        {
            denkbubbleWorstcase.GetComponent<Image>().sprite = sNoLamp;
            Debug.Log("No lampe");
            yield return new WaitForSeconds(2f);
        }

        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);
        listItemsOnMiner.Clear();
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
