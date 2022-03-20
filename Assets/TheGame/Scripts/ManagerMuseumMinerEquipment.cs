using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    private const int MaxItemsRoundEssential = 3;
    private const int MaxItemsRoundProtection = 6;
    private const int MaxItemsRoundSpectialTask = 10;

    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    public EquipmentRound currentRound;
    public Image denkbubbleWorstcase;
    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner  = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();
    private List<MinerEquipmentItem> plainList = new List<MinerEquipmentItem>();
    private IEnumerator worstcasesCoroutine;
    public Sprite sNoHelm, sNoLamp, sNoMask, s4;
    public Button btnCheckEquipment;
    public string 
        round1 = "Ziehe per Drag and Drop 3 überlebensnotwendige Dinge auf den Bergmann.", 
        round2 = "Ziehe dem Bergmann 3 weitere Dinge an die ihn vor schweren Verletzungen schützen?", 
        round3 = "Ziehe den Bergmann fertig an und schaue wofür die restlichen Dinge für den Bergmann wichtig sind.";
    public string headingR1 = "Runde 1", headingR2 = "Runde 2", headingR3 = "Runde 3";
    public TextMeshProUGUI headingRound, textRound;

    // Start is called before the first frame update
    void Start()
    {
        itemsOnMiner = 0;
        currentRound = EquipmentRound.Essential;
        denkbubbleWorstcase.GetComponent<Image>().preserveAspect = true;
    }

    private void SetupMinerEssential()
    {
        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.ResetToTable();
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool maxItemsReached = false;

        switch (currentRound)
        {
            case EquipmentRound.Essential:
                maxItemsReached = itemsOnMiner == MaxItemsRoundEssential;
                break;
            case EquipmentRound.Protection:
                maxItemsReached = itemsOnMiner == MaxItemsRoundProtection;
                break;
            case EquipmentRound.SpecialTask:
                maxItemsReached = itemsOnMiner == MaxItemsRoundSpectialTask;
                break;
        }

        btnCheckEquipment.interactable = maxItemsReached;
        SetTableItemsInactive(maxItemsReached);
    }

    public void CheckRound()
    {
        //Create List which items are snaped to the miner
        plainList.Clear();

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if (i.snapedTo == SnapetTo.Miner)
            {
                if (!plainList.Contains(i.equipmentItem)) plainList.Add(i.equipmentItem);
            }
        }

        //Evaluate List
        switch (currentRound)
        {
            case EquipmentRound.Essential:
                if (plainList.Contains(MinerEquipmentItem.Helm) && plainList.Contains(MinerEquipmentItem.Atemmaske) && plainList.Contains(MinerEquipmentItem.Lampe))
                {
                    //Set Text UI Elements
                    headingRound.text = headingR2;
                    textRound.text = round2;

                    //Set Miner Items
                    ResetToMostImportantItems();
                    return;
                }

                worstcasesCoroutine = PlayWorstcases(plainList.Contains(MinerEquipmentItem.Helm), plainList.Contains(MinerEquipmentItem.Atemmaske), plainList.Contains(MinerEquipmentItem.Lampe));

                StartCoroutine(worstcasesCoroutine);
                
                break;
            case EquipmentRound.Protection:
                if (plainList.Contains(MinerEquipmentItem.Schienbeinschuetzer) && plainList.Contains(MinerEquipmentItem.Sicherheitsschuhe) && plainList.Contains(MinerEquipmentItem.Schutzbrille))
                {
                    //Set Text UI Elements
                    headingRound.text = headingR3;
                    textRound.text = round3;

                    //Set Miner Items
                    ResetToSpecialTaskItems();
                }
                break;
            case EquipmentRound.SpecialTask:
                Debug.Log("SpecialTask");
                break;
        }
    }

    private void ResetToSpecialTaskItems()
    {
        itemsOnMiner = 6;
        currentRound = EquipmentRound.SpecialTask;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if (i.solutionItemRound1 || i.solutionItemRound2)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
                i.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                i.ResetToTable();
            }
        }
    }

    IEnumerator PlayWorstcases(bool helm, bool mask, bool lamp)
    {
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(!helm || !mask || !lamp);

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
            denkbubbleWorstcase.GetComponent<Image>().color = Color.black;
            denkbubbleWorstcase.transform.parent.gameObject.GetComponent<Image>().color = Color.black;
            yield return new WaitForSeconds(1f);
            denkbubbleWorstcase.GetComponent<Image>().color = Color.white;
            denkbubbleWorstcase.transform.parent.gameObject.GetComponent<Image>().color = Color.white;
            Debug.Log("No lampe");
            yield return new WaitForSeconds(2f);
        }

        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);

        //Reset items
        ResetAllMinerItems();
    }

    private void ResetAllMinerItems()
    {
        itemsOnMiner = 0;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.ResetToTable();
        }
    }

    private void ResetToMostImportantItems()
    {
        itemsOnMiner = 3;
        currentRound = EquipmentRound.Protection;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if(i.equipmentItem == MinerEquipmentItem.Helm || i.equipmentItem == MinerEquipmentItem.Atemmaske || i.equipmentItem == MinerEquipmentItem.Lampe)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
                i.GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                i.ResetToTable();
            }
        }
    }

    private void SetTableItemsInactive(bool inactive)
    {
        foreach (MuseumMinerEquipmentItem i in items)
        {
            Color tmpInteractionColor = Color.white;

            switch (i.snapedTo)
            {
                case SnapetTo.Table:
                    if (inactive) tmpInteractionColor = new Color32(189, 189, 189, 255);

                    i.gameObject.GetComponent<Image>().color = tmpInteractionColor;
                    i.gameObject.GetComponent<Image>().raycastTarget = !inactive;
                    break;
            }
        }
    }
}
