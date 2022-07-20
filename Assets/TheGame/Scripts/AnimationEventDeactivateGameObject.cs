using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventDeactivateGameObject : MonoBehaviour
{
    public GameObject[] particleSystems;

    public void Deactivate()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].SetActive(false);
        }
    }

    public void Activate()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].SetActive(true);
        }
    }
}
