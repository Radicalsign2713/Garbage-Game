using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSpriteOnContact : MonoBehaviour
{
    [SerializeField] Sprite sprite1;
    [SerializeField] Sprite sprite2;
    private SpriteRenderer renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            renderer.sprite = sprite2;
        }
    }
}
