using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "SoChapTwoRuntimeData")]
public class SoChapTwoRuntimeData : Runtime
{
    public string hintPostUnlock;
    public string quizPointsCh02 = "***";
    public float quizPointsOverall = 0f;
    public bool updatePoints = false;
    public string singleSelectAwObjName = "--";
    public string generalKeyOverlay = GameData.NameOverlay2112;

    //progress in game
    public bool progressPost211Done, progress212MuseumDone, progressPost213Done, progressPost214Done, progressPost215Done;
    public bool progressPost216Done, progressPost217Done, progressPost218PyritDone, progressPost219VideoDone, progressPost2110GWReinigungDone, progressPost2111QuizDone;
    public bool progressPost2112Done;

    public MuseumWaypoints lastWP = MuseumWaypoints.None;
    
    public bool interactTVDone, fliesspfadeDone = false;
    public bool reinAktivDone, reinPassivDone;
    public bool interactPumpenDone = false;
    public Vector3 groupPosition;


    public bool replayTL2121intro, replayTL2121outro, replay2122TVoutro, replayOverlay2122, replayOverlay2123, replayTL21101Reinigung, replayPumpen;
    public bool replayPyrit;
    public TVStation state;


    public bool h2oFound, fes2Found, o2Found, so4Found, hFound, h2So4Found, feFound;
    internal float instaSliderPos;

    public void SetAllDone()
    {
        replayPyrit = true;
        progressPost211Done = true;
        progress212MuseumDone = true;
        progressPost213Done = progressPost214Done = progressPost215Done = true;
        progressPost216Done = progressPost217Done = progressPost218PyritDone = true;
        progressPost219VideoDone = progressPost2110GWReinigungDone = progressPost2111QuizDone = progressPost2112Done = true;

        replayTL2121intro = replayTL2121outro = replay2122TVoutro = replayOverlay2122 = replayOverlay2123 = replayTL21101Reinigung = true;
        interactTVDone = fliesspfadeDone = true;
        replayPumpen = true;

        reinAktivDone = reinPassivDone = true;
        interactPumpenDone = true;
    }

    private void OnEnable()
    {
        replayPyrit = false;
        updatePoints = false;
        replayPumpen = false;
        postOverlayToLoad = "";
        instaSliderPos = 1f;
        interactPumpenDone = false;
        progressPost211Done = false;
        progressPost2112Done = false;
        progress212MuseumDone = false;
        progressPost213Done = progressPost214Done = progressPost215Done = false;
        progressPost216Done = progressPost217Done = progressPost218PyritDone = false;
        progressPost219VideoDone = progressPost2110GWReinigungDone = progressPost2111QuizDone = false;
        replayTL2121intro = replayTL2121outro = replay2122TVoutro =  replayOverlay2122 = replayOverlay2123 = replayTL21101Reinigung = false;
        h2oFound = fes2Found = o2Found = so4Found = hFound = h2oFound = feFound = false;
        reinAktivDone = reinPassivDone = false;
        quizPointsCh02 = "***";

        groupPosition = new Vector3(12.03f, 2.61f, -4.28f);
        lastWP = MuseumWaypoints.None;
        interactTVDone = fliesspfadeDone = false;
        singleSelectAwObjName = "--";
        quizPointsOverall = 0;
        state = TVStation.IntroOverlay;
    }

}
