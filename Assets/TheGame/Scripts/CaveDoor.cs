using UnityEngine;

public class CaveDoor : MonoBehaviour
{
    public Animator anim;
    private SoSfx sfx;
    public bool playDoorSound;
    

    private void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        gameObject.GetComponent<AudioSource>().clip = sfx.coalmineCaveMoveDoors;
    }

    public void PlayMoveSfx()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.GetComponent<AudioSource>().Play();
        }
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
