using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diashow : MonoBehaviour
{
    public GameObject[] diasArray;
    float timePassed = 0f;
    int currentIndex = -1;

    // Start is called before the first frame update
    void Start()
    {
        NextDia();
    }


    private void NextDia()
    {
        currentIndex++;
        if (currentIndex > diasArray.Length - 1) currentIndex = 0;


        for(int i=0; i < diasArray.Length; i++)
        {
            
            diasArray[i].gameObject.SetActive(false);
        }

        diasArray[currentIndex].SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        if(timePassed > 10f)
        {
            NextDia();
            timePassed = 0f;
        }
    }
}
