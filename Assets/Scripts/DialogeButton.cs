using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogeButton : MonoBehaviour
{
    [SerializeField] private Text playerResponseText;
    private string responseText;

    private string getResponseText()
    {
        if (responseText == null)
        {
            return "";
        }
        else
        {
            return responseText;
        }
    }

    public void setResponseText(string responseText)
    {
        this.responseText = responseText;
    }

    public void onHoverEvent(bool isHovered)
    {
        if (isHovered)
        {
            playerResponseText.text = getResponseText();
        }
        else
        {
            playerResponseText.text = "";
        }
    }
}
