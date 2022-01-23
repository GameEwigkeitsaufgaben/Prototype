using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour
{
    public Animator animator;
    public bool move;

    // Start is called before the first frame update
    void Start()
    {
        //Invoke("StartTrainMoving", 5f);        
    }

    public void StartTrainMoving()
    {
        Debug.Log("Start trin moving called -------------------");
        animator.SetBool("move", true);
        GetComponent<AudioSource>().Play();
    }
}
