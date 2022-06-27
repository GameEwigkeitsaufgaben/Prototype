using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationRandomOffset : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Start()
    {
        var name = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        float animStart = Random.Range(0f,1f);
        anim.Play(name, 0, animStart);
    }
}
