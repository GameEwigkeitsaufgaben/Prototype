using UnityEngine;

public class Train : MonoBehaviour
{
    public Animator animator;
    public bool move;

    public void StartTrainMoving()
    {
        animator.SetBool("move", true);
        GetComponent<AudioSource>().Play();
    }
}
