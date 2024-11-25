using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TaskControl : MonoBehaviour
{
    private int total_garbages;
    private int collected_garbages;
    private int total_upgrades;
    private int collected_upgrades;
    public bool tasks_completed;
    private TMP_Text ui;

    public void AddCompleted(string tag)
    {
        if(tag == "Garbage") {collected_garbages ++;}
        else if(tag == "Upgrade") {collected_upgrades ++;}
        if(total_garbages == collected_garbages & total_upgrades == collected_upgrades) {tasks_completed = true;}
        if(total_upgrades != 0){
        ui.text = $"Garbage: {collected_garbages}/{total_garbages}\nUpgrade: {collected_upgrades}/{total_upgrades}";
        } else{
             ui.text = $"Garbage: {collected_garbages}/{total_garbages}";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tasks_completed = false;
        collected_garbages = 0;
        collected_upgrades = 0;
        total_garbages = GameObject.FindGameObjectsWithTag("Garbage").Length;
        total_upgrades = GameObject.FindGameObjectsWithTag("Upgrade").Length;
        ui = gameObject.GetComponentInChildren<TMP_Text>();
        if(SceneManager.GetActiveScene().buildIndex == 11){
            ui.text = "Talk to Cockroach";
        }
        else if(total_upgrades != 0){
        ui.text = $"Garbage: {collected_garbages}/{total_garbages}\nUpgrade: {collected_upgrades}/{total_upgrades}";
        } else{
             ui.text = $"Garbage: {collected_garbages}/{total_garbages}";
        }
    }
    private void UPdateUI()
    {
        //text.text = $"{currentCompleted}/{taskTargetCount}";
    }

}
