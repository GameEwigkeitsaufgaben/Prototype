using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToogleWasserhaltung : MonoBehaviour
{
    public Image normal, pressed;
    public TMP_Text textOn, textOff;
    public TMP_Text btnTextInBetrieb, btnTextAlleBetrieb;
    private SoGameColors gameColors;

    public void DisableNormal(bool disable)
    {
        normal.gameObject.SetActive(disable);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
        gameObject.GetComponent<Toggle>().colors = GameColors.GetInteractionColorBlock();
        //normal.color = pressed.color = GameColors.defaultInteractionColorNormal;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Toggle>().isOn && normal.gameObject.activeSelf)
        {
            normal.gameObject.SetActive(false);
            textOn.gameObject.SetActive(false);
            textOff.gameObject.SetActive(true);

        }
        else if (!gameObject.GetComponent<Toggle>().isOn && !normal.gameObject.activeSelf)
        {
            normal.gameObject.SetActive(true);
            textOn.gameObject.SetActive(true);
            textOff.gameObject.SetActive(false);

        }
    }
}

