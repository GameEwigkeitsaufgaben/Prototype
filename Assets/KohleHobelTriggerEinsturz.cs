using UnityEngine;

public class KohleHobelTriggerEinsturz : MonoBehaviour
{
    public GameObject prefab;
    public Transform position;
    private GameObject verbruch;
    private SoSfx sfx;
    private AudioSource audioSrc;

    private void Start()
    {
        sfx = Resources.Load<SoSfx>(GameData.NameConfigSfx);
        audioSrc = gameObject.AddComponent<AudioSource>();
        audioSrc.clip = sfx.coalmineBreakingRock;
        audioSrc.playOnAwake = false;
    }

    public void Einsturz()
    {
        verbruch = Instantiate(prefab, position.position,Quaternion.identity);
        audioSrc.Play();
    }

    public void DestroyVerbruch()
    {
        if (verbruch != null)
        {
            Destroy(verbruch);
        }
    }
}
