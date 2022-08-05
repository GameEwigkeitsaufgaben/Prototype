using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class Guckloch : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void ImageDownloader(string str, string fn);

    public static byte[] ssData = null;
    public static string imageFilename = "TestImage";

    public RawImage image;
    public WebCamTexture tex;
    public Texture2D testTex;
    public RectTransform rect;
    public Vector2 pos;
    public Canvas canvas;
    public float scaleFactor;
    public void DownloadScreenshot()
    {
        StartCoroutine(RecordFrame());
    }

    public void DownloadSpecificFrame()
    {
        StartCoroutine(RecordSpecificFrame());
    }

    public byte[] Test()
    {
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);

        // Read screen contents into the texture
        tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        tex.Apply();

        // Encode texture into PNG
        byte[] bytes = tex.EncodeToPNG();

        return bytes;
    }

    void Start()
    {


        WebCamDevice[] devices = WebCamTexture.devices;

        // for debugging purposes, prints available devices to the console
        for (int i = 0; i < devices.Length; i++)
        {
            print("Webcam available: " + devices[i].name);
        }

        //// assuming the first available WebCam is desired
        tex = new WebCamTexture(devices[0].name);
        image.texture = tex;
        tex.Play();
    }

    IEnumerator RecordFrame()
    {
        yield return new WaitForEndOfFrame();
        var texture = ScreenCapture.CaptureScreenshotAsTexture(4);
        // do something with texture

        ssData = texture.EncodeToPNG();

        if (ssData != null)
        {
            Debug.Log("Downloading..." + imageFilename);
            ImageDownloader(System.Convert.ToBase64String(ssData), imageFilename);
        }

        // cleanup
        Object.Destroy(texture);

    }

    IEnumerator RecordSpecificFrame()
    {
        yield return new WaitForEndOfFrame();
        // do something with texture
        scaleFactor = canvas.scaleFactor;
        int width = Screen.width;
        int height = Screen.height;
        Texture2D tex = new Texture2D((int)(rect.rect.width * scaleFactor), (int)(rect.rect.height * scaleFactor), TextureFormat.RGB24, false);

        // Read screen contents into the texture
        int posX = (width/2) + (int)(rect.anchoredPosition.x * scaleFactor);
        int posY = (height/2) + (int)(rect.anchoredPosition.y * scaleFactor);


        tex.ReadPixels(new Rect(posX, posY, rect.sizeDelta.x * scaleFactor, rect.sizeDelta.y * scaleFactor), 0, 0);
        tex.Apply();
        testTex = tex;


        ssData = tex.EncodeToPNG();

        if (ssData != null)
        {
            Debug.Log("Downloading..." + imageFilename);
            ImageDownloader(System.Convert.ToBase64String(ssData), imageFilename);
        }

        // cleanup
        Object.Destroy(tex);

    }
}
