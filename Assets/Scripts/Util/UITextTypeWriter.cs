using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class UITextTypeWriter : MonoBehaviour
{
    [SerializeField] private Text displayText;

    [SerializeField] private float interval;
    private string textToDisplay;
    
    public void showText(string textToDisplay)
    {
        displayText.text = "";
        this.textToDisplay = textToDisplay;

        // TODO: add optional delay when to start
        StartCoroutine ("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in textToDisplay) 
        {
            displayText.text += c;
            yield return new WaitForSeconds (interval);
        }
    }

}