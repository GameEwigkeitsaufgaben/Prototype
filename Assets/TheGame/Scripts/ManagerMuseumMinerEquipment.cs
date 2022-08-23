using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    private const int MaxItemsOnMinerRoundEssential = 3;
    private const int MaxItemsOnMinerRoundProtection = 6;
    private const int MaxItemsOnMinerRoundSpectialTask = 10;
    private const string checkBtnRichtig = "Richtig?";
    private const string checkBtnNochmal = "Nochmal!";


    //3 rounds with different question, not acive rounds will be hidden with a transparent overlay, (altern. set font visibility)
    public MinerEquipmentRound roundEssential, roundProtection, roundSpecialTask;
    public EquipmentRound currentRound;
    public TMP_Text tmpCheckBtn;

    //worstcase scenarios in shown bubble
    public Animator anim;
    public AnimationClip noHelmAnim, noMaskAnim, noLightAnim, badJobAnim, goodJobAnim;
    public Image denkbubbleWorstcase;
    public Sprite sNoHelm, sNoLamp, sNoMask, s4, minerGoodJob, minerBadJob;

    private IEnumerator worstcasesCoroutine, goodJobCoroutine, badJobCoroutine;

    //single items hold in lists
    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    public GameObject dragParentBringItemToFront, reorderParentTop;
    public GameObject parentTable;

    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();
    private List<MinerEquipmentItem> plainList = new List<MinerEquipmentItem>();


    public Button btnCheckEquipment, btnGoToMuseum;

    public AudioClip autsch, lichtaus, husten, badJobClip, goodJobClip;
    public TMP_Text uiNbrItemsEssential, uiNbrItemProtection, uiNbrItemsSpecialTask, btnText;
    public TMP_Text uiTooltipText;
    bool runningCorouine = false;

    private AudioSource audioSrc;
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
    }

    void Start()
    {
        btnGoToMuseum.interactable = false;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.dragObjParent = dragParentBringItemToFront;
            i.orderTopParent = reorderParentTop;
            i.parentTable = parentTable;
            i.isDragableInRound = true;
        }

        currentRound = EquipmentRound.Essential;

        SetRoundsVisible(true, false, false);
        
        itemsOnMiner = 0;
       
        denkbubbleWorstcase.GetComponent<Image>().preserveAspect = true;
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        SetUiTooltip();
        
    }

    public bool IsMaxItemsOnMinerReached()
    {
        bool maxReached = false;

        switch (currentRound)
        {
            case EquipmentRound.Essential:
                maxReached = itemsOnMiner >= MaxItemsOnMinerRoundEssential;
                break;
            case EquipmentRound.Protection:
                maxReached = itemsOnMiner >= MaxItemsOnMinerRoundProtection;
                break;
            case EquipmentRound.SpecialTask:
                maxReached = itemsOnMiner >= MaxItemsOnMinerRoundSpectialTask;
                break;
            default:
                maxReached = false;
                break;
        }

        Debug.Log("Drag and rob allowd: " + maxReached + " items on miner" + itemsOnMiner);

        return maxReached;
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

    private void SetRoundsVisible(bool essential, bool protection, bool spectialTask)
    {
        roundEssential.roundActive = essential;
        roundProtection.roundActive = protection;
        roundSpecialTask.roundActive = spectialTask;
    }

    //called from Inspector button
    public void CheckRound()
    {
        if(tmpCheckBtn.text == checkBtnNochmal)
        {
            Debug.Log("Reset to Nochmal ++++++++++++++++++++++++++++++");
            tmpCheckBtn.text = checkBtnRichtig;
            ResetAllMinerItems();
            currentRound = EquipmentRound.Essential;
            SetRoundsVisible(true, false, false);
            itemsOnMiner = 0;
            foreach (MuseumMinerEquipmentItem i in items)
            {
                i.ResetToTable();
                i.GetComponent<Image>().raycastTarget = true;
                i.isDragableInRound = true;
            }
            return;
        }
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
                    ProceedToMostImportantItems();
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
                    ProceedToSpecialTaskItems();
                    goodJobCoroutine = PlayGoodJob();
                    StartCoroutine(goodJobCoroutine);
                }
                else
                {
                    ProceedToMostImportantItems();
                    badJobCoroutine = PlayBadJob();
                    StartCoroutine(badJobCoroutine);
                }
                break;
            case EquipmentRound.SpecialTask:
                Debug.Log("SpecialTask");
                
                runtimeData.isMinerDone = true;
                tmpCheckBtn.text = checkBtnNochmal;
                break;
        }
    }

    public void GoToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void ProceedToSpecialTaskItems()
    {
        //the items form protesction are on miner = 6
        //itemsOnMiner = MaxItemsOnMinerRoundProtection;
        currentRound = EquipmentRound.SpecialTask;
        //btnText.text = plainTextMine;
        //btnConfirmText.text = "Ab in die Mine!";

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if (i.solutionItemRound1 || i.solutionItemRound2)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
               // i.GetComponent<Image>().color = Color.yellow;
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
        audioSrc.clip = badJobClip;
        audioSrc.Play();
        float length = badJobAnim.length;
        anim.Play("BadJob");
        yield return new WaitForSeconds(length);

        runningCorouine = false;
    }

    IEnumerator PlayGoodJob()
    {
        runningCorouine = true;
        Debug.Log("GOOOOOOOOOOOOOOOOOOOOOOOD");
        float length = goodJobAnim.length;
        anim.Play("GoodJob");

        audioSrc.clip = goodJobClip;
        audioSrc.Play();
        yield return new WaitForSeconds(length);

        runningCorouine = false;

    }
    
    IEnumerator PlayWorstcases(bool helm, bool mask, bool lamp)
    {
        runningCorouine = true;
        bool missingItem = !helm || !mask || !lamp;
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(missingItem);

        if (!helm)
        {
            anim.Play("Alert");
            yield return new WaitForSeconds(2f);
            anim.Play("NoHelmet");
            float length = noHelmAnim.length;
            Debug.Log("No Helm + length: " + length );
            audioSrc.clip = autsch;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!mask)
        {
            anim.Play("Alert");
            yield return new WaitForSeconds(2f);
            anim.Play("NoMask");
            float length = noMaskAnim.length;
            Debug.Log("No Atemmaske + length: " + length );
            audioSrc.clip = husten;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!lamp)
        {
            anim.Play("Alert");
            yield return new WaitForSeconds(2f);
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
        //itemsOnMiner = 0;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.ResetToTable();
        }
    }

    private void ProceedToMostImportantItems()
    {
        //itemsOnMiner = MaxItemsOnMinerRoundEssential;
        currentRound = EquipmentRound.Protection;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if(i.equipmentItem == MinerEquipmentItem.Helm || i.equipmentItem == MinerEquipmentItem.Atemmaske || i.equipmentItem == MinerEquipmentItem.Lampe)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
                //i.GetComponent<Image>().color = Color.yellow;
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

    void Update()
    {
        if(!btnGoToMuseum.interactable && runtimeData.isMinerDone)
        {
            btnGoToMuseum.interactable = true; 
        }

        bool maxItemsReached = false;

        switch (currentRound)
        {
            case EquipmentRound.Essential:
                uiNbrItemsEssential.text = (MaxItemsOnMinerRoundEssential - itemsOnMiner).ToString();
                maxItemsReached = (itemsOnMiner == MaxItemsOnMinerRoundEssential);
                SetRoundsVisible(true, false, false);
                break;
            case EquipmentRound.Protection:
                uiNbrItemProtection.text = (MaxItemsOnMinerRoundProtection - itemsOnMiner).ToString();
                maxItemsReached = (itemsOnMiner == MaxItemsOnMinerRoundProtection);
                SetRoundsVisible(false, true, false);
                break;
            case EquipmentRound.SpecialTask:
                uiNbrItemsSpecialTask.text = (MaxItemsOnMinerRoundSpectialTask - itemsOnMiner).ToString();
                maxItemsReached = (itemsOnMiner == MaxItemsOnMinerRoundSpectialTask);
                SetRoundsVisible(false, false, true);
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


}
