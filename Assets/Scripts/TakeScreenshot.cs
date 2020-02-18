using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventTexture2D : UnityEvent<Texture2D> {}

public class TakeScreenshot : MonoBehaviour
{
    [SerializeField] private GameObject canvas = null;

    private Texture2D _screenshot = null;

    public UnityEventTexture2D onScreenshotTaken = null;
    
    public void TakePicture()
    {
        StartCoroutine(TakePictureCoroutine());
    }
    
    private IEnumerator TakePictureCoroutine()
    {
        yield return new WaitForEndOfFrame();

        canvas.SetActive(false);
        
        DestroyScreenshotTexture();
        
        //Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        Texture2D ss = ScreenCapture.CaptureScreenshotAsTexture();
        //ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        //ss.Apply();

        canvas.SetActive(true);
        onScreenshotTaken?.Invoke(ss);
    }

    private void DestroyScreenshotTexture()
    {
        if (_screenshot != null)
        {
            Destroy(_screenshot);
            _screenshot = null;
        }
    }
    
    private void OnDestroy()
    {
        DestroyScreenshotTexture();
    }
}
