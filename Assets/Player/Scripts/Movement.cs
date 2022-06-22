using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //References to other objects
    // private PlayerInputManager _input; 
    // private Rigidbody rb;
    // private Transform _camera_ref;
    // private MakeNoise _noise;
    // // private CapsuleCollider cap_col;
    // private CanClimbCheck _ledge;
    [Header("MOVEMENT MANAGER")]
    [SerializeField]private Movement_Manager _movement_manager;

    // [Header("Movement Variables")]
    // [SerializeField]private float walk_speed = 5f;
    // [SerializeField]private float crouch_speed = 2.5f;
    // [SerializeField]private float dash_speed = 8f;
    // [SerializeField]private float JumpHeight = 3f;
    // [SerializeField]private float JumpTime = .5f;
    // [SerializeField]private float JumpAceleration = .5f;

    // [Header("Object References")]
    // [SerializeField]private Transform head_pos;
    // [SerializeField]private CapsuleCollider stand_cap;
    // [SerializeField ]private CapsuleCollider crouched_cap;
    // [SerializeField]private Animator _animator;


    // [SerializeField]private float GravityMultiplier = 2.5f;
    private float turn_velocity; //Used for rotation and stuffs don't mind it
    // private float dist_to_ground; // No longer in use
    // [HideInInspector]public float vertical_speed;
    // [HideInInspector]public float _move_speed = 0f;
    // [HideInInspector]public bool is_grounded = false;
    // [HideInInspector]public bool is_dashing = false;
    // [HideInInspector]public bool is_crouched = false;
    // [HideInInspector]public bool is_hanging = false;


    void OnEnable(){
        Debug.Log("Movement script enabled");
        _movement_manager.rb.isKinematic=false;
    }
    void OnDisable(){
        Debug.Log("Movement script disabled");
    }
    // Start is called before the first frame update
    void Start()
    {
        _movement_manager._camera_ref = _movement_manager._input._camera;
        _movement_manager.char_stand(); 
        _movement_manager.rb.isKinematic = false;// Ensures that the RigidBody is Physics enabled
        transform.rotation = _movement_manager.last_rotation;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check_ground(); //Checks if Player is grounded (Perform every frame pls)
        movement();


    }
    
    private float speed_check(){
        float movement_speed;
        if(_movement_manager.is_dashing){
            movement_speed = _movement_manager.dash_speed;
        }
        else if (_movement_manager.is_crouched){
            movement_speed = _movement_manager.crouch_speed;
        }
        else{
            movement_speed = _movement_manager.walk_speed;
        }
        return movement_speed;
    }
    void movement(){

        Vector2 input_dir = _movement_manager._input.JoystickInput;
        // Debug.Log(input_dir);
        Vector3 move_dir;
        float movement_speed = speed_check();

        //XZ PLANE MOVEMENT
        if (input_dir.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(input_dir.x,input_dir.y) * Mathf.Rad2Deg +_movement_manager._camera_ref.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y,targetAngle,ref turn_velocity,Time.deltaTime);
            Quaternion q_rot = Quaternion.Euler(0,angle,0);
            move_dir = q_rot * Vector3.forward; // Rotates the forward vector to the angle of the camera.
            _movement_manager.rb.MoveRotation(q_rot);
            _movement_manager._move_speed = move_dir.normalized.magnitude;
        }
        else {
            move_dir= Vector3.zero;
            _movement_manager._move_speed = 0;
            } 
        
        _movement_manager._noise.Call_Noise(move_dir.magnitude*2);

        // if (_input.Jump && raycast_forward().distance <= 0.1f && is_grounded){
            //Should play wall climb animation;
        // }
        //Jumping Logic
        if (_movement_manager._input.Jump && _movement_manager.is_grounded){
            Debug.Log("Jump!");
            _movement_manager.vertical_speed =  _movement_manager.JumpHeight/_movement_manager.JumpTime - _movement_manager.JumpAceleration*_movement_manager.JumpTime/2f;
            // Equation derived from h = Vot + at²/2
            // Isolating Vo, we get Vo = h/t - at/2
        }
        else if (!_movement_manager.is_grounded && !_movement_manager.is_hanging){
            _movement_manager.vertical_speed += Physics.gravity.y*Time.deltaTime;
            // Debug.Log(vertical_speed);
        }
        else {_movement_manager.vertical_speed = 0f;}

        _movement_manager.rb.velocity = new Vector3(move_dir.x*movement_speed,_movement_manager.vertical_speed,move_dir.z*movement_speed);

    }
    

    // //Dash only works while holding the button, 
    // //so we have a start and stop funcion to work with the input system
    // //Dashing also cancels your crouch;
    // public void start_dash(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    //     is_dashing = true;
    //     is_crouched = false;
    // }
    // public void stop_dash(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    //     is_dashing = false;

    // }
    //     public void crouch(UnityEngine.InputSystem.InputAction.CallbackContext obj){
    //     is_crouched = !is_crouched;
    //     if(is_crouched){
    //         char_crouch();
    //     }
    //     else{
    //         char_stand();
    //     }
    // }


    private void check_ground(){ //Raycast down to check the ground distance
        RaycastHit hitdata;
        Physics.Raycast(transform.position,Vector3.down,out hitdata);
        _movement_manager.is_grounded =  transform.position.y - hitdata.point.y < 0.1;
        //The only way i managed to make this work continuosly is by 
        //checking the distance between the player position, and the hit position
    }

    private RaycastHit raycast_forward(){  // was used for wall movement, now it's here just in case;
        RaycastHit hitdata;
        Physics.Raycast(transform.position,transform.forward,out hitdata,5f);
        return hitdata;
    }


    //Functions for changing the Capsule Collision Size when Crouching and Standing Up
    //These have changed due to collision issues, now we have two capsules that activate or deactive
    //instead of changing the one capsule's size


}
