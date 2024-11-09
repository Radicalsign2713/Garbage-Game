using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskControl : MonoBehaviour
{
    [SerializeField]
    [Header("target missions count")]
    private int taskTargetCount = 5;

    private int currentCompleted = 0;
    private GameObject MissionCompletedPanel;

    /// <summary>
    /// add the completed count of tasks
    /// </summary>
    public void AddCompleted()
    {
        currentCompleted ++;
        UPdateUI();

        if(currentCompleted == taskTargetCount)
        {
            MissionCompletedPanel.SetActive(true);
            Invoke(nameof(HidePanel), 2);
            //Time.timeScale = 0;
            PlayerControl.instance.WaitForCharge();
        }
    }

    private void HidePanel()
    {
        MissionCompletedPanel.SetActive(false);
    }

    private TMP_Text text;

    // Start is called before the first frame update
    void Start()
    {
        MissionCompletedPanel = GameObject.Find("MissionCompletedPanel");
        MissionCompletedPanel.SetActive(false);

        text = transform.GetComponent<TMP_Text>();
        UPdateUI();
    }
    private void UPdateUI()
    {
        text.text = $"{currentCompleted}/{taskTargetCount}";
    }

}
