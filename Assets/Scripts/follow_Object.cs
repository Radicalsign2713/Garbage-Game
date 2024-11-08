using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_Object : MonoBehaviour {
    [SerializeField] GameObject follow_me;
    [SerializeField] float displacement_x;
    [SerializeField] float displacement_y;
    [SerializeField] float displacement_z;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        transform.position = follow_me.transform.position + new Vector3(displacement_x, displacement_y, displacement_z);
    }
}
