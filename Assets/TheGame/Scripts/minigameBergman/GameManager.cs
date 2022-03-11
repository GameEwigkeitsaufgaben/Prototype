using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int numberKohle;
    // Start is called before the first frame update
    void Start()
    {
        numberKohle = 0;
    }

    public void IncrementKohle()
    {
        numberKohle++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
