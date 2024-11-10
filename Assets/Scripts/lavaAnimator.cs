using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaAnimator : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color(1, 1, 1, 0.7f + .8f*Mathf.Sin(2*Time.time)/2);
    }
}
