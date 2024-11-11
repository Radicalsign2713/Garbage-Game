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


    [SerializeField]
    private float noticeTime = 30;
    [SerializeField]
    private float warningTime = 10;
    [SerializeField]
    private float finalTime = 2;

    private bool isNotice = false;//if time less than 30 seconds , show notice by change UI color
    private bool isWarning = false;//if time less than 10 seconds , show warning by change UI color
    public TMP_Text timeText;

    private Image timerBar;
    private GameObject MissionCompletedPanel;

    private void Awake()
    {
        MissionCompletedPanel = GameObject.Find("MissionCompletedPanel");
    }

    void Start()
    {
        timerBar = transform.Find("TimerBar").GetComponent<Image>();
        timerBar.fillAmount = 1.0f;

        totalTime = time;
    }

    void Update()
    {
        // if (PlayerControl.instance.State != GameSate.playMode) return;

        time -= Time.deltaTime;
        if (time < -finalTime) 
        {
            time = 0; 
            // Todo : finish the game

            //Time.timeScale = 0f;

            MissionCompletedPanel.SetActive(true);
            Invoke(nameof(HidePanel), 2);

            TMP_Text txt = MissionCompletedPanel.GetComponentInChildren<TMP_Text>();
            txt.text = "Mission Fail";

            // PlayerControl.instance.WaitForCharge();
        }
        UpdateTimeText();
    }
    private void HidePanel()
    {
        MissionCompletedPanel.SetActive(false);
    }

    void UpdateTimeText() 
    {
        float _time = Math.Clamp(time, 0, totalTime);

        float minutes = (float)Math.Floor(_time / 60);
        float seconds = _time - minutes * 60;
        string str = $"{minutes}:{(int)Math.Floor(seconds)}";
        timeText.text = str; // 3:30

        timerBar.fillAmount = _time / totalTime;

        if(!isWarning && time <= warningTime)
        {
            isWarning = true;
            timerBar.color = Color.red;
        }
        else if(!isNotice && time <= noticeTime)
        {
            isNotice = true;
            timerBar.color = Color.yellow;
        }
    }

    public void ResetTimer()
    {
        timerBar = transform.Find("TimerBar").GetComponent<Image>();
        timerBar.fillAmount = 1.0f;

        time = totalTime;
    }
}
