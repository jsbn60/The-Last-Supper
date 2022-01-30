using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;
using Util;

public class DayManager : MonoBehaviour
{
    private static DayManager _instance;

    [SerializeField] private ImageFade fadingBackground;

    [SerializeField] private float waitTime;

    [SerializeField] private TVUIManager TVUI;

    [SerializeField] private DayButton[] dayButtons;

    [SerializeField] private Canvas menuCanvas;
    public void toggleAllDayButtons(bool turnOn)
    {
        menuCanvas.sortingOrder = 0;
        foreach (DayButton dayButton in dayButtons)
        {
            dayButton.GetComponent<Button>().interactable = turnOn;
            dayButton.gameObject.SetActive(turnOn);
        }
    }
    private float currentTime;
    
    private int currentDay = 0;

    private string[] dayNews =
    {
        "YUP, THERE ARE NEWS FOR DAY 1!",
        "YUP, THERE ARE NEWS FOR DAY 2!",
        "YUP, THERE ARE NEWS FOR DAY 3!",
        "YUP, THERE ARE NEWS FOR DAY 4!",
        "YUP, THERE ARE NEWS FOR DAY 5!",
    };
    
    public static DayManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DayManager>();
            }

            return _instance;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void runNextDay(int day)
    {
        dayToload = day;
        SoundManager.Instance.runBackgroundForDay(-1);
        TVUI.toggleTVOverlay(false);
        TVUI.GetComponentInChildren<UITextTypeWriter>().showText(dayNews[day-1]);
        currentTime = waitTime;
    }

    private int dayToload;
    public void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;

            if (currentTime <= 0)
            {
                //loadNextDay(dayToload);
            }
        } 
    }

    public void onTVNextClicked()
    {
        Debug.Log("IMPLIED");
        loadNextDay(dayToload);
    }

    private void loadNextDay(int day)
    {
        TVUI.toggleTVOverlay(true);
        fadingBackground.runFade(2,true);
        UnityEngine.SceneManagement.SceneManager.LoadScene(day);
    }
}
