using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class TextFade : MonoBehaviour {
 
    // the image you want to fade, assign in inspector
    public Text text;

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
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= duration; i += Time.deltaTime)
            {
                // set color with i as alpha
                text.color = new Color(text.color.r, text.color.g, text.color.b, i);
                yield return null;
            }
            
            text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        }
    }
}