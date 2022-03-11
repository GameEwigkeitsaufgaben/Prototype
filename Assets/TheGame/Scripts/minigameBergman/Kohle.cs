using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kohle : MonoBehaviour
{
    public GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision " + collision.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger " + other.name);
        Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
