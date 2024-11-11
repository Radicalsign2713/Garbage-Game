using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBar : MonoBehaviour
{
    private PlayerMovementController player;
    private float starting_percent;
    private Vector3 initial_scale;
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        initial_scale = transform.localScale;
        player = GameObject.Find("Steve-E").GetComponent<PlayerMovementController>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        starting_percent = player.battery;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(initial_scale.x * player.battery / starting_percent, initial_scale.y, initial_scale.z);
        if(player.battery / starting_percent <= .1) {spriteRenderer.color = Color.red;}
        else if(player.battery / starting_percent <= .3) {spriteRenderer.color = Color.yellow;}
        else {spriteRenderer.color = Color.white;}
        
    }
}
