using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class MinerEquipmentRound : MonoBehaviour
{
    public Image hideRoundwithOverlayImg;
    public TextMeshProUGUI tmpNbrItemsToDropOnMiner;
    public bool roundActive; //only one round can be active at one time;
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
    }

    private void ShowRound()
    {
        if (roundActive)
        {
            hideRoundwithOverlayImg.gameObject.SetActive(false);
        }
        else
        {
            hideRoundwithOverlayImg.gameObject.SetActive(true);
        }
    }
}
