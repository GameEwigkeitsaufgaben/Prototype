using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MinerEquipmentRound : MonoBehaviour
{
    public Image imgHideRound;
    public TextMeshProUGUI tmpNbrItemsToDropOnMiner;
    //public int nbrItemsOnMiner;
    public bool roundActive; //only one round can be active to one time;
    public bool change;

    // Start is called before the first frame update
    void Start()
    {
        change = roundActive;
        ShowRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (change != roundActive)
        {
            ShowRound();

            change = roundActive;
        }

      //  tmpNbrItemsToDropOnMiner.text = nbrItemsOnMiner.ToString(); 
    }

    private void ShowRound()
    {
        if (roundActive)
        {
            imgHideRound.gameObject.SetActive(false);
        }
        else
        {
            imgHideRound.gameObject.SetActive(true);
        }
    }
}
