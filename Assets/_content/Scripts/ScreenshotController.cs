using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(Camera))]
public class ScreenshotController : MonoBehaviour
{
    protected enum ResolutionSize { _320X480, _480X800, _480X854, _720X1280, _800X1280 }
    protected enum ScreenshotMethod { UnityScreenCapture, CustomCameraRenderTexture }

    protected Camera screenshotCamera;
    protected bool takeScreenshot;

    [SerializeField]
    protected ScreenshotMethod screenshotMethod;

    [SerializeField]
    protected ResolutionSize resolutionSizeForCustom;

    [SerializeField]
    protected KeyCode screenshotKey;

    private void Awake()
    {
        screenshotCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(screenshotKey))
        {
            if (screenshotMethod == ScreenshotMethod.CustomCameraRenderTexture)
                TakeScreenshotCustomCameraRenderTexture();
            else
                TakeScreenshotUnityScreenCapture();
        }

    }

    private string GetScreenshotPath()
    {
            System.Text.StringBuilder path = new System.Text.StringBuilder(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop) + 
                "/" + UnityEngine.Application.productName + "Screenshots/");
            if (screenshotMethod == ScreenshotMethod.CustomCameraRenderTexture)
            {
                int width = default;
                int height = default;
                (width, height) = FromResolutionSizeToIntTuple(resolutionSizeForCustom);
                path.Append(width + "X" + height);
            }
            else
                path.Append(Screen.width + "X" + Screen.height);
            //
            if (!Directory.Exists(path.ToString()))
                Directory.CreateDirectory(path.ToString());
            return path.ToString();   
    }

    #region CustomCameraRenderTexture

    private void OnPostRender()
    {
        if(takeScreenshot)
        {
            takeScreenshot = false;
            RenderTexture renderTexture = screenshotCamera.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            byte[] byteArray = renderResult.EncodeToPNG();
            /////////////////
            //



            //string path = "E:/" + renderTexture.height + "X" + renderTexture.width;
            //System.IO.File.WriteAllBytes(UnityEngine.Application.dataPath + "/" + renderTexture.height + "X" + renderTexture.width + "/CameraScreenshot" + Time.time + ".png", byteArray);
            System.IO.File.WriteAllBytes(GetScreenshotPath() + "/CameraScreenshot" + Time.time + ".png", byteArray);

            RenderTexture.ReleaseTemporary(renderTexture);
            screenshotCamera.targetTexture = null;
        }
    }

    private void TakeScreenshotCustomCameraRenderTexture()
    {
        int width = default;
        int height = default;
        (width, height) = FromResolutionSizeToIntTuple(resolutionSizeForCustom);
        screenshotCamera.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshot = true;
    }

    private (int, int) FromResolutionSizeToIntTuple(ResolutionSize resolutionSize)
    {
        int width = default;
        int height = default;

        switch (resolutionSize)
        {
            case ResolutionSize._320X480:
                width = 320;
                height = 480;
                break;
            case ResolutionSize._480X800:
                width = 480;
                height = 800;
                break;
            case ResolutionSize._480X854:
                width = 480;
                height = 854;
                break;
            case ResolutionSize._720X1280:
                width = 720;
                height = 1280;
                break;
            case ResolutionSize._800X1280:
                width = 800;
                height = 1280;
                break;

        }
        return (width, height);
    }

    #endregion

    #region UnityScreenCapture

    private void TakeScreenshotUnityScreenCapture()
    {
        ScreenCapture.CaptureScreenshot(GetScreenshotPath() + "/CameraScreenshot" + Time.time + ".png");

    }
   

    #endregion
}
