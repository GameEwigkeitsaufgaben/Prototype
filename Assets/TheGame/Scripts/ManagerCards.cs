using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerCards : MonoBehaviour
{
    public GameObject prefabCard;
    public GameObject parentCards;
    public SoMuseumCard[] soResourcesCards;
    public Image minerImg;
    public TMP_Text minerMsg;

    public GameObject[] cards;
    
    private int rightSelect, falseSelect;
    [SerializeField] int maxValTrueSolution = 0;
    public Button btnCheck, btnExit;
    SoMuseumConfig myConfig;
    private SoChapOneRuntimeData runtimeData;

    private void Awake()
    {
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeData);
        myConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
    }

    private void Start()
    {
        rightSelect = falseSelect = 0;
        soResourcesCards = GetShuffeldResources();
        CreateCards();
        maxValTrueSolution = GetMaxRightSolutions();
        minerImg.sprite = myConfig.minerIdle;
    }

    int GetMaxRightSolutions()
    {
        int tmpMaxVal = 0;
        foreach (var i in cards)
        {
            //Debug.Log(i.name + " ...... " + i.GetComponent<MuseumCard>().IsStatementTrue());
            if (i.GetComponent<MuseumCard>().IsStatementTrue()) tmpMaxVal++;
        }
        return tmpMaxVal;
    }

    public SoMuseumCard[] GetShuffeldResources()
    {
        List<SoMuseumCard> tmpObjs = new List<SoMuseumCard>();

        foreach(var i in soResourcesCards)
        {
            tmpObjs.Add(i);
        }

        tmpObjs.Shuffle();

        return tmpObjs.ToArray();
    }

    private void CreateCards()
    {
        cards = new GameObject[soResourcesCards.Length];

        for (int i = 0; i < soResourcesCards.Length; i++)
        {
            GameObject tmp = Instantiate(prefabCard);
            cards[i] = tmp;
            cards[i].transform.parent = parentCards.transform;
            cards[i].transform.localScale = Vector3.one;
            cards[i].transform.localPosition = Vector3.Scale(tmp.transform.localPosition, new Vector3(1f, 1f, 0f));

            cards[i].GetComponent<MuseumCard>().myResource = soResourcesCards[i];
            cards[i].name = "Card" + i;
            cards[i].AddComponent<MouseChange>();
            Debug.Log("create  " + cards[i].name);
        }

        foreach (var i in cards)
        {
            i.GetComponent<MuseumCard>().SetDefaults();
        }
    }

    public void CheckPostits()
    {
        rightSelect = falseSelect = 0;

        foreach (var i in cards)
        {
            if (!i.GetComponent<MuseumCard>().cardFaceDown && i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                rightSelect += 1;
            }
            else if(!i.GetComponent<MuseumCard>().cardFaceDown && !i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                falseSelect += 1;
            }
           
        }

        minerMsg.transform.parent.gameObject.SetActive(true);

        if (falseSelect > 0)
        {
            minerImg.sprite = myConfig.minerThumpDown;
            minerMsg.text = "Oh nein!\nRichtig wäre ...";
        }
        else
        {
            minerImg.sprite = myConfig.minerThumpUp;
            minerMsg.text = "\nSuper gemacht!";
        }

        foreach (var i in cards)
        {
            if (i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                i.GetComponent<MuseumCard>().MarkRightSolution();
            }
        }

        foreach(var i in cards)
        {
            if (i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                i.GetComponent<MuseumCard>().SetCardFaceUp();
            }
            else
            {
                i.GetComponent<MuseumCard>().SetCardFaceDown();
            }
        }

        btnCheck.gameObject.SetActive(false);
        btnExit.gameObject.SetActive(true);
    }

}
