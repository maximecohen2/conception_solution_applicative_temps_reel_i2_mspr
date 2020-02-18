using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{

    [SerializeField] private GameObject _canvas;
    
    public void TakePicture()
    {
        StartCoroutine(TakeSSAndShare());
    }
    
    
    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        //Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        Texture2D ss = ScreenCapture.CaptureScreenshotAsTexture();
        //ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        //ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );
	
        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath ).SetSubject( "Subject goes here" ).SetText( "Hello world!" ).Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
    }
}
