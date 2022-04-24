using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Shapes;

public enum ViewpointLocation
{
    Mine,
    Museum
}

public class Viewpoint : MonoBehaviour
{
    //Interactive viewpoints in mine to interact with
    public static string viewpointSole1 = "ViewpointS1";
    public static string viewpointSole2 = "ViewpointS2";
    public static string viewpointSole3Bewetterung = "ViewpointS3PosterBewetterung";
    public static string viewpointSole3OVMine = "ViewpointS3PosterMinehouse";
    public static string viewpointSole3Longwallcutter = "UiLongwallCutter";

    public ViewpointLocation vpLocation;

    //Interactive viewpoints in museum to interact with


    SoGameColors gameColors; //SoGAmeColors
    SoGameIcons gameIcons; //SoGameIcons
    SoChapOneRuntimeData runtimeData;

    Image myViewpointImg;
    Disc myDisc;
    bool discSetToDone = false;

    private void Awake()
    {
        gameColors = Resources.Load<SoGameColors>(GameData.NameGameColors);
        gameIcons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeStoreData);
    }

    // Start is called before the first frame update
    void Start()
    {
        AssignLocalElements();

        myViewpointImg.sprite = gameIcons.viewpointArrow;
        myViewpointImg.color = gameColors.defaultInteractionColor;
    }

    void AssignLocalElements()
    {
        Transform[] elements = GetComponentsInChildren<Transform>(true);
        
        for(int i = 0; i < elements.Length; i++)
        {
            if(elements[i].GetComponent<Image>() != null)
            {
                myViewpointImg = elements[i].GetComponent<Image>();
            }
            else if (elements[i].GetComponent<Disc>() != null)
            {
                myDisc = elements[i].GetComponent<Disc>();
            }
        }
    }

    void SetInteractionDone()
    {
        myDisc.Color = GameColors.discInteractonDoneColor;
        discSetToDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!discSetToDone)
        {
            if(runtimeData.interaction116Done && vpLocation == ViewpointLocation.Mine) SetInteractionDone();
            else if (runtimeData.sole1done && gameObject.name == viewpointSole1) SetInteractionDone();
            else if (runtimeData.sole2done && gameObject.name == viewpointSole2) SetInteractionDone();
            else if (runtimeData.sole3BewetterungDone && gameObject.name == viewpointSole3Bewetterung) SetInteractionDone();
            else if (runtimeData.sole3GebaeudeDone && gameObject.name == viewpointSole3OVMine) SetInteractionDone();
            else if (runtimeData.isLongwallCutterDone && gameObject.name == viewpointSole3Longwallcutter) SetInteractionDone();
        }

    }
}
