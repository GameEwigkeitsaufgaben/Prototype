using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWS;

public class ManagerGWActive : MonoBehaviour
{
    public splineMove mySplineMove;
    public PathManager pBeluftToB1, pB1ToB2, pB2ToSulfat, pSulfatToOsmose, pOsmoseToCleanWater;
    public SpeechManagerMuseumChapTwo speechManager;
    public Viewpoint[] viewpoints;

    private SoChapTwoRuntimeData runtimeDataCh02;
    private SoChaptersRuntimeData runtimeDataChOverlap;
    private MuseumWaypoints targetMuseumStation;

    

    private void Awake()
    {
        runtimeDataCh02 = Resources.Load<SoChapTwoRuntimeData>(GameData.NameRuntimeDataChap02);
        runtimeDataChOverlap = Resources.Load<SoChaptersRuntimeData>(GameData.NameRuntimeDataChapters);
        runtimeDataChOverlap.SetSceneCursor(runtimeDataChOverlap.cursorDefault);
    }


    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i<viewpoints.Length; i++)
        {
           // viewpoints[0].
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
