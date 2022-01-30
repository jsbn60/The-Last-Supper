using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Util;

public class DayButton : MonoBehaviour
{
    [SerializeField] private int day;

    public void loadDay()
    {
        DayManager.Instance.runNextDay(day);
        DayManager.Instance.toggleAllDayButtons(false);
    }
}
