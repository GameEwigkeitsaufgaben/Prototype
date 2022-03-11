using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNextMinerScene : MonoBehaviour
{
    public MinerGameData minerData;
    public Text nbrText;

    public void LoadNextScene()
    {
        if (minerData.GetNbrMinerItems() > 0)
        {
  
            nbrText.color = Color.red;
        }
        else
        {
            SceneManager.LoadScene(1);
        }
       
    }
    
}
