using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class Guckloch : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ImageDownloader(string str, string fn);

    public static byte[] ssData = null;
    public string imageFilename = "TestImage";

    [Header("Setup")]
    public RawImage webcamDisplay;
    private WebCamTexture tex;
    public Texture2D debugtexture;
    public RectTransform frame;
    public Canvas canvas;
    private float scaleFactor;
    public int cameraActive = 0;
    private WebCamDevice[] devices;

    [Header("Error Handling")]
    public GameObject errorNoCamera;
    public GameObject errorMultipleCameras;



    public void DownloadSpecificFrame()
    {
        StartCoroutine(RecordSpecificFrame());
    }

    void Start()
    {
        devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            errorNoCamera.SetActive(true);
            return;
        }
        else if (devices.Length > 1)
        {
            errorMultipleCameras.SetActive(true);
        }

        SetWebCam(0);
    }

    IEnumerator RecordSpecificFrame()
    {
        yield return new WaitForEndOfFrame();
        // do something with texture
        scaleFactor = canvas.scaleFactor;
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D((int)(frame.rect.width * scaleFactor), (int)(frame.rect.height * scaleFactor), TextureFormat.RGB24, false);

        // Read screen contents into the texture
        int posX = (width/2) + (int)(frame.anchoredPosition.x * scaleFactor);
        int posY = (height/2) + (int)(frame.anchoredPosition.y * scaleFactor);


        tex.ReadPixels(new Rect(posX, posY, frame.sizeDelta.x * scaleFactor, frame.sizeDelta.y * scaleFactor), 0, 0);
        tex.Apply();
        debugtexture = tex;


        ssData = tex.EncodeToPNG();

        if (ssData != null)
        {
            Debug.Log("Downloading..." + imageFilename);
            ImageDownloader(System.Convert.ToBase64String(ssData), imageFilename);
        }

        // cleanup
        Object.Destroy(tex);

    }

    public void SetWebCam(int index)
    {
        print("SetWEbCam" + index);

        tex = new WebCamTexture(devices[index].name);
        webcamDisplay.texture = tex;
        tex.Play();
    }
}
