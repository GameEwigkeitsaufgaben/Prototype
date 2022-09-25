using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerMuseumMinerEquipment : MonoBehaviour
{
    private const int MaxItemsOnMinerRoundEssential = 3;
    private const int MaxItemsOnMinerRoundProtection = MaxItemsOnMinerRoundEssential + 3;
    private const int MaxItemsOnMinerRoundSpectialTask = MaxItemsOnMinerRoundProtection + 4;
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

    public AudioClip autsch, lichtaus, husten, badJobClip, goodJobClip, alarm, audioFBoutro;
    public TMP_Text uiNbrItemsEssential, uiNbrItemProtection, uiNbrItemsSpecialTask, btnText;
    public TMP_Text uiTooltipText;
    bool runningCorouine = false;

    private AudioSource audioSrc;
    private SoChapOneRuntimeData runtimeData;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoSfx sfx;
    private int tries = 0;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        
        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorDefault);
        sfx = runtimeDataChapters.LoadSfx();

    }

    void Start()
    {
        btnGoToMuseum.interactable = runtimeData.isMinerDone;
        tries = 0;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.dragObjParent = dragParentBringItemToFront;
            i.orderTopParent = reorderParentTop;
            i.parentTable = parentTable;
            i.isDragableInRound = true;
            i.EnableParticles(false);

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

    private void EnableParticesOnRound(EquipmentRound round, bool enable)
    {
        switch (round)
        {
            case EquipmentRound.Essential:
                items[(int)MinerEquipmentItem.Helm].EnableParticles(enable);
                items[(int)MinerEquipmentItem.Lampe].EnableParticles(enable);
                items[(int)MinerEquipmentItem.Atemmaske].EnableParticles(enable);
                break;
            case EquipmentRound.Protection:
                items[(int)MinerEquipmentItem.Schutzbrille].EnableParticles(enable);
                items[(int)MinerEquipmentItem.Sicherheitsschuhe].EnableParticles(enable);
                items[(int)MinerEquipmentItem.Schienbeinschuetzer].EnableParticles(enable);
                break;
            case EquipmentRound.SpecialTask:
                //Cace could not be reached!
                break;
        }
    }

    //called from Inspector button
    public void CheckRound()
    {
        if(tmpCheckBtn.text == checkBtnNochmal)
        {
            tmpCheckBtn.text = checkBtnRichtig;
            ResetAllMinerItems();
            currentRound = EquipmentRound.Essential;
            SetRoundsVisible(true, false, false);

            foreach (MuseumMinerEquipmentItem i in items)
            {
                i.ResetToTable();
                i.EnableParticles(false);
                i.GetComponent<Image>().raycastTarget = true;
                i.isDragableInRound = true;
            }
            //no explicit reset of items on miner, because ExitTrigger resets minerItems to 0!!
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
                tries++;
                break;
            case EquipmentRound.Protection:
                if (plainList.Contains(MinerEquipmentItem.Schienbeinschuetzer) && plainList.Contains(MinerEquipmentItem.Sicherheitsschuhe) && plainList.Contains(MinerEquipmentItem.Schutzbrille))
                {
                    ProceedToSpecialTaskItems();
                    goodJobCoroutine = PlayGoodJob();
                    StartCoroutine(goodJobCoroutine);
                    return;
                }
                else
                {
                    ProceedToMostImportantItems();
                    badJobCoroutine = PlayBadJob();
                    StartCoroutine(badJobCoroutine);
                    tries++;
                }
                break;
            case EquipmentRound.SpecialTask:
                runtimeData.isMinerDone = true;
                tmpCheckBtn.text = checkBtnNochmal;
                audioSrc.clip = sfx.minerOutro;
                audioSrc.Play();
                break;
        }

        if (tries > 0)
        {
            EnableParticesOnRound(currentRound, true);
        }
    }

    public void GoToMuseum()
    {
        gameObject.GetComponent<SwitchSceneManager>().GoToMuseum();
    }

    private void ProceedToSpecialTaskItems()
    {
        EnableParticesOnRound(currentRound, false);
        currentRound = EquipmentRound.SpecialTask;
        tries = 0;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if (i.solutionItemRound1 || i.solutionItemRound2)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
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
        audioSrc.clip = sfx.badJobSfx;
        audioSrc.Play();
        float length = badJobAnim.length;
        anim.Play("BadJob");
        yield return new WaitForSeconds(length);

        runningCorouine = false;
    }

    IEnumerator PlayGoodJob()
    {
        runningCorouine = true;
        float length = goodJobAnim.length;
        anim.Play("GoodJob");

        audioSrc.clip = sfx.goodJobSfx;
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
            audioSrc.clip = sfx.mineAlarm;
            audioSrc.Play();
            yield return new WaitForSeconds(2f);
            audioSrc.Stop();
            anim.Play("NoHelmet");
            float length = noHelmAnim.length;
            Debug.Log("No Helm + length: " + length );
            audioSrc.clip = sfx.autschSfx;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!mask)
        {
            anim.Play("Alert");
            audioSrc.clip = sfx.mineAlarm;
            audioSrc.Play();
            yield return new WaitForSeconds(2f);
            audioSrc.Stop();
            anim.Play("NoMask");
            float length = noMaskAnim.length;
            Debug.Log("No Atemmaske + length: " + length );
            audioSrc.clip = sfx.mineHusten;
            audioSrc.Play();
            yield return new WaitForSeconds(length);
        }
        if (!lamp)
        {
            anim.Play("Alert");
            audioSrc.clip = sfx.mineAlarm;
            audioSrc.Play();
            yield return new WaitForSeconds(2f);
            audioSrc.Stop();
            anim.Play("NoLight");
            float length = noLightAnim.length;
            audioSrc.clip = lichtaus;
            audioSrc.Play();
            audioSrc.clip = sfx.autschSfx;
            audioSrc.Play();
            Debug.Log("No lampe + length:" + length);

            yield return new WaitForSeconds(length);
        }

        if (missingItem)
        {
            audioSrc.clip = sfx.badJobSfx;
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
        //no explicit reset of itmsOnMiner, is done by exit Trigger!!
        foreach (MuseumMinerEquipmentItem i in items)
        {
            i.ResetToTable();
        }
    }

    private void ProceedToMostImportantItems()
    {
        EnableParticesOnRound(currentRound, false);
        currentRound = EquipmentRound.Protection;
        tries = 0;

        foreach (MuseumMinerEquipmentItem i in items)
        {
            if(i.equipmentItem == MinerEquipmentItem.Helm || i.equipmentItem == MinerEquipmentItem.Atemmaske || i.equipmentItem == MinerEquipmentItem.Lampe)
            {
                i.ResetToMiner();
                i.GetComponent<Image>().raycastTarget = false;
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
