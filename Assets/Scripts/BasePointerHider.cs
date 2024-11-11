using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePointerHider : MonoBehaviour
{
    private TaskControl control;
    private SpriteRenderer renderer;
    private bool set_active = false;
    // Start is called before the first frame update
    void Start()
    {
        control = GameObject.Find("MissionControl").GetComponent<TaskControl>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.color -= new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(control.tasks_completed & !set_active){
            renderer.color += new Color(0, 0, 0, 1);
            set_active = true;
        }
    }
}
