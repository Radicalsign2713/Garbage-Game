using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagehazardv2 : MonoBehaviour
{
     private PlayerMovementController player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.Find("Steve-E").GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(gameObject.tag == "Ice"){
                player.on_ice = true;
            } else if(gameObject.tag == "Lava"){
                player.on_lava = true;
            } else if(gameObject.tag == "Liquid"){
                player.on_liquid = true;
            } else if(gameObject.tag == "Mud"){
                player.on_mud = true;
            }
            else if(gameObject.tag == "Island"){
                player.on_island = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if(gameObject.tag == "Ice"){
                player.on_ice = false;
            } else if(gameObject.tag == "Lava"){
                player.on_lava = false;
            } else if(gameObject.tag == "Liquid"){
                player.on_liquid = false;
            } else if(gameObject.tag == "Mud"){
                player.on_mud = false;
            }
            else if(gameObject.tag == "Island"){
                player.on_island = false;
            }
        }
    }
}
