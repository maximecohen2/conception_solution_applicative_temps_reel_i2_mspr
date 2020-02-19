using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class Share : MonoBehaviour
{
    [SerializeField, Multiline, Tooltip("sets the shared text. Note that the Facebook app will omit text, if exists.")]
    private string text = "#cerealis #coloring #AR";
    
    private Texture2D _texture2D = null;

    public UnityEvent onShared = null;

    public void SetScreenshot(Texture2D texture)
    {
        _texture2D = texture;
    }

    public void CleanScreenshot()
    {
        _texture2D = null;
    }
    
    public void SharePicture()
    {
        if (_texture2D == null)
            return;
        
        string filePath = Path.Combine( Application.temporaryCachePath, "tmpImg.png" );
        File.WriteAllBytes( filePath, _texture2D.EncodeToPNG() );
        
        NativeShare nativeShare = new NativeShare();
        
        nativeShare.AddFile(filePath);
        //nativeShare.SetSubject("Subject goes here");
        nativeShare.SetText(text);
        
        nativeShare.Share();
        
        //new NativeShare().AddFile( filePath ).SetSubject( "Subject goes here" ).SetText( "Hello world!" ).Share();

        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
        
        onShared?.Invoke();
    }
    
}
