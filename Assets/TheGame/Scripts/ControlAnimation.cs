using UnityEngine;
using UnityEngine.UI;

public class ControlAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    public Slider slider;   //Assign the UI slider of your scene in this slot 
    [SerializeField]
    public string stateName;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        anim.Play(stateName, -1, slider.normalizedValue);
    }
}