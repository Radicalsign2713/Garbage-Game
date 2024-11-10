using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] Sprite[] frames;
    [SerializeField] int fps;
    private int numberOfFrames;
    private float accumulator;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        numberOfFrames = frames.Length;
        accumulator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        accumulator += Time.deltaTime * fps;
        if (accumulator >= numberOfFrames) {accumulator -= numberOfFrames;}
        int frame = (int)accumulator;
        spriteRenderer.sprite = frames[frame];
    }
}
