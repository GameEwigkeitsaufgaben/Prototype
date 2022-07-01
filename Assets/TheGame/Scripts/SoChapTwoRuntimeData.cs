using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoChapTwoRuntimeData")]
public class SoChapTwoRuntimeData : Runtime
{
    public string hintPostUnlock;
    public string quizPointsCh02 = "***";
    public string singleSelectAwObjName = "--";

    //progress in game
    public bool progressPost211Done, progress212MuseumDone, progressPost213Done, progressPost214Done, progressPost215Pumpen;
    public bool progressPost216Done, progressPost217Done, progressPost218PyritDone, progressPost219VideoDone, progressPost2110GWReinigungDone, progressPost2111QuizDone;

    public MuseumWaypoints lastWP = MuseumWaypoints.None;
    public bool interactTVDone = false;
    public Vector3 groupPosition;


    public bool h2oFound, fes2Found, o2Found, so4Found, hFound, h2So4Found, feFound;

    public void SetAllDone()
    {
        progressPost211Done = true;
        progress212MuseumDone = true;
        progressPost213Done = progressPost214Done = progressPost215Pumpen = true;
        progressPost216Done = progressPost217Done = progressPost218PyritDone = true;
        progressPost219VideoDone = progressPost2110GWReinigungDone = progressPost2111QuizDone = true;
    }

    private void OnEnable()
    {
        progressPost211Done = true;
        progress212MuseumDone = false;
        progressPost213Done = progressPost214Done = progressPost215Pumpen = false;
        progressPost216Done = progressPost217Done = progressPost218PyritDone = false;
        progressPost219VideoDone = progressPost2110GWReinigungDone = progressPost2111QuizDone = false;
        h2oFound = fes2Found = o2Found = so4Found = hFound = h2oFound = feFound = false;

        groupPosition = new Vector3(12.03f, 2.61f, -4.28f);
        lastWP = MuseumWaypoints.None;
        interactTVDone = false;
        singleSelectAwObjName = "--";

}

}
