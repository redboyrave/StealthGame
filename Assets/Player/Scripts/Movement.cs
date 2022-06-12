using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour

{
    public CharacterController c_ctrler;
    public Transform cam;
    public float speed = 5f;
    public float turn_speed = 0.1f; 
    private float turn_speed_velocity;
    private InputMaster _input; //unity new input system class. Contains Keybinds
    private Vector2 input_dir; //Vector 2 to represent Input Direction from controllers
    private Vector3 move_dir; // Vector3 that indicates movemente direction

    private void Awake(){
        _input = new InputMaster(); //new Instance of the InputSystem
    }

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
            Debug.LogFormat("Angle is " + angle.ToString());
            transform.rotation = Quaternion.Euler(0,angle,0);
           
            Vector3 move_dir = Quaternion.Euler(0,angle,0) * Vector3.forward; // Rotates the forward vector to the angle of the camera.

            c_ctrler.Move(move_dir.normalized*speed*Time.deltaTime); //The actual move function - Takes direction*speed*framerate
        }
        
    }
}

