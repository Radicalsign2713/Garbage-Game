using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPointingArrow : MonoBehaviour
{
    private GameObject player;
    public Vector3 delta_world;
    public Vector3 delta_screen;
    public Camera cam;
    [SerializeField] float hight;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Steve-E");
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // delta_world = transform.parent.position - player.transform.position;
        delta_screen = cam.WorldToViewportPoint(transform.parent.position + (Vector3.up * hight/3.5f)) - cam.WorldToViewportPoint(player.transform.position);
        if (Math.Abs(delta_screen.x) > 0.5 | Math.Abs(delta_screen.y) > 0.5){
            // Edge of screen
            if(Math.Abs(delta_screen.x) > Math.Abs(delta_screen.y)){
                delta_screen = delta_screen / (2 * Math.Abs(delta_screen.x));
            } else{
                delta_screen = delta_screen / (2 * Math.Abs(delta_screen.y));
            }
            delta_world = cam.ViewportToWorldPoint(delta_screen + cam.WorldToViewportPoint(player.transform.position)) - player.transform.position;
            delta_world -= delta_world.normalized * .75f;
            transform.eulerAngles = new Vector3(0f, 0f, Vector3.SignedAngle(Vector3.up, delta_world, Vector3.forward));
            // global position
            transform.position =  delta_world + player.transform.position;
        }
        // if ((transform.parent.position - player.transform.position).magnitude < 5){
        else {
            transform.position = transform.parent.position + (Vector3.up * hight);
            transform.eulerAngles = new Vector3(0f, 0f, 180f); 
        } 
        // else {
        //     delta = (transform.parent.position - player.transform.position).normalized * 2.5f;
        //     transform.eulerAngles = new Vector3(0f, 0f, Vector3.SignedAngle(Vector3.up, delta, Vector3.forward));
        //     // global position
        //     transform.position =  delta + player.transform.position;
        // }
    }
}
