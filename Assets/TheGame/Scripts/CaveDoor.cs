using UnityEngine;

public class CaveDoor : MonoBehaviour
{
    public Animator anim;
    private SoSfx sfx;
    public bool playDoorSound;
    private AudioSource doorMovingSfx;
    

    private void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        doorMovingSfx = gameObject.GetComponent<AudioSource>();
        doorMovingSfx.loop = false;
        doorMovingSfx.playOnAwake = false;
        doorMovingSfx.clip = sfx.coalmineCaveMoveDoors;

    }

    public void PlayMoveSfx()
    {
        if (!doorMovingSfx.isPlaying)
        {
            doorMovingSfx.Play();
        }
    }

    public void StopSfx()
    {
        doorMovingSfx.Stop();
    }

    public void CloseDoorAnim()
    {
        anim.SetBool("move", false);
    }

    public void OpenDoorAnim()
    {
        anim.SetBool("move",true);
    }
}
