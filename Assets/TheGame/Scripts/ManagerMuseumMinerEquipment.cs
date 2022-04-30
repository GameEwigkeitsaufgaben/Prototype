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
    private const string plainTextMine = "Zur Grube!";

    //3 rounds with different question
    public MinerEquipmentRound roundEssential, roundProtection, roundSpecialTask;
    public EquipmentRound currentRound;

    //worstcase scenarios in shown bubble
    public Animator anim;
    public AnimationClip noHelmAnim, noMaskAnim, noLightAnim, badJobAnim, goodJobAnim;
    public Image denkbubbleWorstcase;
    public Sprite sNoHelm, sNoLamp, sNoMask, s4, minerGoodJob, minerBadJob;
    private IEnumerator worstcasesCoroutine, goodJobCoroutine, badJobCoroutine;

    //single items hold in lists
    public MuseumMinerEquipmentItem[] items;
    public int itemsOnMiner;
    private Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem> listItemsOnMiner  = new Dictionary<MinerEquipmentItem, MuseumMinerEquipmentItem>();
    private List<MinerEquipmentItem> plainList = new List<MinerEquipmentItem>();
    public GameObject dragParentBringItemToFront, reorderParentTop;

    public Button btnCheckEquipment;

    public AudioClip autsch, lichtaus, husten, badJobClip, goodJobClip;
    //public TextMeshProUGUI headingRound, textRound;
    public TMP_Text uiNbrItemsEssential, uiNbrItemProtection, uiNbrItemsSpecialTask, btnText;
    private AudioSource audioSrc;
    public TMP_Text uiTooltipText;
    bool runningCorouine = false;
    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("go name " + uiNbrItemsEssential.name);
        Debug.Log("go name + val " + uiNbrItemsEssential.text);

        foreach(MuseumMinerEquipmentItem i in items)
        {
            i.dragObjParent = dragParentBringItemToFront;
            i.orderTopParent = reorderParentTop;
            i.isDragable = true;
        }

        currentRound = EquipmentRound.Essential;

        SetRoundsVisible(true, false, false);
        
        itemsOnMiner = 0;
       
        denkbubbleWorstcase.GetComponent<Image>().preserveAspect = true;
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        SetUiTooltip();
    }

    public bool IsDragItemOk()
    {
        Debug.Log("items on miner " + itemsOnMiner + " current raound: " + currentRound  );
        //itemsOnMiner start value is 0;

        switch (currentRound)
        {
            case EquipmentRound.Essential:
                return itemsOnMiner < MaxItemsOnMinerRoundEssential;
            case EquipmentRound.Protection:
                return itemsOnMiner < MaxItemsOnMinerRoundProtection;
            case EquipmentRound.SpecialTask:
                return itemsOnMiner < MaxItemsOnMinerRoundSpectialTask;
            default:
                return false;
        }
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

    private void SetRoundsVisible(bool essential, bool protection, bool spectialTask)
    {
        roundEssential.roundActive = essential;
        roundProtection.roundActive = protection;
        roundSpecialTask.roundActive = spectialTask;
    }

    //called from Inspector button
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
        //the items form protesction are on miner = 6
        itemsOnMiner = MaxItemsOnMinerRoundProtection;
        currentRound = EquipmentRound.SpecialTask;
        btnText.text = plainTextMine;
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
       // headingRound.gameObject.SetActive(false);
       // uiInfoText.gameObject.SetActive(false);
       // btnConfirmText.gameObject.SetActive(false);

        runningCorouine = true;
        Debug.Log("GOOOOOOOOOOOOOOOOOOOOOOOD");
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(true);
        float length = goodJobAnim.length;
        anim.Play("GoodJob");

        audioSrc.clip = goodJobClip;
        audioSrc.Play();
        yield return new WaitForSeconds(length);
        
        denkbubbleWorstcase.transform.parent.gameObject.SetActive(false);

        runningCorouine = false;
        //headingRound.gameObject.SetActive(true);
        //uiInfoText.gameObject.SetActive(true);
        //btnConfirmText.gameObject.SetActive(true);
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
        itemsOnMiner = MaxItemsOnMinerRoundEssential;
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
}
