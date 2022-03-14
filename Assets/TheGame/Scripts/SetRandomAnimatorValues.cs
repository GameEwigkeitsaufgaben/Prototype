using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SetRandomAnimatorValues : MonoBehaviour
{
    private Animator anim;
    public Vector2 speedValues;
    private float speed;
    private SpriteRenderer rend;
    public bool randomOffset;
    public Vector2 waitTime;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        speed = Random.Range(speedValues.x,speedValues.y);
        anim.speed = speed;
        float animStart = 0f;

        if (randomOffset)
        {
            var name = anim.GetCurrentAnimatorStateInfo(0).shortNameHash;
            animStart = Random.Range(0f, 1f);  
        }
        anim.Play(name, 0, animStart);
    }

    public void Delay()
    {
        StartCoroutine(DelayPlayBack());
    }

    public IEnumerator DelayPlayBack()
    {
        anim.speed = 0;
        if (rend != null)
        {
            rend.enabled = false;
        }
        yield return new WaitForSeconds(Random.Range(waitTime.x,waitTime.y));
        anim.speed = speed;
        if (rend != null)
        {
            rend.enabled = true;
        }
    }
}
