using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationOffset : MonoBehaviour
{
    private Animator anim;
    [Range(0f,1f)]
    public float animStart = 0.0f;
    private SoChapOneRuntimeData runtimeDataCh01;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        runtimeDataCh01 = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        var name =  anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
        runtimeDataCh01.kohlenhobelAnimator = anim;
        //StartKohlenhobelAnim();
    }

    public void StartKohlenhobelAnim()
    {
        Debug.Log("in Anim Offset: " + anim.GetCurrentAnimatorStateInfo(0).ToString() + " animStart " + animStart);
        anim.Play(name, 0, animStart);
    }
}
