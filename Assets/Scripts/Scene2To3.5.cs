using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompleterForScene2 : MonoBehaviour
{
    private TaskControl control;

    // The name of the scene you want to load after completion
    public string nextSceneName = "Scene3.5";

    void Start()
    {
        control = GameObject.Find("MissionControl").GetComponent<TaskControl>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if player entered the trigger and if all tasks are completed
        if (collision.CompareTag("Player") && control.tasks_completed)
        {
            // Load the specific scene by name
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
