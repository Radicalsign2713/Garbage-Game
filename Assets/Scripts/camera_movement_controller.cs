using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_movement_controller : MonoBehaviour {
    public float minX = -25;
    public float maxX = 25;
    public float minY = -25;
    public float maxY = 25;
    [SerializeField] GameObject player;
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        // for some reason camera needs to be offset in the z direction.
        transform.position = player.transform.position + new Vector3(0, 0, -10); 

        // check if the camera is to far to the left 
        if (transform.position.x < minX) { 
            transform.position += new Vector3(minX - transform.position.x, 0, 0);
        }
        // check if the camera is to far to the right
        if (transform.position.x > maxX) { 
            transform.position += new Vector3(maxX - transform.position.x, 0, 0);
        }
        // check if the camera is to far to the bottom
        if (transform.position.y < minY) { 
            transform.position += new Vector3(0, minY - transform.position.y, 0);
        }
        // check if the camera is to far to the top
        if (transform.position.y > maxY) { 
            transform.position += new Vector3(0, maxY - transform.position.y, 0);
        }
    }
}
