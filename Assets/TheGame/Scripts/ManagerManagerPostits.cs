using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerManagerPostits : MonoBehaviour
{
    public GameObject prefabCard;
    public GameObject parentCards;
    public SoMuseumCard[] soResourcesCards;
    public Image minerImg;

    public GameObject[] cards;
    
    int rightSelect = 0;
    [SerializeField] int maxValTrueSolution = 0;
    public Button btnCheck, btnExit;
    SoMuseumConfig myConfig;

    private void Start()
    {
        myConfig = Resources.Load<SoMuseumConfig>(GameData.NameConfigMuseum);
        rightSelect = 0;
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

        Debug.Log("shuffle");
        List<SoMuseumCard> tmpObjs = new List<SoMuseumCard>();
        
        foreach(var i in soResourcesCards)
        {
            Debug.Log("sh " +i.name);
        }

        foreach(var i in soResourcesCards)
        {
            tmpObjs.Add(i);
        }

        tmpObjs.Shuffle();

        foreach (var i in tmpObjs)
        {
            Debug.Log("sh obj " + i.name);
        }

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
            Debug.Log("create  " + cards[i].name);
        }

        foreach (var i in cards)
        {
            //Debug.Log("set defult for  " + i.GetComponent<MuseumCard>().myResource.name);
            i.GetComponent<MuseumCard>().SetDefaults();
        }
    }

    public void CheckPostits()
    {
        rightSelect = 0;

        foreach (var i in cards)
        {
            Debug.Log(i.gameObject.name + " bsu: " + i.GetComponent<MuseumCard>().cardFaceDown + " st: " + i.GetComponent<MuseumCard>().IsStatementTrue());

            if (!i.GetComponent<MuseumCard>().cardFaceDown && i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                rightSelect += 1;
                Debug.Log("+1");
            }
           
        }

        foreach (var i in cards)
        {
            if (i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                i.GetComponent<MuseumCard>().MarkRightSolution();
            }
        }


        if (maxValTrueSolution == rightSelect)
        {
            minerImg.sprite = myConfig.minerThumpUp;
            btnCheck.gameObject.SetActive(false);
            btnExit.gameObject.SetActive(true);
            Debug.Log("Right solution found ----------------");
            return;
        }

        minerImg.sprite = myConfig.minerThumpDown;
        Debug.Log("Sprite sollte gesettz sein");
    }

}
