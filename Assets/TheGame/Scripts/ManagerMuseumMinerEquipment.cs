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

    public Animator anim;
    public AnimationClip noHelmAnim, noMaskAnim, noLightAnim, badJobAnim, goodJobAnim;
    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    public EquipmentRound currentRound;
    public Image denkbubbleWorstcase;
    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner  = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();
    private List<MinerEquipmentItem> plainList = new List<MinerEquipmentItem>();
    private IEnumerator worstcasesCoroutine, goodJobCoroutine, badJobCoroutine;
    public Sprite sNoHelm, sNoLamp, sNoMask, s4, minerGoodJob, minerBadJob;
    public Button btnCheckEquipment;
    
    private string 
        round1 = "Drag & Drop: Ziehe 3 überlebensnotwendige Dinge auf den Bergmann.", 
        round2 = "Drag & Drop: Ziehe ihm 3 Dinge an die direkt vor schweren Verletzungen schützen.", 
        round3 = "Drag & Drop: Wofür braucht der Bergmann die restlichen Dinge? Ziehe ihn fertig an.";
    private string headingR1 = "Runde 1", headingR2 = "Runde 2", headingR3 = "Runde 3";

    public AudioClip autsch, lichtaus, husten, badJobClip, goodJobClip;
    public TextMeshProUGUI headingRound, textRound;
    private AudioSource audioSrc;
    public Text uiTooltipText, uiInfoText, btnConfirmText;
    bool runningCorouine = false;
    private SoChapOneRuntimeData runtimeData;

    // Start is called before the first frame update
    void Start()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
        itemsOnMiner = 0;
        currentRound = EquipmentRound.Essential;
        denkbubbleWorstcase.GetComponent<Image>().preserveAspect = true;
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        SetUiTooltip();
        btnConfirmText = btnCheckEquipment.GetComponentInChildren<Text>();
        uiInfoText.text = round1;
    }

    private void SetUiTooltip()
    {
        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.uiTextTooltip = uiTooltipText; 
        }
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

        //button is not interactable while couroutine is running!
        if (runningCorouine)
        {
            btnCheckEquipment.interactable = false;
        }
        else
        {
            btnCheckEquipment.interactable = maxItemsReached;
        }

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
                    //Set Miner Items
                    ResetToMostImportantItems();
                    goodJobCoroutine = PlayGoodJob();
                    StartCoroutine(goodJobCoroutine);
                    return;
                }

                worstcasesCoroutine = PlayWorstcases(plainList.Contains(MinerEquipmentItem.Helm), plainList.Contains(MinerEquipmentItem.Atemmaske), plainList.Contains(MinerEquipmentItem.Lampe));

                StartCoroutine(worstcasesCoroutine);
                
                break;
            case EquipmentRound.Protection:
                if (plainList.Contains(MinerEquipmentItem.Schienbeinschuetzer) && plainList.Contains(MinerEquipmentItem.Sicherheitsschuhe) && plainList.Contains(MinerEquipmentItem.Schutzbrille))
                {
                    //Set Miner Items
                    ResetToSpecialTaskItems();
                    goodJobCoroutine = PlayGoodJob();
                    StartCoroutine(goodJobCoroutine);
                }
                else
                {
                    ResetToMostImportantItems();
                    badJobCoroutine = PlayBadJob();
                    StartCoroutine(badJobCoroutine);
                }
                break;
            case EquipmentRound.SpecialTask:
                Debug.Log("SpecialTask");
                gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
                runtimeData.isMinerDone = true;
                break;
        }
    }

    private void ResetToSpecialTaskItems()
    {
        itemsOnMiner = 6;
        currentRound = EquipmentRound.SpecialTask;
        uiInfoText.text = round3;
        headingRound.text = headingR3;
        btnConfirmText.text = "Ab in die Mine!";

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

    IEnumerator PlayBadJob()
    {
        runningCorouine = true;
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(true);
        audioSrc.clip = badJobClip;
        audioSrc.Play();
        float length = badJobAnim.length;
        anim.Play("BadJob");
        yield return new WaitForSeconds(length);
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);

        runningCorouine = false;
    }

    IEnumerator PlayGoodJob()
    {
        headingRound.gameObject.SetActive(false);
        uiInfoText.gameObject.SetActive(false);
        btnConfirmText.gameObject.SetActive(false);

        runningCorouine = true;

        denkbubbleWorstcase.transform.parent.gameObject.SetActive(true);
        float length = goodJobAnim.length;
        anim.Play("GoodJob");

        audioSrc.clip = goodJobClip;
        audioSrc.Play();
        yield return new WaitForSeconds(length);
        
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);

        runningCorouine = false;
        headingRound.gameObject.SetActive(true);
        uiInfoText.gameObject.SetActive(true);
        btnConfirmText.gameObject.SetActive(true);
    }
    
    IEnumerator PlayWorstcases(bool helm, bool mask, bool lamp)
    {
        runningCorouine = true;
        bool missingItem = !helm || !mask || !lamp;
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(missingItem);
        denkbubbleWorstcase.transform.parent.gameObject.GetComponent<Image>().color = Color.gray;
        denkbubbleWorstcase.GetComponent<Image>().color = Color.white;

        if (!helm)
        {
            anim.Play("NoHelmet");
            float length = noHelmAnim.length;
            Debug.Log("No Helm + length: " + length );
            audioSrc.clip = autsch;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!mask)
        {
            anim.Play("NoMask");
            float length = noMaskAnim.length;
            Debug.Log("No Atemmaske + length: " + length );
            audioSrc.clip = husten;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!lamp)
        {
            anim.Play("NoLight");
            float length = noLightAnim.length;
            audioSrc.clip = lichtaus;
            audioSrc.Play();
            audioSrc.clip = autsch;
            audioSrc.Play();
            Debug.Log("No lampe + length:" + length);

            yield return new WaitForSeconds(length);
        }

        if (missingItem)
        {
            audioSrc.clip = badJobClip;
            audioSrc.Play();
            float length = badJobAnim.length;
            anim.Play("BadJob");
            yield return new WaitForSeconds(length);
        }
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);

        //Reset items
        ResetAllMinerItems();
        runningCorouine = false;
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
        uiInfoText.text = round2;
        headingRound.text = headingR2;

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
