using UnityEngine;

public class LiftSohleTwo : MonoBehaviour
{
    public AudioSource dad1, youngster2, dad3;

    private bool dad1Played, youngster2Played, dad3Played;
    // Start is called before the first frame update
    void Start()
    {
        if (dad1 == null || youngster2 == null || dad3 == null)
        {
            enabled = false;
            return;
        }
        
    }

    public void PlayAudio ()
    {
        dad1.Play();
        youngster2.PlayDelayed(dad1.clip.length);
        dad3.PlayDelayed(dad1.clip.length + youngster2.clip.length);
        
    }

   
}
