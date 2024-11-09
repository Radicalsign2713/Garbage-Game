using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] float max_speed = 4; // The maximum speed the player can acheive in any one direction
    [SerializeField] float acelleration = 60; // The acelleration imparted by the directional movment keys
    [SerializeField] float player_friction = 45F; // The amount the player slows down when no buttons are pressed
    public void SetPlayerMovementProperty(float maxSpeed, float pAcelleration, float friction)
    {
        max_speed = maxSpeed;
        acelleration = pAcelleration;
        player_friction = friction;
    }

    [SerializeField] string button_left = "left"; // Key for left (should use unity scripting api strings)
    [SerializeField] string button_right = "right"; // all th eother button_ are the same concept
    [SerializeField] string button_up = "up"; 
    [SerializeField] string button_down = "down";
    private string last_pressed_horizontal = "right"; // Representation Invariant: must be either "right" or "left" 
    private string last_pressed_vertical = "up"; // Representation Invariant: must be either "up or "down"
    private string axis_priority; // Tracks weather the last pressed directional input was on the horizontal or vertical axis
                                  // Representation Invariant: must be either "vertical" or "horizontal"
    private float horizontal_velocity = 0; 
    private float vertical_velocity = 0;
    public string direction_facing; // The direction that Steve-E is facing. Always is whatever the last key pressed was, or the only key being held down. 
                                            // Representation Invariant: must be either "up", "down", "left", or "right"
                                            // Read only
    public bool is_moving; // Read only
        
    // Handles the roll over of directional inputs. 
    // Must be called every frame. 
    // Returns "left", "right", or "none"
    string get_horizontal_directional_input() {
        if (Input.GetKeyDown(button_left)) {
            last_pressed_horizontal = "left";
            axis_priority = "horizontal";
        }
        else if (Input.GetKeyDown(button_right)) {
            last_pressed_horizontal = "right";
            axis_priority = "horizontal";
        }
        if (Input.GetKey(button_left) & Input.GetKey(button_right)) {
            if (last_pressed_horizontal == "left"){
                return "left";
            }
            else {
                return "right";
            }
        }
        else if (Input.GetKey(button_left)) {
            return "left";
        }
        else if (Input.GetKey(button_right)) {
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
        if (Input.GetKeyDown(button_up)) {
            last_pressed_vertical = "up";
            axis_priority = "vertical";
        }
        else if (Input.GetKeyDown(button_down)) {
            last_pressed_vertical = "down";
            axis_priority = "vertical";
        }

        if (Input.GetKey(button_up) & Input.GetKey(button_down)) {
            if (last_pressed_vertical == "up") {
                return "up";
            }
            else {
                return "down";
            }
        }
        else if (Input.GetKey(button_up)) {
            return "up";
        }
        else if (Input.GetKey(button_down)) {
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

            if(direction_facing == "right")
            {
                GetterDown.enabled = false;
                GetterRight.enabled = true;
                GetterLeft.enabled = false;
            }
            else if(direction_facing == "left")
            {
                GetterDown.enabled = false;
                GetterRight.enabled = false;
                GetterLeft.enabled = true;
            }
            else
            {
                GetterDown.enabled = true;
                GetterRight.enabled = false;
                GetterLeft.enabled = false;
            }
        }
        else {is_moving = false;}
    }

    // trigger to detect & collect rubish
    private Collider2D GetterDown, GetterRight, GetterLeft;
    // Start is called before the first frame update
    void Start()
    {
        GetterDown = transform.Find("GetterDown").GetComponent<Collider2D>();
        GetterRight = transform.Find("GetterRight").GetComponent<Collider2D>();
        GetterLeft = transform.Find("GetterLeft").GetComponent<Collider2D>();

        direction_facing = "down";
        GetterDown.enabled = true;
        GetterRight.enabled = false;
        GetterLeft.enabled = false;

        axis_priority = "vertical";

        Time.timeScale = 1.0f;

        Physics2D.queriesHitTriggers = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        string dh = get_horizontal_directional_input();
        update_horizontal_velocity(dh);
        string dv = get_vertical_directional_input();
        update_vertical_velocity(dv);
        update_direction_facing(dh, dv);
        transform.position += new Vector3(Time.deltaTime * horizontal_velocity,Time.deltaTime * vertical_velocity,0);

        if (Input.GetMouseButtonDown(0))
        {
            //print("mouse button down 0");
        }
    }
}

