using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PumpenwerkStatus
{
    akitv,
    off,
    showAll
}

public class ManagerGrubenwasserhaltung : MonoBehaviour
{

    public Button btnActive, btnOff, btnNext;
    public GameObject textWHBisher, textWHNeu;
    public Image lohberg, fuerstleopold, augusteVictoria, hausAden, walsum, prosterHaniel, concordia, amalie, zollverein, carolinenglueck, friedlicherNachbar, heinrich, roberMueser;
    public Dictionary<string, Image> pumpwerke;

    private void Awake()
    {
        pumpwerke = new Dictionary<string, Image>();

        pumpwerke.Add(lohberg.name, lohberg);
        pumpwerke.Add(fuerstleopold.name, fuerstleopold);
        pumpwerke.Add(augusteVictoria.name, augusteVictoria);
        pumpwerke.Add(hausAden.name, hausAden);
        pumpwerke.Add(walsum.name, walsum);
        pumpwerke.Add(prosterHaniel.name, prosterHaniel);
        pumpwerke.Add(concordia.name, concordia);
        pumpwerke.Add(amalie.name, amalie);
        pumpwerke.Add(zollverein.name, zollverein);
        pumpwerke.Add(carolinenglueck.name, carolinenglueck);
        pumpwerke.Add(friedlicherNachbar.name, friedlicherNachbar);
        pumpwerke.Add(heinrich.name, heinrich);
        pumpwerke.Add(roberMueser.name, roberMueser);

        //Color32 colorIdle = new Color32(255, 255, 255, 50);
        //MarkAll(colorIdle);
        MarkAll();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MarkActive()
    {
        Color32 colorInactive = new Color32(255, 255, 255, 50);
        Color markerColor = Color.green;
        
        MarkAll(markerColor);
        fuerstleopold.color = augusteVictoria.color = 
        concordia.color = amalie.color = zollverein.color = colorInactive;
        textWHBisher.SetActive(false);
        textWHNeu.SetActive(true);
    }

    public void MarkInactive()
    {
        Color32 colorInactive = new Color32(255, 255, 255, 50);
        Color markColor = Color.red;
        MarkAll(colorInactive);
        fuerstleopold.color = augusteVictoria.color =
        concordia.color = amalie.color = zollverein.color = markColor;
        textWHBisher.SetActive(false);
        textWHNeu.SetActive(true);
    }

    public void MarkAll()
    {
        MarkAll(Color.white);
        textWHBisher.SetActive(true);
        textWHNeu.SetActive(false);
    }

    public void MarkAll(Color color)
    {
        foreach(Image value in pumpwerke.Values)
        {
            value.color = color;
        }
    }
}
