using UnityEngine;
using UnityEngine.UI;

public class ManagerManagerPostits : MonoBehaviour
{
    public GameObject prefabCard;
    public GameObject parentCards;
    public SoMuseumCard[] soResourcesCards;

    public GameObject[] cards;

    

    
    int rightSelect = 0;
    int maxValTrueSolution = 0;
    public Button btnCheck, btnExit;

    private void Start()
    {
        maxValTrueSolution = GetMaxRightSolutions();
        rightSelect = 0;
        CreateCards();
    }

    int GetMaxRightSolutions()
    {
        int tmpMaxVal = 0;
        foreach (var i in cards)
        {
            if (i.GetComponent<MuseumCard>().IsStatementTrue()) tmpMaxVal++;
        }
        return tmpMaxVal;
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
            Debug.Log("set defult for  " + i.GetComponent<MuseumCard>().myResource.name);
            i.GetComponent<MuseumCard>().SetDefaults();
        }
    }

    public void CheckPostits()
    {
        foreach(var i in cards)
        {
            Debug.Log(i.gameObject.name + " bsu: " + i.GetComponent<MuseumCard>().cardFaceDown + " st: " + i.GetComponent<MuseumCard>().IsStatementTrue());

            if (!i.GetComponent<MuseumCard>().cardFaceDown && i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                rightSelect += 1;
                Debug.Log("+1");
            }
           
        }
        Debug.Log("Right select" + rightSelect);
        if (maxValTrueSolution == rightSelect)
        {
            btnCheck.gameObject.SetActive(false);
            btnExit.gameObject.SetActive(true);
            
        }

        foreach(var i in cards)
        {
            if (i.GetComponent<MuseumCard>().IsStatementTrue())
            {
                i.GetComponent<MuseumCard>().MarkRightSolution();
            } 
        }

        rightSelect = 0;
    }

}
