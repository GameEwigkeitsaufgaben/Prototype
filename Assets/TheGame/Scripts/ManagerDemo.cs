using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagerDemo : MonoBehaviour
{
    private const string clipNameBuerger = "ch03demo0-buerger";
    private const string clipNameUmweltschutz = "ch03demo1-umwelt";
    private const string clipNameWissenschaft = "ch03demo2-science";
    private const string clipNamePoldervertreter = "ch03demo3-poldervertreter";
    private const string clipNameGrubenwasservertreter = "ch03demo4-grubenwasservertreter";
    private const string clipNameWasserversorger = "ch03demo5-wasserversorger";

    // Start is called before the first frame update

    public GameObject myPrefab;
    public GameObject player;
    public List<GameObject> mob;
    public Canvas mobCanvas;
    public Button btnBackToInsta;

    public GameObject umweltschutz, wissenschaft, polder, grubenwasser, buerger, wasserversorger;

    private SoConfigChapter3 configCh3;
    private SoChaptersRuntimeData runtimeDataChapters;
    private SoChapThreeRuntimeData runtimeCh03;
    private SoTalkingList demoAudios;
    private SoSfx sfx;
    private List<Demonstrant> demonstranten = new List<Demonstrant>();
    private AudioSource audioSrc;


    public bool buergerDone, umweltDone, scienceDone, polderVertreterDone, gwVertreterDone, wasserversorgerDone;
    public AudioSource audioScrAtmo;

    private int finishedCount = 0;
    private string currentClip = "";

    private Demonstrant demoBuerger, demoUmweltschutz, demoWissenschaft, demoPolder, demoGrubenwasser, demoWasserversorger;
    
    private void Awake()
    {
        runtimeDataChapters = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeCh03 = runtimeDataChapters.LoadChap3RuntimeData();

        runtimeDataChapters.SetSceneCursor(runtimeDataChapters.cursorTexture3DCave);

        configCh3 = Resources.Load<SoConfigChapter3>(GameData.NameConfigCH3Demo);
        demoAudios = Resources.Load<SoTalkingList>(GameData.NameCH3TLDemo);
        sfx = runtimeDataChapters.LoadSfx();
        audioSrc = GetComponent<AudioSource>();

        demoBuerger = buerger.GetComponent<Demonstrant>();
        demoUmweltschutz = umweltschutz.GetComponent<Demonstrant>();
        demoWissenschaft = wissenschaft.GetComponent<Demonstrant>();
        demoPolder = polder.GetComponent<Demonstrant>();
        demoGrubenwasser = grubenwasser.GetComponent<Demonstrant>();
        demoWasserversorger = wasserversorger.GetComponent<Demonstrant>();
    }

    private void Start()
    {
        //0 buerger, 1 umwelt, 2 science, 3 poldervertreter, 4 grubenwasservertreter, 5 wasserversorger
        demoBuerger.characterImage.sprite = configCh3.familie;
        demoBuerger.audioClip = demoAudios.orderedListOfAudioClips[0];
        demonstranten.Add(demoBuerger);
       
        demoUmweltschutz.characterImage.sprite = configCh3.umweltschuetz;
        demoUmweltschutz.audioClip = demoAudios.orderedListOfAudioClips[1];
        demonstranten.Add(demoUmweltschutz);

        demoWissenschaft.characterImage.sprite = configCh3.wissenschaft;
        demoWissenschaft.audioClip = demoAudios.orderedListOfAudioClips[2];
        demonstranten.Add(demoWissenschaft);

        demoPolder.characterImage.sprite = configCh3.poldervertretung;
        demoPolder.audioClip = demoAudios.orderedListOfAudioClips[3];
        demonstranten.Add(demoPolder);

        demoGrubenwasser.characterImage.sprite = configCh3.grubenwasservertretung;
        demoGrubenwasser.audioClip = demoAudios.orderedListOfAudioClips[4];
        demonstranten.Add(demoGrubenwasser);

        demoWasserversorger.characterImage.sprite = configCh3.wasserversorgung;
        demoWasserversorger.audioClip = demoAudios.orderedListOfAudioClips[5];
        demonstranten.Add(demoWasserversorger);

        if (runtimeCh03.IsPostDone(ProgressChap3enum.Post32))
        {
            foreach (Demonstrant demo in demonstranten)
            {
                demo.SetGehoert();
                demo.SetGehoertFeedback(true);
            }

            btnBackToInsta.interactable = true;
            return;
        }

        audioScrAtmo.playOnAwake = false;
        audioScrAtmo.loop = true;
        audioScrAtmo.clip = sfx.atmoDemo;
        audioScrAtmo.Play();
    }

    public void PlayAudio(int protagonist)
    {
        //0 buerger, 1 umwelt, 2 science, 3 poldervertreter, 4 grubenwasservertreter, 5 wasserversorger
        if (audioSrc.isPlaying)
        {
            foreach(Demonstrant e in demonstranten)
            {
                e.SpeechBubbleOn(false);
                if (!e.gehoert) e.SetGehoertFeedback(false);
            }
            audioSrc.Stop();
        }

        audioSrc.clip = demonstranten[protagonist].audioClip;

        Demonstrant demo;

        switch (protagonist)
        {
            case 0: 
                demo = demoBuerger; 
                break;
            case 1:
                demo = demoUmweltschutz;
                break;
            case 2:
                demo = demoWissenschaft;
                break;
            case 3:
                demo = demoPolder;
                break;
            case 4:
                demo = demoGrubenwasser;
                break;
            case 5:
                demo = demoWasserversorger;
                break;
            default:
                demo = null;
                break;
        }

        if (demo != null) demo.SpeechBubbleOn(true);
        demo.SetGehoertFeedback(true);

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

        Demonstrant demonstrant = null;
        
        if (currentClip == clipNameBuerger)
        {
            demoBuerger.SetGehoert();
            demoBuerger.SetGehoertFeedback(true);
            buergerDone = true;
            demonstrant = demoBuerger;
        }
        else if (currentClip == clipNameUmweltschutz) 
        {
            umweltDone = true;
            demoUmweltschutz.SetGehoert();
            demoUmweltschutz.SetGehoertFeedback(true);
            demonstrant = demoUmweltschutz;
        }
        else if (currentClip == clipNameWissenschaft)
        {
            demoWissenschaft.SetGehoert();
            demoWissenschaft.SetGehoertFeedback(true);
            scienceDone = true;
            demonstrant = demoWissenschaft;
        }
        else if (currentClip == clipNamePoldervertreter)
        {
            demoPolder.SetGehoert();
            demoPolder.SetGehoertFeedback(true);
            polderVertreterDone =  true;
            demonstrant = demoPolder;
        }
        else if (currentClip == clipNameGrubenwasservertreter)
        {
            demoGrubenwasser.SetGehoert();
            demoGrubenwasser.SetGehoertFeedback(true);
            gwVertreterDone = true;
            demonstrant = demoGrubenwasser;
        }
        else if (currentClip == clipNameWasserversorger)
        {
            demoWasserversorger.SetGehoert();
            demoWasserversorger.SetGehoertFeedback(true);
            wasserversorgerDone = true;
            demonstrant = demoWasserversorger;
        }

        demonstrant.SpeechBubbleOn(false);
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
