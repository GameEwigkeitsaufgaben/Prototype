using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSchild : MonoBehaviour
{

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        Invoke("StartMyAnimation", Random.Range(1,5));
        
    }

    void StartMyAnimation()
    {
        animator.Play("Schild");
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }
}
