using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ProgressChap3
{
    public string postName = "--";
    public bool done = false;

    public ProgressChap3(string pName)
    {
        postName = pName;
    }
}

public enum ProgressChap3enum
{
    Post32, Post33,
    Post34, Post35,
    Post36, Post37,
    Post38, Post39,
    Post310, Post311,
    Post312, Post313,
    Post314, Post315,
    Post316, Post317
}

[CreateAssetMenu(menuName = "SoChapThreeRuntimeData")]
public class SoChapThreeRuntimeData : Runtime
{

    public string[] namesEnum = System.Enum.GetNames(typeof(ProgressChap3enum));
    public ProgressChap3[] progressDone;
    public string quizPointsCh03 = "***";
    public string singleSelectAwObjName = "--";


    //Called in Entry
    public void SetPostDone(ProgressChap3enum post)
    {
        progressDone[(int)post].done = true;
    }

    //Called in Entry
    public bool IsPostDone(ProgressChap3enum post)
    {
        return progressDone[(int)post].done;
    }

    private void OnEnable()
    {
        //Init Progress;
        progressDone = new ProgressChap3[namesEnum.Length];

        for (int i = 0; i < progressDone.Length; i++)
        {
            progressDone[i] = new ProgressChap3(namesEnum[i]);
            progressDone[i].done = false;
        }
    }

    public void SetAllDone()
    {

        for (int i = 0; i < progressDone.Length; i++)
        {
            progressDone[i] = new ProgressChap3(namesEnum[i]);
            progressDone[i].done = true;
        }
    }
}
