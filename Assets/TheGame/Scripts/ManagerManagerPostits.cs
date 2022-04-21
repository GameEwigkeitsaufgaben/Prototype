using UnityEngine;
using UnityEngine.UI;

public class ManagerManagerPostits : MonoBehaviour
{
    public GameObject prefabCard;
    public GameObject parentCards;
    public SoMuseumCard[] soResourcesCards;

    public GameObject[] card;

    
    public PostItDrag[] postits;
    
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
        foreach (PostItDrag i in postits)
        {
            if (i.statementTrue) tmpMaxVal++;
        }
        return tmpMaxVal;
    }

    private void CreateCards()
    {
        foreach(var i in soResourcesCards)
        {
           GameObject tmp = Instantiate(prefabCard);
           tmp.transform.parent = parentCards.transform;
            tmp.transform.localScale = Vector3.one;
        }
    }

    public void CheckPostits()
    {
        foreach(PostItDrag i in postits)
        {
            Debug.Log(i.gameObject.name + " bsu: " + i.backsideUp + " st: " + i.statementTrue);

            if (!i.backsideUp && i.statementTrue)
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

        foreach(PostItDrag i in postits)
        {
            if (i.statementTrue)
            {
                i.MarkRightSolution();
            } 
        }

        rightSelect = 0;
    }

}
