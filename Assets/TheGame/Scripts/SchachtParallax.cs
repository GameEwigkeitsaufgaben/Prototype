using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchachtParallax : MonoBehaviour
{
    public GameObject prefabSchacht;
    public GameObject schachtParent;
    public GameObject einstieg;
    public GameObject sohle1;
     
    GameObject obj;
    

    // Start is called before the first frame update
    void Start()
    {
        
        //CreateSchacht();
        
    }

    private void CreateSchacht()
    {
        obj = Instantiate(prefabSchacht);
        obj.transform.SetParent(schachtParent.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.name == "TriggerSpawnSchacht" && GameData.nbrSchacht < 2)
        {
            Debug.Log("TriggerSpawnSchacht");
            CreateSchacht();
            GameData.nbrSchacht++;
            Debug.Log(GameData.nbrSchacht);
        }
        if(other.name == "TriggerSelfDestroy")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.moveCave)
        {
            transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
        }

        if(GameData.nbrSchacht == 2)
        {
            GameData.moveCave = false;
        }
    }
}
