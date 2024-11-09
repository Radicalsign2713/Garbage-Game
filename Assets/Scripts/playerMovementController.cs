using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float max_speed = 3; // The maximum speed the player can acheive in any one direction
    [SerializeField] float acelleration = 2; // The acelleration imparted by the directional movment keys
    [SerializeField] float player_friction = .7F; // The amount the player slows down when no buttons are pressed
    [SerializeField] string button_left1; // Key for left (should use unity scripting api strings)
    [SerializeField] string button_right1; // all th eother button_ are the same concept
    [SerializeField] string button_up1; 
    [SerializeField] string button_down1;
    [SerializeField] string button_left2;
    [SerializeField] string button_right2;
    [SerializeField] string button_up2;
    [SerializeField] string button_down2;
    private string last_pressed_horizontal = "right"; // Representation Invariant: must be either "right" or "left" 
    private string last_pressed_vertical = "up"; // Representation Invariant: must be either "up or "down"
    private string axis_priority; // Tracks weather the last pressed directional input was on the horizontal or vertical axis
                                  // Representation Invariant: must be either "vertical" or "horizontal"
    private float horizontal_velocity = 0; 
    private float vertical_velocity = 0;
    private Rigidbody2D rb;
    public string direction_facing; // The direction that Steve-E is facing. Always is whatever the last key pressed was, or the only key being held down. 
                                            // Representation Invariant: must be either "up", "down", "left", or "right"
                                            // Read only
    public bool is_moving; // Read only
        
    // Handles the roll over of directional inputs. 
    // Must be called every frame. 
    // Returns "left", "right", or "none"
    string get_horizontal_directional_input() {
        if (Input.GetKeyDown(button_left1) | Input.GetKeyDown(button_left2)) {
            last_pressed_horizontal = "left";
            axis_priority = "horizontal";
        }
        else if (Input.GetKeyDown(button_right1) | Input.GetKeyDown(button_right2)) {
            last_pressed_horizontal = "right";
            axis_priority = "horizontal";
        }
        if ((Input.GetKey(button_left1) | Input.GetKey(button_left2)) & (Input.GetKey(button_right1) | Input.GetKey(button_right2))) {
            if (last_pressed_horizontal == "left"){
                return "left";
            }
            else {
                return "right";
            }
        }
        else if (Input.GetKey(button_left1) | Input.GetKey(button_left2)) {
            return "left";
        }
        else if (Input.GetKey(button_right1) | Input.GetKey(button_right2)) {
            return "right";
        }
        else {
            return "none";
        }
    }

    // Behaves in the same way as get_horizontal_directional_input. 
    // Returns "up", "down", or "none". 
    // Must be called every frame.
    string get_vertical_directional_input() { 
        if (Input.GetKeyDown(button_up1) | Input.GetKeyDown(button_up2)) {
            last_pressed_vertical = "up";
            axis_priority = "vertical";
        }
        else if (Input.GetKeyDown(button_down1) | Input.GetKeyDown(button_down2)) {
            last_pressed_vertical = "down";
            axis_priority = "vertical";
        }

        if ((Input.GetKey(button_up1) | Input.GetKey(button_up2)) & (Input.GetKey(button_down1) | Input.GetKey(button_down2))) {
            if (last_pressed_vertical == "up") {
                return "up";
            }
            else {
                return "down";
            }
        }
        else if (Input.GetKey(button_up1) | Input.GetKey(button_up2)) {
            return "up";
        }
        else if (Input.GetKey(button_down1) | Input.GetKey(button_down2)) {
            return "down";
        }
        else {
            return "none";
        }
    }

    // Updates the horizontal velocity of Steve-E based on the given input. 
    //Precondition: dh must either be "right", "left", or "none"
    void update_horizontal_velocity(string dh) { 
        if (dh == "right") {
            if (horizontal_velocity < max_speed) {
                horizontal_velocity += acelleration * Time.deltaTime;
                if (horizontal_velocity > max_speed) {
                    horizontal_velocity = max_speed;
                }
            }
        }
        else if (dh == "left") {
            if (horizontal_velocity > -max_speed) {
                horizontal_velocity -= acelleration * Time.deltaTime;
                if (horizontal_velocity < -max_speed) {
                    horizontal_velocity = -max_speed;
                }
            }

        }
        else if (dh == "none") {
            if (horizontal_velocity > 0) {
                horizontal_velocity -= player_friction * Time.deltaTime;
                if (horizontal_velocity < 0) {
                    horizontal_velocity = 0;
                }
            }
            else if (horizontal_velocity < 0) {
                horizontal_velocity += player_friction * Time.deltaTime;
                if (horizontal_velocity > 0) {
                    horizontal_velocity = 0;
                }
            }
        }
    }

    // Updates the horizontal velocity of Steve-E based on the given input. 
    // Precondition: dv must either be "up", "down", or "none"
    void update_vertical_velocity(string dv) { 
        if (dv == "up") {
            if (vertical_velocity < max_speed) {
                vertical_velocity += acelleration * Time.deltaTime;
                if (vertical_velocity > max_speed) {
                    vertical_velocity = max_speed;
                }
            }
        }
        else if (dv == "down") {
            if (vertical_velocity > -max_speed) {
                vertical_velocity -= acelleration * Time.deltaTime;
                if (vertical_velocity < -max_speed) {
                    vertical_velocity = -max_speed;
                }
            }

        }
        else if (dv == "none") {
            if (vertical_velocity > 0) {
                vertical_velocity -= player_friction * Time.deltaTime;
                if (vertical_velocity < 0) {
                    vertical_velocity = 0;
                }
            }
            else if (vertical_velocity < 0) {
                vertical_velocity += player_friction * Time.deltaTime;
                if (vertical_velocity > 0) {
                    vertical_velocity = 0;
                }
            }
        }
    }

    //Updates the direction Steve-E is facing to the direction he is moving, or last moved. 
    // Preconditions: dh must be either "left", "right", or "none"
    //                dv must be either "up", "down" or "none"
    void update_direction_facing(string dh, string dv) { 
        if (!(horizontal_velocity == 0.0 && vertical_velocity == 0.0)) {
            is_moving = true;
            string chosen_axis;
            if (Mathf.Abs(horizontal_velocity) > Mathf.Abs(vertical_velocity)) {chosen_axis = "horizontal";}
            else if (Mathf.Abs(vertical_velocity) > Mathf.Abs(horizontal_velocity)) {chosen_axis = "vertical";}
            else {chosen_axis = axis_priority;}

            if (chosen_axis == "vertical") {
                if (vertical_velocity > 0.0) {direction_facing = "up";}
                else {direction_facing = "down";}
            }
            // chosen_axis == "horizontal" 
            else { 
                if (horizontal_velocity > 0.0) {direction_facing = "right";}
                else {direction_facing = "left";}
            }

        }
        else {is_moving = false;}
    }

    // Start is called before the first frame update
    void Start()
    {
        direction_facing = "down";
        axis_priority = "vertical";
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        string dh = get_horizontal_directional_input();
        horizontal_velocity = rb.velocity.x;
        update_horizontal_velocity(dh);
        string dv = get_vertical_directional_input();
        vertical_velocity = rb.velocity.y;
        update_vertical_velocity(dv);
        update_direction_facing(dh, dv);
        rb.AddForce(new Vector2(horizontal_velocity - rb.velocity.x, vertical_velocity - rb.velocity.y), ForceMode2D.Impulse);
        // transform.position += new Vector3(Time.deltaTime * horizontal_velocity,Time.deltaTime * vertical_velocity,0);
    }
}