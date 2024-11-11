using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryBar : MonoBehaviour
{
    private PlayerMovementController player;
    private float starting_percent;
    private Vector3 initial_scale;
    // Start is called before the first frame update
    void Start()
    {
        initial_scale = transform.localScale;
        player = GameObject.Find("Steve-E").GetComponent<PlayerMovementController>();
        starting_percent = player.battery;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(initial_scale.x * player.battery / starting_percent, initial_scale.y, initial_scale.z);
    }
}
