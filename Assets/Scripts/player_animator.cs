using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator : MonoBehaviour {
    private SpriteRenderer spriteRenderer;
    private string direction;
    private int numberOfFrames; // length of sprite arrays
    private float accumulator; // total time.delta time, used to ensure 15 fps
    private int frame;
    [SerializeField] int fps;
    [SerializeField] PlayerMovementController movement_Script;
    [SerializeField] Sprite[] left_movement; // Loop to play when traveling left
    [SerializeField] Sprite[] right_movement; // Loop to play when traveling right ect.
    [SerializeField] Sprite[] up_movement; // Frame should advance every 1/15 of a second. Once it reaches the last index of the 
    [SerializeField] Sprite[] down_movement; // List, it will restart at 0.
    [SerializeField] Sprite[] left_idle; // Loop to play when standing still and facing left
    [SerializeField] Sprite[] right_idle; // Loop to play when standing still and facing right ect.
    [SerializeField] Sprite[] up_idle; // Will still switch in the same manner as the movement lists
    [SerializeField] Sprite[] down_idle;
    // PRECONDITION: the length of all of the sprite arrays must be equal!
    // when switching between directions the animation will start in the middle at the n+1th frame
    // Start is called before the first frame update
    void Start() {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        numberOfFrames = left_movement.Length;
        accumulator = 0;
    }

    // Update is called once per frame
    void Update() {
        accumulator += Time.deltaTime * fps;
        if (accumulator >= numberOfFrames) {accumulator -= numberOfFrames;}
        frame = (int)accumulator;

        spriteRenderer.color = Color.white;
        if (movement_Script.on_lava){
            float period = (3*fps/(1 + movement_Script.lava_intensity)) + 0.2f*fps;
            if (accumulator % period <= period/2){
                spriteRenderer.color = new Color(1, 0.5f, 0.5f);
            } else {spriteRenderer.color = Color.white;}
        }

        direction = movement_Script.direction_facing;

        if (movement_Script.is_moving) {
            if (direction == "down") {
                spriteRenderer.sprite = down_movement[frame];
            }
            else if (direction == "left") {
                spriteRenderer.sprite = left_movement[frame];
            }
            else if (direction == "right") {
               spriteRenderer.sprite = right_movement[frame];
            }
            else if (direction == "up") {
                spriteRenderer.sprite = up_movement[frame];
            }
        }
        // Steve_E is not moving
        else {  
            if (direction == "down") {
                spriteRenderer.sprite = down_idle[frame];
            }
            else if (direction == "left") {
                spriteRenderer.sprite = left_idle[frame];
            }
            else if (direction == "right") {
               spriteRenderer.sprite = right_idle[frame];
            }
            else if (direction == "up") {
                spriteRenderer.sprite = up_idle[frame];
            }
        }
    }
}
