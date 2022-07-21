using UnityEngine;
using UnityEngine.UI;

public class DemoNoName : MonoBehaviour
{
    private SoConfigChapter3 configChap3;
    Sprite[] noNames = new Sprite[8];

    private void Awake()
    {
        configChap3 = Resources.Load<SoConfigChapter3>("ConfigCh3");
        noNames[0] = configChap3.demoNoNameFemale1;
        noNames[1] = configChap3.demoNoNameFemale2;
        noNames[2] = configChap3.demoNoNameFemale3;
        noNames[3] = configChap3.demoNoNameFemale4;
        noNames[4] = configChap3.demoNoNameMale1; 
        noNames[5] = configChap3.demoNoNameMale2;
        noNames[6] = configChap3.demoNoNameMale3;
        noNames[7] = configChap3.demoNoNameMale4;
    }

    void Start()
    {
        gameObject.GetComponent<Image>().sprite = noNames[Random.Range(0, noNames.Length)];
        int sign = Random.Range(0, 2) == 0 ? -1 : 1;
        gameObject.transform.localScale = new Vector3(sign, 1, 1);
    }

}
