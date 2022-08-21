using UnityEngine;
using UnityEngine.UI;

public class ToggleFliesspfade : MonoBehaviour
{
    public Image normal, pressed;

    public void DisableNormal(bool disable)
    {
        normal.gameObject.SetActive(disable);
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Toggle>().isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<Toggle>().isOn && normal.gameObject.activeSelf)
        {
            normal.gameObject.SetActive(false);
            gameObject.GetComponent<Toggle>().interactable = false;
        }
        else if (!gameObject.GetComponent<Toggle>().isOn && !normal.gameObject.activeSelf)
        {
            normal.gameObject.SetActive(true);
            gameObject.GetComponent<Toggle>().interactable = true;
        }
    }
}
