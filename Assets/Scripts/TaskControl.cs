using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskControl : MonoBehaviour
{
    private int total_garbages;
    private int collected_garbages;
    private int total_upgrades;
    private int collected_upgrades;
    public bool tasks_completed;
    

    public void AddCompleted(string tag)
    {
        if(tag == "Garbage") {collected_garbages ++;}
        else if(tag == "Upgrade") {collected_upgrades ++;}
    }

    // Start is called before the first frame update
    void Start()
    {
        tasks_completed = false;
    }
    private void UPdateUI()
    {
        //text.text = $"{currentCompleted}/{taskTargetCount}";
    }

}
