using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour

{
    public CharacterController c_ctrler;
    public Transform cam; //Camera Reference for Rotations
    public Animator anim_manager; //Animator Reference
    public PlayerInfo p_info; //Reference to Player Info Scriptable Object.<- Keeps Track of player related stats.
    public float speed = 5f; //Default move speed
    public float crouch_speed = 2f; //Crouched move speed
    public float dash_speed = 8f; //Dashing move speed
    public float turn_speed = 0.1f; //TurningSpeed
    private float turn_speed_velocity; //Necessary for DampedRotation Function
    private InputMaster _input; //unity new input system class. Contains Keybinds
    private Vector2 input_dir; //Vector 2 to represent Input Direction from controllers
    private Vector3 move_dir; // Vector3 that indicates movemente direction

    //Bools for velocity and change//
    private Boolean is_dashing=false; 
    private Boolean is_crouching=false;

    //HASH VALUES FOR ANIMATIONS//
    int crouch_hash;
    int dash_hash;
    int vel_hash;

    private void Awake(){
        _input = new InputMaster(); //new Instance of the InputSystem
    }

    private void Start(){
    _input.Player.Crouch.performed += crouch;
    _input.Player.Dash.performed += start_dash;
    _input.Player.Dash.canceled += stop_dash;

    //CONVERT TO HASH THE STRINGS//
    crouch_hash = Animator.StringToHash("Is_crouching");
    dash_hash = Animator.StringToHash("Is_dashing");
    vel_hash = Animator.StringToHash("Movement_speed");

    }


    private void crouch(InputAction.CallbackContext obj)
    {
        is_crouching = !is_crouching;
        anim_manager.SetBool(crouch_hash,is_crouching);
        if(is_crouching){
            char_crouch();
        }
        else{
            char_stand();
        }
        
    }
    private void start_dash(InputAction.CallbackContext obj)
    {
        is_dashing = true;
        anim_manager.SetBool(dash_hash,is_dashing);
        if (is_crouching){
            is_crouching = false;
            anim_manager.SetBool(crouch_hash,is_crouching);
            char_stand();
        }
    }
    private void stop_dash(InputAction.CallbackContext obj)
    {
        is_dashing = false;
        anim_manager.SetBool(dash_hash,is_dashing);
    }

    // private void sound_debug(InputAction.CallbackContext obj)
    // {
    //     MakeNoise soundmaker = col.GetComponent<MakeNoise>();
    //     soundmaker.Call_Noise(UnityEngine.Random.Range(5,50));
    // }
    private void OnEnable(){
        _input.Player.Enable(); //Enables the Input

    }
    private void OnDisable(){
        _input.Player.Disable(); //Disables the Input
    }

    private void FixedUpdate(){
        input_dir = _input.Player.Move.ReadValue<Vector2>(); //Reads the value from input and stores them as Vector2
        //DEBUG
        // Debug.Log("Controllers are Outputing " + input_dir);    


        if (input_dir.magnitude >= 0.1f){    //Checks the magnitude of input to move (avoids moving to 0,0 always + adds deadzone)
            float targetAngle = Mathf.Atan2(input_dir.x,input_dir.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y,targetAngle,ref turn_speed_velocity,turn_speed);
            // Debug.LogFormat("Angle is " + angle.ToString());
            transform.rotation = Quaternion.Euler(0,angle,0);
            Vector3 move_dir = Quaternion.Euler(0,angle,0) * Vector3.forward; // Rotates the forward vector to the angle of the camera.
           
            anim_manager.SetFloat(vel_hash,move_dir.normalized.magnitude);
            Vector3 movement;
            if (is_dashing){
                movement =move_dir.normalized*dash_speed*Time.deltaTime; //Higher speed for the dash move
            }
            else if (is_crouching){
                movement = move_dir.normalized*crouch_speed*Time.deltaTime; //Lower Speed for the crouching movement
            }
            else{
                movement = move_dir.normalized*speed*Time.deltaTime; //The base move speed
            }
            GetComponent<MakeNoise>().Call_Noise(movement.magnitude*20);
            c_ctrler.Move(movement); //The actual move function - Takes direction*speed*framerate
        }
    anim_manager.SetFloat(vel_hash,input_dir.magnitude);
    p_info.PlayerCurrentCoordinates = this.transform.position;
        
    }


    //Functions for changing the Character Controller Collision Size when Crouching and Standing Up
    private void char_crouch(){
        c_ctrler.height = 1.2f;
        c_ctrler.radius = 0.35f;
        c_ctrler.center = new Vector3(0,0.6f,0);
    }
    private void char_stand(){
        c_ctrler.height =1.85f;
        c_ctrler.radius = 0.25f;
        c_ctrler.center = new Vector3(0,0.9f,0);
    }
}

