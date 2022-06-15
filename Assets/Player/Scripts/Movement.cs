using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //References to other objects
    private PlayerInputManager _input; 
    private Rigidbody rb;
    private Transform _camera_ref;
    private MakeNoise _noise;
    private CapsuleCollider cap_col;
    private CanClimbCheck _ledge;

    [Header("Movement Variables")]
    [SerializeField]private float walk_speed = 5f;
    [SerializeField]private float crouch_speed = 2.5f;
    [SerializeField]private float dash_speed = 8f;
    [SerializeField]private float JumpHeight = 3f;
    [SerializeField]private float JumpTime = .5f;
    [SerializeField]private float JumpAceleration = .5f;
    // [SerializeField]private float GravityMultiplier = 2.5f;
    private float turn_velocity; //Used for rotation and stuffs don't mind it
    private float dist_to_ground;
    private float vertical_speed;
    [HideInInspector]public bool is_grounded = false;
    [HideInInspector]public bool is_dashing = false;
    [HideInInspector]public bool is_crouched = false;
    [HideInInspector]public bool is_hanging = false;



    // Start is called before the first frame update
    void Start()
    {
        //Get References
        _input = GetComponent<PlayerInputManager>();
        rb = GetComponent<Rigidbody>();
        _noise = GetComponent<MakeNoise>();
        _ledge = GetComponentInChildren<CanClimbCheck>();
        _camera_ref = _input._camera;
        cap_col = GetComponent<CapsuleCollider>();

        rb.isKinematic = false;// Ensures that the RigidBody is Physics enabled
        remove_child_collision();//Stops collisions between main collider and children colliders

        dist_to_ground = cap_col.bounds.extents.y - cap_col.center.y; //Calculates distance to ground for GroundCheck
        Debug.Log(_ledge);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        is_grounded = check_ground(); //Checks if Player is grounded (Perform every frame pls)
        
        movement();
        if (_ledge.IsGrabingLedge){
            wall_hang();         
        }
    }
    
    private float speed_check(){
        float movement_speed;
        if(is_dashing){
            movement_speed = dash_speed;
        }
        else if (is_crouched){
            movement_speed = crouch_speed;
        }
        else{
            movement_speed = walk_speed;
        }
        return movement_speed;
    }
    void movement(){
        Vector2 input_dir = _input.JoystickInput;
        Vector3 move_dir;
        float movement_speed = speed_check();

        //XZ PLANE MOVEMENT
        if (input_dir.magnitude >= 0.1f){
            float targetAngle = Mathf.Atan2(input_dir.x,input_dir.y) * Mathf.Rad2Deg + _camera_ref.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y,targetAngle,ref turn_velocity,Time.deltaTime);
            Quaternion q_rot = Quaternion.Euler(0,angle,0);
            move_dir = q_rot * Vector3.forward; // Rotates the forward vector to the angle of the camera.
            rb.MoveRotation(q_rot);
        }
        else {move_dir= Vector3.zero;}; 
        
        _noise.Call_Noise(move_dir.magnitude);

        //Jumping Logic
        if (_input.Jump && is_grounded){
            Debug.Log("Jump!");
            vertical_speed =  JumpHeight/JumpTime - JumpAceleration*JumpTime/2f;
            // Equation derived from h = Vot + at²/2
            // Isolating Vo, we get Vo = h/t - at/2
        }
        else if (!is_grounded){
            vertical_speed += Physics.gravity.y*Time.deltaTime;
            // Debug.Log(vertical_speed);
        }
        else {vertical_speed = 0f;}

        rb.velocity = new Vector3(move_dir.x*movement_speed,vertical_speed,move_dir.z*movement_speed);

    }
    
    void wall_hang(){
        is_hanging = true;
        rb.isKinematic = true;
        RaycastHit wall_hit = raycast_forward();
        rb.MoveRotation(Quaternion.Euler(wall_hit.normal));
        

    }

    public void crouch(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        is_crouched = !is_crouched;
        if(is_crouched){
            char_crouch();
        }
        else{
            char_stand();
        }
        Debug.Log("Crouch Command");
    }

    private void remove_child_collision(){
        Collider[] cols = GetComponentsInChildren<Collider>();
        foreach (Collider col in cols){
            if (col == GetComponent<Collider>()){
                Debug.Log("I've found Myself");
                continue;
            }
            Physics.IgnoreCollision(GetComponent<Collider>(),col,true);
        }
    }

    private bool check_ground(){
        return Physics.Raycast(transform.position,Vector3.down, dist_to_ground);
    }

    private RaycastHit raycast_forward(){ 
        Ray f_ray = new Ray(transform.position,transform.forward);
        RaycastHit hitdata;
        Physics.Raycast(transform.position,transform.forward,out hitdata,1f);
        return hitdata;
    }


    //Functions for changing the Capsule Collision Size when Crouching and Standing Up
    private void char_crouch(){
        cap_col.height = 1.2f;
        cap_col.radius = 0.35f;
        cap_col.center = new Vector3(0,0.6f,0);
        dist_to_ground = cap_col.bounds.extents.y - cap_col.center.y;
    }
    private void char_stand(){
        cap_col.height =1.82f;
        cap_col.radius = 0.2f;
        cap_col.center = new Vector3(0,0.9f,0);
        dist_to_ground = cap_col.bounds.extents.y - cap_col.center.y;
    }
}
