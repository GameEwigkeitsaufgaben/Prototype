using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprechblaseController : MonoBehaviour
{
    public Sprechblase sprechblaseDad, sprechblaseGabi;
    public AudioClip introDad, gabispeed1, dadspeed2;
    public Cave cave;
    public CaveColliderBottom caveColliderButtom;
    
    private bool audioPlayedOnce;

    float delayNextClip;
    AudioClip followingClip;
    
    // Start is called before the first frame update
    void Start()
    {
        sprechblaseDad.CreateAudioSource();
        sprechblaseGabi.CreateAudioSource();
    }


    public IEnumerator PlayNextAudio(string sprechbaseCharater, float delay, AudioClip clip)
    {
        
        yield return new WaitForSeconds(delay);


        if (sprechbaseCharater == "spdad")
        {
            sprechblaseDad.SetAudioClip(clip);
            sprechblaseDad.SetSprechblaseInPlayingMode();


        }else if (sprechbaseCharater == "spgabi")
        {
            sprechblaseGabi.SetAudioClip(clip);
            sprechblaseGabi.SetSprechblaseInPlayingMode();

        }

        yield return new WaitForSeconds(delay);
    }



    // Update is called once per frame
    void Update()
    {
        if (cave.currentStop == CoalmineStop.EntryArea)
        {
         
            //if(!(sprechblaseDad.btnInteraction.image.name == "dummy-videoPlayIcon") && GameData.introPlayedOnce)
            //{
            //    sprechblaseDad.PlayedOnceMode = true;
            //    cave.EnableButtons(true);
            //    sprechblaseDad.SetSprechblaseInPlayedOnceMode();
            //}
        }
                
        //if (!sprechblaseDad.GetAudioSource().isPlaying && !GameData.introPlayedOnce)
        //{
        //    GameData.introPlayedOnce = true;
        //    //audioPlayedOnce = false;
            //cave.EnableButtons(true);
        //    sprechblaseDad.SetSprechblaseInPlayedOnceMode();
        //}
    }
}
