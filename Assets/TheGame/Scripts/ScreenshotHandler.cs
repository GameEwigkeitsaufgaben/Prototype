using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private RectTransform captureRect;
    private float canvasScaleFactor;

    private Camera myCam;
    private bool takeScreenshotOnNextFrame;

    private void Awake()
    {
        instance = this;
        myCam = gameObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        RenderPipelineManager.endCameraRendering += RenderPipelineMgrEndCamRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= RenderPipelineMgrEndCamRendering;
    }

    private void RenderPipelineMgrEndCamRendering(ScriptableRenderContext arg1, Camera arg2)
    {
        if (takeScreenshotOnNextFrame)
        {
            //https://www.youtube.com/watch?v=lT-SRLKUe5k
            //https://www.youtube.com/watch?v=d5nENoQN4Tw&t=483s

            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = myCam.targetTexture;
            
            Texture2D renderResult = new Texture2D((int)(captureRect.rect.width * canvasScaleFactor), (int)(captureRect.rect.height * canvasScaleFactor), TextureFormat.RGB24, false);

            //// Read screen contents into the texture
            int posX = (renderTexture.width / 2) + (int)(captureRect.anchoredPosition.x * canvasScaleFactor);
            int posY = (renderTexture.height / 2) + (int)(Mathf.Abs(captureRect.anchoredPosition.y * canvasScaleFactor)) - (int)(captureRect.rect.height * canvasScaleFactor);

            Rect rect = new Rect(
                                posX, 
                                posY, 
                                captureRect.sizeDelta.x * canvasScaleFactor, 
                                captureRect.sizeDelta.y * canvasScaleFactor);
            
            renderResult.ReadPixels(rect, 0, 0);
            renderResult.Apply();

            byte[] byteArray = renderResult.EncodeToPNG();
            System.IO.File.WriteAllBytes(Application.persistentDataPath + "/fotoplatz.png", byteArray);
            Debug.Log("Saved CameraScreenshot.png into: " + Application.dataPath);

            RenderTexture.ReleaseTemporary(renderTexture);
            myCam.targetTexture = null;
        }
    }
    public static void TakeScreenshotStatic(int width, int height, RectTransform rect, float scaleFactor)
    {
        instance.TakeScreenshot(width, height, rect, scaleFactor);
    }

    private void TakeScreenshot(int width, int height, RectTransform rect, float scaleFactor)
    {
        myCam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
        captureRect = rect;
        this.canvasScaleFactor = scaleFactor;
    }

 }
