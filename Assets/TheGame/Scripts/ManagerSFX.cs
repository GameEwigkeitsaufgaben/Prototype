using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSFX : MonoBehaviour
{
    private SoSfx sfx;

    bool playInstaMenuBGMusic = false;

    private void Start()
    {
        sfx = Resources.Load<SoSfx>("SfxConfig");
    }






}
