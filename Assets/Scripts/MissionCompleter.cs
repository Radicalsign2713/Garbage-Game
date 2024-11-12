using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MissionCompleter : MonoBehaviour
{
    private TaskControl control;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("MissionControl").GetComponent<TaskControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") & control.tasks_completed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
