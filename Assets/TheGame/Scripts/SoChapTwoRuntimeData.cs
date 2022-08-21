using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoChapTwoRuntimeData")]
public class SoChapTwoRuntimeData : Runtime
{
    public string hintPostUnlock;
    public string quizPointsCh02 = "***";
    public float quizPointsOverall = 0;
    public string singleSelectAwObjName = "--";
    public string generalKeyOverlay = GameData.NameOverlay2112;

    //progress in game
    public bool progressPost211Done, progress212MuseumDone, progressPost213Done, progressPost214Done, progressPost215Pumpen;
    public bool progressPost216Done, progressPost217Done, progressPost218PyritDone, progressPost219VideoDone, progressPost2110GWReinigungDone, progressPost2111QuizDone;

    public MuseumWaypoints lastWP = MuseumWaypoints.None;
    
    public bool interactTVDone, fliesspfadeDone = false;
    public bool reinAktivDone, reinPassivDone;
    public Vector3 groupPosition;

    public bool replayTL2121intro, replayTL2121outro, replay2122TVoutro, replayOverlay2122, replayOverlay2123, replayTL21101Reinigung;
    public TVStation state;


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
        replayTL2121intro = replayTL2121outro = replay2122TVoutro =  replayOverlay2122 = replayOverlay2123 = replayTL21101Reinigung = false;
        h2oFound = fes2Found = o2Found = so4Found = hFound = h2oFound = feFound = false;
        reinAktivDone = reinPassivDone = false;

        groupPosition = new Vector3(12.03f, 2.61f, -4.28f);
        lastWP = MuseumWaypoints.None;
        interactTVDone = fliesspfadeDone = false;
        singleSelectAwObjName = "--";
        quizPointsOverall = 0;
        state = TVStation.IntroOverlay;
    }

}
