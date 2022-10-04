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
    }

    public void StartKohlenhobelAnim()
    {
        anim.Play(name, 0, animStart);
    }
}
