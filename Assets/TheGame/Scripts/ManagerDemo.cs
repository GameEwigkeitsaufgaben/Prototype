using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerDemo : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject myPrefab;
    public GameObject player;
    public List<GameObject> mob;
    public Canvas mobCanvas;
    public Button btnBackToInsta;

    public Image umweltschutz, wissenschaft, polder, grubenwasser, buerger, wasserversorger;

    private SoConfigChapter3 configCh3;
    private SoChapThreeRuntimeData runtimeCh03;
    private SoTalkingList demoAudios;
    private List<Demonstrant> demonstranten = new List<Demonstrant>();
    private AudioSource audioSrc;

    public bool buergerDone, umweltDone, scienceDone, polderVertreterDone, gwVertreterDone, wasserversorgerDone;
    

    private int finishedCount = 0;

    private Color feedbackGehoert = Color.white;

    private string currentClip = "";
    
    private void Awake()
    {
        configCh3 = Resources.Load<SoConfigChapter3>(GameData.NameConfigCH3Demo);
        runtimeCh03 = Resources.Load<SoChapThreeRuntimeData>(GameData.NameRuntimeDataChap03);
        demoAudios = Resources.Load<SoTalkingList>(GameData.NameCH3TLDemo);
        
        audioSrc = GetComponent<AudioSource>();

        //0 buerger, 1 umwelt, 2 science, 3 poldervertreter, 4 grubenwasservertreter, 5 wasserversorger
        buerger.GetComponent<Image>().sprite = configCh3.familie;
        buerger.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[0];
        demonstranten.Add(buerger.GetComponent<Demonstrant>());

        umweltschutz.GetComponent<Image>().sprite = configCh3.umweltschuetz;
        umweltschutz.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[1];
        demonstranten.Add(umweltschutz.GetComponent<Demonstrant>());

        wissenschaft.GetComponent<Image>().sprite = configCh3.wissenschaft;
        wissenschaft.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[2];
        demonstranten.Add(wissenschaft.GetComponent<Demonstrant>());

        polder.GetComponent<Image>().sprite = configCh3.poldervertretung;
        polder.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[3];
        demonstranten.Add(polder.GetComponent<Demonstrant>());

        grubenwasser.GetComponent<Image>().sprite = configCh3.grubenwasservertretung;
        grubenwasser.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[4];
        demonstranten.Add(grubenwasser.GetComponent<Demonstrant>());

        wasserversorger.GetComponent<Image>().sprite = configCh3.wasserversorgung;
        wasserversorger.GetComponent<Demonstrant>().audioClip = demoAudios.orderedListOfAudioClips[5];
        demonstranten.Add(wasserversorger.GetComponent<Demonstrant>());

        foreach(Demonstrant d in demonstranten)
        {
            d.gameObject.GetComponent<Image>().color = new Color32(99,99,99,255);
            d.SpeechBubbleOn(false);
        }
    }

    public void PlayAudio(int protagonist)
    {
        //0 buerger, 1 umwelt, 2 science, 3 poldervertreter, 4 grubenwasservertreter, 5 wasserversorger
        if (audioSrc.isPlaying)
        {
            audioSrc.Stop();
        }

        audioSrc.clip = demonstranten[protagonist].audioClip;

        Demonstrant demo;

        switch (protagonist)
        {
            case 0: 
                demo = buerger.GetComponent<Demonstrant>(); 
                break;
            case 1:
                demo = umweltschutz.GetComponent<Demonstrant>();
                break;
            case 2:
                demo = wissenschaft.GetComponent<Demonstrant>();
                break;
            case 3:
                demo = polder.GetComponent<Demonstrant>();
                break;
            case 4:
                demo = grubenwasser.GetComponent<Demonstrant>();
                break;
            case 5:
                demo = wasserversorger.GetComponent<Demonstrant>();
                break;
            default:
                demo = null;
                break;
        }

        if (demo != null) demo.SpeechBubbleOn(true);

        currentClip = audioSrc.clip.name;
        audioSrc.Play();
    }

    public void CreateDemoPeopleAroundPoint(int num, Vector3 point, float radius)
    {
        //Generate people in along a circle in a canvas
        //CreateDemoPeopleAroundPoint(20, player.transform.position, 43f);

        GameObject tmpObj = new GameObject(radius+"radius");
        tmpObj.transform.SetParent(mobCanvas.transform);

        for (int i = 0; i < num; i++)
        {
            /* Distance around the circle */
            var radians = 2 * Mathf.PI / num * i;

            /* Get the vector direction */
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians);

            var spawnDir = new Vector3(horizontal, 0, vertical);

            /* Get the spawn position */
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point

            /* Now spawn */
            var enemy = Instantiate(myPrefab, spawnPos, Quaternion.identity) as GameObject;

            /* Rotate the enemy to face towards player */
            enemy.transform.LookAt(point);

            /* Adjust height */
            enemy.transform.Translate(new Vector3(0, enemy.transform.localScale.y / 2, 0));

            enemy.transform.SetParent(tmpObj.transform);
        }

        //tmpObj.transform.rotation = Quaternion.Euler(0,Random.Range(0f,360f),0);
    }

    private void AudioFinished()
    {
        if (currentClip == "") return;

        Image demonstrant = null;
        
        if (currentClip == "ch03demo0-buerger")
        {
            buerger.GetComponent<Demonstrant>().gehoert = buergerDone = true;
            buerger.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = buerger;
            //buerger.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }
        else if (currentClip == "ch03demo1-umwelt") 
        {
            umweltschutz.GetComponent<Demonstrant>().gehoert = umweltDone = true;
            umweltschutz.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = umweltschutz;
            //umweltschutz.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }
        else if (currentClip == "ch03demo2-science")
        {
            wissenschaft.GetComponent<Demonstrant>().gehoert = scienceDone = true;
            wissenschaft.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = wissenschaft;
            //wissenschaft.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }
        else if (currentClip == "ch03demo3-poldervertreter")
        {
            polder.GetComponent<Demonstrant>().gehoert = polderVertreterDone =  true;
            polder.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = polder;
            //polder.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }
        else if (currentClip == "ch03demo4-grubenwasservertreter")
        {
            grubenwasser.GetComponent<Demonstrant>().gehoert = gwVertreterDone = true;
            grubenwasser.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = grubenwasser;
            //grubenwasser.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }
        else if (currentClip == "ch03demo5-wasserversorger")
        {
            wasserversorger.GetComponent<Demonstrant>().gehoert = wasserversorgerDone = true;
            wasserversorger.GetComponent<Image>().color = feedbackGehoert;
            demonstrant = wasserversorger;
            //wasserversorger.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        }

        demonstrant.GetComponent<Demonstrant>().SpeechBubbleOn(false);
        currentClip = "";
        audioSrc.clip = null;
    }

    private void Update()
    {
        if (audioSrc == null) return;
        // if (audioSrc.clip == null) return;

        if (!audioSrc.isPlaying)
        {
            finishedCount++;
            if(finishedCount >= 1)
            {
                AudioFinished();
            }
            else
            {
                finishedCount = 0;
            }
        }

        if (!runtimeCh03.IsPostDone(ProgressChap3enum.Post32))
        {
            if(buergerDone && umweltDone && scienceDone && polderVertreterDone && gwVertreterDone && wasserversorgerDone)
            {
                runtimeCh03.SetPostDone(ProgressChap3enum.Post32);
            }
        }
                
        else
        {
           if (btnBackToInsta.interactable) return;

            btnBackToInsta.interactable = true;
        }
        
    }
}
