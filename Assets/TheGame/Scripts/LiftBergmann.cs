using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiftBergmann : MonoBehaviour
{
    public Transform targetpoint;
    public bool move = false,liftReached = false;
    public float speed;
    public GameObject sprechblase;
    public Button btnEinsteigen; 

    private void Start()
    {
        move = true;
    }

    private void Update()
    {
        if(move)
        transform.position = Vector3.MoveTowards(transform.position, targetpoint.position, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Invoke("SetSprechblaseActive",0.3f);
        move = false;
    }

    private void SetSprechblaseActive()
    {
        sprechblase.SetActive(true);
        liftReached = true;
        btnEinsteigen.gameObject.SetActive(true);
        btnEinsteigen.interactable = true;
    }
}
