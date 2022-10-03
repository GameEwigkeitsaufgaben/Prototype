using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BehaviourButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    //MouseCLick works only with Button Sprite Simple not with Scliced
    MouseChange mouse;
    AudioSource audioSrc;
    SoSfx sfx;

    private void Awake()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        gameObject.AddComponent<MouseChange>();
        //audioSrc = gameObject.AddComponent<AudioSource>();
        //audioSrc.clip = sfx.mouseHammer;
        //audioSrc.playOnAwake = false;
        //audioSrc.loop = false;
    }


    private void Start()
    {
        mouse = gameObject.GetComponent<MouseChange>();
        //gameObject.GetComponent<Button>().onClick.AddListener(PlaySound);


        if (gameObject.GetComponent<Image>() == null) return;
        
        if (gameObject.GetComponent<Image>().sprite.name == "UISprite") return;
        
        gameObject.AddComponent<ReactOnlyOnInTranspartentParts>();
    }

    private void PlaySound()
    {
        Debug.Log("Play Sound " + gameObject.name);
        //if (audioSrc == null) return;
        //if (!audioSrc.isActiveAndEnabled) return;
        //if (audioSrc.isPlaying) return;

        //mouse.PlaySfx();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouse.MouseEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouse.MouseExit();
    }

}
