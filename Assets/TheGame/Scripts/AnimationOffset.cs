using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationOffset : MonoBehaviour
{
    private Animator anim;
    [Range(0f,1f)]
    public float animStart = 0.2f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        var name =  anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        anim.Play(name, 0, animStart);
    }
}
