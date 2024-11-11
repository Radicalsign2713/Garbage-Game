using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ResourceControl : MonoBehaviour
{
    private TaskControl taskControl;

    private void CollectResource()
    {
        // if (PlayerControl.instance.State != GameSate.playMode) return;
        Destroy(gameObject);

        taskControl.AddCompleted(gameObject.tag);
    }

    private void Awake()
    {
        taskControl = GameObject.Find("MissionControl").GetComponent<TaskControl>();
    }

    private void Update()
    {
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CollectResource();
        }
    }
}
