using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KohlehobelManager : MonoBehaviour
{
    public Player player;
    public SwitchSceneManager switchScene;

    // Start is called before the first frame update
    void Start()
    {
        player.followAnker = false;
        if (false)
        { //testing ohne sohle3
            GameData.cavePosX = 0.12937f;
            GameData.cavePosY = -176.2351f;
            GameData.cavePosZ = 0.46835f;
            GameData.sohleToReload = (int)CurrentStop.Sohle3;
        }
    }

    public void TeleportToTafelTransportKohle(GameObject obj)
    {
        player.SetTarget(obj);
    }

    public void TeleportToKohleHobel(GameObject obj)
    {
        player.SetTarget(obj);
    }

    public void GoToBlackscreen()
    {
        switchScene.SwitchSceneWithTransition(GameData.scene1165Blackscreen);
        //GameData.gotToKohlehobel = true;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
