using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFliesspfade : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAnimation(string triggerName)
    {
        animator.SetTrigger(triggerName);
    }
}
