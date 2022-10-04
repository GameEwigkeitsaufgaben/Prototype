using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ControlMultipleAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    public Slider slider;   //Assign the UI slider of your scene in this slot 

    [SerializeField]
    public List<Clips> states;

    public float interval;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = 0;
        interval = 1.0f / (float)states.Count;
        float val = 0;

        for (int i = 0; i < states.Count; i++)
        {
            states[i].startValue = val;
            val += interval;
            states[i].endValue = val;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Clips clip = GetClip(states, slider.normalizedValue);
        float val = Mathf.InverseLerp(clip.startValue,clip.endValue,slider.normalizedValue);
        anim.Play(clip.name, -1, val);
    }

    public Clips GetClip(List<Clips> c, float val)
    {
        if (val <= 0)
        {
            return c[0];
        }
        else if (val >= 1)
        {
            return c[c.Count - 1];
        }

        for (int i = 0; i < c.Count; i++)
        {
            if (val > c[i].startValue && val < c[i].endValue)
            {
                return c[i];
            }
        }

        return null;
    }



    [Serializable]
    public class Clips
    {
        public string name;
        public float startValue;
        public float endValue;
    }
}