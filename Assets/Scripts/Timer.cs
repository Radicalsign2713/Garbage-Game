using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float time = 120; // seconds
    private float totalTime = 0;
    private bool isWarning = false;//if time less than 30 seconds , show warning by change UI color
    public TMP_Text timeText;
    //private Image timerBar;


    void Start()
    {
        //timerBar = transform.Find("TimerBar").GetComponent<Image>();
        //timerBar.fillAmount = 1.0f;

        totalTime = time;
    }

    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0) 
        {
            time = 0;
            UpdateTimeText();
            Time.timeScale = 0;
        }
        UpdateTimeText();
    }

    void UpdateTimeText() 
    {
        // float minutes = (float)Math.Floor(time / 60);
        // float seconds = time - minutes * 60;
        int seconds = (int)Math.Ceiling(time);
        int minutes = seconds / 60;
        seconds = seconds % 60;

        string str;
        if (seconds <= 9) {str = $"{minutes}:0{seconds}";}
        else {str = $"{minutes}:{seconds}";}

        timeText.text = str; // 3:30

        // timerBar.fillAmount = time / totalTime;

        if(!isWarning && time <= 5)
        {
            isWarning = true;
            //timerBar.color = Color.red;
        }
    }
}