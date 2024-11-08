using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicControler : MonoBehaviour {
    [SerializeField] AudioClip firstClip;
    [SerializeField] AudioClip repeatClip;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = firstClip;
        audioSource.loop = false;
        audioSource.Play();

    }

    // Update is called once per frame
    void Update() {
        if (!audioSource.isPlaying) {
            audioSource.clip = repeatClip;
            audioSource.Play();
        }
    }
}
