using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class ImageFade : MonoBehaviour {
 
    // the image you want to fade, assign in inspector
    public Image img;

    public void runFade(int duration, bool fadeAway)
    {
        StartCoroutine(FadeImage(duration, fadeAway));
    }
 
    IEnumerator FadeImage(int duration, bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = duration; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
            
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= duration; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(img.color.r, img.color.g, img.color.b, i);
                yield return null;
            }
            
            img.color = new Color(img.color.r, img.color.g, img.color.b, 1);
        }
    }
}