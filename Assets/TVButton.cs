using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVButton : MonoBehaviour
{
    public void onTVButtonClicked()
    {
        DayManager.Instance.onTVNextClicked();
    }
}
