using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoNoName : MonoBehaviour
{
    public Sprite[] demoNonameSprites;
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.GetComponent<Image>().sprite = demoNonameSprites[Random.Range(0, demoNonameSprites.Length)];
        Quaternion goRotation = gameObject.transform.rotation;
        int sign = Random.Range(0, 2) == 0 ? -1 : 1;
        gameObject.transform.rotation = Quaternion.Euler(goRotation.x, goRotation.y * sign , goRotation.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
