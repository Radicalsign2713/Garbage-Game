using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class steve_E_audio_controller : MonoBehaviour
{
    private PlayerMovementController movement;
    private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        movement = gameObject.GetComponent<PlayerMovementController>();
        audio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.mute = !movement.is_moving;
    }
}
