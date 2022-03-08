using UnityEngine;
using UnityEngine.UI;

public class MuseumOverlay : MonoBehaviour
{
    public Canvas panel;
    public Image imgInfo, imgWelt, imgMiner, imgInkohlung, imgMythos;


    public void ActivateOverlay(MuseumWaypoints wp)
    {
        imgInfo.transform.parent.gameObject.SetActive(true);

        if(wp == MuseumWaypoints.WPInfo)
        {
            imgInfo.gameObject.SetActive(true);
        }

    }

}
