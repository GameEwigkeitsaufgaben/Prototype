using UnityEngine;
using UnityEngine.UI;
using Shapes;
using UnityEngine.SceneManagement;

public enum ViewpointLocation
{
    Mine,
    Museum,
    OutdoorGW
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
    public static string viewpointMuseumMiner = "ViewpointMusumMiner";
    public static string viewpointMuseumMyth = "ViewpointMuseumMyth";
    public static string viewpointMuseumInkohlung = "ViewpointMuseumInkohlung";
    public static string viewpointMuseumWorld = "ViewpointMuseumWorld";

    //Interactive viewpoints in GW Chap02
    public static string viewpointBeluftung = "ViewpointGWBeluftung";
    public static string viewpointAbsetzbecken = "ViewpointAbsetzbecken";
    public static string viewpointKalkmilch = "ViewpointKalkmilch";
    public static string viewpointSulfat = "ViewpointSulfat";
    public static string viewpointOsmose = "ViewpointOsmose";
    public static string viewpointCleanWater = "ViewpointCleanWater";



    SoGameColors gameColors; //SoGAmeColors
    SoGameIcons gameIcons; //SoGameIcons
    SoChapOneRuntimeData runtimeData;

    Image myViewpointImg;
    Button btnOpenInPlaceOverlay;
    Disc myDisc;
    bool discSetToDone = false;

    private void Awake()
    {
        gameColors = Resources.Load<SoGameColors>(GameData.NameGameColors);
        gameIcons = Resources.Load<SoGameIcons>(GameData.NameGameIcons);
        runtimeData = Resources.Load<SoChapOneRuntimeData>(GameData.NameRuntimeDataChap01);

    }

    // Start is called before the first frame update
    void Start()
    {
        btnOpenInPlaceOverlay = null;

        AssignLocalElements();

        myViewpointImg.sprite = gameIcons.viewpointArrow;
        myViewpointImg.color = gameColors.defaultInteractionColor;
        myDisc.Color = new Color32(100,100,100,100);
    }

    void AssignLocalElements()
    {
        Transform[] elements = GetComponentsInChildren<Transform>(true);
        
        for(int i = 0; i < elements.Length; i++)
        {
            if(elements[i].GetComponent<Image>() != null)
            {
                if (elements[i].GetComponent<OverlayInPlace>())
                {
                    btnOpenInPlaceOverlay = elements[i].GetComponent<Button>();
                }
                else
                {
                    myViewpointImg = elements[i].GetComponent<Image>();
                }
                
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

    void Update()
    {
        if (btnOpenInPlaceOverlay != null)
        {
            if (btnOpenInPlaceOverlay.gameObject.activeSelf == myViewpointImg.gameObject.activeSelf)
            {
                if (btnOpenInPlaceOverlay.name == "BtnOverlayInfo") return;

                btnOpenInPlaceOverlay.GetComponent<OverlayInPlace>().ActivateOverlay(!myViewpointImg.gameObject.activeSelf);
            }
        }
        
        if (!discSetToDone)
        {
            if(runtimeData.interaction116Done && vpLocation == ViewpointLocation.Mine) SetInteractionDone();
            else if (runtimeData.sole1Done && gameObject.name == viewpointSole1) SetInteractionDone();
            else if (runtimeData.sole2Done && gameObject.name == viewpointSole2) SetInteractionDone();
            else if (runtimeData.sole3BewetterungDone && gameObject.name == viewpointSole3Bewetterung) SetInteractionDone();
            else if (runtimeData.sole3GebaeudeDone && gameObject.name == viewpointSole3OVMine) SetInteractionDone();
            else if (runtimeData.isLongwallCutterDone && gameObject.name == viewpointSole3Longwallcutter) SetInteractionDone();

            if (runtimeData.interaction117Done && vpLocation == ViewpointLocation.Museum) SetInteractionDone();
            else if (runtimeData.isMinerDone && gameObject.name == viewpointMuseumMiner) SetInteractionDone();
            else if (runtimeData.isCoalifiationDone && gameObject.name == viewpointMuseumInkohlung) SetInteractionDone();
            else if (runtimeData.isCarbonificationPeriodDone && gameObject.name == viewpointMuseumWorld) SetInteractionDone();
            else if (runtimeData.isMythDone && gameObject.name == viewpointMuseumMyth) SetInteractionDone();
        }
    }
}
